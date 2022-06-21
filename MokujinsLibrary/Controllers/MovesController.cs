using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using MokujinsLibrary.Dtos;
using MokujinsLibrary.Entities;
using MokujinsLibrary.Repositories;
using MongoDB.Bson.Serialization.Attributes;

namespace MokujinsLibrary.Controllers
{
    [ApiController]
    [Route("moves")]
    public class MovesController: ControllerBase
    {
        private readonly IMoveRepo repos;

        public MovesController(IMoveRepo reposit)
        {
            repos = reposit;
        }

        //GET /moves/{character}
        [HttpGet("{character}")]
        public async Task<IEnumerable<MoveDto>> GetCharMoves(string character)
        {
            character = character.ToLower();
            var moves = (await repos.GetCharMovesAsync(character)).Select(move => move.AsDto());
            return moves;
        }
        
        //GET /moves/{character}, {input}
        [HttpGet("{character}/{input}")]
        public async Task<ActionResult<MoveDto>> GetMove(string character, string input)
        {
            character = character.ToLower();
            input = input.ToLower();
            
            var move = await repos.GetMoveAsync(character, input);

            if (move is null)
            {
                return NotFound();
            }
            
            
            return move.AsDto();
        }

        //POST /moves
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<MoveDto>> CreateMove(CreateMoveDto moveDto)
        {
            
            Move move = new()
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
            await repos.CreateMoveAsync(move);
            
            return CreatedAtAction(nameof(CreateMove),move.AsDto());
        }

        // PUT /moves/{character}, {input}
        [HttpPut("{character}, {input}")]
        public async Task<ActionResult> UpdateMove(string character, string input, UpdateMoveDto moveDto)
        {
            character = character.ToLower();
            input = input.ToLower();
            
            var existingMove = await repos.GetMoveAsync(input, character);

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
            
            await repos.UpdateMoveAsync(updatedMove);

            return NoContent();
        }

        //DELETE /moves/{character}/{input}
        [HttpDelete("{character}, {input}")]
        public async Task<ActionResult> DeleteMove(string character, string input)
        {
            character = character.ToLower();
            input = input.ToLower();
            
            var existingMove = await repos.GetMoveAsync(input, character);

            if (existingMove is null)
            {
                return NotFound();
            }
            
            await repos.DeleteMoveAsync(character, input);

            return NoContent();
        }
        
    }
}