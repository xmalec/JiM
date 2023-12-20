﻿using AutoMapper;
using BL.Models.File;
using File = DAL.Models.File;

namespace BL.Mapping
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileDto>();
        }
    }
}
