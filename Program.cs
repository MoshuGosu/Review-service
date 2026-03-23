using Microsoft.EntityFrameworkCore;
using Review_service_WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseSqlite("Data Source=reviews.db");
    }
    else
    {
        options.UseSqlite("Data Source=/home/site/wwwroot/reviews.db");
    }
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactBookReviews", policy =>
        policy.WithOrigins("http://localhost:5174")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

//Apply database migrations at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbcontext = services.GetRequiredService<ApplicationDbContext>();
    dbcontext.Database.Migrate();
}

// Configure the HTTP request pipeline
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactBookReviews");

app.UseAuthorization();

app.MapControllers();

app.Run();