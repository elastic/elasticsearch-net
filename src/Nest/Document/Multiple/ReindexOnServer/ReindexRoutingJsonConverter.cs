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

using Nest.Utf8Json;

namespace Nest
{
	internal class ReindexRoutingFormatter : IJsonFormatter<ReindexRouting>
	{
		public ReindexRouting Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var value = reader.ReadString();
			switch (value)
			{
				case "keep": return ReindexRouting.Keep;
				case "discard": return ReindexRouting.Discard;
				default: return new ReindexRouting(value);
			}
		}

		public void Serialize(ref JsonWriter writer, ReindexRouting value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) writer.WriteNull();
			else writer.WriteString(value.ToString());
		}
	}
}
