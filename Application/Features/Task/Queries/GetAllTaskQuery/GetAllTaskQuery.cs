using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Task.Queries.GetAllTaskQuery
{
    public class GetAllTaskQuery : IRequest<Response<List<TaskDto>>>
    {
        public class GetAllTaskQueryHandler : IRequestHandler<GetAllTaskQuery, Response<List<TaskDto>>>
        {
            private readonly IRepositoryAsync<Domain.Entities.Tasks> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllTaskQueryHandler(IRepositoryAsync<Domain.Entities.Tasks> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<TaskDto>>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
            {
                var tasks = await _repositoryAsync.ListAsync();

                List<TaskDto> dto = _mapper.Map<List<TaskDto>>(tasks);

                return new Response<List<TaskDto>>(dto);
            }
        }
    }
}
