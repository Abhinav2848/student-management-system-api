using API_studentManagement.Helper;
using API_studentManagement.Interface;
using API_studentManagement.Models;
using API_studentManagement.Repository;
using API_studentManagement.Service;
using Microsoft.EntityFrameworkCore;

namespace API_studentManagement.Extension
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();


            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
