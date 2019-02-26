using System;

namespace Utf8Json
{
    public delegate void JsonSerializeAction<T>(ref JsonWriter writer, T value, IJsonFormatterResolver resolver);
    public delegate T JsonDeserializeFunc<T>(ref JsonReader reader, IJsonFormatterResolver resolver);

    public interface IJsonFormatter
    {
    }

    public interface IJsonFormatter<T> : IJsonFormatter
    {
        void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }

    public interface IObjectPropertyNameFormatter<T>
    {
        void SerializeToPropertyName(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        T DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }

    public static class JsonFormatterExtensions
    {
        public static string ToJsonString<T>(this IJsonFormatter<T> formatter, T value, IJsonFormatterResolver formatterResolver)
        {
            var writer = new JsonWriter();
            formatter.Serialize(ref writer, value, formatterResolver);
            return writer.ToString();
        }
    }
}