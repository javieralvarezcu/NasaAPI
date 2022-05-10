﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaApi.Moq.Utils
{
    public static class ReadSamplePaypalJson
    {
        public static string ReadFormatJson(string filename)
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var file = Path.Combine(projectFolder, @"ExampleData\", filename);
            var json = File.ReadAllText(file);
            var jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);

            return jsonFormatted;
        }
    }
}
