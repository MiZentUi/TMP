using System.Collections.Generic;
using LR13_14.Domain.Entities;

namespace LR13_14.Application.RoomCategoryUseCases.Commands;

public sealed record GetAllCategoriesRequest() : IRequest<IEnumerable<RoomCategory>> { }
