using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooking
{
    internal class BookingDatesResponse
    {
        [JsonProperty("checkin")]
        public string? Checkin { get; set; }

        [JsonProperty("checkout")]
        public string? Checkout { get; set; }
    }
}
