
using AMI_ProjectAPI.Data.Repository;
using AMI_ProjectAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AMI_ProjectAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
         

            var builder = WebApplication.CreateBuilder(args);

            // ✅ Add DbContext
            builder.Services.AddDbContext<AmiContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDB"));
            });

            // ✅ Generic Repository registration
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // ✅ Controllers with JSON settings
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // ✅ CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://localhost:7071") // Update to your frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // ✅ Swagger setup with JWT Auth
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AMI Project API",
                    Version = "v1",
                    Description = "Advanced Metering Infrastructure API with JWT Authentication"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] then your token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // ✅ JWT Authentication Configuration
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

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
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            var app = builder.Build();

            // ✅ Seed SuperAdmin (only once)
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AmiContext>();
                context.Database.EnsureCreated();

                if (!context.Users.Any(u => u.Role == "SuperAdmin"))
                {
                    var superAdmin = new User
                    {
                        UserName = "superadmin",
                        Email = "superadmin@ami.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("super@123"),
                        Role = "SuperAdmin"
                    };
                    context.Users.Add(superAdmin);
                    context.SaveChanges();
                    Console.WriteLine("✅ SuperAdmin created: superadmin@ami.com / super@123");
                }
            }

            // ✅ Configure middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthentication(); // must come before UseAuthorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
