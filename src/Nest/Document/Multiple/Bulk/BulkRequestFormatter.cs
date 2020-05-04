// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class BulkRequestFormatter : IJsonFormatter<IBulkRequest>
	{
		private const byte Newline = (byte)'\n';

		private static SourceWriteFormatter<object> SourceWriter { get; } = new SourceWriteFormatter<object>();

		public IBulkRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, IBulkRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Operations == null)
				return;

			var settings = formatterResolver.GetConnectionSettings();
			var inferrer = settings.Inferrer;
			var formatter = formatterResolver.GetFormatter<object>();

			for (var index = 0; index < value.Operations.Count; index++)
			{
				var op = value.Operations[index];
				op.Index ??= value.Index ?? op.ClrType;
				if (op.Index.Equals(value.Index)) op.Index = null;
				op.Id = op.GetIdForOperation(inferrer);
				op.Routing = op.GetRoutingForOperation(inferrer);

				writer.WriteBeginObject();
				writer.WritePropertyName(op.Operation);

				formatter.Serialize(ref writer, op, formatterResolver);
				writer.WriteEndObject();
				writer.WriteRaw(Newline);

				var body = op.GetBody();
				if (body == null)
					continue;

				if (op.Operation == "update" || body is ILazyDocument)
				{
					var requestResponseSerializer = settings.RequestResponseSerializer;
					requestResponseSerializer.SerializeUsingWriter(ref writer, body, settings, SerializationFormatting.None);
				}
				else
					SourceWriter.Serialize(ref writer, body, formatterResolver);
				writer.WriteRaw(Newline);
			}
		}
	}
}
