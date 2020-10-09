using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LibraryOnlineSystem.Models
{
    public class RecommendationData
    {
        [JsonProperty("data")]
        public List<Recommendation> recommendations { get; set; }
        public int count { get; set; }

    }
}