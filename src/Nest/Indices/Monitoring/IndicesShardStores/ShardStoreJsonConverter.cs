using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class ShardStoreJsonConverter : JsonConverter
	{
		private readonly VerbatimDictionaryKeysJsonConverter _dictionaryConverter =
		new VerbatimDictionaryKeysJsonConverter();

		private readonly PropertyJsonConverter _elasticTypeConverter = new PropertyJsonConverter();

		public override bool CanRead => true;
		public override bool CanWrite => false;
		public override bool CanConvert(Type objectType) => objectType == typeof(IDictionary<string, IFieldMapping>);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var r = new ShardStore();
			JObject o = JObject.Load(reader);
			var properties = o.Properties().ToListOrNullIfEmpty();
			var id = properties.First();
			properties.AddRange(id.Value.Value<JObject>().Properties());

			r.Id = id.Name;

			foreach (var p in properties)
			{
				switch(p.Name)
				{
					case "name":
						r.Name = p.Value.Value<string>();
						break;
					case "transport_address":
						r.TransportAddress = p.Value.Value<string>();
						break;
					case "version":
						r.Version = p.Value.Value<long>();
						break;
					case "store_exception":
						r.StoreException = p.Value.ToObject<ShardStoreException>();
						break;
					case "allocation":
						r.Allocation = p.Value.ToObject<ShardStoreAllocation>();
						break;
					case "attributes":
						r.Attributes = p.Value.ToObject<Dictionary<string, object>>();
						break;
				}
			}
			return r;
		}
	}
}
