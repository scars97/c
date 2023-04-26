﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySQLtest.DBContext;
using MySQLtest.Model;

namespace MySQLtest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly LanguageContext _context;

        public LanguagesController(LanguageContext context)
        {
            _context = context;
        }

        // GET: api/Languages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
          if (_context.Languages == null)
          {
              return NotFound();
          }
            return await _context.Languages.ToListAsync();
        }

        // GET: api/Languages/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguage(int id)
        {
          if (_context.Languages == null)
          {
              return NotFound();
          }
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            return language;
        }

        // PUT: api/Languages/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguage(int id, Language language)
        {
            if (id != language.LanguageId)
            {
                return BadRequest();
            }

            _context.Entry(language).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Languages
        [HttpPost]
        public async Task<ActionResult<Language>> PostLanguage(Language language)
        {
          if (_context.Languages == null)
          {
              return Problem("Entity set 'LanguageContext.Languages'  is null.");
          }
            _context.Languages.Add(language);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanguage", new { id = language.LanguageId }, language);
        }

        // DELETE: api/Languages/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            if (_context.Languages == null)
            {
                return NotFound();
            }
            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LanguageExists(int id)
        {
            return (_context.Languages?.Any(e => e.LanguageId == id)).GetValueOrDefault();
        }
    }
}
