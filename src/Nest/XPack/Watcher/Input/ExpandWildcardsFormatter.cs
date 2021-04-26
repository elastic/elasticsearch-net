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

using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	internal class ExpandWildcardsFormatter : IJsonFormatter<IEnumerable<ExpandWildcards>>
	{
		public IEnumerable<ExpandWildcards> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			return token == JsonToken.BeginArray
				? formatterResolver.GetFormatter<IEnumerable<ExpandWildcards>>().Deserialize(ref reader, formatterResolver)
				: new[] { formatterResolver.GetFormatter<ExpandWildcards>().Deserialize(ref reader, formatterResolver) };
		}

		public void Serialize(ref JsonWriter writer, IEnumerable<ExpandWildcards> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var wildcards = value.ToArray();

			switch (wildcards.Length)
			{
				case 1:
					var singleFormatter = formatterResolver.GetFormatter<ExpandWildcards>();
					singleFormatter.Serialize(ref writer, wildcards.First(), formatterResolver);
					break;
				case > 1:
					var formatter = formatterResolver.GetFormatter<IEnumerable<ExpandWildcards>>();
					formatter.Serialize(ref writer, wildcards, formatterResolver);
					break;
			}
		}
	}
}
