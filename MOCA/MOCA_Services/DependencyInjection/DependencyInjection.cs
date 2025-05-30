using Microsoft.Extensions.DependencyInjection;
using MOCA_Services.Interfaces;
using MOCA_Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddTransient<IAuthenService, AuthenService>();
            service.AddTransient<ILessonServices, LessonServices>();
            service.AddTransient<IChapterServices, ChapterServices>();
            service.AddTransient<ICourseServices, CourseServices>();
            service.AddTransient<IPackageService, PackageService>();
            service.AddTransient<IMomProfileService, MomProfileService>();
            service.AddTransient<IDoctorProfileService, DoctorProfileService>();
            service.AddTransient<IUserPregnanciesService, UserPregnanciesService>();





            return service;
        }

    }
}
