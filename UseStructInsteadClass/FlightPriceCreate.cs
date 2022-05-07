using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UseStructInsteadClass;

public static class FlightPriceCreate
{
    private static readonly DateTime BaseTime = new DateTime(2020,12,12, 12, 12, 12);
    
    public static readonly FlightPriceStruct[] FlightPrices = Enumerable.Range(0,
#if DEBUG
        10000
#else
        150_0000
#endif
    ).Select(index =>
        new FlightPriceStruct
        {
            Airline = $"C{(char)(index % 26 + 'A')}",
            Start = $"SH{(char)(index % 26 + 'A')}",
            End = $"PE{(char)(index % 26 + 'A')}",
            FlightNo = $"{index % 1000:0000}",
            Cabin = $"{(char)(index % 26 + 'A')}",
            Price = index % 1000,
            DepDate = DateOnly.FromDateTime(BaseTime.AddHours(index)),
            DepTime = TimeOnly.FromDateTime(BaseTime.AddHours(index)),
            ArrDate = DateOnly.FromDateTime(BaseTime.AddHours(3 + index)),
            ArrTime = TimeOnly.FromDateTime(BaseTime.AddHours(3 + index)),
        }).ToArray();

    
    public static FlightPriceClass[] GetClassStore()
    {
        var arrays = new FlightPriceClass[FlightPrices.Length];
        for (int i = 0; i < FlightPrices.Length; i++)
        {
            ref var item = ref FlightPrices[i];
            arrays[i] = new FlightPriceClass
            {
                Airline = item.Airline,
                Start = item.Start,
                End = item.End,
                FlightNo = item.FlightNo,
                Cabin = item.Cabin,
                Price = item.Price,
                DepDate = item.DepDate,
                DepTime = item.DepTime,
                ArrDate = item.ArrDate,
                ArrTime = item.ArrTime
            };
        }

        return arrays;
    }
    
    public static FlightPriceStruct[] GetStructStore()
    {
        var arrays = new FlightPriceStruct[FlightPrices.Length];
        for (int i = 0; i < FlightPrices.Length; i++)
        {
            ref var item = ref FlightPrices[i];
            arrays[i] = new FlightPriceStruct
            {
                Airline = item.Airline,
                Start = item.Start,
                End = item.End,
                FlightNo = item.FlightNo,
                Cabin = item.Cabin,
                Price = item.Price,
                DepDate = item.DepDate,
                DepTime = item.DepTime,
                ArrDate = item.ArrDate,
                ArrTime = item.ArrTime
            };
        }

        return arrays;
    }

    
    public static unsafe FlightPriceStructExplicit[] GetStructStoreStructExplicit()
    {
        var arrays = new FlightPriceStructExplicit[FlightPrices.Length];
        for (int i = 0; i < FlightPrices.Length; i++)
        {
            ref var item = ref FlightPrices[i];
            arrays[i] = new FlightPriceStructExplicit
            {
                Price = item.Price,
                DepDate = item.DepDate,
                DepTime = item.DepTime,
                ArrDate = item.ArrDate,
                ArrTime = item.ArrTime
            };
            ref var val = ref arrays[i];

            fixed (char* airline = val.Airline)
            fixed (char* start = val.Start)
            fixed (char* end = val.End)
            fixed (char* flightNo = val.FlightNo)
            fixed (char* cabin = val.Cabin)
            {
                item.Airline.SetTo(airline);
                item.Start.SetTo(start);
                item.End.SetTo(end);
                item.FlightNo.SetTo(flightNo);
                item.Cabin.SetTo(cabin);
            }
        }

        return arrays;
    }
    
    
    public static unsafe int GetStructStoreUnManageMemory(out IntPtr ptr)
    {
        var unManagerPtr = Marshal.AllocHGlobal(Unsafe.SizeOf<FlightPriceStructExplicit>() * FlightPrices.Length);
        ptr = unManagerPtr;
        var arrays = new Span<FlightPriceStructExplicit>(unManagerPtr.ToPointer(), FlightPrices.Length);
        for (int i = 0; i < FlightPrices.Length; i++)
        {
            ref var item = ref FlightPrices[i];
            arrays[i] = new FlightPriceStructExplicit
            {
                Price = item.Price,
                DepDate = item.DepDate,
                DepTime = item.DepTime,
                ArrDate = item.ArrDate,
                ArrTime = item.ArrTime
            };
            ref var val = ref arrays[i];

            fixed (char* airline = val.Airline)
            fixed (char* start = val.Start)
            fixed (char* end = val.End)
            fixed (char* flightNo = val.FlightNo)
            fixed (char* cabin = val.Cabin)
            {
                item.Airline.SetTo(airline);
                item.Start.SetTo(start);
                item.End.SetTo(end);
                item.FlightNo.SetTo(flightNo);
                item.Cabin.SetTo(cabin);
            }
        }
        
        return arrays.Length;
    }
}