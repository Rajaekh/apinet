using LesApi.Models;
using LesApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TransfertDatabaseSettings>(
    builder.Configuration.GetSection(nameof(TransfertDatabaseSettings)));

builder.Services.AddSingleton<ITransfertDatabaseSettings>(
    sp=>sp.GetRequiredService<IOptions<TransfertDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("TransfertDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<IFrais,FraisService>();
builder.Services.AddScoped<IClient, ClientService>();
builder.Services.AddScoped<IBeneficiaire,BeneficiaireService>();
builder.Services.AddScoped<ITransfert,TransfereService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddControllers();
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
app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
