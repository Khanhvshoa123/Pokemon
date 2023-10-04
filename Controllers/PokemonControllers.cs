
using AutoMapper;
using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PokemonReviewApp.Fluent;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.NewFolder;
using SendGrid.Helpers.Errors.Model;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PokemonControllers : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<PokemonDto> _valid;

        public PokemonControllers(IPokemonRepository pokemonRepository, IMapper mapper, IValidator<PokemonDto> valid)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
            _valid = valid;
        }
        [HttpGet]

        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());



            return Ok(pokemons);


        }
        [HttpGet("{pokeId}")]

        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemon);
        }
        [HttpGet("{pokeId}/rating")]

        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();
            var rating = _pokemonRepository.GetPokemonRating(pokeId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(rating);
        }
        [HttpPost]
        public IActionResult AddPokemon([FromBody] PokemonDto pokemonDto)
        {
            try
            {
                _pokemonRepository.AddPokemon(pokemonDto);
                return Ok("Pokemon added successfully");
            }
            catch (ValidationException ex)
            {
                // Handle validation errors
                return BadRequest(ex.Errors);
            }
        }
            [HttpPut("{pokeId}")]
        public IActionResult EditPokemon(int pokeId, [FromBody] PokemonDto pokemonDto)
            {
                try
                {
                    _pokemonRepository.EditPokemon(pokeId, pokemonDto);
                    return Ok("Pokemon updated successfully");
                }
                catch (ValidationException ex)
                {
                    // Handle validation errors
                    return BadRequest(ex.Errors);
                }
                catch (NotFoundException ex)
                {
                    // Handle not found exception
                    return NotFound(ex.Message);
                }

            
        }
        [HttpDelete]
        public IActionResult DeletePokemon(int pokeId)
        {
            try
            {
                _pokemonRepository.DeletePokemon(pokeId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "K tim thay");
            }
        }
    } }