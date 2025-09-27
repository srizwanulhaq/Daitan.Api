using Daitan.Business.Helpers;
using Daitan.Business.Repositories.Dashboards;
using Daitan.Business.Repositories.GatewayDevices;
using Daitan.Business.Repositories.Gateways;
using Daitan.Business.Repositories.Settings;
using Daitan.Business.Repositories.Users;
using Daitan.Data.DBContexts;
using Daitan.Data.Enums;
using Daitan.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

// Add controllers and Swagger
builder.Services.AddControllers();

builder.Services.AddHttpClient();

//builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
          policy => policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

#region Repository Services Configurations

//builder.Services.Configure<MqttSettings>(builder.Configuration.GetSection("MqttSettings"));
//builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

//builder.Services.AddSingleton<MqttClt>();
builder.Services.AddSingleton<EmailUtility>();
//builder.Services.AddSingleton<StripePayment>();
builder.Services.AddSingleton<HttpHelper>();

builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IGatewayRepository, GatewayRepository>();
builder.Services.AddScoped<IGatewayDeviceRepository, GatewayDeviceRepository>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


#endregion



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { UserRoleEnums.ADMIN.ToString(),
        UserRoleEnums.MANAGER.ToString(), UserRoleEnums.USER.ToString() };

    //foreach (var roleName in roleNames)
    //{
    //    var roleExist = await roleManager.RoleExistsAsync(roleName);
    //    if (!roleExist)
    //    {
    //        await roleManager.CreateAsync(new ApplicationRole()
    //        {
    //            Name = roleName,
    //        });
    //    }
    //}
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.UseCors("AllowAngular");

app.Run();


