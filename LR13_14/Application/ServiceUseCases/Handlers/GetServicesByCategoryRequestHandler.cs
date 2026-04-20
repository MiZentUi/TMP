using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LR13_14.Application.ServiceUseCases.Commands;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;

namespace LR13_14.Application.ServiceUseCases.Handlers;

internal class GetServicesByCategoryRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetServicesByCategoryRequest, IEnumerable<Service>>
{
    public async Task<IEnumerable<Service>> Handle(GetServicesByCategoryRequest request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.ServiceRepository.ListAsync(t => t.RoomCategoryId.Equals(request.Id), cancellationToken);
    }
}