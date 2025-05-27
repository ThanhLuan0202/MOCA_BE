using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models;
using MOCA_Repositories.Models.LessonDTO;

namespace MOCA_Repositories.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<xxx, yyy>().ReverseMap();
            CreateMap<Lesson, LessonViewModel>();
            CreateMap<Lesson, AddLessonModel>().ReverseMap();
            CreateMap<Lesson, UpdateLessonModel>().ReverseMap();
            









        }
    }
}
