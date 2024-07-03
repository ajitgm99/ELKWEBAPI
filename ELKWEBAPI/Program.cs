using ELKWEBAPI.DATA;
using ELKWEBAPI.Model;
using ELKWEBAPI.Services;
using Microsoft.EntityFrameworkCore;
using Nest;

var builder = WebApplication.CreateBuilder(args);



var setting =new ConnectionSettings(new Uri("http://localhost:9200/")).DefaultIndex("esemployee");
// Add services to the container.

var client=new ElasticClient(setting);

builder.Services.AddSingleton(client);

builder.Services.AddScoped<IElasticSerachService<Employee>, ElasticSearchService<Employee>>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
