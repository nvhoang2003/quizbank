using QuizzBankBE;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Environment);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
startup.Configure(app, builder.Environment);
// Add services to the container.

/*builder.Services.AddControllers();
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
*/