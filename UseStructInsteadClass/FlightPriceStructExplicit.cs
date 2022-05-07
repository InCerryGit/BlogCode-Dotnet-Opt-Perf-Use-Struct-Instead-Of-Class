using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UseStructInsteadClass;

[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
[SkipLocalsInit]
public struct FlightPriceStructExplicit
{
    [FieldOffset(0)]
    public unsafe fixed char Airline[2];

    [FieldOffset(4)]
    public unsafe fixed char Start[3];

    [FieldOffset(10)]
    public unsafe fixed char End[3];

    [FieldOffset(16)]
    public unsafe fixed char FlightNo[4];

    [FieldOffset(24)]
    public unsafe fixed char Cabin[2];

    [FieldOffset(28)]
    public decimal Price;

    [FieldOffset(44)]
    public DateOnly DepDate;

    [FieldOffset(48)]
    public TimeOnly DepTime;

    [FieldOffset(56)]
    public DateOnly ArrDate;

    [FieldOffset(60)]
    public TimeOnly ArrTime;
    
    public static unsafe bool EqualsAirline(FlightPriceStructExplicit item, string airline)
    {
        return airline.SpanEquals(item.Airline, 2);
    }
    
    public static unsafe bool EqualsStart(FlightPriceStructExplicit item, string start)
    {
        return start.SpanEquals(item.Start, 3);
    }
    
    public static unsafe bool EqualsEnd(FlightPriceStructExplicit item, string end)
    {
        return end.SpanEquals(item.End, 3);
    }
    
    public static unsafe bool EqualsFlightNo(FlightPriceStructExplicit item, string flightNo)
    {
        return flightNo.SpanEquals(item.FlightNo, 4);
    }
    
    public static unsafe bool EqualsCabin(FlightPriceStructExplicit item, string cabin)
    {
        return cabin.SpanEquals(item.Cabin, 2);
    }
    
    public static bool IsPriceLess(FlightPriceStructExplicit item, decimal min)
    {
        return item.Price < min;
    }
    
    public static unsafe bool EqualsAirlineRef(ref FlightPriceStructExplicit item, string airline)
    {
        fixed(char* ptr = item.Airline)
        {
            return airline.SpanEquals(ptr, 2);
        }
    }
    
    public static unsafe bool EqualsStartRef(ref FlightPriceStructExplicit item, string start)
    {
        fixed (char* ptr = item.Start)
        {
            return start.SpanEquals(ptr, 3);
        }
    }
    
    public static unsafe bool EqualsEndRef(ref FlightPriceStructExplicit item, string end)
    {
        fixed (char* ptr = item.End)
        {
            return end.SpanEquals(ptr, 3);
        }
    }
    
    public static unsafe bool EqualsFlightNoRef(ref FlightPriceStructExplicit item, string flightNo)
    {
        fixed (char* ptr = item.FlightNo)
        {
            return flightNo.SpanEquals(ptr, 4);
        }
    }
    
    public static unsafe bool IsCabinRef(ref FlightPriceStructExplicit item, string cabin)
    {
        fixed (char* ptr = item.Cabin)
        {
            return cabin.SpanEquals(ptr, 2);
        }
    }
    
    public static bool IsPriceLessRef(ref FlightPriceStructExplicit item, decimal min)
    {
        return item.Price < min;
    }
}