using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class MultiSearchResponseFormatter : IJsonFormatter<IMultiSearchResponse>
	{
		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(MultiSearchResponseFormatter).GetMethod(nameof(CreateSearchResponse), BindingFlags.Static | BindingFlags.NonPublic);

		private readonly IRequest _request;
		private readonly IConnectionSettingsValues _settings;

		public MultiSearchResponseFormatter(IConnectionSettingsValues settings, IRequest request)
		{
			_settings = settings;
			_request = request;
		}

		public IMultiSearchResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (_settings == null || _request == null)
				return null;

			var response = new MultiSearchResponse();
			var responses = new List<ArraySegment<byte>>();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				if (propertyName == "responses")
				{
					var arrayCount = 0;
					while (reader.ReadIsInArray(ref arrayCount))
						responses.Add(reader.ReadNextBlockSegment());
					break;
				}
			}

			if (responses.Count == 0)
				return response;

			IEnumerable<SearchHitTuple> withMeta;
			switch (_request)
			{
				case IMultiSearchRequest multiSearch:
					withMeta = responses.Zip(multiSearch.Operations,
						(doc, desc) => new SearchHitTuple
						{
							Hit = doc,
							Descriptor = new KeyValuePair<string, ICovariantSearchRequest>(desc.Key, desc.Value)
						});
					break;
				case IMultiSearchTemplateRequest multiSearchTemplate:
					withMeta = responses.Zip(multiSearchTemplate.Operations,
						(doc, desc) => new SearchHitTuple
						{
							Hit = doc,
							Descriptor = new KeyValuePair<string, ICovariantSearchRequest>(desc.Key, desc.Value)
						});
					break;
				default:
					throw new InvalidOperationException($"Request must be an instance of {nameof(IMultiSearchRequest)}"
						+ $" or {nameof(IMultiSearchTemplateRequest)}");
			}

			var settings = formatterResolver.GetConnectionSettings();

			foreach (var m in withMeta)
			{
				var baseType = m.Descriptor.Value.ClrType ?? typeof(object);
				var cachedDelegate = settings.Inferrer.CreateSearchResponseDelegates.GetOrAdd(baseType, t =>
				{
					// TODO: Look to move this out of here.
					// Compile a delegate from an expression
					var methodInfo = MakeDelegateMethodInfo.MakeGenericMethod(t);
					var tupleParameter = Expression.Parameter(typeof(SearchHitTuple), "tuple");
					var serializerParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
					var multiHitCollection = Expression.Parameter(typeof(IDictionary<string, IResponse>), "collection");
					var parameterExpressions = new[] { tupleParameter, serializerParameter, multiHitCollection };
					var call = Expression.Call(null, methodInfo, parameterExpressions);
					var lambda = Expression.Lambda<Action<SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IResponse>>>(
						call, parameterExpressions);
					return lambda.Compile();
				});

				cachedDelegate(m, formatterResolver, response.Responses);
			}

			return response;
		}

		public void Serialize(ref JsonWriter writer, IMultiSearchResponse value, IJsonFormatterResolver formatterResolver) => DynamicObjectResolver
			.ExcludeNullCamelCase.GetFormatter<IMultiSearchResponse>()
			.Serialize(ref writer, value, formatterResolver);

		private static void CreateSearchResponse<T>(SearchHitTuple tuple, IJsonFormatterResolver formatterResolver,
			IDictionary<string, IResponse> collection
		)
			where T : class
		{
			var formatter = formatterResolver.GetFormatter<SearchResponse<T>>();
			var reader = new JsonReader(tuple.Hit.Array, tuple.Hit.Offset);
			var response = formatter.Deserialize(ref reader, formatterResolver);
			collection.Add(tuple.Descriptor.Key, response);
		}

		internal class SearchHitTuple
		{
			public KeyValuePair<string, ICovariantSearchRequest> Descriptor { get; set; }
			public ArraySegment<byte> Hit { get; set; }
		}
	}
}
