using System.Text;
using AutoMapper;
// using EasyBank.Api.AutoMapper;
// using EasyBank.Api.Contract.NaturezaDeLancamento;
// using EasyBank.Api.Damain.Repository.Classes;
// using EasyBank.Api.Damain.Repository.Interfaces;
// using EasyBank.Api.Damain.Services.Classes;
// using EasyBank.Api.Damain.Services.Interfaces;
// using EasyBank.Api.Data;
using EasyBank.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

    // var config = new MapperConfiguration(cfg => {
    //     cfg.AddProfile<UsuarioProfile>();
    //     cfg.AddProfile<NaturezaDeLancamentoProfile>();
    //     cfg.AddProfile<ApagarProfile>();
    //     cfg.AddProfile<AreceberProfile>();
    // });

    // IMapper mapper = config.CreateMapper();

    builder.Services
    .AddSingleton(builder.Configuration)
    .AddSingleton(builder.Environment);
    // .AddSingleton(mapper);
    // .AddScoped<TokenService>()
    // .AddScoped<IUsuarioRepository, UsuarioRepository>();
    // .AddScoped<IUsuarioService, UsuarioService>()
    // .AddScoped<INaturezaDeLancamentoRepository, NaturezaDeLancamentoRepository>()
    // .AddScoped<IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, long>, NaturezaDeLancamentoService>()
    // .AddScoped<IApagarRepository, ApagarRepository>()
    // .AddScoped<IService<ApagarRequestContract, ApagarResponseContract, long>, ApagarService>()
    // .AddScoped<IAreceberRepository, AreceberRepository>()
    // .AddScoped<IService<AreceberRequestContract, AreceberResponseContract, long>, AreceberService>();
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
            Description = "JTW Authorization header using the Beaerer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
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
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["KeySecret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
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

    app.UseEndpoints(endpoints => endpoints.MapControllers());

    app.MapControllers();
}
