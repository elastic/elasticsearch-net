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

		public MultiSearchConverter(MultiSearchDescriptor descriptor)
		{
			_descriptor = descriptor;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
		private static void CreateMultiHit<T>(MultiHitTuple tuple, JsonSerializer serializer, IDictionary<string, object> collection) where T : class
		{
			var hit = new QueryResponse<T>();
			var reader = tuple.Hit.CreateReader();
			serializer.Populate(reader, hit);

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
			foreach (var m in withMeta)
			{
				var generic = MakeDelegateMethodInfo.MakeGenericMethod(m.Descriptor.Value._ClrType);
				generic.Invoke(null, new object[] { m, serializer, response._Responses });
			}

			return response;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(MultiSearchResponse);
		}
	}
}