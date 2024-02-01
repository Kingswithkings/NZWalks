using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;
using NZWalks.Api.Models;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        /*[HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>

            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pixel.com"
                },
                new Region
                {
                    Id = Guid.NewGuid(), // Added missing comma and corrected Guid.NewGuid syntax
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "http://images.pixels.com" // Corrected the protocol to "http"
                }
            };

           return Ok(regions);
        }*/

        /*//GET ALL REGIONS     
        //GET:https://localhost:portnumber/api/regions
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
        }*/


        ////GET ALL REGIONS     
        ////GET:https://localhost:portnumber/api/regions
        //[HttpGet]
        //public async Task <IActionResult> GetAll()
        //{
        //    // Get Data from Database - Domain Models
        //    var regionsDomain = await dbContext.Regions.ToListAsync();

        //    // Map Domain Models to DTOs
        //    var regionsDto = new List<RegionDto>();
        //    foreach (var region in regionsDomain)
        //    {
        //        regionsDto.Add(new RegionDto()
        //        {
        //            Id = region.Id,
        //            Code = region.Code,
        //            Name = region.Name,
        //            RegionImageUrl = region.RegionImageUrl,
        //        });
        //    }

        //    // Return DTOs
        //    return Ok(regionsDto);
        //}


        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    // Get Data from Database - Domain Models
        //    var regionsDomain = await regionRepository.GetAllAsync();

        //    // Map Domain Models to DTOs using LINQ projection
        //    var regionsDto = regionsDomain.Select(regionDomain => new RegionDto
        //    {
        //        Id = regionDomain.Id,
        //        Code = regionDomain.Code,
        //        Name = regionDomain.Name,
        //        RegionImageUrl = regionDomain.RegionImageUrl,
        //    }).ToList();

        //    return Ok(regionsDto); //Note i used this for the repository pattern
        //}

        //Map Domain Models to DTOs


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from Database - Domain Models
            var regionsDomain = await regionRepository.GetAllAsync();
            // This is using the AutMapper
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            // Returns DTOs
            return Ok(regionsDto);



            /*// GET SINGLE REGION (Get Region By ID)
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
            }*/
            // GET SINGLE REGION (Get Region By ID)
            // GET: https://localhost:Portnumber/api/regions/{id}
            //[HttpGet]
            //[Route("{id:Guid}")]
            //public async Task<IActionResult> GetById(Guid id)
            //{
            //    //var region = dbContext.regions.Find(id);
            //    // or
            //    var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //    if (region == null)
            //    {
            //        return NotFound(); // 404 Not Found when the region with the specified ID is not found
            //    }

            //    return Ok(region); // 200 OK with the region data as the response
            //}
        }
            [HttpGet]
            [Route("{id:Guid}")]
            public async Task<IActionResult> GetById([FromRoute] Guid id)
            {
                //var region = dbContext.Regions.Find(id);
                //var RegionDomainModel from Database
                var regionDomain = await regionRepository.GetByIdAsync(id);

                if (regionDomain == null)
                {
                    return NotFound("Region not found"); // You can customize the message
                }

                // Map/Convert Region Domain Model to Region DTO
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomain.Id,
                //    Code = regionDomain.Code,
                //    Name = regionDomain.Name,
                //    RegionImageUrl = regionDomain.RegionImageUrl,
                //};

                //return Ok(regionDto);
             // This is for Repository Pattern

                //Return DTO back to Client
                return Ok(mapper.Map<RegionDto>(regionDomain));
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
            /*//Post To Create New Region
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
            }*/
            //Post To Create New Region
            //Post:https://localhost:portnumber/api/regions
            //[HttpPost]
            //public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
            //{
            //    //Map or Convert DTO to Domain Model
            //    var regionDomainModel = new Region
            //    {
            //        Code = addRegionRequestDto.Code,
            //        Name = addRegionRequestDto.Name,
            //        RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            //    };

            //    //Use Domain Model to Create Region
            //    dbContext.Regions.Add(regionDomainModel);
            //    dbContext.SaveChanges();
            //    //Map Domain Model Back To DTO
            //    var regionDto = new RegionDto
            //    {
            //        Code = regionDomainModel.Code,
            //        Name = regionDomainModel.Name,
            //        RegionImageUrl = regionDomainModel.RegionImageUrl,
            //    };
            //    return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            //}
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
            {
                //Map or Convert DTO to Domain Model
                var regionDomainModel = new Region
                {
                    Code = addRegionRequestDto.Code,
                    Name = addRegionRequestDto.Name,
                    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
                };

                //Use Domain Model to Create Region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
                //Map Domain Model Back To DTO
                // from this convert line so that you can use the automaper but if it is the repository and the NZWalksDbContext uncomment it

                //var regionDto = new RegionDto
                //{
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};
                //Map Domain Model Back To DTO
                // from this convert line so that you can use the automaper but if it is the repository and the NZWalksDbContext uncomment it
               var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            }


            /*//Update region
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
            }*/
            //Update region
            //Put:https:localhost:portnumber/api/regions/{id}
            //[HttpPut]
            //[Route("{id:Guid}")]
            //public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
            //{
            //    // Check if region exists
            //    var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //    // If region does not exist, return NotFound
            //    if (regionDomainModel == null)
            //    {
            //        return NotFound();
            //    }

            //    // Map DTO to Domain Model
            //    // Assuming that updateRegionRequestDto.Id is not allowed to change
            //    regionDomainModel.Code = updateRegionRequestDto.Code;
            //    regionDomainModel.Name = updateRegionRequestDto.Name;
            //    regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //    // Save changes to the database
            //    dbContext.SaveChanges();

            //    // Convert Domain Model to DTO
            //    var regionDto = new RegionDto
            //    {
            //        Id = regionDomainModel.Id,
            //        Name = regionDomainModel.Name,
            //        Code = regionDomainModel.Code,
            //        RegionImageUrl = regionDomainModel.RegionImageUrl,
            //    };

            //    return Ok(regionDto);
            //}
            [HttpPut]
            [Route("{id:Guid}")]
            public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
            {
                //Map DTO to Domain Model
                var regionDomainModel = new Region
                {
                    Code = updateRegionRequestDto.Code,
                    Name = updateRegionRequestDto.Name,
                    RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
                };
                //check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                //Map DTO to Domain Model
                regionDomainModel.Code = updateRegionRequestDto.Code;
                regionDomainModel.Name = updateRegionRequestDto.Name;
                regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
                await dbContext.SaveChangesAsync();
                //Convert Domain Model to DTO
                // from this convert line so that you can use the automaper but if it is the repository and the NZWalksDbContext uncomment it
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //};
                //return Ok(regionDto);


                //Convert Domain Model to DTO
                 return Ok(mapper.Map<RegionDto>(regionDomainModel));
            }


            //    [HttpPut]
            //    [Route("{id:Guid}")]
            //    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
            //    {
            //        //Map DTO to Domain Model
            //        var regionDomainModel = new Region
            //        {
            //            var regionDomainModel = new Region
            //            {
            //                Code = updateRegionRequestDto.Code,
            //                Name = updateRegionRequestDto.Name,
            //                RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
            //            }
            //        };

            //        // Check if region exists
            //         regionDomainModel = await RegionRepository.UpdateAsync(id, regionDomainModel);

            //        if (regionDomainModel == null)
            //        {
            //            return NotFound();
            //        }

            //        // If region does not exist, return NotFound
            //        if (regionDomainModel == null)
            //        {
            //            return NotFound();
            //        }
            //        // Map DTO to Domain Model
            //        regionDomainModel.Code = updateRegionRequestDto.Code;
            //        regionDomainModel.Name = updateRegionRequestDto.Name;
            //        regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            //        await dbContext.SaveChangesAsync();

            //        //Convert Domain Model to DTO
            //        var regionDto = new RegionDto
            //        {
            //            Id = regionDomainModel.Id,
            //            Code = regionDomainModel.Code,
            //            Name = regionDomainModel.Name,
            //            RegionImageUrl = regionDomainModel.RegionImageUrl,
            //        };
            //        return Ok (regionDto);
            //    }
            //}


            //Delete Region
            //Delete:https://localhost:portnumber/api/regions/{id}
            [HttpDelete]
            [Route("{id:Guid}")]
            //public IActionResult Delete([FromRoute] Guid id)
            //{
            //    //find region in the database
            //    var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            //    // if region doesnot exist return not found
            //    if (regionDomainModel == null)
            //    {
            //        return NotFound();
            //    }

            //    //Delete Region from the database
            //    dbContext.Regions.Remove(regionDomainModel);
            //    dbContext.SaveChanges();
            //    //return deleted Region back
            //    //map DomainModel to DTO
            //    var regionDto = new RegionDto
            //    {
            //        Id = regionDomainModel.Id,
            //        Code = regionDomainModel.Code,
            //        Name = regionDomainModel.Name,
            //        RegionImageUrl = regionDomainModel.RegionImageUrl,
            //    };
            //    //return the deleted region
            //    return Ok(regionDto);
            //}

                [HttpDelete]
                [Route("{id:Guid}")]
                public async Task<IActionResult> Delete([FromRoute] Guid id)
                {
                    var regionDomainModel = await regionRepository.DeleteAsync(id);


                    // if region doesnot exist return not found
                    if (regionDomainModel == null)
                    {
                        return NotFound();
                    }
                    return Ok(mapper.Map<RegionDto>(regionDomainModel));
                    //    //Delete Region from the database
                    //    dbContext.Regions.Remove(regionDomainModel);
                    //    dbContext.SaveChanges();
                    //    //return deleted Region back
                    //    //map DomainModel to DTO
                    //    var regionDto = new RegionDto
                    //    {
                    //        Id = regionDomainModel.Id,
                    //        Code = regionDomainModel.Code,
                    //        Name = regionDomainModel.Name,
                    //        RegionImageUrl = regionDomainModel.RegionImageUrl,
                    //    };

                }
        
    }
}



