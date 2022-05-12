using AutoMapper;
using MediatR;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Base.Api.Application.Dtos.Categories;

namespace Base.Api.Application.Features.Command.Categories;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryDto, Response<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<NoContent>> Handle(UpdateCategoryDto request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Category>(request);

        _unitOfWork.CategoryWriteRepository().Update(entity);

        bool result = await _unitOfWork.SaveChangesAsync() > 0;

        if (!result)
        {
            return Response<NoContent>.Fail("hata meydana geldi", 500);
        }

        return Response<NoContent>.Success(204);
    }
}