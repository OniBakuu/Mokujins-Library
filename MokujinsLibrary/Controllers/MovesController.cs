using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MokujinsLibrary.Dtos;
using MokujinsLibrary.Entities;
using MokujinsLibrary.Repositories;

namespace MokujinsLibrary.Controllers
{
    [ApiController]
    [Route("moves")]
    public class MovesController: ControllerBase
    {
        private readonly IInMemMoveRepo repos;

        public MovesController(IInMemMoveRepo reposit)
        {
            repos = reposit;
        }

        //GET /moves
        [HttpGet("{character}")]
        public IEnumerable<MoveDto> GetCharMoves(string character)
        {
            var moves = repos.GetMoves(character).Select(move => move.AsDto());
            return moves;
        }
        
        //GET /moves/{character}/{input}
        [HttpGet("{character}, {input}")]
        public ActionResult<MoveDto> GetMove(string character, string? input)
        {
            var move = repos.GetMove(character, input);

            if (move is null)
            {
                return NotFound();
            }
            
            
            return move.AsDto();
        }

        //POST /moves
        [HttpPost]
        public ActionResult<MoveDto> CreateMove(CreateMoveDto moveDto)
        {
            Move move = new Move()
            {
                character = moveDto.character,
                moveName = moveDto.moveName,
                input = moveDto.input,
                damage = moveDto.damage,
                hitLevel = moveDto.hitLevel,
                framesStartup = moveDto.framesStartup,
                framesOnBlock = moveDto.framesOnBlock,
                notes = moveDto.notes
            };
            repos.CreateMove(move);
            
            return CreatedAtAction(nameof(GetMove), move.AsDto());
        }

        // PUT /moves/{character}/{input}
        [HttpPut("{character}, {input}")]
        public ActionResult UpdateMove(string character, string input, UpdateMoveDto moveDto)
        {
            var existingMove = repos.GetMove(input, character);

            if (existingMove is null)
            {
                return NotFound();
            }

            Move updatedMove = existingMove with
            {
                character = moveDto.character,
                moveName = moveDto.moveName,
                input = moveDto.input,
                damage = moveDto.damage,
                hitLevel = moveDto.hitLevel,
                framesStartup = moveDto.framesStartup,
                framesOnBlock = moveDto.framesOnBlock,
                notes = moveDto.notes
            };
            
            repos.UpdateMove(updatedMove);

            return NoContent();
        }

        //DELETE /moves/{character}/{input}
        [HttpDelete("{character}, {input}")]
        public ActionResult DeleteMove(string character, string input)
        {
            var existingMove = repos.GetMove(input, character);

            if (existingMove is null)
            {
                return NotFound();
            }
            
            repos.DeleteMove(character, input);

            return NoContent();
        }
        
    }
}