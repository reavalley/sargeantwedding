using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Nelibur.ObjectMapper;
using OpenIddict.Core;
using OpenIddict.Models;
using WeddingSite.Data;
using WeddingSite.Data.Items;
using WeddingSite.Data.Users;
using WeddingSite.ViewModels;

namespace WeddingSite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();


            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(c =>
            {
                return Configuration;
            });
            // Add framework services.
            services.AddMvc();

            services.AddEntityFramework();
            services.AddDbContext<ApplicationDbContext>(options => {

                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
                options.UseOpenIddict();
            });

            services
                .AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Cookies.ApplicationCookie.AutomaticChallenge = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Register the OpenIddict services.
            services.AddOpenIddict()
                // Register the Entity Framework stores.
                .AddEntityFrameworkCoreStores<ApplicationDbContext>()

                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                .AddMvcBinders()

                .UseJsonWebTokens()

                // Enable the token endpoint.
                .EnableTokenEndpoint(Configuration["Authentication:OpenIddict:TokenEndPoint"])
                .EnableAuthorizationEndpoint(Configuration["Authentication:OpenIddict:AuthorizationEndPoint"])

                // Enable the password flow.
                .AllowPasswordFlow()

                .AllowAuthorizationCodeFlow()
                .AllowImplicitFlow()
                .AllowRefreshTokenFlow()

                // During development, you can disable the HTTPS requirement.
                .DisableHttpsRequirement()

                // Register a new ephemeral key, that is discarded when the application
                // shuts down. Tokens signed using this key are automatically invalidated.
                // This method should only be used during development.
                .AddEphemeralSigningKey();

            services.AddSingleton<DbSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DbSeeder dbSeeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseDefaultFiles();
                 
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = (context) =>
                {
                    context.Context.Response.Headers["Cache-Control"] = Configuration["StaticFiles:Headers:Cache-Control"];
                    context.Context.Response.Headers["Pragma"] = Configuration["StaticFiles:Headers:Pragma"]; 
                    context.Context.Response.Headers["Expires"] = Configuration["StaticFiles:Headers:Expires"]; 
                }
            });
            
           

            app.UseIdentity();

            app.UseFacebookAuthentication(new FacebookOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AppId = Configuration["FacebookAppId"],
                AppSecret = Configuration["FacebookAppSecret"],
                CallbackPath = "/signin-facebook",
                Scope =
                {
                    "email"
                }
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                ClientId = Configuration["GoogleAppId"],
                ClientSecret = Configuration["GoogleAppSecret"],
                CallbackPath = "/signin-google",
                Scope =
                {
                    "email"
                }
            });
            
            app.UseOAuthValidation();

            //app.UseJwtProvider();

            //app.UseDeveloperExceptionPage();

            app.UseOpenIddict();            

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                Authority = Configuration["Authentication:OpenIddict:Authority"],
                TokenValidationParameters = new TokenValidationParameters
                {
                    //IssuerSigningKey = JwtProvider.SecurityKey,
                    //ValidateIssuerSigningKey = true,
                    //ValidIssuer = JwtProvider.Issuer,
                    ValidateIssuer = false,
                    ValidateAudience = false                    
                }
            });

            app.UseMvc();
            
            TinyMapper.Bind<Item, ItemvViewModel>();

            try
            {
                dbSeeder.SeedAsync().Wait();
                InitializeAsync(app.ApplicationServices, Configuration, CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (AggregateException e)
            {
                throw new Exception(e.ToString());
            }
        }

        private async Task InitializeAsync(IServiceProvider services, IConfiguration configuration, CancellationToken cancellationToken)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync();

                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                if (await manager.FindByClientIdAsync(configuration["Authentication:OpenIddict:ClientId"], cancellationToken) == null)
                {
                    var application = new OpenIddictApplication
                    {
                        Id = configuration["Authentication:OpenIddict:ApplicationId"],
                        ClientId = configuration["Authentication:OpenIddict:ClientId"],
                        DisplayName = configuration["Authentication:OpenIddict:DisplayName"],
                        RedirectUri = $"{configuration["Authentication:OpenIddict:Authority"]}{configuration["Authentication:OpenIddict:TokenEndPoint"]}",
                        LogoutRedirectUri = configuration["Authentication:OpenIddict:Authority"],
                        Type = OpenIddictConstants.ClientTypes.Public
                    };

                    await manager.CreateAsync(application, cancellationToken);
                }
            }
        }
    }    
}
