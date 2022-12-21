using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
    (JwtBearerDefaults.AuthenticationScheme, opts =>
    {
        opts.Authority = "https://localhost:7211"; // token'� yay�nlayan
        opts.Audience = "resource_api1";            // port'u dinleyen api kayna��
    }); // authentication'daki scheme ile Jwt'deki sheme ismi ayn� verilirse uygulamadaki auth mekanizmas�, jwt'ye ba�lanm�� olur

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("ReadProduct", policy =>  //policy; yetkilendirmenin sa�lanmas� i�in gereken bir �artname, ko�ul
    {
        policy.RequireClaim("scope", "api1.read");
    });

    opts.AddPolicy("UpdateOrCreate", policy =>
    {
        policy.RequireClaim("scope", new[] { "api1.write", "api1.create" });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
