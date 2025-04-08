using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Utils;
using Taxually.TechnicalTest.Utils.VatRegsitrationFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register services and clients
builder.Services.AddSingleton<ITaxuallyHttpClient, TaxuallyHttpClient>();
builder.Services.AddSingleton<ITaxuallyQueueClient, TaxuallyQueueClient>();
builder.Services.AddSingleton<ISerializerManager, SerializerManager>();

// Register the VAT registration implementations
builder.Services.AddScoped<UKVatRegistration>();
builder.Services.AddScoped<FrenchVatRegistration>();
builder.Services.AddScoped<GermanVatRegistration>();
builder.Services.AddScoped<IVatRegistrationFactory, VatRegistrationFactory>();

builder.Services.AddControllers();
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