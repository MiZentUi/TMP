using System;

namespace LR13_14.Domain.Entities;

public class Service : Entity
{
    private Service() { }

    public Service(ServiceData data, double? cost = 0)
    {
        Data = data;
        Cost = cost!.Value;
    }

    public ServiceData? Data { get; private set; }

    public double Cost { get; private set; }

    public int? RoomCategoryId { get; private set; }

    public void AddToRoomCategory(int categoryId)
    {
        if (categoryId <= 0) return;
        RoomCategoryId = categoryId;
    }

    public void RemoveRoomCategory()
    {
        RoomCategoryId = 0;
    }

    public void ChangeCost(double cost)
    {
        if (cost < 0 || cost > 10) return;
        Cost = cost;
    }
}

public sealed record ServiceData(string Name, TimeOnly Begin, TimeSpan Duration);