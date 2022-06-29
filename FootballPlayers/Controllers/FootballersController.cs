using FootballPlayers.Hubs;
using System.Threading.Tasks;
using FootballPlayers.Repositories;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using FootballPlayers.Models;
using FootballPlayers.Models.Transfers;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace FootballPlayers.Controllers
{
    [Route("{controller}")]
    public class FootballersController : Controller
    {
        private readonly IMapper mapper;
        private readonly Repository<Footballer> footballerRepository;
        private readonly Repository<Team<Footballer>> teamRepository;
        private readonly IHubContext<FootballerHub> footballerHub;
        public FootballersController(IMapper mapper, Repository<Footballer> footballerRep,
            Repository<Team<Footballer>> teamRep, IHubContext<FootballerHub> footballerHub)
        {
            footballerRepository = footballerRep;
            this.mapper = mapper;
            teamRepository = teamRep;
            this.footballerHub = footballerHub;
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromForm] CreateFootballerDto dto)
        {
            var player = mapper.Map<Footballer>(dto);

            var team = await teamRepository.FindByIdAsync(dto.TeamId);
            player.Team = team;

            await footballerRepository.InsertAsync(player);

            var showDto = mapper.Map<ShowFootballerDto>(player);
            await footballerHub.Clients.Groups("footballers").SendAsync("UpdateFootballerTable", showDto);

            return Redirect("/Page/First");
        }

        [HttpPost("{playerId}")]
        public async Task Edit([FromRoute] Guid playerId, [FromForm] CreateFootballerDto dto)
        {
            var player = new Footballer(playerId);
            mapper.Map(dto, player);

            await footballerRepository.UpdateOrInsertAsync(player);

            Response.Redirect("/Page/Second");
        }
    }
}
