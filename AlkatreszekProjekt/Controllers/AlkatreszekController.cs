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
    }
}
