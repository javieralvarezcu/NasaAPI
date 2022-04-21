namespace NasaApi.Models

    // TODO: cada clase en un .cs
    // TODO: no dejes comentarios

{
    public class Close_Approach_Data
    {
        public string close_approach_date { get; set; }
        public Relative_Velocity relative_velocity { get; set; }
        public string orbiting_body { get; set; }
    }

    public class Relative_Velocity
    {
        //public string kilometers_per_second { get; set; }
        public string kilometers_per_hour { get; set; }
        //public string miles_per_hour { get; set; }
    }
}
