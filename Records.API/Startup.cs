using Microsoft.EntityFrameworkCore;
using Records.BLL;
using Records.BLL.Interfaces;
using Records.BLL.Services;
using Records.DAL;
using Records.DAL.Interfaces;
using Records.DAL.Repositories;

namespace Records.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<RecordsContext>(options =>
            options.UseSqlite("Data Source=RecordsDatabase.db")
        );
        
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IBestRecordRepository, BestRecordRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IBestRecordService, BestRecordService>();
        
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}