using Gamma.RoboKP.Application.Extensions;
using Gamma.RoboKP.Extensions;

//TODO: при регистрации для подтверждения почты отправлять код на почту 
//TODO: при попытке входа в аккаунт, у которого не подтвержденный аккаунт

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