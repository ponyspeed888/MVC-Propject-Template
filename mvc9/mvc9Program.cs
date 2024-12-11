using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc9;
using mvc9.Data;

namespace mvc9
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            MoreFeatures.SetupIdentityDB(builder);
            MoreFeatures.SetupIdentityOptions(builder);

            builder.Services.AddServerSideBlazor();

            var app = builder.Build();

            await MoreFeatures.EnsureDatabaseExist(app);

#if SeedUser
                 await MoreFeatures.SeedIdentityUser (app);
#endif


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAntiforgery();


            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();
            app.MapBlazorHub();

            app.Run();
        }
    }
}
