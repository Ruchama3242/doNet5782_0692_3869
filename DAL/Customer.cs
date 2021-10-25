using System;

public struct Customer
{
    public int id { get; set; }
    public string name { get; set; }
    public string phone { get; set; }
    public double longitude { get; set; }
    public double lattitude { get; set; }

    public override string ToString()
    {
        String result = " ";
        result += $"ID is {ID}, \n";
        result += $"Name is {Name}, \n";
        result += $"Telephone is 0 {Phone.Substring(0, 2) + '-' + Phone.Substring(2)}, \n";
        result += $"Lattitude is {Latitude}, \n";
        result += $"longitude is {Longitude}, \n";
        return result;
    }

}
