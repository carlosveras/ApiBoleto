using ApiBoleto.Context;
using ApiBoleto.Repository;
using ApiBoleto.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql()
.AddDbContext<ApiBoletoDataContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("ApiBoletoConn"));
    }
);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IBancoRepository, BancoRepository>();
builder.Services.AddScoped<IBoletoRepository, BoletoRepository>();

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
