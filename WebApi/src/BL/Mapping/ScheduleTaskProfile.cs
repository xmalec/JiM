using AutoMapper;
using BL.Models.ScheduleTask;
using DAL.Models;

namespace BL.Mapping
{
    public class ScheduleTaskProfile : Profile
    {
        public ScheduleTaskProfile()
        {
            CreateMap<ScheduleTask, ScheduleTaskDto>().ReverseMap();
        }
    }
}
