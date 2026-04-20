using System.Threading;
using System.Threading.Tasks;
using LR13_14.Application.RoomCategoryUseCases.Commands;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;

namespace LR13_14.Application.RoomCategoryUseCases.Handlers;

internal class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, RoomCategory>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<RoomCategory> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        RoomCategory newCategory = new(request.Name, request.RoomsCount, request.StarsCount);
        await _unitOfWork.RoomCategoryRepository.AddAsync(newCategory, cancellationToken);
        await _unitOfWork.SaveAllAsync();
        return newCategory;
    }
}