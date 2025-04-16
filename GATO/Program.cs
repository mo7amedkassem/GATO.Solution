using Gato.Core.IRepositories;
using Gato.Repository.Data;
using Gato.Repository.identity;
using Gato.Repository.Repositories;
using GATO.Helpers;
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


            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICommentRepo, CommentRepo>();
            builder.Services.AddScoped<IPostRepo, PostRepo>();
            builder.Services.AddScoped<ILikesRepo,LikeRepo>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
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

            app.UseAuthorization();


            app.MapControllers();


            #endregion

            app.Run();
        }
    }
}
