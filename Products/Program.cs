using Microsoft.EntityFrameworkCore;
using Serilog;
using Products.Data;
using Products.Repositories;
using Products.Services;
using System.Reflection;

namespace Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new Exception("Configure path failed!, Working directory is null!");
            Environment.SetEnvironmentVariable("BASEDIR", path);
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog();
            // Add services to the container.

            // Configure MySQL database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                )
            );

            builder.Services.AddControllers();
            // Comment out the in-memory database configuration
            // builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("ProductsDb"));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
