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

using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	[JsonFormatter(typeof(SqlValueFormatter))]
	public class SqlValue : LazyDocument
	{
		internal SqlValue(byte[] bytes, IJsonFormatterResolver formatterResolver)
			: base(bytes, formatterResolver) { }
	}

	internal class SqlValueFormatter : IJsonFormatter<SqlValue>
	{
		public SqlValue Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			var arraySegment = reader.ReadNextBlockSegment();
			return new SqlValue(BinaryUtil.ToArray(ref arraySegment), formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, SqlValue value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			for (var i = 0; i < value.Bytes.Length; i++)
				writer.WriteByte(value.Bytes[i]);
		}
	}
}
