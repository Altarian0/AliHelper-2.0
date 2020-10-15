using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHelper_2._0.Models
{
    public partial class Creative
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("request")]
        public Request Request { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("code")]
        public Uri Code { get; set; }
    }

    public partial class Request
    {
        [JsonProperty("offerId")]
        public long OfferId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }

        [JsonProperty("offerType")]
        public string OfferType { get; set; }
    }
}
