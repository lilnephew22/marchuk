using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Lab5.Models;
using Lab5.Services;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Lab5
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            string domain = builder.Configuration["Auth0:Domain"];
            var auth0TokenService = new Auth0TokenService(builder.Configuration);
            string managementApiToken = await auth0TokenService.GetManagementApiTokenAsync();

            builder.Services.AddSingleton(new ManagementApiClient(managementApiToken, new Uri($"https://{domain}/api/v2")));
            builder.Services.AddSingleton(new UserService(auth0TokenService, domain));

            builder.Services.AddControllersWithViews();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = $"https://{domain}";
                options.ClientId = builder.Configuration["Auth0:ClientId"];
                options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email"); 
                options.CallbackPath = "/signin-auth0";

                options.Events = new OpenIdConnectEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var claimsIdentity = (System.Security.Claims.ClaimsIdentity)context.Principal.Identity;

                        foreach (var claim in claimsIdentity.Claims)
                        {
                            Console.WriteLine($"Claim type: {claim.Type}, Claim value: {claim.Value}");
                        }

                        var email = claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                        var name = claimsIdentity.FindFirst("name")?.Value;

                        if (string.IsNullOrEmpty(email))
                        {
                            throw new Exception("Відсутній email у claims.");
                        }

                        var userService = context.HttpContext.RequestServices.GetRequiredService<UserService>();
                        await userService.EnsureUserExistsAsync(email, name);
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/Home/Error");
                        return Task.CompletedTask;
                    }
                };

            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
