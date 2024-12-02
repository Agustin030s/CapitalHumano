using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Skills.Commands.DeleteSkillCommand
{
    public class DeleteSkillCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Response<int>>
        {
            private readonly IRepositoryAsync<Skill> _repositoryAsync;

            public DeleteSkillCommandHandler(IRepositoryAsync<Skill> repositoryAsync)
            {
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<int>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
            {
                Skill skill = await _repositoryAsync.GetByIdAsync(request.Id);
                if(skill == null)
                {
                    return new Response<int>($"Registro no encontrado con el Id: {request.Id}");
                }

                await _repositoryAsync.DeleteAsync(skill);
                return new Response<int>(skill.Id, "Skill eliminada con éxito");
            }
        }
    }
}
