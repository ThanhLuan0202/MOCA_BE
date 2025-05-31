using Microsoft.Extensions.DependencyInjection;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            //service.AddTransient<Ixxx, yyy>();
            service.AddTransient<IAuthenRepository, AuthenRepository>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient<ILessonsRepository, LessonsRepository>();
            service.AddTransient<IChapterRepository, ChapterRepository>();
            service.AddTransient<ICourseRepository, CourseRepository>();
            service.AddTransient<IPacakgeRepository, PackageRepository>();
            service.AddTransient<IMomProfileRepository, MomProfileRepository>();
            service.AddTransient<IDoctorProfileRepository, DoctorProfileRepository>();
            service.AddTransient<IUserPregnanciesRepository, UserPregnanciesRepository>();
            service.AddTransient<IPregnancyTrackingRepository, PregnancyTrackingRepository>();
            service.AddTransient<IPurchasedCourseRepository, PurchasedCourseRepository>();
            service.AddTransient<IBabyTrackingRepository, BabyTrackingRepository>();






            return service;
        }
    }
}
