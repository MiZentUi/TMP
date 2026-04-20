using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LR13_14.Application.RoomCategoryUseCases.Commands;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;

namespace LR13_14.Application.RoomCategoryUseCases.Handlers;

internal class GetAllCategoriesRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCategoriesRequest, IEnumerable<RoomCategory>>
{
    public async Task<IEnumerable<RoomCategory>> Handle(GetAllCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.RoomCategoryRepository.ListAllAsync(cancellationToken);
    }
}