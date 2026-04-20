using System.Collections.Generic;

namespace LR13_14.Domain.Entities;

public class RoomCategory : Entity
{
    private readonly List<Service> _services = [];

    private RoomCategory() { }

    public RoomCategory(string name, int roomsCount, int starsCount)
    {
        Name = name;
        RoomsCount = roomsCount;
        StarsCount = starsCount;
    }

    public string? Name { get; set; }

    public int RoomsCount { get; private set; }

    public int StarsCount { get; private set; }

    public IReadOnlyList<Service> Services
    {
        get => _services.AsReadOnly();
    }
}