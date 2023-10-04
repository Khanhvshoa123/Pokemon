using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.NewFolder;
using FluentValidation;
using SendGrid.Helpers.Errors.Model;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<PokemonDto> _valid;

        public PokemonRepository(DataContext context, IMapper mapper,IValidator<PokemonDto> valid)
        {
            _context = context;
            _mapper = mapper;
            _valid = valid;
        }

        public void AddPokemon(PokemonDto pokemonDto)
        {
            // Validate the incoming PokemonDto
            var validationResult = _valid.Validate(pokemonDto);
            if (!validationResult.IsValid)
            {
                // Handle validation errors (e.g., log or throw an exception)
                throw new ValidationException(validationResult.Errors);
            }

            // Continue with your logic to add Pokemon
            var pokemon = _mapper.Map<Pokemon>(pokemonDto);
            _context.Pokemon.Add(pokemon);
            _context.SaveChanges();
        }

        public void DeletePokemon(int pokeId)
        {
            var existingPokemon = _context.Pokemon.Find(pokeId);
            if (existingPokemon == null)
            {
                throw new NotFoundException("Pokemon with ID { pokeId } not found");
            }
            
            _context.Pokemon.Remove(existingPokemon);
            _context.SaveChanges();




        }

        public void EditPokemon(int pokeId, PokemonDto pokemonDto)
        {
            var validationResult =_valid.Validate(pokemonDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var existingPokemon = _context.Pokemon.Find(pokeId);
            if(existingPokemon == null)
            {
                throw new NotFoundException("Pokemon with ID { pokeId } not found");
            }
            var edit = _mapper.Map(pokemonDto,existingPokemon);
            _context.SaveChanges() ;

        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemon.Any(p => p.Id == pokeId);
        }
    }
}
