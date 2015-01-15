using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Converter for converting Uri to String and vica versa
	/// </summary>
	/// <remarks>
	/// Code originated from http://stackoverflow.com/a/8087049/106909
	/// </remarks>
	[Obsolete("Scheduled to be removed in 2.0")]
	public sealed class UriJsonConverter : JsonConverter
	{
		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType"></param>
		/// <returns></returns>
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Uri);
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="objectType"></param>
		/// <param name="existingValue"></param>
		/// <param name="serializer"></param>
		/// <returns></returns>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
				return new Uri((string)reader.Value);

			if (reader.TokenType == JsonToken.Null)
				return null;

			throw new InvalidOperationException("Unhandled case for UriConverter. Check to see if this converter has been applied to the wrong serialization type.");
		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="value"></param>
		/// <param name="serializer"></param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (null == value)
			{
				writer.WriteNull();
				return;
			}

			var uriValue = value as Uri;
			if (uriValue != null)
			{
				writer.WriteValue(uriValue.OriginalString);
				return;
			}

			throw new InvalidOperationException("Unhandled case for UriConverter. Check to see if this converter has been applied to the wrong serialization type.");
		}
	}
}
