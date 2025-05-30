﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.IO
{
    public class TransitionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Func<float, float>);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.Value as String)
            {
                case "Cubic":
                    return Function.Cubic;
                case "Linear":
                    return Function.Linear;
                default:
                    throw new Exception();
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {

            var j = value as Func<float, float>;
            var name = j.Method.Name;
            writer.WriteValue(name);
        }
    }
}
