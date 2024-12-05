using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.CustomActionFilter;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;
using NZWalkAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegionsController : Controller
    {
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Region> regions = await regionRepository.GetRegionsAsync();

            #region Replace with Automapper
            //var regionDtos = new List<RegionDTO>();
            //foreach (var regionDomain in regions)
            //{
            //    regionDtos.Add(new RegionDTO
            //    {
            //        Id = regionDomain.Id,
            //        Name = regionDomain.Name,
            //        Code = regionDomain.Code,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    });
            //}
            #endregion
            var regionDtos = mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var regions = NZWalkDbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetRegionsByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            #region Replace with AutoMapper
            //var regionDto = new RegionDTO
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            #endregion

            var regionDto = mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> InsertRegion([FromBody] AddRegionDTO addRegionDTO)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionDTO);

            await regionRepository.InsertRegionAsync(regionDomainModel);

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDTO);

        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {

            var regionDomain = mapper.Map<Region>(updateRegionDTO);

            regionDomain = await regionRepository.UpdateRegionAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDTO);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteRegionAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(regionDomain);
        }
    }
}

