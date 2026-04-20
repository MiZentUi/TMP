using LR13_14.Domain.Entities;

namespace LR13_14.Application.RoomCategoryUseCases.Commands;

public sealed record AddCategoryCommand(string Name, int RoomsCount, int StarsCount) : IRequest<RoomCategory> { }