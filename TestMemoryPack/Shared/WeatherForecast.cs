using MemoryPack;

namespace TestMemoryPack.Shared
{

    [MemoryPackable(GenerateType.VersionTolerant)]
    public partial class WeatherForecast
    {
        [MemoryPackOrder(0)]
        public DateOnly Date { get; set; }
        [MemoryPackOrder(1)]
        public int TemperatureC { get; set; }
        [MemoryPackOrder(2)]
        public string? Summary { get; set; }
        [MemoryPackOrder(3)]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}