using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHelper_2._0.Models
{
    public class SSID
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("request")]
        public object[] Request { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

    
    }

    

    public partial class Attributes
    {
        [JsonProperty("ssid_token")]
        public string SsidToken { get; set; }
    }

}
