using Microsoft.EntityFrameworkCore;
using renad.net.Data;

namespace renad.net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add Entity Framework services
            builder.Services.AddDbContext<AppDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));   


                }
            );

            var app = builder.Build();

            // Create or update the database (including the department/gender lookups) on startup.
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
            }

            // Gregorian culture so dates bind the same way on servers set to the Hijri calendar.
            app.UseRequestLocalization(new RequestLocalizationOptions()
                .SetDefaultCulture("en-US")
                .AddSupportedCultures("en-US")
                .AddSupportedUICultures("en-US"));

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
