using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class MultiSearchResponseJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanWrite => false;
		public override bool CanRead => true;

		private readonly IRequest _request;

		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(MultiSearchResponseJsonConverter).GetMethod(nameof(CreateSearchResponse), BindingFlags.Static | BindingFlags.NonPublic);
		private readonly IConnectionSettingsValues _settings;

		public MultiSearchResponseJsonConverter(IConnectionSettingsValues settings, IRequest request)
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

			IEnumerable<SearchHitTuple> withMeta;
			if (this._request is IMultiSearchRequest multiSearch)
			{
				withMeta = docsJarray.Zip(multiSearch.Operations,
					(doc, desc) => new SearchHitTuple { Hit = doc, Descriptor = new KeyValuePair<string, ICovariantSearchRequest>(desc.Key, desc.Value) });
			}
			else
			{
				if (!(this._request is IMultiSearchTemplateRequest multiSearchTemplate))
					throw new InvalidOperationException($"Request must be an instance of {nameof(IMultiSearchRequest)} or {nameof(IMultiSearchTemplateRequest)}");

				withMeta = docsJarray.Zip(multiSearchTemplate.Operations,
					(doc, desc) => new SearchHitTuple { Hit = doc, Descriptor = new KeyValuePair<string, ICovariantSearchRequest>(desc.Key, desc.Value) });
			}

			foreach (var m in withMeta)
			{
				var descriptor = m.Descriptor.Value;
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
			public KeyValuePair<string, ICovariantSearchRequest> Descriptor { get; set; }
		}

		private static void CreateSearchResponse<T>(SearchHitTuple tuple, JsonSerializer serializer, IDictionary<string, object> collection)
			where T : class
		{
			var response = tuple.Hit.ToObject<SearchResponse<T>>(serializer);

			collection.Add(tuple.Descriptor.Key, response);
		}
	}
}
