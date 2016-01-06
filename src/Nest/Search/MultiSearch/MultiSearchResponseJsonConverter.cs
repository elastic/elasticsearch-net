using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class MultiSearchResponseJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanWrite => false;
		public override bool CanRead => true;

		private readonly IMultiSearchRequest _request;

		private static readonly MethodInfo MakeDelegateMethodInfo = typeof(MultiSearchResponseJsonConverter).GetMethod(nameof(CreateMultiHit), BindingFlags.Static | BindingFlags.NonPublic);
		private readonly IConnectionSettingsValues _settings;

		public MultiSearchResponseJsonConverter(IConnectionSettingsValues settings, IMultiSearchRequest request)
		{
			this._settings = settings;
			_request = request;
		}

		public MultiSearchResponseJsonConverter() { }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (this._settings == null)
			{
				var realConverter = serializer.GetStatefulConverter<MultiSearchResponseJsonConverter>();
				var mr = realConverter.ReadJson(reader, objectType, existingValue, serializer) as MultiSearchResponse;
				return mr;
			}

			var response = new MultiSearchResponse();
			var jsonObject = JObject.Load(reader);

			var docsJarray = (JArray)jsonObject["responses"];
			if (docsJarray == null)
				return response;
			var multiSearchDescriptor = this._request;
			if (this._request == null)
				return multiSearchDescriptor;

			var withMeta = docsJarray.Zip(this._request.Operations, (doc, desc) => new MultiHitTuple { Hit = doc, Descriptor = desc });
			var originalResolver = serializer.ContractResolver;
			foreach (var m in withMeta)
			{
				var descriptor = m.Descriptor.Value;
				var concreteTypeSelector = descriptor.TypeSelector;
				var baseType = m.Descriptor.Value.ClrType ?? typeof(object);
				
				var generic = MakeDelegateMethodInfo.MakeGenericMethod(baseType);

				if (concreteTypeSelector != null)
				{
					var state = typeof(ConcreteTypeConverter<>).CreateGenericInstance(baseType, concreteTypeSelector) as JsonConverter;
					if (state != null)
					{
						var elasticSerializer = new JsonNetSerializer(this._settings, state);

						generic.Invoke(null, new object[] { m, elasticSerializer.Serializer, response.Responses, this._settings });
						continue;
					}
				}
				generic.Invoke(null, new object[] { m, serializer, response.Responses, this._settings });
			}
			
			return response;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		private class MultiHitTuple
		{
			public JToken Hit { get; set; }
			public KeyValuePair<string, ISearchRequest> Descriptor { get; set; }
		}

		private static void CreateMultiHit<T>(
			MultiHitTuple tuple, 
			JsonSerializer serializer, 
			IDictionary<string, object> collection, 
			IConnectionSettingsValues settings
		)
			where T : class
		{
			var response = new SearchResponse<T>();
			var reader = tuple.Hit.CreateReader();
			serializer.Populate(reader, response);

			ServerError error;
			if (tuple.Hit.TryParseServerError(serializer, out error))
				response.MultiSearchError = error ;

			collection.Add(tuple.Descriptor.Key, response);
		}
	}
}
