using System.Threading;
using System.Threading.Tasks;
using LR13_14.Application.ServiceUseCases.Commands;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;

namespace LR13_14.Application.ServiceUseCases.Handlers;

internal class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Service>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateServiceCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Service> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        Service newService = new(new ServiceData(request.Name, request.Begin, request.Duration), request.Cost)
        {
            Id = request.Service.Id
        };
        if (request.CategoryId.HasValue)
        {
            newService.AddToRoomCategory(request.CategoryId!.Value);
        }
        await _unitOfWork.ServiceRepository.UpdateAsync(newService, cancellationToken);
        await _unitOfWork.SaveAllAsync();
        return newService;
    }
}