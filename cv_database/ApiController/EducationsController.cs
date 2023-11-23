using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cv_database.Data;
using cv_database.Models;

using cv_database.Helpers;

using Microsoft.VisualBasic;
using AutoMapper;
using cv_database.DTOs.EducationsDTOs;
using cv_database.Repositories;


namespace cv_database.ApiController
{
    [Authentication]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly cv_databaseContext _context;
        private readonly IGenericRepos _genericRepos;

        public EducationsController(IMapper mapper,cv_databaseContext context, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _context = context;
            _genericRepos = genericRepos;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducation()
        {
         
            var education = await _genericRepos.GetAll<Education>();
            var record = _mapper.Map<List<EducationsReadDTOs>>(education);
            return Ok(record);
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<EducationsReadDTOs>>> GetEducation(int id)
        {
          
            var education = await _genericRepos.GetUserData<Education>(userData => userData.information_id == id);

            if (education == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<List<EducationsReadDTOs>>(education);
            
            return Ok(record);
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducation(int id, EducationsUpdateDTOs educationsUpdateDTOs)
        {
            var info = await _genericRepos.GetOne<Education>(id);
            if (id != educationsUpdateDTOs.information_id)
            {
                return BadRequest();
            }

            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }

            _mapper.Map(educationsUpdateDTOs, info);
            await _genericRepos.UpdateAll(info);
            
            var infoReadDTO = _mapper.Map<EducationsReadDTOs>(info);
            return Ok(infoReadDTO);

        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EducationsReadDTOs>> PostEducation(EducationsCreateDTOs educationsCreateDTOs)
        {
         

            var infos = _mapper.Map<Education>(educationsCreateDTOs);
            await _genericRepos.PostAll(infos);
            await _context.SaveChangesAsync();

            var educationsReadDTOs = _mapper.Map<EducationsReadDTOs>(infos);
            return CreatedAtAction("GetEducation", new { id = educationsReadDTOs.education_id }, educationsReadDTOs);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
           
            var education = await _genericRepos.GetOne<Education>(id);
            if (education == null)
            {
                return NotFound();
            }

            await _genericRepos.DeleteAll(education);
    
            

            return NoContent();
        }

        private bool EducationExists(int id)
        {
            return (_context.Education?.Any(e => e.education_id == id)).GetValueOrDefault();
        }
    }
}
