namespace NasaApi.Models
{
    public class Near_Earth_Objects
    {
        public string id { get; set; }
        public string name { get; set; }
        public Estimated_Diameter estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public Close_Approach_Data[] close_approach_data { get; set; }
    }
}
