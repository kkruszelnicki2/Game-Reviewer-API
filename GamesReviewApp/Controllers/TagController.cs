﻿using AutoMapper;
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
    }
}
