using Microsoft.EntityFrameworkCore;
using PatientSolution.Api.Data;
using PatientSolution.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OracleDbConnection");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = Environment.GetEnvironmentVariable("PATIENT_APP_DB_CONNECTION_STRING");
}

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Der Datenbank-Connection String (OracleDbConnection) ist weder in User Secrets, appsettings.json noch als Umgebungsvariable (PATIENT_APP_DB_CONNECTION_STRING) gesetzt. Bitte setzen Sie ihn.");
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PatientContext>(options =>
    options.UseOracle(connectionString, o => o.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion23))
);

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
