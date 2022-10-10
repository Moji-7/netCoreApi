using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using TransferApi;
// using TransferApi;
// using TransferApi.Infrastructure.ExceptionHandling;
// using TransferApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();//(options =>
// {
//     options.Filters.Add<HttpResponseExceptionFilter>();
// });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("myDb"));

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
//builder.Services.AddDbContext<TransferContext>(opt => opt.UseInMemoryDatabase("TransferList"));


// Add services to the container.




// builder.Services.AddSingleton<IClassroomService, ClassroomService>();
// builder.Services.AddSingleton<ILog, MyLogger>(); ;
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseHttpsRedirection();



app.MapControllers();

app.Run();

public partial class Program { }