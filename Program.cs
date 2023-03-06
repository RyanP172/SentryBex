using Microsoft.EntityFrameworkCore;
using SentryBex.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SentryBex.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SentryBex.Services;
using SentryBex.Services.Logger;
using Microsoft.AspNetCore.Identity;
using SentryBex.Services.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SentryBex.Services.Account;

var builder = WebApplication.CreateBuilder(args);

var AllowSpecificOrigins = "SentryBexCORSRules";

string connString = builder.Configuration.GetValue<string>("DbContext:ConnectionString");
// Add services to the container.

builder.Services.AddControllers(setupAction =>
{
    setupAction.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson(setupAction =>
{
    setupAction.SerializerSettings.ContractResolver =
    new CamelCasePropertyNamesContractResolver();
    setupAction.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


//CORS policy setting is here
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7042",
                                              "https://localhost:7042", "https://localhost:44414", "http://localhost:4200")
                                            .AllowAnyHeader().AllowAnyMethod();
                                               
                      });
});

builder.Services.AddControllersWithViews();


builder.Services.AddTransient<IEpeEmployeeRepository, EpeEmployeeRepository>();
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<ILoggerRepository, LoggerRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

//Register DBContext services for working with database
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.User.AllowedUserNameCharacters = null;
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(option => 
{
    //Connection string
    option.UseLazyLoadingProxies(true);
    option.UseSqlServer(connString);
});

builder.Services.AddDbContext<AspNetContext>(option =>
{
    //Connection string
    option.UseLazyLoadingProxies(true);
    option.UseSqlServer(connString);
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opt => {
                   var secretByte = Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]);
                   opt.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidIssuer = builder.Configuration["Authentication:Issuer"],

                       ValidateAudience = true,
                       ValidAudience = builder.Configuration["Authentication:Audience"],

                       ValidateLifetime = true,
                       IssuerSigningKey = new SymmetricSecurityKey(secretByte),
                   };
               });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.UseCors(AllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
