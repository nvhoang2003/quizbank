using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using QuizzBankBE.DataAccessLayer.DataObject;
using QuizzBankBE.DataAccessLayer.Data;
using QuizzBankBE.Services.AuthServices;
using QuizzBankBE.Services.UserServices;
using QuizzBankBE.JWT;
using AutoMapper;

namespace QuizzBankBE
{
    public class Startup
    {
        public IConfiguration configroot { get; }

        public Startup(IConfiguration configuration)
        {
            configroot = configuration;
        }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            configroot = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthServices, AuthServices>();
              //services.AddScoped<IMenuServices, MenuServicesImpl>();
                //services.AddScoped<IRoleServices, RoleServicesImpl>();
                //services.AddScoped<IPermissionServices, PermissionServicesImpl>();
            services.AddScoped<IUserServices, UserServices>();
                //services.AddScoped<IGmcServices, GmcServicesImpl>();
                //services.AddScoped<ISaleServices, SaleServicesImpl>();
                //services.AddScoped<IWebconfigServices, WebconfigServicesImpl>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IjwtProvider, JwtProvider>();


            services.AddOptions();
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(configroot.GetSection("AppSettings:TokenKeySecret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddDbContext<DataContext>(option =>
            {
                option.UseMySQL(configroot["ConnectionStrings:DefaultConnection"]);
            }
            );

            services.AddEndpointsApiExplorer();
            services.AddCors(option =>
            {
                option.AddPolicy("AllowAll",
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("x-pagination");
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CSM API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme, e.g. \"Bearer {token} \"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.Configure<GzipCompressionProviderOptions>(option => option.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(option =>
            {
                option.Providers.Add<GzipCompressionProvider>();
                option.MimeTypes = new[]
                {
                    "text/plain",
                    "text/css",
                    "application/javascript",
                   "text/html",
                   "application/xml",
                   "text/xml",
                   "application/json",
                   "text/json",

                   "img/svg+xml",
                   "application/font-woff2"
            };
            });

            services.Configure<HstsOptions>(option =>
            {
                option.IncludeSubDomains = true;
                option.MaxAge = TimeSpan.FromDays(365);
            });

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseCors("AllowAll");
            app.UseCorsMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseResponseCompression();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int cacheExpirationInSeconds = 60 * 60 * 24 * 30; //a month
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                    "public,max-age=" + cacheExpirationInSeconds;
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
