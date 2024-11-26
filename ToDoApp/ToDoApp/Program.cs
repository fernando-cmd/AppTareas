using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;


namespace ToDoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MiPoliticaCors", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")  // Reemplaza con el dominio permitido
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            // Configuración de la base de datos SQLite
            builder.Services.AddDbContext<ToDoContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("ToDoConnection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configurar middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("MiPoliticaCors");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}