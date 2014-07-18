using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers.Converters
{
	public class SortCollectionConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(IList<ISort>).IsAssignableFrom(objectType);
		}

		public override bool CanRead
		{
			get { return false; }
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return new InvalidOperationException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartArray();
			var sorts = value as IList<ISort>;
			foreach (var sort in sorts)
			{
				writer.WriteStartObject();
				var contract = serializer.ContractResolver as SettingsContractResolver;
				var fieldName = contract.Infer.PropertyPath(sort.SortKey);
				writer.WritePropertyName(fieldName);
				serializer.Serialize(writer, sort);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}
	}
}
