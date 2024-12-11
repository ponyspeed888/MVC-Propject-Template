The default is using SQLite, to use SQL local db, define SQLSERVER symbol
if you want to seed user data, set SeedUserData to true when creating a new project or add this line
                 await MoreFeatures.SeedIdentityUser (app);
