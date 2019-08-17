using AutoMapper;
using RoutineCatalogue.Models.Entities;
using RoutineCatalogue.Models.ViewModels;
namespace RoutineCatalogue.Models.AutoMapperProfiles
{
    public class SetProfile : Profile
    {
        public SetProfile()
        {
            CreateMap<Set, SetViewModel>()
                .ForMember(dest => dest.Exercise, map => map.MapFrom(source => source.Exercise.Name))
                .ForMember(dest => dest.RoutineId, map => map.MapFrom(source => source.Routine.Id))
                .ForMember(dest => dest.ExerciseId, map => map.MapFrom(source => source.Exercise.Id));
            CreateMap<SetViewModel, Set>()
                .ForMember(dest => dest.Exercise, map => map.MapFrom(source => source.ExerciseId))
                .ForMember(dest => dest.Routine, map => map.MapFrom(source => source.RoutineId))
                .ForMember(dest => dest.UpdateDate, map => map.Ignore())
                .ForMember(dest => dest.UpdateBy, map => map.Ignore())
                .ForMember(dest => dest.CreateBy, map => map.Ignore());
            CreateMap<Set, SetIndexViewModel>()
                .ForMember(dest => dest.Exercise, map => map.MapFrom(source => source.Exercise.Name));
        }
    }
}