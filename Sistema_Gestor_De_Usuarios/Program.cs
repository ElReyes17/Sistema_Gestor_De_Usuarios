
using Sistema_Gestor_De_Usuarios.Extensions;
using Sistema_Gestor_De_Usuarios.Infrastructure.Persistence;
using Sistema_Gestor_De_Usuarios.Core.Application;
using Sistema_Gestor_De_Usuarios.Endpoints;
using Sistema_Gestor_De_Usuarios.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RegexOption>(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseSession();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.MapControllers();

app.MapGroup("/api/user").MapUserEndpoint();
app.MapGroup("/api/login").MapLoginEndpoint();

app.Run();
