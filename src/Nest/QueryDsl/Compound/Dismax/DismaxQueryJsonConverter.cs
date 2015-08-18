using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	internal class DismaxQueryJsonConverter : ReadAsTypeJsonConverter<DisMaxQueryDescriptor<object>>
	{
		public override bool CanConvert(Type objectType) => true;

		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			return base.ReadJson(reader, objectType, existingValue, serializer);
		}

		public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			var d = value as IDisMaxQuery;
			if (d == null) return;

			writer.WriteStartObject();
			{
				if (!d.Name.IsNullOrEmpty()) {
					writer.WritePropertyName("_name");
					writer.WriteValue(d.Name);
				}
				if (d.TieBreaker.HasValue)
				{
					writer.WritePropertyName("tie_breaker");
					serializer.Serialize(writer, d.TieBreaker);
				}
				if (d.Boost.HasValue)
				{
					writer.WritePropertyName("boost");
					writer.WriteValue(d.Boost.Value);
				}
				if (d.Queries.HasAny() && d.Queries.Any(q => !q.IsConditionless))
				{
					writer.WritePropertyName("queries");
					writer.WriteStartArray();
					{
						foreach (var q in d.Queries)
						{
							if (!q.IsConditionless)
							{
								serializer.Serialize(writer, q);
							}
						}
					}
					writer.WriteEndArray();
				}
			}
			writer.WriteEndObject();
		}
	}
}
