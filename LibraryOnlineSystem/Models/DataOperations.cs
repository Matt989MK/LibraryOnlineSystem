using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace LibraryOnlineSystem.Models
{
    public class DataOperations
    {
        public RecommendationData GetJsonData()
        {
             HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/api/Recommendation");
             HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
             StreamReader reader = new StreamReader(response.GetResponseStream());
             string _recommenderData = reader.ReadToEnd();
             RecommendationData recommendation = JsonConvert.DeserializeObject<RecommendationData>(_recommenderData);

             var bookBaseId = recommendation.recommendations[0].bookBase;
             var test = recommendation.recommendations[0].bookBase;

             recommendation.count = recommendation.recommendations.Count;

            return recommendation;
        }



}
}