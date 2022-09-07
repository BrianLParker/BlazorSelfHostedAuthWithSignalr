using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorSelfHostedAuthWithSignalr.Server.Data;
using BlazorSelfHostedAuthWithSignalr.Server.Hubs;
using BlazorSelfHostedAuthWithSignalr.Server.Models;
using BlazorSelfHostedAuthWithSignalr.Server.Models.Configurations;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BlazorSelfHostedAuthWithSignalr;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration.Get<LocalConfiguration>();

        // Add services to the container.
        builder.Services.AddSingleton(configuration);
        builder.Services.AddSignalR();

        IEnumerable<string> responseCompressionMimeTypes =
            ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });

        builder.Services.AddResponseCompression(responseCompressionOptions =>
            responseCompressionOptions.MimeTypes = responseCompressionMimeTypes);

        string connectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection") ??
            throw new InvalidOperationException(message: "Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomUserClaimsPrincipalFactory>();

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            {
                const string OpenId = "openid";

                options.IdentityResources[OpenId].UserClaims.Add(JwtClaimTypes.Email);
                options.ApiResources.Single().UserClaims.Add(JwtClaimTypes.Email);

                options.IdentityResources[OpenId].UserClaims.Add(JwtClaimTypes.Role);
                options.ApiResources.Single().UserClaims.Add(JwtClaimTypes.Role);

            });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(JwtClaimTypes.Role);

        builder.Services.AddAuthentication()
            .AddIdentityServerJwt();

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.TryAddEnumerable(
            descriptor: ServiceDescriptor.Singleton<
                IPostConfigureOptions<JwtBearerOptions>,
                ConfigureChatHubBearerToken>());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseResponseCompression();
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapHub<ChatHub>(pattern: $"/{configuration.Chat.Endpoint}");
        app.MapFallbackToFile(filePath: "index.html");

        app.Run();
    }

    public class CustomUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            ClaimsPrincipal principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            //var claims = new List<Claim>
            //{
            //    new Claim(CustomClaimTypes.DateOfBirth, JsonSerializer.Serialize(user.DateOfBirth))
            //};

            //if (!string.IsNullOrWhiteSpace(user.DisplayName))
            //{
            //    identity.AddClaim(new Claim(CustomClaimTypes.DisplayName, user.DisplayName));
            //}

            //identity.AddClaims(claims);
            return principal;
        }
    }
}
