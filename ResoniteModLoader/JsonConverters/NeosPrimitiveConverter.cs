using Elements.Core;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace ResoniteModLoader.JsonConverters
{
    internal class ResonitePrimitiveConverter : JsonConverter
    {
        private static readonly Assembly BASEX = typeof(colorX).Assembly;

        public override bool CanConvert(Type objectType)
        {
            // handle all non-enum Resonite Primitives in the BaseX assembly
            return !objectType.IsEnum && BASEX.Equals(objectType.Assembly) && Coder.IsEnginePrimitive(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string serialized)
            {
                // use Resonite's built-in decoding if the value was serialized as a string
                return typeof(Coder<>).MakeGenericType(objectType).GetMethod("DecodeFromString").Invoke(null, new object[] { serialized });
            }

            throw new ArgumentException($"Could not deserialize a BaseX type: {objectType} from a {reader?.Value?.GetType()}");
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            string serialized = (string)typeof(Coder<>).MakeGenericType(value!.GetType()).GetMethod("EncodeToString").Invoke(null, new object[] { value });
            writer.WriteValue(serialized);
        }
    }
}