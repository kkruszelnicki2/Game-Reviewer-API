using AutoMapper;
using GamesReviewApp.Dto;
using GamesReviewApp.Interfaces;
using GamesReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var reviews = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("game/{reviewId}")]
        [ProducesResponseType(200,Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForAGame(int gameId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAGame(gameId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }
    }
}
