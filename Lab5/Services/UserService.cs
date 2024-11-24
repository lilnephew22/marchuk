using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab5.Services
{
    using Auth0User = Auth0.ManagementApi.Models.User; 

    public class UserService
    {
        private readonly ManagementApiClient _managementApiClient;
        private readonly string _domain;

        public UserService(Auth0TokenService tokenService, string domain)
        {
            _domain = domain;

            var apiToken = tokenService.GetManagementApiTokenAsync().Result;
            Console.WriteLine($"Token: {apiToken}");
            _managementApiClient = new ManagementApiClient(apiToken, new Uri($"https://{_domain}/api/v2"));
        }

        public async Task<Auth0User> CreateUserAsync(string email, string password, string fullName, string userName, string phoneNumber)
        {
            var userCreateRequest = new UserCreateRequest
            {
                Email = email,
                Password = password,
                Connection = "Username-Password-Authentication",
                UserMetadata = new Dictionary<string, object>
                {
                    { "user_name", userName },
                    { "full_name", fullName },
                    { "phone_number", phoneNumber }
                }
            };

            try
            {
                var createdUser = await _managementApiClient.Users.CreateAsync(userCreateRequest);
                return createdUser; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка створення користувача: {ex.Message}");
            }
        }

        public async Task<Auth0User> GetUserAsync(string userId)
        {
            try
            {
                var user = await _managementApiClient.Users.GetAsync(userId);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка отримання користувача: {ex.Message}");
            }
        }

        public async Task<Auth0User> UpdateUserAsync(string userId, string fullName, string phoneNumber)
        {
            var userUpdateRequest = new UserUpdateRequest
            {
                UserMetadata = new Dictionary<string, object>
                {
                    { "full_name", fullName },
                    { "phone_number", phoneNumber }
                }
            };

            try
            {
                var updatedUser = await _managementApiClient.Users.UpdateAsync(userId, userUpdateRequest);
                return updatedUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка оновлення користувача: {ex.Message}");
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            try
            {
                await _managementApiClient.Users.DeleteAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка видалення користувача: {ex.Message}");
            }
        }

        public async Task<Auth0User> EnsureUserExistsAsync(string email, string fullName)
        {
            try
            {
                var users = await _managementApiClient.Users.GetUsersByEmailAsync(email);

                if (users.Count > 0)
                {
                    return users[0];
                }

                var newUser = await CreateUserAsync(
                    email: email,
                    password: "DefaultPassword123!", 
                    fullName: fullName,
                    userName: email.Split('@')[0], 
                    phoneNumber: null 
                );

                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка забезпечення існування користувача: {ex.Message}");
            }
        }
    }
}
