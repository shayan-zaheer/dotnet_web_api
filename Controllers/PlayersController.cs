using Core_Web_API.Data;
using Core_Web_API.Helpers;
using Core_Web_API.Models.DTOs;
using Core_Web_API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Core_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlayersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _context.Players.ToListAsync();
            var playersDto = _mapper.Map<List<ReadPlayerDto>>(players);

            return Ok(ResponseHelper<List<ReadPlayerDto>>.SuccessResponse(playersDto));
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(CreatePlayerDto createPlayerDto)
        {
            var player = _mapper.Map<Player>(createPlayerDto);

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            var playerDto = _mapper.Map<ReadPlayerDto>(player);

            return Ok(ResponseHelper<ReadPlayerDto>.SuccessResponse(playerDto, "Player created successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound(ResponseHelper<ReadPlayerDto>.FailureResponse($"Player with ID {id} not found!"));
            }
            
            var readPlayerDto = _mapper.Map<ReadPlayerDto>(player);

            return Ok(ResponseHelper<ReadPlayerDto>.SuccessResponse(readPlayerDto, "Player retrieved successfully!"));
        }
    }
}