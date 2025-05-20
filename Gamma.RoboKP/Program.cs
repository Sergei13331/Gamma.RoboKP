using Gamma.RoboKP.Domain.Extensions;
using Gamma.RoboKP.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.RegisterMapster();

builder
    .AddBearerAuthentication()
    .AddOptions()
    .AddData()
    .AddSwagger()
    .AddApplicationServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();