﻿using scorednameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using scorednameAPI.Healthchecks;
using App.Metrics.Health.Builder;
using App.Metrics.Health;

namespace scorednameAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ScoredNameController : Controller
    {
        private readonly ScoredNameContext _context;
      
        public ScoredNameController(ScoredNameContext context)
        {
            _context = context;
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