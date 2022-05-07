using System.Diagnostics;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Diagnostics.Windows;
using UseStructInsteadClass;

const bool size = false;
#if DEBUG
if (size)
{
    var sizeBench = new SizeBench();
    // sizeBench.GetStructStoreFlightPriceStructUnManage();
}
else
{
    var speedBench = new SpeedBench();
    speedBench.GetClassStore();
    speedBench.GetStructStoreRef();
    speedBench.GetStructExplicitStoreRef();
    speedBench.GetStructStoreUnManageMemoryRef();
}
#else

if (size)
{
    var runner = BenchmarkRunner.Run<SizeBench>();
}
else
{
    var runner = BenchmarkRunner.Run<SpeedBench>();
}

#endif

[GcForce(true)]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class SizeBench
{
    [Benchmark(Baseline = true)]
    public void GetClassStore()
    {
        _ = FlightPriceCreate.GetClassStore();
    }

    [Benchmark]
    public void GetStructStore()
    {
        _ = FlightPriceCreate.GetStructStore();
    }

    [Benchmark]
    public void GetStructExplicitStore()
    {
        _ = FlightPriceCreate.GetStructStoreStructExplicit();
    }

    [Benchmark]
    public void GetStructStoreUnManageMemory()
    {
        _ = FlightPriceCreate.GetStructStoreUnManageMemory(out var ptr);
        Marshal.FreeHGlobal(ptr);
    }
}


[GcForce(true)]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[HardwareCounters(
    HardwareCounter.LlcMisses,
    HardwareCounter.LlcReference)]
public class SpeedBench : IDisposable
{
    private static readonly FlightPriceClass[] FlightPrices = FlightPriceCreate.GetClassStore();
    private static readonly FlightPriceStruct[] FlightPricesStruct = FlightPriceCreate.GetStructStore();
    private static readonly FlightPriceStructExplicit[] FlightPricesStructExplicit =
        FlightPriceCreate.GetStructStoreStructExplicit();

    private static IntPtr _unManagerPtr;
    private static readonly int FlightPricesStructExplicitUnManageMemoryLength =
        FlightPriceCreate.GetStructStoreUnManageMemory(out _unManagerPtr);
    
    
    [Benchmark(Baseline = true)]
    public int GetClassStore()
    {
        var caAirline = 0;
        var shaStart = 0;
        var peaStart = 0;
        var ca0001FlightNo = 0;
        var priceLess500 = 0;
        for (int i = 0; i < FlightPrices.Length; i++)
        {
            var item = FlightPrices[i];
            if (item.EqualsAirline("CA"))caAirline++;
            if (item.EqualsStart("SHA"))shaStart++;
            if (item.EqualsEnd("PEA"))peaStart++;
            if (item.EqualsFlightNo("0001"))ca0001FlightNo++;
            if (item.IsPriceLess(500))priceLess500++;
        }
        Debug.WriteLine($"{caAirline},{shaStart},{peaStart},{ca0001FlightNo},{priceLess500}");
        return caAirline + shaStart + peaStart + ca0001FlightNo + priceLess500;
    }
    
    [Benchmark]
    public int GetStructStore()
    {
        var caAirline = 0;
        var shaStart = 0;
        var peaStart = 0;
        var ca0001FlightNo = 0;
        var priceLess500 = 0;
        for (int i = 0; i < FlightPricesStruct.Length; i++)
        {
            var item = FlightPricesStruct[i];
            if (item.EqualsAirline("CA"))caAirline++;
            if (item.EqualsStart("SHA"))shaStart++;
            if (item.EqualsEnd("PEA"))peaStart++;
            if (item.EqualsFlightNo("0001"))ca0001FlightNo++;
            if (item.IsPriceLess(500))priceLess500++;
        }
        Debug.WriteLine($"{caAirline},{shaStart},{peaStart},{ca0001FlightNo},{priceLess500}");
        return caAirline + shaStart + peaStart + ca0001FlightNo + priceLess500;
    }
    
    [Benchmark]
    public int GetStructExplicitStore()
    {
        var caAirline = 0;
        var shaStart = 0;
        var peaStart = 0;
        var ca0001FlightNo = 0;
        var priceLess500 = 0;
        for (int i = 0; i < FlightPricesStructExplicit.Length; i++)
        {
            var item = FlightPricesStructExplicit[i];
            if (FlightPriceStructExplicit.EqualsAirline(item,"CA"))caAirline++;
            if (FlightPriceStructExplicit.EqualsStart(item,"SHA"))shaStart++;
            if (FlightPriceStructExplicit.EqualsEnd(item,"PEA"))peaStart++;
            if (FlightPriceStructExplicit.EqualsFlightNo(item,"0001"))ca0001FlightNo++;
            if (FlightPriceStructExplicit.IsPriceLess(item,500))priceLess500++;
        }
        Debug.WriteLine($"{caAirline},{shaStart},{peaStart},{ca0001FlightNo},{priceLess500}");
        return caAirline + shaStart + peaStart + ca0001FlightNo + priceLess500;
    }
    
    [Benchmark]
    public unsafe int GetStructStoreUnManageMemory()
    {
        var caAirline = 0;
        var shaStart = 0;
        var peaStart = 0;
        var ca0001FlightNo = 0;
        var priceLess500 = 0;
        var arrays = new Span<FlightPriceStructExplicit>(_unManagerPtr.ToPointer(), FlightPricesStructExplicitUnManageMemoryLength);
        for (int i = 0; i < arrays.Length; i++)
        {
            var item = arrays[i];
            if (FlightPriceStructExplicit.EqualsAirline(item,"CA"))caAirline++;
            if (FlightPriceStructExplicit.EqualsStart(item,"SHA"))shaStart++;
            if (FlightPriceStructExplicit.EqualsEnd(item,"PEA"))peaStart++;
            if (FlightPriceStructExplicit.EqualsFlightNo(item,"0001"))ca0001FlightNo++;
            if (FlightPriceStructExplicit.IsPriceLess(item,500))priceLess500++;
        }
        Debug.WriteLine($"{caAirline},{shaStart},{peaStart},{ca0001FlightNo},{priceLess500}");
        return caAirline + shaStart + peaStart + ca0001FlightNo + priceLess500;
    }
    
    [Benchmark]
    public unsafe int GetStructStoreUnManageMemoryRef()
    {
        var caAirline = 0;
        var shaStart = 0;
        var peaStart = 0;
        var ca0001FlightNo = 0;
        var priceLess500 = 0;
        var arrays = new Span<FlightPriceStructExplicit>(_unManagerPtr.ToPointer(), FlightPricesStructExplicitUnManageMemoryLength);
        for (int i = 0; i < arrays.Length; i++)
        {
            ref var item = ref arrays[i];
            if (FlightPriceStructExplicit.EqualsAirlineRef(ref item,"CA"))caAirline++;
            if (FlightPriceStructExplicit.EqualsStartRef(ref item,"SHA"))shaStart++;
            if (FlightPriceStructExplicit.EqualsEndRef(ref item,"PEA"))peaStart++;
            if (FlightPriceStructExplicit.EqualsFlightNoRef(ref item,"0001"))ca0001FlightNo++;
            if (FlightPriceStructExplicit.IsPriceLessRef(ref item,500))priceLess500++;
        }
        Debug.WriteLine($"{caAirline},{shaStart},{peaStart},{ca0001FlightNo},{priceLess500}");
        return caAirline + shaStart + peaStart + ca0001FlightNo + priceLess500;
    }
        
    [Benchmark]
    public int GetStructStoreRef()
    {
        var caAirline = 0;
        var shaStart = 0;
        var peaStart = 0;
        var ca0001FlightNo = 0;
        var priceLess500 = 0;
        for (int i = 0; i < FlightPricesStruct.Length; i++)
        {
            ref var item = ref FlightPricesStruct[i];
            if (FlightPriceStruct.EqualsAirline(ref item,"CA"))caAirline++;
            if (FlightPriceStruct.EqualsStart(ref item,"SHA"))shaStart++;
            if (FlightPriceStruct.EqualsEnd(ref item,"PEA"))peaStart++;
            if (FlightPriceStruct.EqualsFlightNo(ref item,"0001"))ca0001FlightNo++;
            if (FlightPriceStruct.IsPriceLess(ref item,500))priceLess500++;
        }
        Debug.WriteLine($"{caAirline},{shaStart},{peaStart},{ca0001FlightNo},{priceLess500}");
        return caAirline + shaStart + peaStart + ca0001FlightNo + priceLess500;
    }
    
    [Benchmark]
    public int GetStructExplicitStoreRef()
    {
        var caAirline = 0;
        var shaStart = 0;
        var peaStart = 0;
        var ca0001FlightNo = 0;
        var priceLess500 = 0;
        for (int i = 0; i < FlightPricesStructExplicit.Length; i++)
        {
            ref var item = ref FlightPricesStructExplicit[i];
            if (FlightPriceStructExplicit.EqualsAirlineRef(ref item,"CA"))caAirline++;
            if (FlightPriceStructExplicit.EqualsStartRef(ref item,"SHA"))shaStart++;
            if (FlightPriceStructExplicit.EqualsEndRef(ref item,"PEA"))peaStart++;
            if (FlightPriceStructExplicit.EqualsFlightNoRef(ref item,"0001"))ca0001FlightNo++;
            if (FlightPriceStructExplicit.IsPriceLessRef(ref item,500))priceLess500++;
        }
        Debug.WriteLine($"{caAirline},{shaStart},{peaStart},{ca0001FlightNo},{priceLess500}");
        return caAirline + shaStart + peaStart + ca0001FlightNo + priceLess500;
    }
 
    
    private void ReleaseUnmanagedResources()
    {
        if (_unManagerPtr == IntPtr.Zero) return;
        Marshal.FreeHGlobal(_unManagerPtr);
        _unManagerPtr = IntPtr.Zero;
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~SpeedBench()
    {
        ReleaseUnmanagedResources();
    }
}