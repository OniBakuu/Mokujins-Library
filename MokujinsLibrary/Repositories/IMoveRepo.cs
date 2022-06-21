using System.Collections.Generic;
using MokujinsLibrary.Entities;

namespace MokujinsLibrary.Repositories
{
    public interface IMoveRepo
    {
        IEnumerable<Move> GetMoves(string character);
        Move GetMove(string input, string character);
        void CreateMove(Move move);
        void UpdateMove(Move move);
        void DeleteMove(string character, string input);
    }
}