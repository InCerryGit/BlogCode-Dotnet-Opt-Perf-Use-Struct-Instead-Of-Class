public class FlightPriceClass
{
    /// <summary>
    /// 航司二字码 如 中国国际航空股份有限公司：CA
    /// </summary>
    public string Airline { get; set; }

    /// <summary>
    /// 起始机场三字码 如 上海虹桥国际机场：SHA
    /// </summary>
    public string Start { get; set; }

    /// <summary>
    /// 抵达机场三字码 如 北京首都国际机场：PEK
    /// </summary>
    public string End { get; set; }

    /// <summary>
    /// 航班号 如 CA0001
    /// </summary>
    public string FlightNo { get; set; }

    /// <summary>
    /// 舱位代码 如 Y
    /// </summary>
    public string Cabin { get; set; }

    /// <summary>
    /// 价格 单位：元
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// 起飞日期 如 2017-01-01
    /// </summary>
    public DateOnly DepDate { get; set; }

    /// <summary>
    /// 起飞时间 如 08:00
    /// </summary>
    public TimeOnly DepTime { get; set; }

    /// <summary>
    /// 抵达日期 如 2017-01-01
    /// </summary>
    public DateOnly ArrDate { get; set; }

    /// <summary>
    /// 抵达时间 如 08:00
    /// </summary>
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
        return Price < min;
    }
    
}