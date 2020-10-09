using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LibraryOnlineSystem.Models
{
    public class Recommendation
    {

        [JsonProperty("bookBase")]
        public int bookBase { get; set; }

        [JsonProperty("bookRecommended")]
        public int bookRecommended{ get; set; }

        [JsonProperty("creation_Date")]
        public DateTime creation_Date { get; set; }

        [JsonProperty("confidence")]
        public float confidence { get; set; }

        [JsonProperty("support")]

        public float support { get; set; }

        [JsonProperty("lift")]
        public float lift { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

    }
}