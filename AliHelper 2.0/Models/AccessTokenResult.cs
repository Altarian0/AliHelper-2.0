using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliHelper_2._0.Models
{
    public partial class AccessTokenResult
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("request")]
        public object[] Request { get; set; }
    }

   

    public partial class Attributes
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
