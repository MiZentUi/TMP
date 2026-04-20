using System.Threading;
using System.Threading.Tasks;
using LR13_14.Application.ServiceUseCases.Commands;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;

namespace LR13_14.Application.ServiceUseCases.Handlers;

internal class AddServiceCommandHandler : IRequestHandler<AddServiceCommand, Service>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddServiceCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Service> Handle(AddServiceCommand request, CancellationToken cancellationToken)
    {
        Service newService = new(new ServiceData(request.Name, request.Begin, request.Duration), request.Cost);
        if (request.CategoryId.HasValue)
        {
            newService.AddToRoomCategory(request.CategoryId!.Value);
        }
        await _unitOfWork.ServiceRepository.AddAsync(newService, cancellationToken);
        await _unitOfWork.SaveAllAsync();
        return newService;
    }
}