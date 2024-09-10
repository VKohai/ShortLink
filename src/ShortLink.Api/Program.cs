using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AnyOrigin", cfg =>
    {
        cfg.AllowAnyOrigin();
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(
        connectionString: connectionString,
        serverVersion: ServerVersion.AutoDetect(connectionString));
}, ServiceLifetime.Scoped);

builder.Services.AddTransient<ShortLinkRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.MapGet(pattern: "/{guid}",
    handler: [EnableCors("AnyOrigin")]
async (HttpContext context, [FromServices] ShortLinkRepository repos) =>
    {
        var guid = context.Request.RouteValues["guid"]?.ToString();
        UrlMapper? mapper;
        try
        {
            if (string.IsNullOrWhiteSpace(guid))
                return Results.BadRequest(guid);

            mapper = await repos.GetByGuidAsync(guid);
            if (mapper == null)
                return Results.NotFound(guid);

            mapper.Views += 1;
            await repos.UpdateAsync(mapper);
            return Results.Redirect(mapper.OriginalUrl);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    });

app.MapControllers();

app.Run();
