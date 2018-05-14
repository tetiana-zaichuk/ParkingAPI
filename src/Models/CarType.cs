using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ParkingAPI
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CarType
    {
        Motorcycle = 1,
        Bus = 2,
        Passenger = 3,
        Truck = 4
    };
}
