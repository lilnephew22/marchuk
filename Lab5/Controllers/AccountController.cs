using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Lab5.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ManagementApiClient _managementApiClient;

        public AccountController(ApplicationDbContext context, ManagementApiClient managementApiClient)
        {
            _context = context;
            _managementApiClient = managementApiClient;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserCreateRequest
                {
                    Email = model.Email,          
                    Password = model.Password,    
                    UserName = model.UserName,    
                    Connection = "Username-Password-Authentication", 
                    UserMetadata = new Dictionary<string, object>
            {
                { "full_name", model.FullName },  
                { "phone_number", model.PhoneNumber }
            }
                };

                try
                {
                    var createdUser = await _managementApiClient.Users.CreateAsync(user);

                    var newUser = new Models.User
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Password = model.Password 
                    };
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    return await Login(new LoginViewModel { UserName = model.UserName, Password = model.Password });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Помилка реєстрації користувача: {ex.Message}");
                }
            }

            return View(model);
        }


        //[HttpGet]
        //public IActionResult Login()
        //{
        //    // Перенаправлення на Auth0 для аутентифікації
        //    return Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectDefaults.AuthenticationScheme);
        //}
        //[HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
                if (result?.Principal != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectDefaults.AuthenticationScheme);
                }
            }

            ModelState.AddModelError(string.Empty, "Невірний логін або пароль");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<string> GetManagementApiAccessToken()
        {
            var client = new HttpClient();
            var requestBody = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", "sJoiHCfunKjIFZPNhzyyIyPwUj88UBuJ" },
                { "client_secret", "CnDccOynQwtleVfGxnYWWjgf3rsExRSNU_nZ4dsdPp17bpT_dxoPvoDvWqvDBeDP" }, 
                { "audience", "https://dev-mo8gtikxxnn38u5k.us.auth0.com/api/v2/" },
                { "grant_type", "client_credentials" }
            });

            var response = await client.PostAsync("https://dev-mo8gtikxxnn38u5k.us.auth0.com/oauth/token", requestBody);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve access token: {responseContent}");
            }

            var tokenResponse = JsonConvert.DeserializeObject<JObject>(responseContent);
            var accessToken = tokenResponse["access_token"]?.ToString();

            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("Access token is missing in the response.");
            }

            return accessToken;
        }
    }
}
