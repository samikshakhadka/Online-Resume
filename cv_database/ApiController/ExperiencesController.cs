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
using AutoMapper;
using cv_database.DTOs.ExperiencesDTOs;
using Microsoft.VisualBasic;

using cv_database.Repositories;



namespace cv_database.ApiController
{
    [Authentication]
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly cv_databaseContext _context;
        private readonly IGenericRepos _genericRepos;

        public ExperiencesController(IMapper mapper, cv_databaseContext context, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _context = context;
            _genericRepos = genericRepos;
        }

        // GET: api/Experiences
        [HttpGet]
        public async Task<ActionResult<List<Experience>>> GetExperience()
        {

   
            var experience = await _genericRepos.GetAll<Experience>();
            var record = _mapper.Map<List<ExperiencesReadDTOs>>(experience);
            return Ok(record);
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ExperiencesReadDTOs>>> GetExperience(int id)
        {

            var experience = await _genericRepos.GetUserData<Experience>(userData => userData.information_id == id);
             

            if (experience == null)
            {
                return NotFound();
            }
            var record = _mapper.Map<List<ExperiencesReadDTOs>>(experience);
            return Ok(record);
        }

        // PUT: api/Experiences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperience(int id, ExperiencesUpdateDTOs experiencesUpdateDTOs)
        {
            var info = await _genericRepos.GetOne<Experience>(id);
            if (id != experiencesUpdateDTOs.information_id)
            {
                return BadRequest();
            }

            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }

            _mapper.Map(experiencesUpdateDTOs, info);
            await _genericRepos.UpdateAll(info);
            var infoReadDTO = _mapper.Map<ExperiencesReadDTOs>(info);
            return Ok(infoReadDTO);

        }

        // POST: api/Experiences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExperiencesReadDTOs>> PostExperience(ExperiencesCreateDTOs experiencesCreateDTOs)
        {
          
            var infos = _mapper.Map<Experience>(experiencesCreateDTOs);
            await _genericRepos.PostAll(infos);
            

            var experiencesReadDTOs = _mapper.Map<ExperiencesReadDTOs>(infos);
            return CreatedAtAction("GetExperience", new { id = experiencesReadDTOs.experience_id }, experiencesReadDTOs);
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
           
            var experience = await _genericRepos.GetOne<Experience>(id);
            if (experience == null)
            {
                return NotFound();
            }

            await _genericRepos.DeleteAll(experience);
            
           

            return NoContent();
        }

       
    }
}
