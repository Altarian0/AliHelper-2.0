using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHelper_2._0.Models.RequestModel
{
   // public class LinkRed
    //{

    public partial class LinkReduction
    {
        [JsonProperty("urlContainer")]
        public UrlContainer[] UrlContainer { get; set; }

        [JsonProperty("domainCutter")]
        public string DomainCutter { get; set; }

                
    }

    public partial class UrlContainer
    {
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url { get; set; }

        [JsonProperty("domainCutter", NullValueHandling = NullValueHandling.Ignore)]
        public string DomainCutter { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        
    }
    //}
}
