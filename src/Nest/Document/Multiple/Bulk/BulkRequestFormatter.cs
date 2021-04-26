/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
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
