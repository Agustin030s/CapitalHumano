using Application.DTOs;
using Application.Features.Departaments.Commands.CreateDepartamentCommand;
using Application.Features.Employees.Command.CreateEmployeeCommand;
using Application.Features.Positions.Commands.AddSkillToPositionCommand;
using Application.Features.Positions.Commands.CreatePositionCommand;
using Application.Features.Skills.Commands.CreateSkillCommand;
using Application.Features.Task.Commands.CreateTaskCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Dtos
            CreateMap<Departament, DepartamentDto>();
            CreateMap<Position, PositionDto>()
                .ForMember(dest => dest.DepartamentName, opt => opt.MapFrom(src => src.Departament.Description));
            CreateMap<Domain.Entities.Tasks, TaskDto>();
            CreateMap<Skill, SkillDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Salary, SalaryDto>();
            CreateMap<PositionSkill, PositionsSkillDto>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Description));
            CreateMap<PositionHistory, PositionHistoryDto>();
            #endregion
            #region Commands
            CreateMap<CreateDepartamentCommand, Departament>();
            CreateMap<CreatePositionCommand, Position>();
            CreateMap<CreateTaskCommand, Domain.Entities.Tasks>();
            CreateMap<CreateSkillCommand, Skill>();
            CreateMap<AddSkillToPositionCommand, PositionSkill>();
            CreateMap<CreateEmployeeCommand, Employee>();
            #endregion
        }
    }
}
