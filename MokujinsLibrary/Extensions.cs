using MokujinsLibrary.Dtos;
using MokujinsLibrary.Entities;

namespace MokujinsLibrary
{
    public static class Extensions
    {
        public static MoveDto AsDto(this Move move)
        {
            return new MoveDto()
            {
                moveName = move.moveName,
                character = move.character,
                input = move.input,
                damage = move.damage,
                framesStartup = move.framesStartup,
                framesOnBlock = move.framesOnBlock,
                notes = move.notes,
                hitLevel = move.hitLevel
            };
        }
        
    }
}