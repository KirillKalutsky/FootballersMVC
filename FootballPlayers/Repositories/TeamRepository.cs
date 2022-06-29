using System.Linq;
using FootballPlayers.DB;
using FootballPlayers.Models;
using AutoMapper;
using System.Threading.Tasks;

namespace FootballPlayers.Repositories
{
    public class TeamRepository : Repository<Team<Footballer>>
    {
        private readonly IMapper mapper;
        public TeamRepository(FootballContext context, IMapper mapper) 
            : base(context)
        {
            this.mapper = mapper;
        }

        public override async Task<PageList<Team<Footballer>>> GetAllAsync(int currentPage, int pageSize)
        {
            return await Task.Run(() =>
                new PageList<Team<Footballer>>(objects.OrderBy(team=>team.Name), currentPage, pageSize)
            );
        }

        public override async Task UpdateOrInsertAsync(Team<Footballer> obj)
        {
            var oldTeam = await FindByIdAsync(obj.Id);

            if (oldTeam == null)
            {
                await InsertAsync(obj);
                return;
            }

            mapper.Map(obj, oldTeam);

            await SaveAsync();
        }
    }
}
