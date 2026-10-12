using Club_Management_System_Api.Data;
using Club_Management_System_Api.Mapping;
using Club_Management_System_Api.Reposatories.Implementation;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// register the repository and service classes and the auto-mapper

builder.Services.AddScoped<IMemberRepo,MemberRepo>();
builder.Services.AddScoped<IMemberShipRepo,MemberShipRepo>();
builder.Services.AddScoped<IFacilityRepo,FacilityRepo>();
builder.Services.AddScoped<IBookingRepo,BookingRepo>();
builder.Services.AddScoped<IPaymentRepo,PaymentRepo>();
builder.Services.AddScoped<IunitOfWork,UnitOfWork>();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(typeof(MappingProfiles));


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
