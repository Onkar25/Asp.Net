using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.CustomActionFilter;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO.Walks;
using NZWalkAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    public class WalksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> InsertWalks([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                var walkDomainData = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.InsertWalkData(walkDomainData);

                return Ok(mapper.Map<WalkDTO>(walkDomainData));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkDomainData = await walkRepository.GetAllWalks();

            var walkDtoData = mapper.Map<List<WalkDTO>>(walkDomainData);

            return Ok(walkDtoData);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walkDomainData = await walkRepository.GetWalkById(id);
            if (walkDomainData == null)
                return NotFound();

            var walkDtoData = mapper.Map<WalkDTO>(walkDomainData);

            return Ok(walkDtoData);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            var walkDomainData = mapper.Map<Walk>(updateWalkDto);
            walkDomainData = await walkRepository.UpdateWalk(id, walkDomainData);

            if (walkDomainData == null)
                return NotFound();

            return Ok(mapper.Map<WalkDTO>(walkDomainData));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomainData = await walkRepository.DeleteWalk(id);

            if (walkDomainData == null)
                return NotFound();

            return Ok(mapper.Map<WalkDTO>(walkDomainData));
        }
    }
}

