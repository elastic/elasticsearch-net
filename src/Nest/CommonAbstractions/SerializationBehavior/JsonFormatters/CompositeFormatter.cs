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
