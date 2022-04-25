using NasaApi.Models.DTO;
using System;
using System.Collections.Generic;

namespace NasaApi.Moq.Utils
{
    public static class RandomDTONeosGenerator
    {
        public static List<NearEarthObjectDTO> GenerateNeosList(int number)
        {
            List<NearEarthObjectDTO> list = new List<NearEarthObjectDTO>();
            for (int i = 0; i < number; i++)
            {
                list.Add(GenerateNeo());
            }
            return list;
        }
        public static NearEarthObjectDTO GenerateNeo()
        {
            var rand = new Random();
            NearEarthObjectDTO neo = new NearEarthObjectDTO()
            {
                Id = rand.Next(100).ToString(),
                Nombre = rand.Next(100).ToString(),
                Diametro = rand.Next(1000000),
                Fecha = new DateTime().ToString("yyyy-MM-dd"),
                Velocidad = rand.Next(1000000).ToString(),
                Planeta = "Earth"
            };
            return neo;
        }
    }
}
