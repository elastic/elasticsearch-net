// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Resolvers;

namespace Nest
{
	internal class MultiSearchFormatter : IJsonFormatter<IMultiSearchRequest>
	{
		private const byte Newline = (byte)'\n';

		public IMultiSearchRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<IMultiSearchRequest>().Deserialize(ref reader, formatterResolver);

		public void Serialize(ref JsonWriter writer, IMultiSearchRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Operations == null)
				return;

			var settings = formatterResolver.GetConnectionSettings();
			var memoryStreamFactory = settings.MemoryStreamFactory;
			var serializer = settings.RequestResponseSerializer;

			foreach (var operation in value.Operations.Values)
			{
				var p = operation.RequestParameters;

				string GetString(string key) => p.GetResolvedQueryStringValue(key, settings);

				IUrlParameter indices = value.Index == null || !value.Index.Equals(operation.Index)
					? operation.Index
					: null;
				var operationIndex = indices?.GetString(settings);

				var searchType = GetString("search_type");
				if (searchType == "query_then_fetch")
					searchType = null;

				var header = new
				{
					index = operationIndex != value.Index ? operationIndex : null,
					search_type = searchType,
					preference = GetString("preference"),
					routing = GetString("routing"),
					ignore_unavailable = GetString("ignore_unavailable")
				};

				writer.WriteSerialized(header, serializer, settings, SerializationFormatting.None);
				writer.WriteRaw(Newline);
				writer.WriteSerialized(operation, serializer, settings, SerializationFormatting.None);
				writer.WriteRaw(Newline);
			}
		}
	}
}
