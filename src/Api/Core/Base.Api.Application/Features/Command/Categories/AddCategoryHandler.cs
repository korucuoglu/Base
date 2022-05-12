﻿using AutoMapper;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Command.Categories
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryDto, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HashService _hashService;

        public AddCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, HashService hashService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hashService = hashService;
        }

        public async Task<Response<string>> Handle(AddCategoryDto request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.CategoryWriteRepository().AddAsync(_mapper.Map<Category>(request));

            var result = await _unitOfWork.SaveChangesAsync() > 0;

            if (!result)
            {
                return Response<string>.Fail("hata meydana geldi", 500);
            }

            return Response<string>.Success(data: _hashService.Encode(data.Id), 201);
        }
    }
}