using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Departaments.Commands.CreateDepartamentCommand
{
    public class CreateDepartamentCommand : IRequest<Response<int>>
    {
        public string DepartamentCode { get; set; }
        public string Description { get; set; }
    }

    public class CreateDepartamentCommandHandler : IRequestHandler<CreateDepartamentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Departament> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateDepartamentCommandHandler(IRepositoryAsync<Departament> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateDepartamentCommand request, CancellationToken cancellationToken)
        {
            List<Departament> departaments = await _repositoryAsync.ListAsync();
            Departament filterDepartaments = departaments.FirstOrDefault(x => x.DepartamentCode == request.DepartamentCode);
            if (filterDepartaments == null)
                return new Response<int>($"El código de departamento {request.DepartamentCode} ya existe");


            Departament newRecord = _mapper.Map<Departament>(request);

            Departament data = await _repositoryAsync.AddAsync(newRecord, cancellationToken);

            return new Response<int>(data.Id, "Departamento creado con éxito");
        }
    }
}
