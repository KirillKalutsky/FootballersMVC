using System.Threading.Tasks;
using FootballPlayers.Repositories;
using AutoMapper;
using FootballPlayers.Models;
using FootballPlayers.Models.Transfers;
using Microsoft.AspNetCore.Mvc;
using FootballPlayers.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FootballPlayers.Controllers
{
    [Route("controller")]
    [ApiController]
    public class FootballTeamsController : Controller
    {
        private readonly IMapper mapper;
        private readonly Repository<Team<Footballer>> teamRepository;
        private readonly IHubContext<FootballerHub> footballerHub;
        public FootballTeamsController(IMapper mapper, Repository<Team<Footballer>> teamRepository,
            IHubContext<FootballerHub> footballerHub)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
            this.footballerHub = footballerHub;
        }

        [HttpPost]
        public async Task<ActionResult> InsertTeam([FromForm] CreateTeamDto dto)
        {
            var team = mapper.Map<Team<Footballer>>(dto);

            await teamRepository.InsertAsync(team);

            var insertTeamDto = mapper.Map<ShowTeamDto>(team);
            await footballerHub.Clients.Groups("teams").SendAsync("UpdateTeamsList",insertTeamDto);

            return NoContent();
        }
    }
}
