using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHelper_2._0.Models
{
    public partial class ShortLink
    {
        [JsonProperty("data")]
        public Data2 Data2 { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("request")]
        public Request Request { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("result")]
        public Uri Result { get; set; }
    }

    public class Data2
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public Attributes[] Attributes { get; set; }
    }

    public partial class Request
    {
        [JsonProperty("urlContainer")]
        public UrlContainer[] UrlContainer { get; set; }
    }

    public partial class UrlContainer
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("domainCutter")]
        public string DomainCutter { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
