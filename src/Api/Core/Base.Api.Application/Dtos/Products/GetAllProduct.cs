﻿using Base.Api.Application.Dtos.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Base.Api.Application.Dtos.Products;

public class GetAllProduct : IRequest<Response<List<ProductDto>>>
{
}