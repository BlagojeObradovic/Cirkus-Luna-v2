using CirkusLuna.ClassLibrary.Interfaces;
using CirkusLuna.ClassLibrary.Repositories;
using CirkusLuna.ClassLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSingleton<ITicketTypeRepository, TicketTypeRepository>();
builder.Services.AddSingleton<ICircusCityRepository, CircusCityRepository>();
builder.Services.AddSingleton<IArtistRepository, ArtistRepository>();
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<INewsRepository, NewsRepository>();
builder.Services.AddSingleton<IPerformanceRepository, PerformanceRepository>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();

builder.Services.AddSingleton<ReservationService>();

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();
