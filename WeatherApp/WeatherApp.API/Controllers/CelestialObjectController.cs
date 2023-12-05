using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Custom_Attributes;
using WeatherApp.API.Models.Domains;
using WeatherApp.API.Models.DTOs;
using WeatherApp.API.Repositories;

namespace WeatherApp.API.Controllers;

[Route("api/v1/celestialobjects")]
[ApiController]
public class CelestialObjectController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICelestialObjectRepository _repository;

    public CelestialObjectController(IMapper mapper,ICelestialObjectRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    //GET ALL CELESTIAL OBJECTS
    //GET: /api/v1/celestialobjects
    [HttpGet]
    [Authorize(Roles = "Reader,Changer")]
    public async Task<IActionResult> GetAll(string? filterOn = null,string? filterQuery = null, 
        string? sortBy = null,bool? isAscending = null,int pageNumber = 1,int pageSize = 1000)
    {
        //Get Data from Database
        var celestialObjects = await _repository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending,pageNumber,pageSize);

        //Map Domain Models to DTOs
        var celestialObjectDto = _mapper.Map<List<CelestialObjectDto>>(celestialObjects);

        //Return DTOs
        return Ok(celestialObjectDto);
    }
    
    //GET CELESTIAL OBJECT BY ID
    //GET: /api/v1/celestialobjects/{id}
    [HttpGet]
    [Route("{id:guid}")]
    [Authorize(Roles = "Reader,Changer")]
    public async Task<IActionResult> GetById(Guid id)
    {
        //Get Data From Database - Domain Models
        var celestialObjectDomain = await _repository.GetByIdAsync(id);

        //Check if the object exists
        if (celestialObjectDomain == null)
            return NotFound();

        //Map Domain Models to DTOs
        var celestialObjectDto = _mapper.Map<CelestialObjectDto>(celestialObjectDomain);

        return Ok(celestialObjectDto);
    }
    
    //CREATE CELESTIAL OBJECT
    //POST: /api/v1/celestialobjects
    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Changer")]
    public async Task<IActionResult> Create([FromBody] CelestialObjectDto celestialObjectDto)
    {
        //Map DTO to Domain model
        var celestialObjectDomain = _mapper.Map<CelestialObject>(celestialObjectDto);

        await _repository.CreateAsync(celestialObjectDomain);

        return Ok(celestialObjectDto);
    }

    //UPDATE CELESTIAL OBJECT
    //PUT: /api/v1/celestialobjects/{id}
    [HttpPut]
    [Route("{id:guid}")]
    [ValidateModel]
    [Authorize(Roles = "Changer")]
    public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] CelestialObjectDto celestialObjectDto)
    {
        //Map DTO to Domain Model
        var celestialObjectDomain = _mapper.Map<CelestialObject>(celestialObjectDto);

        celestialObjectDomain = await _repository.UpdateAsync(id, celestialObjectDomain);

        if (celestialObjectDomain == null)
            return NotFound();
        
        //Convert Domain Model to DTO
        var dto = _mapper.Map<CelestialObjectDto>(celestialObjectDomain);

        return Ok(dto);
    }
    
    //DELETE CELESTIAL OBJECT
    //DELETE: /api/v1/celestialobjects/{id}
    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize(Roles = "Changer")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var objectToDelete = await _repository.DeleteAsync(id);

        if (objectToDelete == null)
            return NotFound();
        
        return Ok(_mapper.Map<CelestialObjectDto>(objectToDelete));
    }
}