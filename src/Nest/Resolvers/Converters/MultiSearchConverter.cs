using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Nest.Resolvers.Converters
{

	public class MultiSearchConverter : JsonConverter
	{
		private class MultiHitTuple
		{
			public JToken Hit { get; set; }
			public KeyValuePair<string, SearchDescriptorBase> Descriptor { get; set; }
		}

		private readonly MultiSearchDescriptor _descriptor;

		private static MethodInfo MakeDelegateMethodInfo = typeof(MultiSearchConverter).GetMethod("CreateMultiHit", BindingFlags.Static | BindingFlags.NonPublic);
		private readonly IConnectionSettings _settings;

		public MultiSearchConverter(IConnectionSettings settings, MultiSearchDescriptor descriptor)
		{
			this._settings = settings;
			_descriptor = descriptor;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
		private static void CreateMultiHit<T>(
			MultiHitTuple tuple, 
			JsonSerializer serializer, 
			IDictionary<string, object> collection, 
			IConnectionSettings settings
		)
			where T : class
		{
			var hit = new QueryResponse<T>();
			var reader = tuple.Hit.CreateReader();
			serializer.Populate(reader, hit);

			var errorProperty = tuple.Hit.Children<JProperty>().FirstOrDefault(c=>c.Name == "error");
			if (errorProperty != null)
			{
				hit.IsValid = false;
				hit.ConnectionStatus = new ConnectionStatus(settings, new ConnectionException(
					msg: errorProperty.Value.ToString(),
					response: errorProperty.Value.ToString()
				));
			}

			collection.Add(tuple.Descriptor.Key, hit);

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var response = new MultiSearchResponse();
			var jsonObject = JObject.Load(reader);

			var docsJarray = (JArray)jsonObject["responses"];
			if (docsJarray == null)
				return response;
			var multiSearchDescriptor = this._descriptor;
			if (this._descriptor == null)
				return multiSearchDescriptor;

			var withMeta = docsJarray.Zip(this._descriptor._Operations, (doc, desc) => new MultiHitTuple { Hit = doc, Descriptor = desc });
			var originalConverters = serializer.Converters.ToList();
			var originalResolver = serializer.ContractResolver;
			foreach (var m in withMeta)
			{
				//bool newConverter = false;
				//if (m.Descriptor.Value._Types != null && m.Descriptor.Value._Types.Count() > 0 && m.Descriptor.Value._Types.Count() > m.Descriptor.Value._Types.Where(x => x.Type == m.Descriptor.Value._ClrType).Count())
				//{
				//	var typeDict = m.Descriptor.Value._Types.ToDictionary(t => t.Resolve(this._settings), t => t.Type);
				//	Func<dynamic, Hit<dynamic>, Type> typeSelector = (o, h) =>
				//	{
				//		Type t;
				//		if (!typeDict.TryGetValue(h.Type, out t))
				//			return m.Descriptor.Value._ClrType;
				//		return t;
				//	};
				//	serializer.Converters.Clear();
				//	foreach (var orig in originalConverters)
				//	{
				//		if (!(orig is MultiSearchConverter))
				//			serializer.Converters.Add(orig);
				//	}
				//	var converter = new ConcreteTypeConverter(m.Descriptor.Value._ClrType, typeSelector);
				//	serializer.Converters.Add(converter);
				//	serializer.ContractResolver = new ElasticContractResolver(this._settings);
				//	(serializer.ContractResolver as ElasticContractResolver).ConcreteTypeConverter = converter;
				//	newConverter = true;
				//}

				//var generic = MakeDelegateMethodInfo.MakeGenericMethod(m.Descriptor.Value._ClrType);
				//generic.Invoke(null, new object[] { m, serializer, response._Responses, this._settings });

				//if (newConverter)
				//{
				//	serializer.Converters.Clear();
				//	serializer.ContractResolver = originalResolver;
				//	foreach (var converter in originalConverters)
				//		serializer.Converters.Add(converter);
				//}
			}

			return response;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(MultiSearchResponse);
		}
	}
}
