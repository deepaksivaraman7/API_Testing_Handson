﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooking
{
    internal class BookingIdResponse
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }

        [JsonProperty("booking")]
        public BookingDataResponse? Booking { get; set; }
    }
}
