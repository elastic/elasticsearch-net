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

namespace Nest
{
	[JsonFormatter(typeof(ContextFormatter))]
	public class Context : Union<string, GeoLocation>
	{
		public Context(string category) : base(category) { }

		public Context(GeoLocation geo) : base(geo) { }

		public string Category => Item1;
		public GeoLocation Geo => Item2;

		public static implicit operator Context(string context) => new Context(context);

		public static implicit operator Context(GeoLocation context) => new Context(context);
	}

	internal class ContextFormatter : IJsonFormatter<Context>
	{
		public Context Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Union<string, GeoLocation>>();
			var union = formatter.Deserialize(ref reader, formatterResolver);
			switch (union.Tag)
			{
				case 0:
					return new Context(union.Item1);
				case 1:
					return new Context(union.Item2);
				default:
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Context value, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<Union<string, GeoLocation>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
