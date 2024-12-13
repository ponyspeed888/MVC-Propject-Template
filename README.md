[![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fponyspeed888%2FMVC-Propject-Template&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
This project include two major changes to standard asp.net core 9 mvc project

First it integrate the rendering of 3 major asp.net core framework (mvc, razor and Blazor) into one project. Second, it change the code to make project more suitable during study phase of asp.net core dev.  It come from my need to repeatedly creating new .NET Core MVC project with asp.net identity (for STUDY or TESTING, NOT SUITABLE FOR PRODUCTION) while studying asp.net core.  The standard project template created a project that require you to :

1. Run migration to create a new database
2. Add user name and password that must meet strong password requirement
3. randomized the identity db name
4. randomized the port of the project

The project eliminate all these steps.
1. No need for migration, identity db is already created
2. You can add user with only a single character password
3. identity db name is always asp.net.identity.db
4. ports are provided at creation time, default to 5000 and 44300 (ssl)
5. program.cs and HomeController.cs files are appended with project name so that you know which project's program.cs you are editing when you have many projects.


Adding blazor server functionality is by following the article "Integrate ASP.NET Core Razor components with MVC or Razor Pages" in Microsoft Learn. The project also show a common use case why you want to add Razor server component to a mvc project : server push, as implemented in ServerTime component

In addition, this project include 

Code to seed identity user.  The seeded user is a@a.com with single letter password 'a'

Add template.json so that this project can be install as project template




WHO might find this project useful ?

Those who need to create asp.net core app with individual account often (for STUDY or TESTING), and may need to swtich between SQLite and SQL Server Localdb


The only explanation that is needed is that in order to allow unrestricted password , these files are copied from identity scalfolding and modified as needed

Register.cshtml
Register.cshtml.cs

If you want the original login behavior, remove everything under Areas

You probably want to open this project, and modify template.json according your need and export this project as a project template

To install/uninstall this project as a project template, open a developer command line prompt

dotnet commands ;

dotnet new install .

dotnet new list ponymvc9

dotnet new uninstall .





Please note this line :

#if SeedUser

is processed when creating new template, it won't show up in the created source code








