using System.Collections.Generic;
using LR13_14.Domain.Entities;

namespace LR13_14.Application.ServiceUseCases.Commands;

public sealed record GetServicesByCategoryRequest(int Id) : IRequest<IEnumerable<Service>> { }
