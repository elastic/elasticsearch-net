// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class CompositeFormatter<T, TRead, TWrite> : IJsonFormatter<T>
		where TRead : IJsonFormatter<T>, new()
		where TWrite : IJsonFormatter<T>, new()
	{
		public CompositeFormatter()
		{
			Read = new TRead();
			Write = new TWrite();
		}

		private TRead Read { get; set; }
		private TWrite Write { get; set; }

		public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			Read.Deserialize(ref reader, formatterResolver);

		public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver) =>
			Write.Serialize(ref writer, value, formatterResolver);
	}
}
