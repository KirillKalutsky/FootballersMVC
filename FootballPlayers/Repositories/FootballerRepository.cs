using System.Linq;
using FootballPlayers.Models;
using AutoMapper;
using System.Threading.Tasks;
using FootballPlayers.DB;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.Repositories
{
    public class FootballerRepository : Repository<Footballer>
    {
        private readonly IMapper mapper;
        public FootballerRepository(FootballContext context, IMapper mapper)
            :base(context)
        {
            this.mapper = mapper;
        }

        public override async Task<PageList<Footballer>> GetAllAsync(int currentPage, int pageSize)
        {
            return await Task.Run(()=>
                new PageList<Footballer>(objects
                    .Include(x=>x.Team).OrderBy(footballer=>footballer.BirthDate), currentPage, pageSize)
            );
        }

        public override async Task UpdateOrInsertAsync(Footballer obj)
        {
            var oldFootballer = await FindByIdAsync(obj.Id);

            if(oldFootballer==null)
            {
                await InsertAsync(obj);
                return;
            }

            mapper.Map(obj, oldFootballer);

            await SaveAsync();
        }
    }
}
