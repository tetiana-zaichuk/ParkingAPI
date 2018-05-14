using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
