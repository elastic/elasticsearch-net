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
	internal class MultiGetResponseFormatter : IJsonFormatter<MultiGetResponse>
	{
		private static readonly MethodInfo MakeDelegateMethodInfo =
			typeof(MultiGetResponseFormatter).GetMethod(nameof(CreateMultiHit), BindingFlags.Static | BindingFlags.NonPublic);

		private readonly IMultiGetRequest _request;

		public MultiGetResponseFormatter(IMultiGetRequest request) => _request = request;

		public MultiGetResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (_request == null)
				return null;

			var response = new MultiGetResponse();
			var responses = new List<ArraySegment<byte>>();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				if (propertyName == "docs")
				{
					var arrayCount = 0;
					while (reader.ReadIsInArray(ref arrayCount))
						responses.Add(reader.ReadNextBlockSegment());
					break;
				}

				// skip any other properties that are not "docs"
				reader.ReadNextBlock();
			}

			if (responses.Count == 0)
				return response;

			var withMeta = responses.Zip(_request.Documents,
				(doc, desc) => new MultiHitTuple
				{
					Hit = doc,
					Descriptor = desc
				});

			foreach (var m in withMeta)
			{
				var cachedDelegate = formatterResolver.GetConnectionSettings()
					.Inferrer.CreateMultiHitDelegates.GetOrAdd(m.Descriptor.ClrType, t =>
					{
						// Compile a delegate from an expression
						var methodInfo = MakeDelegateMethodInfo.MakeGenericMethod(t);
						var tupleParameter = Expression.Parameter(typeof(MultiHitTuple), "tuple");
						var serializerParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
						var multiHitCollection = Expression.Parameter(typeof(ICollection<IMultiGetHit<object>>), "collection");
						var parameterExpressions = new[] { tupleParameter, serializerParameter, multiHitCollection };
						// ReSharper disable once CoVariantArrayConversion
						var call = Expression.Call(null, methodInfo, parameterExpressions);
						var lambda =
							Expression.Lambda<Action<MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>>(call,
								parameterExpressions);
						return lambda.Compile();
					});

				cachedDelegate(m, formatterResolver, response.InternalHits);
			}

			return response;
		}

		public void Serialize(ref JsonWriter writer, MultiGetResponse value, IJsonFormatterResolver formatterResolver) => DynamicObjectResolver
			.ExcludeNullCamelCase.GetFormatter<MultiGetResponse>()
			.Serialize(ref writer, value, formatterResolver);

		private static void CreateMultiHit<T>(MultiHitTuple tuple, IJsonFormatterResolver formatterResolver,
			ICollection<IMultiGetHit<object>> collection
		)
			where T : class
		{
			var formatter = formatterResolver.GetFormatter<MultiGetHit<T>>();
			var reader = new JsonReader(tuple.Hit.Array, tuple.Hit.Offset);
			var hit = formatter.Deserialize(ref reader, formatterResolver);
			collection.Add(hit);
		}

		internal class MultiHitTuple
		{
			public IMultiGetOperation Descriptor { get; set; }
			public ArraySegment<byte> Hit { get; set; }
		}
	}
}
