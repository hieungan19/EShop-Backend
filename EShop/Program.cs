using EShop.Data;
using EShop.JWT;
using EShop.Models.Account;
using EShop.Services.CartServices;
using EShop.Services.CategoryService;
using EShop.Services.CouponServices;
using EShop.Services.LoginService;
using EShop.Services.OptionServices;
using EShop.Services.OrderServices;
using EShop.Services.ProductService;
using EShop.Services.RegisterService;
using EShop.Services.RoleService;
using EShop.Services.TestService;
using EShop.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);//;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EShopDBContext>();

builder.Services.AddIdentity<ApiUser, ApiRole>(options =>
        options.User.RequireUniqueEmail = true)
        .AddEntityFrameworkStores<EShopDBContext>()
        .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = false,
            ValidIssuer = "EShop",
            ValidAudience = "EShop",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = JWTConfig.SymmetricKey
        };
    });

builder.Services.AddCors(o => o.AddPolicy("corsFixLocalhost", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddAuthentication();

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICouponService, CouponService>(); 
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOptionService, OptionService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsFixLocalhost");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
