using System.Collections.Generic;
using System.Linq;
using MokujinsLibrary.Entities;

namespace MokujinsLibrary.Repositories
{
    public class InMemMoveRepo : IInMemMoveRepo
    {
        private readonly List<Move> moves = new()
        {
            new Move
            {
                character = "dragunov", moveName = "razer", input = "d+2", damage = "17", hitLevel = "l",
                framesStartup = "18", framesOnBlock = "+1"
            }, 
            
            new Move
            {
                character = "dragunov", moveName = "blizzard hammer", input = "b+1+2", damage = "26", hitLevel = "m",
                framesStartup = "22~23", framesOnBlock = "+6~+7"
            }
        };

        public IEnumerable<Move> GetMoves(string character)
        {
            character = character.ToLower();
            return moves.Where(move => move.character == character);
        }

        public Move GetMove(string character, string input)
        {
            character = character.ToLower();
            input = input.ToLower();

            return moves.SingleOrDefault(move => move.input == input & move.character == character);
        }

        public void CreateMove(Move move)
        {
            moves.Add(move);
        }

        public void UpdateMove(Move move)
        {
            var index = moves.FindIndex(existingMove =>
                existingMove.character == move.character && existingMove.input == move.input);
            moves[index] = move;
        }
        
        public void DeleteMove(string character, string input)
        {
            var index = moves.FindIndex(existingMove =>
                existingMove.character == character && existingMove.input == input);
            
            moves.RemoveAt(index);
        }

    }
}