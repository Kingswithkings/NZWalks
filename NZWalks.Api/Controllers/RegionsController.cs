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

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
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
        *Change bMethod to Use Dto in model folder cretae a new folder DTO with a class RegionDto.cs*/

        }
    }


