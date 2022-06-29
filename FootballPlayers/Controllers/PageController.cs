using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using FootballPlayers.Models; 
using FootballPlayers.Repositories;
using FootballPlayers.Models.Transfers;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;

namespace FootballPlayers.Controllers
{
    [Route("[controller]")]
    public class PageController : Controller
    {
        private readonly IMapper mapper;
        private readonly Repository<Team<Footballer>> teamsRep;
        private readonly Repository<Footballer> footballersRep;
        public PageController(IMapper mapper, Repository<Team<Footballer>> teamsRep,
            Repository<Footballer> footballersRep)
        {
            this.mapper = mapper;
            this.teamsRep = teamsRep;
            this.footballersRep = footballersRep;
        }

        [HttpGet("First")]
        public async Task<ActionResult> GetFirstPageView()
        {
            var teams = await teamsRep.GetAllAsync(1, 100);
            ViewBag.Teams = new SelectList(teams.Items, "Id", "Name");
            ViewBag.MethodName = "Insert";
            ViewBag.ControllerName = "Footballers";
            return View(@".\Pages\Main\FirstPage.cshtml");
        }

        [HttpGet("Second")]
        public async Task<ActionResult> GetSecondPageView()
        {
            var footballers = await footballersRep.GetAllAsync(1, 100);
            ViewBag.Footballers = footballers.Items;
            return View(@".\Pages\Main\SecondPage.cshtml");
        }

        [HttpGet("Edit/{playerId}")]
        public async Task<ActionResult> GetEditPageView([FromRoute] Guid playerId)
        {
            var player = await footballersRep.FindByIdAsync(playerId);

            if(player == null)
            {
                throw new ArgumentException();
            }

            var teams = await teamsRep.GetAllAsync(1, 100);
            ViewBag.Teams = new SelectList(teams.Items, "Id", "Name", player.TeamId);
            ViewBag.MethodName = "Edit";
            ViewBag.ControllerName = "Footballers";
            ViewBag.ModelId = player.Id;

            var dto = mapper.Map<CreateFootballerDto>(player);

            return View(@".\Pages\Main\EditPage.cshtml", dto);
        }

        [HttpGet("LoadTeamDialog")]
        public ActionResult GetTeamDialogView()
        {
            return View(@".\Pages\Components\CreateTeamDialog.cshtml");
        }

        [HttpGet("FootballerForm")]
        public ActionResult GetFootballerFormView()
        {
            return View(@".\Pages\Components\FootballerForm.cshtml");
        }
    }
}
