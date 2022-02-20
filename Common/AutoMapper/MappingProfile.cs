using AutoMapper;
using Common.DTOs;
using Common.Entities;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        private readonly ProjectsRepository projectsRepository = new ProjectsRepository(); 

        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectToMember, MemberDTO>();
            CreateMap<Assignment, AssignmentDTO>();
            CreateMap<Assignment, AssignmentDetailsDTO>();
            CreateMap<AssignmentComment, CommentDTO>();
            CreateMap<HoursLog, HoursLogDTO>();
        }
    }
}
