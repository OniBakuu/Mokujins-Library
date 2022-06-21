using System.Collections.Generic;
using System.Threading.Tasks;
using MokujinsLibrary.Entities;

namespace MokujinsLibrary.Repositories
{
    public interface IMoveRepo
    {
        Task<IEnumerable<Move>> GetCharMovesAsync(string character);
        Task<Move> GetMoveAsync(string input, string character);
        Task CreateMoveAsync(Move move);
        Task UpdateMoveAsync(Move move);
        Task DeleteMoveAsync(string character, string input);
    }
}