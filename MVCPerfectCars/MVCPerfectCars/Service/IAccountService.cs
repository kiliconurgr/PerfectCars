using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MVCPerfectCars.Models;
using MVCPerfectCarsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPerfectCars.Service
{
    public interface IAccountService
    {
        Task InitAsync();

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> RegisterAsync(RegisterViewModel model);

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model);

    }

    public class AccountService : IAccountService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public AccountService(
            IConfiguration configuration,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager
            )
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task InitAsync()
        {

            new[]
            {
                new Role { Name = "Administrators"},
                new Role { Name = "Representatives"},
            }
            .ToList()
            .ForEach(role => {
                if (!roleManager.RoleExistsAsync(role.Name).Result)
                    roleManager.CreateAsync(role).Wait();
            });


            var user = await userManager.FindByNameAsync(configuration.GetValue<string>("App:Security:UserName"));
            if(user is null)
            {
                user = new User
                {
                    Name = configuration.GetValue<string>("App:Security:Name"),
                    UserName = configuration.GetValue<string>("App:Security:UserName"),
                    Enabled= true,
                    EmailConfirmed =true,
                    
                };

                var result = await userManager.CreateAsync(user, configuration.GetValue<string>("App:Security:Password"));
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrators");
                }
            }

        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsPersistent, true);
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
