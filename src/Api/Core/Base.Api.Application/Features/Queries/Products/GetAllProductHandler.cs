﻿using AutoMapper;
using Base.Api.Application.Dtos.Products;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Queries.Products;

public class GetAllProductHandler : IRequestHandler<GetAllProduct, Response<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<List<ProductDto>>> Handle(GetAllProduct request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.ProductReadRepository().GetAllQueryableAsync();

        var dtos = await _mapper.ProjectTo<ProductDto>(entities).ToListAsync();

        return Response<List<ProductDto>>.Success(data: dtos, statusCode: 200);
    }
}