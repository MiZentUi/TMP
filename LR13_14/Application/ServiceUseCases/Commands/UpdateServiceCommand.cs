using System;
using LR13_14.Domain.Entities;

namespace LR13_14.Application.ServiceUseCases.Commands;

public sealed record UpdateServiceCommand(Service Service, string Name, TimeOnly Begin, TimeSpan Duration, double Cost, int? CategoryId) : IRequest<Service> { }
