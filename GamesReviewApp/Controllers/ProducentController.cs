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
        private readonly ICountryRepository _countryRepository;

        public ProducentController(IProducentRepository producentRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _producentRepository = producentRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProducent([FromQuery] int countryId, [FromBody] ProducentDto producentCreate)
        {
            if (producentCreate == null)
                return BadRequest(ModelState);

            var producent = _producentRepository.GetProducers()
                .Where(p => p.Name.Trim().ToUpper() == producentCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(producent != null)
            {
                ModelState.AddModelError("", "Producent already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producentMap = _mapper.Map<Producent>(producentCreate);

            producentMap.Country = _countryRepository.GetCountry(countryId);

            if(!_producentRepository.CreateProducent(producentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
