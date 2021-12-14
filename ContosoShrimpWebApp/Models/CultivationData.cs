using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoShrimpWebApp.Models
{
    public class CultivationData
    {   [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }  //working

        [JsonProperty(PropertyName = "cultivationID")]
        public int CultivationID { get; set; }

        [JsonProperty(PropertyName = "estimated_Length")] //working
        public int Estimated_Length { get; set; }

        [JsonProperty(PropertyName = "Genetic_Origin")]  //working
        public string Genetic_Origin { get; set; }

        [JsonProperty(PropertyName = "Number_of_seeds")]
        public int Number_of_seeds { get; set; }

        [JsonProperty(PropertyName = "estimated_survival")]
        public int Estimated_survival { get; set; }

        [JsonProperty(PropertyName = "est_kg_produced")]
        public int Est_kg_produced { get; set; } //working
    }
}
