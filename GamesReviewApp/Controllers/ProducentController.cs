using AutoMapper;
using GamesReviewApp.Dto;
using GamesReviewApp.Interfaces;
using GamesReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducentController : Controller
    {
        private readonly IProducentRepository _producentRepository;
        private readonly IMapper _mapper;

        public ProducentController(IProducentRepository producentRepository, IMapper mapper)
        {
            _producentRepository = producentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Producent>))]
        public IActionResult GetProducers()
        {
            var producers = _mapper.Map<List<ProducentDto>>(_producentRepository.GetProducers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producers);
        }

        [HttpGet("{producentId}")]
        [ProducesResponseType(200,Type = typeof(Producent))]
        [ProducesResponseType(400)]
        public IActionResult GetProducent(int producentId)
        {
            if(!_producentRepository.ProducentExists(producentId))
                return NotFound();

            var producent = _mapper.Map<ProducentDto>(_producentRepository.GetProducent(producentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producent);
        }

        [HttpGet("{producentId}/game")]
        [ProducesResponseType(200,Type = typeof(Producent))]
        [ProducesResponseType(400)]
        public IActionResult GetGameByProducent(int producentId)
        {
            if (!_producentRepository.ProducentExists(producentId))
                return NotFound();

            var producent = _mapper.Map<List<GameDto>>(_producentRepository.GetGameByProducent(producentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(producent);
        }
    }
}
