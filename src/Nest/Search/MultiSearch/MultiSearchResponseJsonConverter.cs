using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(MultiSearchResponseJsonConverter).GetMethod(nameof(CreateSearchResponse), BindingFlags.Static | BindingFlags.NonPublic);

		private readonly IConnectionSettingsValues _settings;

		public MultiSearchResponseJsonConverter(IConnectionSettingsValues settings, IMultiSearchRequest request)
		{
			this._settings = settings;
			this._request = request;
		}

		public MultiSearchResponseJsonConverter() { }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (this._settings == null)
			{
				var realConverter = serializer.GetStatefulConverter<MultiSearchResponseJsonConverter>();
				return realConverter.ReadJson(reader, objectType, existingValue, serializer);
			}

			var response = new MultiSearchResponse();
			var jsonObject = JObject.Load(reader);

			var docsJarray = (JArray)jsonObject["responses"];
			if (docsJarray == null)
				return response;
			var multiSearchDescriptor = this._request;
			if (this._request == null)
				return multiSearchDescriptor;

			var withMeta = docsJarray.Zip(this._request.Operations, (doc, desc) => new SearchHitTuple { Hit = doc, Descriptor = desc });

			foreach (var m in withMeta)
			{
				var descriptor = m.Descriptor.Value;
				var concreteTypeSelector = descriptor.TypeSelector;
				var baseType = m.Descriptor.Value.ClrType ?? typeof(object);
				var cachedDelegate = serializer.GetConnectionSettings().Inferrer.CreateSearchResponseDelegates.GetOrAdd(baseType, t =>
				{
					// Compile a delegate from an expression
					var methodInfo = MakeDelegateMethodInfo.MakeGenericMethod(t);
					var tupleParameter = Expression.Parameter(typeof(SearchHitTuple), "tuple");
					var serializerParameter = Expression.Parameter(typeof(JsonSerializer), "serializer");
					var multiHitCollection = Expression.Parameter(typeof(IDictionary<string, object>), "collection");
					var parameterExpressions = new[] { tupleParameter, serializerParameter, multiHitCollection };
					var call = Expression.Call(null, methodInfo, parameterExpressions);
					var lambda = Expression.Lambda<Action<SearchHitTuple, JsonSerializer, IDictionary<string, object>>>(call, parameterExpressions);
					return lambda.Compile();
				});

				if (concreteTypeSelector != null)
				{
					var state = typeof(ConcreteTypeConverter<>).CreateGenericInstance(baseType, concreteTypeSelector) as JsonConverter;
					if (state != null)
					{
						var elasticSerializer = this._settings.StatefulSerializer(state) as JsonNetSerializer;
						if (elasticSerializer != null)
						{
							cachedDelegate(m, elasticSerializer.Serializer, response.Responses);
							continue;
						}
					}
				}

				cachedDelegate(m, serializer, response.Responses);
			}

			return response;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		internal class SearchHitTuple
		{
			public JToken Hit { get; set; }
			public KeyValuePair<string, ISearchRequest> Descriptor { get; set; }
		}

		private static void CreateSearchResponse<T>(
			SearchHitTuple tuple,
			JsonSerializer serializer,
			IDictionary<string, object> collection) where T : class
		{
			var response = tuple.Hit.ToObject<SearchResponse<T>>(serializer);

			ServerError error;
			if (tuple.Hit.TryParseServerError(serializer, out error))
				response.MultiSearchError = error;

			collection.Add(tuple.Descriptor.Key, response);
		}
	}
}
