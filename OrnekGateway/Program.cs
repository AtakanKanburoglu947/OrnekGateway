using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Token;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = TokenSettings.Issuer,
            ValidAudience = TokenSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(TokenSettings.Secret))
        };
    });

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("customPolicy", opt =>
    {
        opt.PermitLimit = 1;
        opt.Window = TimeSpan.FromSeconds(12);
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 1;
    });
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Rate limiting reverse proxy'den ÖNCE olmalý
app.UseRateLimiter();

// Custom middleware reverse proxy'den ÖNCE olmalý

// YARP kesinlikle controllers'tan ÖNCE olmalý!
app.MapReverseProxy();

// Controllers en alta! (aksi halde YARP çalýþmaz)
app.MapControllers();

app.Run();
