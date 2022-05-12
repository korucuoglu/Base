using Base.Api.Application.Interfaces.Repositories;
using Base.Api.Domain.Entities;
using Base.Api.Persistence.Context;

namespace Base.Api.Persistence.Repositories;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}