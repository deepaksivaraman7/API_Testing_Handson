﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExample
{
    internal class UserDataResponse
    {
        [JsonProperty("data")]
        public UserData? Data { get; set; }
    }
}
