using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.DSL;

namespace Nest.Resolvers.Converters
{
	public class TermConverter : JsonConverter
	{
		public static readonly Type[] _types = new Type[] { typeof(Term), typeof(Wildcard) };
		public override bool CanConvert(Type objectType)
		{
			return _types.Contains(objectType);
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var term = value as Term;
			if (term != null)
			{
				writer.WriteStartObject();
				writer.WritePropertyName(term.Field);
				writer.WriteStartObject();
	
					writer.WritePropertyName("value");
					writer.WriteValue(term.Value);

				if (term.Boost.HasValue)
				{
					writer.WritePropertyName("boost");
					writer.WriteValue(term.Boost.Value);
				}
				writer.WriteEndObject();
				writer.WriteEndObject();
			}
			else
				writer.WriteNull();
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

	}
}
