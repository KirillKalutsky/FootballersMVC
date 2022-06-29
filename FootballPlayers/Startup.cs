using FootballPlayers.Hubs;
using FootballPlayers.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using FootballPlayers.Models;
using FootballPlayers.Models.Transfers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using FootballPlayers.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnect = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                "host=localhost;port=5432;database=Footballers;username=postgres;password=abrakadabra77";

            services.AddRazorPages();
            services.AddControllers();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<FootballContext>(opt=>
                opt.UseNpgsql(dbConnect));
            services.AddAutoMapper
                (
                    cfg => 
                    {
                        cfg.CreateMap<CreateTeamDto, Team<Footballer>>();
                        cfg.CreateMap<Team<Footballer>, Team<Footballer>>();
                        cfg.CreateMap<Team<Footballer>, ShowTeamDto>();
                        cfg.CreateMap<Footballer, Footballer>();
                        cfg.CreateMap<CreateFootballerDto, Footballer>();
                        cfg.CreateMap<Footballer, CreateFootballerDto>()
                        .ForMember(dto => dto.TeamId, opt => opt.MapFrom(player=> player.Team.Id));
                        cfg.CreateMap<Footballer, ShowFootballerDto>()
                        .ForMember(dto => dto.TeamName, opt => opt.MapFrom(player => player.Team.Name))
                        .ForMember(dto => dto.Sex, opt => opt.MapFrom(player => player.Sex.ToString()))
                        .ForMember(dto => dto.Country, opt => opt.MapFrom(player => player.Country.ToString()));
                    }
                );

            services.AddScoped<Repository<Footballer>, FootballerRepository>();
            services.AddScoped<Repository<Team<Footballer>>, TeamRepository>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        private static void Prepare(IApplicationBuilder app)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            using (var context = app.ApplicationServices.CreateScope())
            {
                var supportContext = context.ServiceProvider.GetService<FootballContext>();
                supportContext.Database.Migrate();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();
            app.UseDefaultFiles();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<FootballerHub>("/footballersHub");
            });

            Prepare(app);
        }
    }
}
