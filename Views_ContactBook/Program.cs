using ContactBook.Database;
using ContactBook.Interfaces;
using ContactBook.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHttpContextAccessor();

ConfigureDb(builder.Services);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

//Configure Database
static void ConfigureDb(IServiceCollection services)
{
    var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("Default");
    services.AddDbContext<AppDbContext>(b => b.UseSqlServer(connectionString));
}

