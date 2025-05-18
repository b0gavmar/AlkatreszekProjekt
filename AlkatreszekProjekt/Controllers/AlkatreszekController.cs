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
        public async Task<int> GetNumberOfAlkatreszek()
        {
            return await _context.Alkatreszeks.CountAsync();
        }

        [HttpGet("beszallitok/count")]
        public async Task<int> GetNumberOfBeszallitok()
        {
            return await _context.Beszallitoks.CountAsync();
        }

        [HttpGet("megrendelok/count")]
        public async Task<int> GetNumberOfMegrendelok()
        {
            return await _context.Megrendeloks.DistinctBy(m => m.Id).CountAsync();
        }

        [HttpGet("megrendelesek/teljesitett/count")]
        public async Task<int> GetTeljesitettNumber()
        {
            return await _context.Megrendeleseks.Where(m=> m.Teljesitve == "True").CountAsync();
        }




        [HttpGet("alkatreszek/laptop/count")]
        public async Task<int> GetNumberOfLaptopAlkatresz()
        {
            return await _context.Alkatreszeks.Where(a => a.LaptopAlkatresz == "True").CountAsync();
        }

        [HttpGet("megrendelok/budapest/count")]
        public async Task<int> GetNumberOfMegrendeloFromBudapest()
        {
            return await _context.Megrendeloks.Where(m => m.Lakhely == "Budapest").CountAsync();
        }

        [HttpGet("beszallitok/debrecen/count")]
        public async Task<int> GetNumberOfBeszallitoFromDebrecen()
        {
            return await _context.Beszallitoks.Where(b => b.BeszallitoTelephely == "Debrecen").DistinctBy(b=>b.Id).CountAsync();
        }
    }
}
