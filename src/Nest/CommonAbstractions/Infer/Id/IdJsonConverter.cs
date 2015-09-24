using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest.Resolvers;

namespace Nest
{
	internal class IdJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(Id) == objectType;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return new Id(reader.Value as string);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var id = value as Id;
			if (id == null)
			{
				writer.WriteNull();
				return;
			}
			if (id.Document != null)
			{
				var contract = serializer.ContractResolver as SettingsContractResolver;
				if (contract != null && contract.ConnectionSettings != null)
				{
					var indexName = contract.Infer.Id(id.Document.GetType(), id.Document);
					writer.WriteValue(indexName);
				}
				else throw new Exception("If you use a custom contract resolver be sure to subclass from ElasticResolver");
            }
			else writer.WriteValue(id.Value);
		}
	}
}
