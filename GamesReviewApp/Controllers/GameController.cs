using AutoMapper;
using GamesReviewApp.Dto;
using GamesReviewApp.Interfaces;
using GamesReviewApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GamesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public GameController(IGameRepository gameRepository, 
            IMapper mapper,
            IReviewRepository reviewRepository)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        public IActionResult GetGames()
        {
            var games = _mapper.Map<List<GameDto>>(_gameRepository.GetGames());

            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(games);
        }

        [HttpGet("{gameId}")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]
        public IActionResult GetGame(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var game =_mapper.Map<GameDto>(_gameRepository.GetGame(gameId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(game);
        }

        [HttpGet("{gameId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetGameRating(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var rate = _gameRepository.GetGameRating(gameId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rate);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGame([FromQuery] int producentId, [FromQuery] int tagId ,[FromBody] GameDto gameCreate)
        {
            if (gameCreate == null)
                return BadRequest(ModelState);

            var games = _gameRepository.GetGames()
                .Where(g => g.Name.Trim().ToUpper() == gameCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(games != null)
            {
                ModelState.AddModelError("", "Game already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gameMap = _mapper.Map<Game>(gameCreate);

            if (!_gameRepository.CreateGame(producentId,tagId,gameMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{gameId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGame(int gameId, [FromQuery] int producentId, 
            [FromQuery] int tagId,[FromBody] GameDto gameUpdate)
        {
            if (gameUpdate == null)
                return BadRequest(ModelState);

            if (gameId != gameUpdate.Id)
                return BadRequest(ModelState);

            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gameMap = _mapper.Map<Game>(gameUpdate);

            if(!_gameRepository.UpdateGame(producentId, tagId, gameMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{gameId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGame(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();

            var reviewsToDelete = _reviewRepository.GetReviewsOfAGame(gameId);
            var gameToDelete = _gameRepository.GetGame(gameId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_gameRepository.DeleteGame(gameToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500);
            }

            return Ok("Successfully deleted");
        }
    }
}
