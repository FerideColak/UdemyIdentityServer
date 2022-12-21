var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(opts =>  // Auth Server ile haberleþme
{   /*
     Þemalarýn kullaným amacý, cookie'leri kullaným amacýna gore ayýrabilmektir.
     */
    opts.DefaultScheme = "Cookies";
    opts.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies").AddOpenIdConnect("oidc", opts =>  //kimlik doðrulama cookie ile gerçekleþtirilir
{
    opts.SignInScheme = "Cookies";
    opts.Authority = "https://localhost:7211"; 
    opts.ClientId = "Client1-Mvc";
    opts.ClientSecret = "secret";
    opts.ResponseType = "code id_token"; //code, access token almak için authorization code; id_token, token'ý doðrulamak için
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
