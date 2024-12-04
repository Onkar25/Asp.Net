using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegionsController : Controller
    {
        public RegionsController(NZWalkDbContext nZWalkDbContext)
        {
            NZWalkDbContext = nZWalkDbContext;
        }

        public NZWalkDbContext NZWalkDbContext { get; }

        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = NZWalkDbContext.Regions.ToList();
            var regionDtos = new List<RegionDTO>();
            foreach (var regionDomain in regions)
            {
                regionDtos.Add(new RegionDTO
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            return Ok(regionDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var regions = NZWalkDbContext.Regions.Find(id);
            var regionDomain = NZWalkDbContext.Regions.FirstOrDefault(rg => rg.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult InsertRegion([FromBody] AddRegionDTO addRegionDTO)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionDTO.Code,
                Name = addRegionDTO.Name,
                RegionImageUrl = addRegionDTO.RegionImageUrl
            };

            NZWalkDbContext.Regions.Add(regionDomainModel);
            NZWalkDbContext.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDTO);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            var regionDomain = NZWalkDbContext.Regions.FirstOrDefault(reg => reg.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            regionDomain.Code = updateRegionDTO.Code;
            regionDomain.Name = updateRegionDTO.Name;
            regionDomain.RegionImageUrl = updateRegionDTO.RegionImageUrl;

            NZWalkDbContext.SaveChanges();
            var regionDTO = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = NZWalkDbContext.Regions.FirstOrDefault(reg => reg.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            NZWalkDbContext.Regions.Remove(regionDomain);
            NZWalkDbContext.SaveChanges();

            return Ok();
        }
    }
}

