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
            return Ok(await _context.Beszallitoks.OrderBy(b=>b.BeszallitoNev));
        }
    }
}
