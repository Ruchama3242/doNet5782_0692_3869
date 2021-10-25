using System;

public struct Drone
{
    public int id { get; set; }
    public string model { get; set; }
    public WeightCategories weight { get; set; }
    public DroneStatus status { get; set; }
    public double battery { get; set; }

    public override string ToString()
    {
        String result = " ";
        result += $"ID is {ID}, \n";
        result += $"model is {model}, \n";
        result += $"weight  is {weight}, \n";
        result += $"status is {status}, \n";
        result += $"battery is {battery}, \n";
        return result;
    }
}