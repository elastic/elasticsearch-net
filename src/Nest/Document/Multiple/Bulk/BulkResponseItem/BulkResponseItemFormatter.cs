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
using Nest.Utf8Json;
namespace Nest
{
	internal class BulkResponseItemFormatter : IJsonFormatter<BulkResponseItemBase>
	{
		private static readonly AutomataDictionary Operations = new AutomataDictionary
		{
			{ "delete", 0 },
			{ "update", 1 },
			{ "index", 2 },
			{ "create", 3 }
		};

		public BulkResponseItemBase Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			BulkResponseItemBase bulkResponseItem = null;

			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			reader.ReadIsBeginObjectWithVerify();
			var operation = reader.ReadPropertyNameSegmentRaw();
			if (Operations.TryGetValue(operation, out var value))
			{
				switch (value)
				{
					case 0:
						bulkResponseItem = formatterResolver.GetFormatter<BulkDeleteResponseItem>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 1:
						bulkResponseItem = formatterResolver.GetFormatter<BulkUpdateResponseItem>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 2:
						bulkResponseItem = formatterResolver.GetFormatter<BulkIndexResponseItem>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 3:
						bulkResponseItem = formatterResolver.GetFormatter<BulkCreateResponseItem>()
							.Deserialize(ref reader, formatterResolver);
						break;
				}
			}
			else
				reader.ReadNextBlock();

			reader.ReadIsEndObjectWithVerify();
			return bulkResponseItem;
		}

		public void Serialize(ref JsonWriter writer, BulkResponseItemBase value, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();
	}
}
