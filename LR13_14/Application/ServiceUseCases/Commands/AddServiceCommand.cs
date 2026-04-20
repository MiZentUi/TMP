using System;

namespace LR13_14.Application.ServiceUseCases.Commands;

public sealed record AddServiceCommand(string Name, TimeOnly Begin, TimeSpan Duration, double Cost, int? CategoryId) : IRequest<Service> { }