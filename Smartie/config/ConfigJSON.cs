﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartie
{
    internal struct ConfigJSON
    {
        [JsonProperty("token")]
        public string token { get; private set; }

        [JsonProperty("prefix")]
        public string prefix { get; private set; }
    }
}