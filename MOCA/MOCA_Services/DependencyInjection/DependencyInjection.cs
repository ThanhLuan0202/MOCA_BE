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
            service.AddTransient<IPregnancyTrackingService, PregnancyTrackingService>();
            service.AddTransient<IPurchasedCourseServices, PurchasedCourseServices>();
            service.AddTransient<IBabyTrackingService, BabyTrackingService>();
            service.AddTransient<IFeedbackService, FeedbackService>();
            service.AddTransient<IEmailService, EmailService>();
            service.AddTransient<ICourseCategoryService, CourseCategoryService>();
            service.AddTransient<IDiscountService, DiscountService>();
            service.AddTransient<ICourseVnPayService, CourseVnPayService>();
            service.AddTransient<ICommunityPostService, CommunityPostService>();
            service.AddTransient<ICommunityReplyService, CommunityReplyService>();  
            service.AddTransient<IPostLikeService, PostLikeService>();
            service.AddTransient<IAdvertisementService, AdvertisementService>();
            service.AddTransient<IChatAdviceService, ChatAdviceService>();
            service.AddTransient<IPurchasedPackageService, PurchasedPackageService>();
            service.AddTransient<IDoctorBookingService, DoctorBookingService>();
            service.AddTransient<ICourseCartService, CourseCartService>();
            service.AddTransient<IOrderCourseService, OrderCourseService>();
            service.AddTransient<ICoursePaymentService, CoursePaymentService>();
            service.AddTransient<IBookingPaymentService, BookingPaymentService>();
            service.AddTransient<IPregnancyDiaryService, PregnancyDiaryService>();
            service.AddTransient<PrenatalReminderService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<ChatService>();
            service.AddTransient<IDoctorContactService, DoctorContactService>();
            service.AddTransient<IPayOSService, PayOSService>();
            service.AddTransient<IPayPackageService, PayPackageService>();













            return service;
        }

    }
}
