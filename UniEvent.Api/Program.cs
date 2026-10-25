using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using UniEvent.Application.Services.Implement;
using UniEvent.Application.Services.Interface;
using UniEvent.Application.Validators;
using UniEvent.Domain.IRepository;
using UniEvent.Persistence;
using UniEvent.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreateEventDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEventRegistrationDtoValidator>();

builder.Services.AddFluentValidationAutoValidation();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


# region DBcontext Configure

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UniEventDbContext>(options =>
    options.UseSqlServer(connectionString));


#endregion


# region AddScoped irepo and repo

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRegistrationRepository, EventRegistrationRepository>();
# endregion

#region AddScoped Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRegistrationService, EventRegistrationService>();
#endregion

#region Controller
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
