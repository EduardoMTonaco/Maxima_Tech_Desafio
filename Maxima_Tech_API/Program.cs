using MassTransit;
using Maxima_Tech_API.Class.Global;
using Maxima_Tech_API.Class.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira o token JWT com o prefixo 'Bearer '",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Authentication service
string SecretKey = SecretKeyGenerator.GenerateSecretKey(32);
builder.Services.AddSingleton<AuthService>(provider => new AuthService(SecretKey));

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Connection string (se precisar)
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

// Reconfigura as fontes de configuração para priorizar o appsettings.Docker.json no container
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

if (isDocker)
{
    builder.Configuration.AddJsonFile("appsettings.Docker.json", optional: true, reloadOnChange: true);
}

builder.Configuration.AddEnvironmentVariables();

string conection = builder.Configuration.GetConnectionString("DefaultConnection");
FillSQLConnectionString global = new FillSQLConnectionString(conection);

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        // Lê do appsettings ou variáveis de ambiente, com fallback para "rabbitmq"
        var rabbitHost = builder.Configuration["RabbitMQ:Host"] ?? Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq";
        var rabbitUser = builder.Configuration["RabbitMQ:Username"] ?? Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest";
        var rabbitPass = builder.Configuration["RabbitMQ:Password"] ?? Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest";

        // Configura conexão com RabbitMQ
        cfg.Host(rabbitHost, "/", h =>
        {
            h.Username(rabbitUser);
            h.Password(rabbitPass);
        });

        // Aqui você pode configurar receive endpoints se precisar, exemplo:
        // cfg.ReceiveEndpoint("nome-da-fila", e =>
        // {
        //     e.Consumer<SeuConsumer>(context);
        // });
    });
});
builder.WebHost.UseUrls("http://0.0.0.0:7020");
var app = builder.Build();

// Middleware customizado de autenticação JWT (se precisar)
app.UseMiddleware<JwtMiddleware>();

// Swagger para ambiente de desenvolvimento

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1"));


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();