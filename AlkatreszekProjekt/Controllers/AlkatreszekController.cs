using AlkatreszekProjekt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlkatreszekProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlkatreszController : ControllerBase
    {
        private readonly AlkatreszekContext _context = new();

        [HttpGet("alkatreszek/count")]
        public async Task<IActionResult> GetNumberOfAlkatreszek()
        {
            return Ok(await _context.Alkatreszeks.CountAsync());
        }

        [HttpGet("beszallitok/count")]
        public async Task<IActionResult> GetNumberOfBeszallitok()
        {
            return Ok(await _context.Beszallitoks.CountAsync());
        }

        [HttpGet("megrendelok/count")]
        public async Task<IActionResult> GetNumberOfMegrendelok()
        {
            return Ok(await _context.Megrendeloks.DistinctBy(m => m.Id).CountAsync());
        }

        [HttpGet("megrendelesek/teljesitett/count")]
        public async Task<IActionResult> GetTeljesitettNumber()
        {
            return Ok(await _context.Megrendeleseks.Where(m => m.Teljesitve == "True").CountAsync());
        }




        [HttpGet("alkatreszek/laptop/count")]
        public async Task<IActionResult> GetNumberOfLaptopAlkatresz()
        {
            return Ok(await _context.Alkatreszeks.Where(a => a.LaptopAlkatresz == "True").CountAsync());
        }

        [HttpGet("megrendelok/budapest/count")]
        public async Task<IActionResult> GetNumberOfMegrendeloFromBudapest()
        {
            return Ok(await _context.Megrendeloks.Where(m => m.Lakhely == "Budapest").CountAsync());
        }

        [HttpGet("beszallitok/debrecen/count")]
        public async Task<IActionResult> GetNumberOfBeszallitoFromDebrecen()
        {
            return Ok(await _context.Beszallitoks.Where(b => b.BeszallitoTelephely == "Debrecen").DistinctBy(b => b.Id).CountAsync());
        }




        [HttpGet("beszallitok/nevsorrend")]
        public async Task<IActionResult> GetBeszallitokOrderedByName()
        {
            return Ok(await _context.Beszallitoks.OrderBy(b=>b.BeszallitoNev).ToListAsync());
        }

        [HttpGet("megrendelok/ferfi")]
        public async Task<IActionResult> GetMaleMegrendelok()
        {
            return Ok(await _context.Megrendeloks.Where(m=>m.Ferfi == "True").ToListAsync());
        }

        [HttpGet("megrendelesek/datum/csokkeno")]
        public async Task<IActionResult> GetMegrendelesekOrderedByDateDesc()
        {
            return Ok(await _context.Megrendeleseks.OrderByDescending(m=>m.Datum).ToListAsync());
        }

        [HttpGet("megrendelesek/megrendelo/lakhely")]
        public async Task<IActionResult> GetMegrendelesekOrderedByLakhely()
        {
            return Ok(await _context.Megrendeleseks
                                                    .Join(_context.Megrendeloks, rendeles => rendeles.MegrendeloId, rendelo => rendelo.Id, (rendeles,rendelo) => new {rendeles= rendeles, lakhely = rendelo.Lakhely })
                                                    .OrderBy(m=>m.lakhely)
                                                    .ToListAsync());
        }

        [HttpGet("beszallitok/distinct")]
        public async Task<IActionResult> GetBeszallitokDistinct()
        {
            return Ok(await _context.Beszallitoks.DistinctBy(m => m.Id).ToListAsync());
        }

        [HttpGet("alkatreszek/mennyisegcsokkeno")]
        public async Task<IActionResult> GetAlkatreszekCsokkeno()
        {
            return Ok(await _context.Alkatreszeks.OrderByDescending(a=>a.Raktaron).ToListAsync());
        }

        [HttpGet("megrendelesek/feb10utan")]
        public async Task<IActionResult> GetMegrendelesekFeb10Utan()
        {
            return Ok(await _context.Megrendeleseks.Where(m => string.Compare(m.Datum, "2025-02-10") > 0).ToListAsync());
        }



        [HttpGet("alkatreszek/kategoriankent")]
        public async Task<IActionResult> GetAlkatreszekKategoriankent()
        {
            return Ok(await _context.Alkatreszeks.Join(_context.Kategoraiks, a => a.KategoriaId, k => k.Id, (a,k) => new {kategoria= k.KategoriaNev, alkatreszek_szama = a.Raktaron})
                .GroupBy(g=>g.kategoria)
                .Select(s=> new {kategoria = s.Key, szama = s.Sum(x=> x.alkatreszek_szama)})
                .ToListAsync());
            // egy groupba rakja azokat az new{kategoria,alkatreszek_szama} -kat ahol ugyan az a kategoria,
            // majd a selectel kivalasztja a kulcsot, ami a kategoria mert az alapajan csoportositottunk,
            // a masik pedig maga az "s" érték az a lista amiben ezek vannak, szoval a Sum(x=>x.alkatreszek_szama) vegigmegy rajta es osszeadja a kello dolgokat

        }

        [HttpGet("alkatreszek/kategoria_id/{kategoriaid}")]
        public async Task<IActionResult> GetAlkatreszekKategoriankentId([FromRoute] int kategoriaid)
        {
            return Ok(await _context.Alkatreszeks.Where(a=>a.KategoriaId == kategoriaid).ToListAsync());
            
        }

        [HttpGet("alkatreszek/kategoria/{kategoria}")]
        public async Task<IActionResult> GetAlkatreszekKategoriankentString([FromRoute] string kategoria)
        {
            Kategoraik kategoria2 = await _context.Kategoraiks.FirstOrDefaultAsync(k=>k.KategoriaNev.ToLower() == kategoria.ToLower());

            return Ok(await _context.Alkatreszeks.Where(a => a.KategoriaId == kategoria2.Id).ToListAsync());

        }


        [HttpGet("rendelesek/megrendelo")]
        public async Task<IActionResult> GetRendelesekMegrendelo()
        {
            return Ok(await _context.Megrendeloks.Join(_context.Megrendeleseks, rendelo => rendelo.Id, megrendeles => megrendeles.MegrendeloId, (rendelo, megrendeles) => new { rendelo, megrendeles })
                                .GroupBy(g => g.rendelo.MegrendeloNev)
                                .Select(s => new { s.Key, szam = s.Count() }).ToListAsync());
        }

        [HttpGet("rendelesek/megrendelo/{megrendelo}")]
        public async Task<IActionResult> GetRendelesekMegrendeloString([FromRoute] string megrendelo)
        {
            Megrendelok megrendelo2 = await _context.Megrendeloks.FirstOrDefaultAsync(m => m.MegrendeloNev.ToLower() == megrendelo.ToLower());
            return Ok(await _context.Megrendeleseks.Where(m => m.MegrendeloId == megrendelo2.Id).ToListAsync());
        }

        [HttpGet("osszalkatreszSZam")]
        public async Task<IActionResult> GetNumberOfAlkatresz()
        {
            return Ok(await _context.Alkatreszeks.SumAsync(a=>a.Raktaron));
        }

        [HttpGet("rendeles/rendelestetel/{rendelesid}")]
        public async Task<IActionResult> GetNumberOfrendelestetel([FromQuery] int rendelesid)
        {
            return Ok(await _context.RendelesTeteleks.Where(r=>r.MegrendelesId == rendelesid).CountAsync());
        }

        [HttpGet("kategoria/tobbmint3alkatresz")]
        public async Task<IActionResult> GetKategoriaWithMoreThan3()
        {
            return Ok(await _context.Alkatreszeks.Join(_context.Kategoraiks,a=>a.KategoriaId, k=>k.Id, (a,k) => new {kategoria = k.KategoriaNev, alkatresz = a})
                .GroupBy(g=>g.kategoria)
                .Select(g=> new {g.Key, szam = g.Count() })
                .Where(g=>g.szam>3)
                .ToListAsync());
        }

        [HttpGet("nempestibeszallitok")]
        public  async Task<IActionResult> NemPestiSzallito()
        {
            return Ok(await _context.Beszallitoks.Where(b => b.BeszallitoTelephely != "Budapest").ToListAsync());
        }

        [HttpGet("atlagnalnagyobbrendeles")]
        public async Task<IActionResult> AtlagnalNagyobbRendelesek()
        {
            double atlagrendeles = (double)await _context.RendelesTeteleks.GroupBy(g => g.MegrendelesId).Select(g => new { g.Key, ossz = g.Sum(r => r.AruMennyiseg) }).AverageAsync(g=>g.ossz);
            return Ok(await _context.RendelesTeteleks
                .GroupBy(g=>g.MegrendelesId)
                .Select(s=> new {s.Key, mennyiseg = s.Sum(x=>x.AruMennyiseg)})
                .Where(s=>s.mennyiseg>atlagrendeles)
                .Join(_context.Megrendeleseks, s=>s.Key, m=>m.Id, (s,m) => new { m.MegrendeloId, m.Datum, m.Id, m.Teljesitve, s.mennyiseg})
                .ToListAsync());
        }

        [HttpGet("alkatreszek/beszallito")]
        public async Task<IActionResult> GetAlkatreszekBeszallitoneve()
        {
            return Ok(await _context.Alkatreszeks.Join(_context.Beszallitoks,a=>a.BeszallitoId, b=>b.Id, (a,b)=> new {alkatresz = a.Nev, beszallitonev= b.BeszallitoNev}).ToListAsync());
        }

        [HttpGet("megrendelesek/{datumkezd}/{datumveg}")]
        public async Task<IActionResult> DatumfeltetelesMegrendelesek([FromRoute] string datumkezd, [FromRoute] string datumveg)
        {
            return Ok(await _context.Megrendeleseks.Where(m=> string.Compare(m.Datum,datumkezd)>=0 && string.Compare(m.Datum,datumveg) <=0).ToListAsync());
        }

        /*[HttpGet("rendelesek/beszallito/{beszallitoid}")]
        public async Task<IActionResult> GetRendelesekBeszallito([FromRoute] int beszallitoid)
        {
            return Ok(await _context.Megrendeleseks.Where(m=>m.));
        }*/

    }
}
