// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	internal class MultiSearchResponseFormatter : IJsonFormatter<MultiSearchResponse>
	{
		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(MultiSearchResponseFormatter).GetMethod(nameof(CreateSearchResponse), BindingFlags.Static | BindingFlags.NonPublic);

		private readonly IRequest _request;

		public MultiSearchResponseFormatter(IRequest request) => _request = request;

		public MultiSearchResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (_request == null)
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
				else if (propertyName == "took")
				{
					response.Took = reader.ReadInt64();
					continue;
				}

				reader.ReadNextBlock();
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
							Descriptor = new KeyValuePair<string, ITypedSearchRequest>(desc.Key, desc.Value)
						});
					break;
				case IMultiSearchTemplateRequest multiSearchTemplate:
					withMeta = responses.Zip(multiSearchTemplate.Operations,
						(doc, desc) => new SearchHitTuple
						{
							Hit = doc,
							Descriptor = new KeyValuePair<string, ITypedSearchRequest>(desc.Key, desc.Value)
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
					// ReSharper disable once CoVariantArrayConversion
					var call = Expression.Call(null, methodInfo, parameterExpressions);
					var lambda = Expression.Lambda<Action<SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IResponse>>>(
						call, parameterExpressions);
					return lambda.Compile();
				});

				cachedDelegate(m, formatterResolver, response.Responses);
			}

			return response;
		}

		public void Serialize(ref JsonWriter writer, MultiSearchResponse value, IJsonFormatterResolver formatterResolver) =>
			DynamicObjectResolver
				.ExcludeNullCamelCase.GetFormatter<MultiSearchResponse>()
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
			public KeyValuePair<string, ITypedSearchRequest> Descriptor { get; set; }
			public ArraySegment<byte> Hit { get; set; }
		}
	}
}
