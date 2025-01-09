using ExampleExam;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(new PlayGroundRepository()); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(
    opts =>
    {
        opts.AddPolicy("defPolicy", pol =>
        {
            pol.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        }); 
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("defPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
