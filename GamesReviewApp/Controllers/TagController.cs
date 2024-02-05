using AutoMapper;
using GamesReviewApp.Dto;
using GamesReviewApp.Interfaces;
using GamesReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Tag>))]
        public IActionResult GetTags()
        {
            var tags = _mapper.Map<List<TagDto>>(_tagRepository.GetTags());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tags);
        }

        [HttpGet("{tagId}")]
        [ProducesResponseType(200, Type=typeof(Game))]
        [ProducesResponseType(400)]
        public IActionResult GetTag(int tagId)
        {
            if (!_tagRepository.TagExists(tagId))
                return NotFound();

            var tag = _mapper.Map<TagDto>(_tagRepository.GetTag(tagId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tag);
        }

        [HttpGet("game/{tagId}")]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult GetGameByTagId(int tagId)
        {
            var games = _mapper.Map<List<GameDto>>(_tagRepository.GetGameByTag(tagId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(games);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTag([FromBody] TagDto tagCreate)
        {
            if (tagCreate == null)
                return BadRequest(ModelState);

            var tag = _tagRepository.GetTags()
                .Where(t => t.Name.Trim().ToUpper() == tagCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(tag != null)
            {
                ModelState.AddModelError("", "Tag already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tagMap = _mapper.Map<Tag>(tagCreate);

            if(!_tagRepository.CreateTag(tagMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{tagId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTag(int tagId, [FromBody] TagDto updateTag)
        {
            if (updateTag == null)
                return BadRequest(ModelState);

            if (tagId != updateTag.Id)
                return BadRequest(ModelState);

            if (!_tagRepository.TagExists(tagId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tagMap = _mapper.Map<Tag>(updateTag);

            if(!_tagRepository.UpdateTag(tagMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete("{tagId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTag(int tagId)
        {
            if (!_tagRepository.TagExists(tagId))
                return NotFound();

            var tagToDelete = _tagRepository.GetTag(tagId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_tagRepository.DeleteTag(tagToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500);
            }

            return Ok("Successfully deleted");
        }
    }
}
