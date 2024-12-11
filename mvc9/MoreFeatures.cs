using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc9.Data;

namespace mvc9
{
    public class MoreFeatures
    {
        public static void SetupIdentityDB (WebApplicationBuilder builder )
        {

            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var root = builder.Environment.WebRootPath;


#if SQLSERVER
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;AttachDbFileName={root}\\Data\\asp.net.identity.mdf;Integrated Security=true") ) ;



#else

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite ($"DataSource={root}\\Data\\asp.net.identity.db"));
#endif


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

        }


        public static void SetupIdentityOptions(WebApplicationBuilder builder)
        {

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;


                //// Lockout settings.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;

                //// User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //options.User.RequireUniqueEmail = false;


            });

        }



        public static async Task EnsureDatabaseExist (WebApplication app)
        {

            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.EnsureCreated() == false)
            {
                // TODO detect if migration is needed.
                //   context.Database.Migrate();
            }



        }

        public static async Task SeedIdentityUser (WebApplication app)
        {


            using (var scope = app.Services.CreateScope())
            {
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Seed your users here

                await CreateUser("a@a.com", "a", userMgr);
                await CreateUser("b@a.com", "a", userMgr);

            }


        }


        public static async Task<bool> CreateUser(string email, string password, UserManager<IdentityUser> userMgr)
        {
            var usr = await userMgr.FindByNameAsync(email);

            if (usr == null)
            {
                usr = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    //Id = Guid.NewGuid().ToString(),
                    //PhoneNumber = "1234567890",

                };

                var result = userMgr.CreateAsync(usr, password).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

            }
            else
            {
                //logger.LogDebug("alice already exists");
            }

            return true;



        }


    }
}
