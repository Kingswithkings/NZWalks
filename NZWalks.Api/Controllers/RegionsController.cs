using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var regions = new List<Region>

        //    {
        //        new Region
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Auckland Region",
        //            Code = "AKL",
        //            RegionImageUrl = "https://images.pixel.com"
        //        },
        //        new Region
        //        {
        //            Id = Guid.NewGuid(), // Added missing comma and corrected Guid.NewGuid syntax
        //            Name = "Wellington Region",
        //            Code = "WLG",
        //            RegionImageUrl = "http://images.pixels.com" // Corrected the protocol to "http"
        //        }
        //    };

        //    return Ok(regions);
        //}

        //GET ALL REGIONS
        //GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data from Database - Domain Models
            var regionsDomain = dbContext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            // Return DTOs
            return Ok(regionsDto);
        }

        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:Portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            //var region = dbContext.regions.Find(id);
            // or
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound(); // 404 Not Found when the region with the specified ID is not found
            }

            return Ok(region); // 200 OK with the region data as the response
        }
        /*The find method only takes the primary key so you can't use it to find cod or other properties
        *UNDERSTANDING DTOs and DOMAIN MODELs DTOs
        Used to transfer data between differnt Layers
        Typically Contain a subset of the properties in the domainmodel
        For example transferring data over a network
        Domain Model relate with the database at Api point 
        DTO relate with the client from API if the client want to add new resource information it is recieved as a Dto then to Domain Model and then we send the Domain Model through entity framework to the database 
        Advantages of DTOs
        *Seperation of concerns *Performance *Security *Versoning
        *Change Method to Use Dto in model folder cretae a new folder DTO with a class RegionDto.cs*/
        //Post To Create New Region
        //Post:https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto  addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            //Use Domain Model to Create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();
            //Map Domain Model Back To DTO
            var regionDto = new RegionDto
            {
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        //Update region
        //Put:https:localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if region exists
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            // If region does not exist, return NotFound
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            // Assuming that updateRegionRequestDto.Id is not allowed to change
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            // Save changes to the database
            dbContext.SaveChanges();

            // Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        //Delete Region
        //Delete:https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            //find region in the database
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            // if region doesnot exist return not found
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete Region from the database
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();
            //return deleted Region back
            //map DomainModel to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };
            //return the deleted region
            return Ok(regionDto);
        }
    }
}


