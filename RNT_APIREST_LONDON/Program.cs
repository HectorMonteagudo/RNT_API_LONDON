using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RNT_APIREST_LONDON.Repositorios;
using RNT_APIREST_LONDON.Repositorios.Servicios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IClientesRepository, ClientesRepository>();
builder.Services.AddTransient<IEmpaqueRepository, EmpaqueRepository>();
builder.Services.AddTransient<IEspecieRepository, EspecieRepository>();
builder.Services.AddTransient<IEtapaRepository, EtapaRepository>();
builder.Services.AddTransient<IGrupoCanalRepository, GrupoCanalRepository>(); 
builder.Services.AddTransient<ILineaRepository, LineaRepository>();
builder.Services.AddTransient<IMarcaRepository, MarcaRepository>();
builder.Services.AddTransient<IPresentacionesRepository, PresentacionesRepository>();
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddTransient<IVariedadRepository, VariedadRepository>();
builder.Services.AddTransient<IUserService,UserService>();


builder.Services.AddSwaggerGen();


//Configurando seguridad
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
var secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                        throw new InvalidOperationException("No se configuro el SecretKey"));
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience  = false,
        ValidateLifetime = true,
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
