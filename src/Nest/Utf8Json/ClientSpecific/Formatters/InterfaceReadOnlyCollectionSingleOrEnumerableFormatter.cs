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
using System.Collections.ObjectModel;

namespace Nest.Utf8Json
{
	internal class InterfaceReadOnlyCollectionSingleOrEnumerableFormatter<T> : IJsonFormatter<IReadOnlyCollection<T>>
	{
		public IReadOnlyCollection<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			return token == JsonToken.BeginArray
				? formatterResolver.GetFormatter<IReadOnlyCollection<T>>().Deserialize(ref reader, formatterResolver)
				: new ReadOnlyCollection<T>(new List<T>(1) { formatterResolver.GetFormatter<T>().Deserialize(ref reader, formatterResolver) });
		}

		public void Serialize(ref JsonWriter writer, IReadOnlyCollection<T> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var formatter = formatterResolver.GetFormatter<IReadOnlyCollection<T>>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
