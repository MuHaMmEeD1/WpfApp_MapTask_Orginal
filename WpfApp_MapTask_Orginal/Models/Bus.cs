using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp_MapTask_Orginal.Models
{
    public class Bus
    {
        [JsonPropertyName("@attributes")]
        public Atribut attributes { get; set; }


    }
}
