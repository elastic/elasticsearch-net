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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(StopWordsFormatter))]
	public class StopWords : Union<string, IEnumerable<string>>
	{
		public StopWords(string item) : base(item) { }

		public StopWords(IEnumerable<string> item) : base(item) { }

		public static implicit operator StopWords(string first) => new StopWords(first);

		public static implicit operator StopWords(List<string> second) => new StopWords(second);

		public static implicit operator StopWords(string[] second) => new StopWords(second);
	}

	internal class StopWordsFormatter : IJsonFormatter<StopWords>
	{
		public StopWords Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.BeginArray)
			{
				var stopwords = formatterResolver.GetFormatter<IEnumerable<string>>()
					.Deserialize(ref reader, formatterResolver);
				return new StopWords(stopwords);
			}

			var stopword = reader.ReadString();
			return new StopWords(stopword);
		}

		public void Serialize(ref JsonWriter writer, StopWords value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Tag)
			{
				case 0:
					writer.WriteString(value.Item1);
					break;
				case 1:
					var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
					formatter.Serialize(ref writer, value.Item2, formatterResolver);
					break;
			}
		}
	}
}
