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
using cv_database.DTOs.InformationsDTOs;
using cv_database.Repositories;

namespace cv_database.ApiController
{
    [Authentication]
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {

        private const string ApiKeyUserID = "UserEmail";
        private const string ApiKeyUserPassword = "Password";
        private readonly IMapper _mapper;
        private readonly IGenericRepos _genericRepos;

       
        
        public InformationController(IMapper mapper, cv_databaseContext context ,  IGenericRepos genericRepos)
        {
            _mapper = mapper;
            
            _genericRepos = genericRepos;

        }

        [HttpGet]
        [Route("/api/login")]
        public async Task<ActionResult<Information>> GetLogin()
        {
            if ((!HttpContext.Request.Headers.TryGetValue(ApiKeyUserID, out var Username)) ||
                (!HttpContext.Request.Headers.TryGetValue(ApiKeyUserPassword, out var password)))
            {
                return BadRequest("Both User Id and Password are required");
            }
            else
            {
                var UserId = await _genericRepos.GetByName<Information>(user => user.Name == Username.ToString());
                if (UserId == null)
                {
                    return NotFound("Invalid Username or Password");
                }
                if (UserId.Password == password.ToString())
                {
                    var record = _mapper.Map<InformationsReadDTOs>(UserId);
                    return Ok(record);
                }
                else
                {
                    return NotFound("Incorrect Password");
                }
            }

        }



         // GET: api/Information
         [HttpGet]
        
        public async Task<ActionResult<List<Information>>>GetInformation()
        {
            var information= await _genericRepos.GetAll<Information>();
            var record = _mapper.Map<List<InformationsReadDTOs>>(information);
            return Ok(record);
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformationsReadDTOs>>GetInformation(int id)
        {
         
            
            var information = await _genericRepos.GetOne<Information>(id);
           

            if (information == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<InformationsReadDTOs>(information);

            return Ok(record);
        }

        // PUT: api/Information/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]



        public async Task<IActionResult> PutInformation(int id, InformationsUpdateDTOs informationsUpdateDTOs)
        {

            var info = await _genericRepos.GetOne<Information>(id);
            if (id != informationsUpdateDTOs.information_id)
            {
                return BadRequest();
            }

            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }

            _mapper.Map(informationsUpdateDTOs, info);
            await _genericRepos.UpdateAll(info);
           
            var infoReadDTO = _mapper.Map<InformationsReadDTOs>(info);
            return Ok(infoReadDTO);


        }
        // POST: api/Information
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<ActionResult<InformationsReadDTOs>> PostInformation(InformationsCreateDTOs informationsCreateDTOs)
        {
           
            var infos = _mapper.Map<Information>(informationsCreateDTOs);
           
            await _genericRepos.PostAll(infos);

            var informationReadDTO = _mapper.Map<InformationsReadDTOs>(infos);

            return CreatedAtAction("GetInformation", new { id = informationReadDTO.information_id }, informationReadDTO);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
           
            var information = await _genericRepos.GetOne<Information>(id);
            if (information == null)
            {
                return NotFound();
            }

           
            await _genericRepos.DeleteAll(information);
           
            return NoContent();
        }

       
    }
}
