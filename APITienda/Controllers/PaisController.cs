using APITienda.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infraestructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace APITienda.Controllers;
public class PaisController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public PaisController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PaisDto>>> Get()
    {
        var pais = await unitofwork.Paises.GetAllAsync();
        return mapper.Map<List<PaisDto>>(pais);
    }

   /*  [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Get(int id)
    {
        var pais = await unitofwork.Paises.GetByIdAsync(id);
        return Ok(pais);
    } */

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PaisDto>> Get(int id)
    {
        var pais = await unitofwork.Paises.GetByIdAsync(id);
        if (pais == null){
            return NotFound();
        }
        return this.mapper.Map<PaisDto>(pais);
    }
    
   /*  [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pais>> Post(Pais pais){
        unitofwork.Paises.Add(pais);
        await unitofwork.SaveAsync();
        if (pais == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post),new {id=pais.Id},pais);
    } */
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pais>> Post(PaisDto paisDto)
    {
        var pais = this.mapper.Map<Pais>(paisDto);
        this.unitofwork.Paises.Add(pais);
        await unitofwork.SaveAsync();
        if(pais == null)
        {
            return BadRequest();
        }
        paisDto.Id = pais.Id;
        return CreatedAtAction(nameof(Post), new {id = paisDto.Id}, paisDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PaisDto>> Put(int id, [FromBody]PaisDto paisDto){
        if(paisDto == null)
        {
            return NotFound();
        }
        var pais = this.mapper.Map<Pais>(paisDto);
        unitofwork.Paises.Update(pais);
        await unitofwork.SaveAsync();
        return paisDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var pais = await unitofwork.Paises.GetByIdAsync(id);
        if(pais == null)
        {
            return NotFound();
        }
        unitofwork.Paises.Remove(pais);
        await unitofwork.SaveAsync();
        return NoContent();
    }

}

internal class MapToApiVersionAttribute : Attribute
{
}