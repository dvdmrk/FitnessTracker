using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
namespace RoutineCatalogue.MVC.AutoMapperProfiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseViewModel>();
            CreateMap<ExerciseViewModel, Exercise>()
                .ForMember(dest => dest.UpdateDate, map => map.Ignore())
                .ForMember(dest => dest.CreateDate, map => map.Ignore())
                .ForMember(dest => dest.UpdateBy, map => map.Ignore())
                .ForMember(dest => dest.CreateBy, map => map.Ignore());
            CreateMap<Exercise, ExerciseIndexViewModel>()
                .ForMember(dest => dest.UpdateBy, map => map.MapFrom(source => source.UpdateBy != null ? source.UpdateBy.UserName : source.CreateBy.UserName))
                .ForMember(dest => dest.UpdateDate, map => map.MapFrom(source => source.UpdateDate != null ? source.UpdateDate : source.CreateDate));
            CreateMap<Exercise, SelectListItem>()
                .ForMember(dest => dest.Value, map => map.MapFrom(source => source.Id))
                .ForMember(dest => dest.Text, map => map.MapFrom(source => source.Name));
        }

    }
}
