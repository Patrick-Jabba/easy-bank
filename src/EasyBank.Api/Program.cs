using System.Text;
using AutoMapper;
using EasyBank.Api.Profiles;
using EasyBank.Api.Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EasyBank.Api.Domain.Repository.Classes;
using EasyBank.Api.Domain.Repository.Interfaces;
using EasyBank.Api.Domain.Services.Classes;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.APagar;

var builder = WebApplication.CreateBuilder(args);

ConfigurarServices(builder);

ConfigurarInjecaoDeDependencia(builder);

var app = builder.Build();

ConfigurarAplicacao(app);

app.Run("http://localhost:8080");

// Metodo que configrua as injeções de dependencia do projeto.
static void ConfigurarInjecaoDeDependencia(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration["PADRAO"];
    
    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseNpgsql(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);

    var config = new MapperConfiguration(cfg => {
        cfg.AddProfile<UsuarioProfile>();
        cfg.AddProfile<NaturezaDeLancamentoProfile>();
        cfg.AddProfile<APagarProfile>();
        cfg.AddProfile<AReceberProfile>();
    });

    IMapper mapper = config.CreateMapper();

    builder.Services
    .AddSingleton(builder.Configuration)
    .AddSingleton(builder.Environment)
    .AddSingleton(mapper)
    // Aqui quando me pede a classe eu devolvo ela mesma
    .AddScoped<TokenService>()
    // Quando o usuario me pede uma interface eu devolvo uma classe
    .AddScoped<IUsuarioRepository, UsuarioRepository>()
    .AddScoped<IUsuarioService, UsuarioService>()
    .AddScoped<INaturezaDeLancamentoRepository, NaturezaDeLancamentoRepository>()
    .AddScoped<INaturezaDeLancamentoService, NaturezaDeLancamentoService>()
    .AddScoped<IAPagarRepository, APagarRepository>()
    .AddScoped<IAPagarService, APagarService>()
    .AddScoped<IAReceberRepository, AReceberRepository>()
    .AddScoped<IAReceberService, AReceberService>();
}

// Configura o serviços da API.
static void ConfigurarServices(WebApplicationBuilder builder)
{

    builder.Services
    .AddCors()
    .AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }).AddNewtonsoftJson();

    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme",
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

        c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyBank.Api", Version = "v1" });   
    });

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })

    .AddJwtBearer(x =>
    {
        var keySecret = builder.Configuration["KeySecret"];


        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        if(!string.IsNullOrEmpty(keySecret))
        {
            x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keySecret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        }
    });
}

// Configura os serviços na aplicação.
static void ConfigurarAplicacao(WebApplication app)
{
    // Configura o contexto do postgreSql para usar timestamp sem time zone.
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    app.UseDeveloperExceptionPage()
        .UseRouting();

    app.UseSwagger()
        .UseSwaggerUI(c =>
        {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyBank.Api v1");
                c.RoutePrefix = string.Empty;
        });

    app.UseCors(x => x
        .AllowAnyOrigin() // Permite todas as origens
        .AllowAnyMethod() // Permite todos os métodos
        .AllowAnyHeader()) // Permite todos os cabeçalhos
        .UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();
}
