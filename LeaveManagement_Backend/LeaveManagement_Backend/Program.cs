using LeaveManagement_Models.DataContext;
using LeaveManagement_Models.IRepository;
using LeaveManagement_Models.Models;
using LeaveManagement_Models.Repository;
using LeaveManagement_Services.IServices;
using LeaveManagement_Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configure identity options here.
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    // Lockout settings  
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(builder.Configuration.GetSection("AccountLockOutInfo:LockoutTimeSpan").Value));
    options.Lockout.MaxFailedAccessAttempts = Convert.ToInt32(builder.Configuration.GetSection("AccountLockOutInfo:MaxFailedAccessAttempts").Value);
    options.Lockout.AllowedForNewUsers = false;
    // User settings  
    options.User.RequireUniqueEmail = true;
    // Default SignIn settings.
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
}).AddEntityFrameworkStores<LeaveManagementDataContext>()
 .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LeaveManagementDataContext>(
    options =>
    {
        options.UseSqlServer(
        builder.Configuration.GetConnectionString("connection"), x => x.MigrationsAssembly("LeaveManagement_Models"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

builder.Services.AddScoped(typeof(IgenericRepository<>), typeof(genericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IleaveBalanceService, LeaveBalanceService>();
builder.Services.AddScoped<IleaveRepository, LeaveRepository>();
builder.Services.AddScoped<IleaveService, LeaveService>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:key").Value));
    options.TokenValidationParameters = new
    TokenValidationParameters
    {
        IssuerSigningKey = serverSecret,
        ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("JWT:Audience").Value,
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
