using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using indexProcessorWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace indexProcessorWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ScoredNameController : Controller
    {
        private readonly ScoredNameContext _context;

        public ScoredNameController(ScoredNameContext context )
        {
            _context = context;

            if (_context.ScoredNames.Count() == 0)
            {
                Random rnd = new Random();
                _context.ScoredNames.Add(new ScoredName { Name = "someRandomName", Score = rnd.Next(1000) });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<ScoredName> GetAll()
        {
            return _context.ScoredNames.ToList();
        }

        [HttpGet("{id}"), ActionName("GetScoredName")]
        public IActionResult GetById(long id)
        {
            var name = _context.ScoredNames.FirstOrDefault(t => t.Id == id);
            if (name == null)
            {
                return NotFound();
            }

            return new ObjectResult(name);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody]JArray jsonArray)
        {
            IEnumerable<ScoredName> scoredNames = jsonArray.ToObject<List<ScoredName>>();
            _context.ScoredNames.AddRange(scoredNames);
            _context.SaveChanges();
            return Ok();
   
        }

    }
}