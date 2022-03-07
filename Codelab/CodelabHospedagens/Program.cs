using CodelabHospedagens.Domain.ChaleAggregate;
using CodelabHospedagens.Domain.ClienteAggregate;
using CodelabHospedagens.Domain.HospedagemAggregate;
using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Infra;
using CodelabHospedagens.Infra.DataBaseSettings;
using CodelabHospedagens.Service.ClienteCommand;
using CodelabHospedagens.Service.Mapeamentos;
using CodelabHospedagens.Service.ChaleCommand;
using CodelabHospedagens.Infra.Repository;
using CodelabHospedagens.Service.HospedagemCommand;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbSession>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IChaleService, ChaleService>();
builder.Services.AddScoped<IHospedagemService, HospedagemService>();

builder.Services.AddScoped<IRepository<Cliente>, ClienteRepository>();
builder.Services.AddScoped<IRepository<Chale>, ChaleRepository>();
builder.Services.AddScoped<IChaleRepository, ChaleRepository>();
builder.Services.AddScoped<IHospedagemRepository, HospedagemRepository>();

builder.Services.AddScoped<IConversor<Cliente, ClienteDto>, ClienteConversor>();
builder.Services.AddScoped<IConversor<Chale, ChaleDto>, ChaleConversor>();
builder.Services.AddScoped<IConversor<Hospedagem, HospedagemDto>, HospedagemConversor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
