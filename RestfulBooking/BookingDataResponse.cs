using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooking
{
    internal class BookingDataResponse
    {
        [JsonProperty("firstname")]
        public string? FirstName { get; set; }

        [JsonProperty("lastname")]
        public string? LastName { get; set; }

        [JsonProperty("totalprice")]
        public string? TotalPrice { get; set; }

        [JsonProperty("depositpaid")]
        public string? DepositPaid { get; set; }

        [JsonProperty("bookingdates")]
        public BookingDatesResponse? BookingDates { get; set; }

        [JsonProperty("additionalneeds")]
        public string? AdditionalNeeds { get; set; }
    }
}
