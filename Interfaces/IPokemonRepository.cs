using PokemonReviewApp.Models;
using PokemonReviewApp.NewFolder;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExists(int pokeId);
        void AddPokemon(PokemonDto pokemonDto);
        void EditPokemon(int PokeId , PokemonDto pokemonDto);
        void DeletePokemon(int pokeId);

    }
}
