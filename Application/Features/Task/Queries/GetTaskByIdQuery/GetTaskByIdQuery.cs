using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Task.Queries.GetTaskByIdQuery
{
    public class GetTaskByIdQuery : IRequest<Response<TaskDto>>
    {
        public int Id { get; set; }
        public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Response<TaskDto>>
        {
            private readonly IRepositoryAsync<Domain.Entities.Tasks> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetTaskByIdQueryHandler(IRepositoryAsync<Domain.Entities.Tasks> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
            {
                var task = await _repositoryAsync.GetByIdAsync(request.Id);

                if (task == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id: {request.Id}");
                }
                else
                {
                    TaskDto dto = _mapper.Map<TaskDto>(task);

                    return new Response<TaskDto>(dto);
                }
            }
        }
    }
}
