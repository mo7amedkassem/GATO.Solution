using Gato.Core.IRepositories;
using Gato.Core.Service_Contract;
using Gato.Repository.Data;
using Gato.Repository.Entities;
using Gato.Repository.identity;
using Gato.Repository.Repositories;
using Gato.Service;
using GATO.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GATO
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Configure  SERVICE



            builder.Services.AddDbContext<StoreDBContext>(Options =>
            {
                                                                                
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<AppIdentityDBContext>()
            .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICommentRepo, CommentRepo>();
            builder.Services.AddScoped<IPostRepo, PostRepo>();
            builder.Services.AddScoped<ILikesRepo,LikeRepo>();
            builder.Services.AddScoped<ISavedPosts,SavedPostRepo>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped(typeof(IAuthServices), typeof(AuthService));
            builder.Services.AddTransient<IEmailService, EmailService>();





            #endregion


            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreDBContext>();
            var _identityDbcontext = services.GetRequiredService<AppIdentityDBContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbcontext.Database.MigrateAsync();//upate-database
                await _identityDbcontext.Database.MigrateAsync();//upate-database
                var _usermanager = services.GetRequiredService<UserManager<User>>();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been accured during applying migration ");
            }


            #region cofigure kestrel middelwares


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseSwagger();
            //app.UseSwaggerUI();


            app.UseHttpsRedirection();


            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();


            #endregion

            app.Run();
        }
    }
}
