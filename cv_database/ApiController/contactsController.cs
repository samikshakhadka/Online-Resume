using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cv_database.Data;
using cv_database.Models;
using AutoMapper;
using cv_database.DTOs.ContactsDTOs;
using cv_database.Helpers;
using cv_database.DTOs.InformationsDTOs;
using Microsoft.VisualBasic;
using cv_database.Repositories;

namespace cv_database.ApiController
{
    [Authentication]
    [Route("api/[controller]")]
    [ApiController]
    public class contactsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly cv_databaseContext _context;
        private readonly IGenericRepos _genericRepos;

        public contactsController(IMapper mapper,cv_databaseContext context, IGenericRepos genericRepos)
        {
            _mapper = mapper;
            _context = context;
            _genericRepos = genericRepos;
        }

        // GET: api/contacts
        [HttpGet]
        public async Task<ActionResult<List<contact>>> Getcontact()
        {
          
            var infos = await _genericRepos.GetAll<contact>();
            var record = _mapper.Map<List<ContactsReadDTOs>>(infos);
            return Ok(record);
        }

        // GET: api/contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ContactsReadDTOs>>> Getcontact(int id)
        {
          
            var contacts = await _genericRepos.GetUserData<contact>(userData => userData.information_id == id);

            if (contacts== null)
            {
                return NotFound();
            }
            var record = _mapper.Map<List<ContactsReadDTOs>>(contacts);
            return Ok(record);
        }

        // PUT: api/contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcontact(int id, ContactsUpdateDTOs contactsUpdateDTOs)
        {
            var info = await _genericRepos.GetOne<contact>(id);
            if (id != contactsUpdateDTOs.information_id)
            {
                return BadRequest();
            }

            if (info == null)
            {
                throw new Exception($"Information {id} is not found.");
            }

            _mapper.Map(contactsUpdateDTOs, info);
            await _genericRepos.UpdateAll(info);
            
            var infoReadDTO = _mapper.Map<InformationsReadDTOs>(info);
            return Ok(infoReadDTO);

        }

        // POST: api/contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<contact>> Postcontact(ContactsCreateDTOs contactsCreateDTOs)
        {
          
            var infos = _mapper.Map<contact>(contactsCreateDTOs);
            await _genericRepos.PostAll(infos);
            

            var contactsReadDTO = _mapper.Map<ContactsReadDTOs>(infos);
            return CreatedAtAction("Getcontact", new { id = contactsReadDTO.contact_id }, contactsReadDTO);
        }

        // DELETE: api/contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecontact(int id)
        {
           
            var contacts = await _genericRepos.GetOne<contact>(id);
            if (contacts == null)
            {
                return NotFound();
            }

            await _genericRepos.DeleteAll(contacts);
            

            return NoContent();
        }

        private bool contactExists(int id)
        {
            return (_context.contact?.Any(e => e.contact_id == id)).GetValueOrDefault();
        }
    }
}
