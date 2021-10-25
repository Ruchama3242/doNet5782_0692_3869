using System;

public struct Station
{
    public int id { get; set; }
    public string name { get; set; }
    public double longitude { get; set; }
    public double lattitude { get; set; }
    public int chargeSlots { get; set; }

    public override string ToString()
    {
        String result = " ";
        result += $"ID is {ID}, \n";
        result += $"Name is {name}, \n";
        result += $"Latitude is {Latitude}, \n";
        result += $"charge slolts is {chargeSlots}, \n";
        return result;
    }
}