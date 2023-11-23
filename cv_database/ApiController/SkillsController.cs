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
using cv_database.DTOs.SkillsDTOs;

using cv_database.Repositories;

namespace cv_database.ApiController
{
    [Authentication]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly cv_databaseContext _context;
        private readonly IGenericRepos _genericRepos;

        public SkillsController(IMapper mapper,cv_databaseContext context, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _context = context;
            _genericRepos = genericRepos;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<List<Skills>>> GetSkills()
        {
          
          var skill = await _genericRepos.GetAll<Skills>();
          var record = _mapper.Map<List<SkillsReadDTOs>>(skill);    
          return Ok(record);
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SkillsReadDTOs>>> GetSkills(int id)
        {
         
            var skills = await _genericRepos.GetUserData<Skills>(userData => userData.information_id == id);

            if (skills == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<List<SkillsReadDTOs>>(skills);
            return Ok(record);
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkills(int id, SkillsUpdateDTOs skillsUpdateDTOs)
        {
            var info = await _genericRepos.GetOne<Skills>(id);
            if (id != skillsUpdateDTOs.skill_id)
            {
                return BadRequest();
            }

            if (info == null)
            {
                throw new Exception($"Skill {id} was not found.");
            }

            _mapper.Map(skillsUpdateDTOs, info);
            await _genericRepos.UpdateAll(info);
            
            var infoReadDTO = _mapper.Map<SkillsReadDTOs>(skillsUpdateDTOs);

            return Ok(infoReadDTO);
        }

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillsReadDTOs>> PostSkills(SkillsCreateDTOs skillsCreateDTOs)
        {
         
            var infos = _mapper.Map<Skills>(skillsCreateDTOs);
            await _genericRepos.PostAll(infos);
           
            
            var skillsReadDTOs = _mapper.Map<SkillsReadDTOs>(infos);

            return CreatedAtAction("GetSkills", new { id = skillsReadDTOs.information_id }, skillsReadDTOs);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkills(int id)
        {
           
            var skills = await _genericRepos.GetOne<Skills>(id);
            if (skills == null)
            {
                return NotFound();
            }

            await _genericRepos.DeleteAll(skills);


            return NoContent();
        }

       
    }
}
