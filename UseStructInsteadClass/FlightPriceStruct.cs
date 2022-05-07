using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Auto)]
public struct FlightPriceStruct
{
    public string Airline { get; set; }

    public string Start { get; set; }

    public string End { get; set; }

    public string FlightNo { get; set; }

    public string Cabin { get; set; }

    public decimal Price { get; set; }

    public DateOnly DepDate { get; set; }

    public TimeOnly DepTime { get; set; }

    public DateOnly ArrDate { get; set; }

    public TimeOnly ArrTime { get; set; }
    
    public bool EqualsAirline(string airline)
    {
        return Airline == airline;
    }
    
    public bool EqualsStart(string start)
    {
        return Start == start;
    }
    
    public bool EqualsEnd(string end)
    {
        return End == end;
    }
    
    public bool EqualsFlightNo(string flightNo)
    {
        return FlightNo == flightNo;
    }
    
    public bool IsCabin(string cabin)
    {
        return Cabin == cabin;
    }
    
    public bool IsPriceLess(decimal min)
    {
        return Price == min;
    }
    
    public static bool EqualsAirline(ref FlightPriceStruct item, string airline)
    {
        return item.Airline == airline;
    }
    
    public static bool EqualsStart(ref FlightPriceStruct item, string start)
    {
        return item.Start == start;
    }
    
    public static bool EqualsEnd(ref FlightPriceStruct item, string end)
    {
        return item.End == end;
    }
    
    public static bool EqualsFlightNo(ref FlightPriceStruct item, string flightNo)
    {
        return item.FlightNo == flightNo;
    }
    
    public static bool IsCabin(ref FlightPriceStruct item, string cabin)
    {
        return item.Cabin == cabin;
    }
    
    public static bool IsPriceLess(ref FlightPriceStruct item, decimal min)
    {
        return item.Price < min;
    }
}