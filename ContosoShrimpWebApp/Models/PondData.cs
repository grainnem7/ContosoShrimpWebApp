using System;
using Newtonsoft.Json;

namespace ContosoShrimpWebApp.Models
{
    public class PondData
    {
       
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }  //working

        [JsonProperty(PropertyName = "pondID")]
        public int PondID { get; set; }

        [JsonProperty(PropertyName = "start_date")] //working
        public DateTimeOffset Start_date { get; set; }

        [JsonProperty(PropertyName = "initial_population")]  //working
        public int Initial_population { get; set; }

        [JsonProperty(PropertyName = "initial_average_weight")]
        public int Initial_average_weight { get; set; } 

        [JsonProperty(PropertyName = "estimated_end_date")]
        public DateTimeOffset Estimated_end_date { get; set; }

        [JsonProperty(PropertyName = "estimated_harvest_average_weight")]
        public double Estimated_harvest_average_weight { get; set; }

        [JsonProperty(PropertyName = "estimated_survival")]
        public double Estimated_survival { get; set; }

        [JsonProperty(PropertyName = "food_type")]
        public string Food_type { get; set; } //working

    }
}