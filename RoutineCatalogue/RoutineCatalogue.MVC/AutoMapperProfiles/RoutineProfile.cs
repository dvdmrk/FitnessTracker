using AutoMapper;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
namespace RoutineCatalogue.MVC.AutoMapperProfiles
{
    public class RoutineProfile : Profile
    {
        public RoutineProfile()
        {
            CreateMap<Routine, RoutineViewModel>();
            CreateMap<RoutineViewModel, Routine>()
                .ForMember(dest => dest.CreateDate, map => map.Ignore())
                .ForMember(dest => dest.UpdateDate, map => map.Ignore())
                .ForMember(dest => dest.UpdateBy, map => map.Ignore())
                .ForMember(dest => dest.CreateBy, map => map.Ignore());
            CreateMap<Routine, RoutineIndexViewModel>();
        }
    }
}