using Microsoft.EntityFrameworkCore;
using Records.BLL;
using Records.BLL.Interfaces;
using Records.BLL.Services;
using Records.DAL;
using Records.DAL.Interfaces;
using Records.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecordsContext>(options =>
    options.UseSqlite("Data Source=RecordsDatabase.db")
);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IBestRecordRepository, BestRecordRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBestRecordService, BestRecordService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();