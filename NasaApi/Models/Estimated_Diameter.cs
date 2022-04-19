namespace NasaApi.Models
{
    public class Estimated_Diameter
    {
        //public Kilometers kilometers { get; set; }
        public Meters meters { get; set; }
    }

    //public class Kilometers
    //{
    //    public float estimated_diameter_min { get; set; }
    //    public float estimated_diameter_max { get; set; }
    //}

    public class Meters
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }
}
