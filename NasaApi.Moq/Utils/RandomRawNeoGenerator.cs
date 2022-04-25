using NasaApi.Models.Raw;
using NasaApi.Models.Raw.Diameter;
using System;

namespace NasaApi.Moq.Utils
{
    public class RandomRawNeoGenerator
    {
        public Near_Earth_Objects near_earth_objects;
        public bool Hazardous { get; set; }
        public DateTime NeoDate { get; set; }

        public Near_Earth_Objects Observation
        {
            get
            {
                var rand = new Random();

                near_earth_objects = new Near_Earth_Objects()
                {
                    id = rand.Next(100).ToString(),
                    name = "test_name_" + rand.Next(100),
                    estimated_diameter = new Estimated_Diameter()
                    {
                        meters = new Meters()
                        {
                            estimated_diameter_max = rand.Next(100),
                            estimated_diameter_min = rand.Next(100)
                        }
                    },
                    close_approach_data = new Close_Approach_Data[]
                    {
                        new Close_Approach_Data()
                        {
                            close_approach_date = NeoDate.ToString("yyyy-MM-dd"),
                            orbiting_body = "Earth",
                            relative_velocity = new Relative_Velocity()
                            {
                                kilometers_per_hour=rand.Next(100000).ToString()
                            }

                        }
                    },
                    is_potentially_hazardous_asteroid = Hazardous
                };
                return near_earth_objects;
            }
        }
    }
}
