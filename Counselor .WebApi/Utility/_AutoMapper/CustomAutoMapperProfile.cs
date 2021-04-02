using AutoMapper;
using Counselor.Model;
using Counselor.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counselor_.WebApi.Utility._AutoMapper
{
    public class CustomAutoMapperProfile : Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<AdminInfo, AdminDTO>();
            base.CreateMap<CounselorInfo, CounselorDTO>();
            base.CreateMap<WorkInfo, WorkDTO>()
                .ForMember(dest => dest.CounselorName, sourse => sourse.MapFrom(src => src.CounselorInfo.Name));
            base.CreateMap<RoadInfo, RoadDTO>()
                .ForMember(dest => dest.CounselorName, sourse => sourse.MapFrom(src => src.CounselorInfo.Name));
            base.CreateMap<NoticeInfo, NoticeDTO>()
                .ForMember(dest => dest.AdminName, sourse => sourse.MapFrom(src => src.AdminInfo.Name));
        }
        
    }
}
