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
using System.Collections.Generic;
using System.Linq;
using Nest.Utf8Json;

namespace Nest
{
	internal class MultiGetRequestFormatter : IJsonFormatter<IMultiGetRequest>
	{
		private static readonly IdFormatter IdFormatter = new IdFormatter();

		public IMultiGetRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, IMultiGetRequest value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginObject();
			if (!(value?.Documents.HasAny()).GetValueOrDefault(false))
			{
				writer.WriteEndObject();
				return;
			}

			List<IMultiGetOperation> docs;

			// if an index is specified at the request level and all documents have the same index, remove the index
			if (value.Index != null)
			{
				var settings = formatterResolver.GetConnectionSettings();
				var resolvedIndex = value.Index.GetString(settings);
				docs = value.Documents.Select(d =>
					{
						if (d.Index == null)
							return d;

						// TODO: not nice to resolve index for each doc here for comparison, only for it to be resolved later in serialization.
						// Might be better to simply remove the flattening logic.
						var docIndex = d.Index.GetString(settings);
						if (string.Equals(resolvedIndex, docIndex)) d.Index = null;
						return d;
					})
					.ToList();
			}
			else
				docs = value.Documents.ToList();

			var flatten = docs.All(p => p.CanBeFlattened);

			writer.WritePropertyName(flatten ? "ids" : "docs");

			IJsonFormatter<IMultiGetOperation> formatter = null;
			if (!flatten)
				formatter = formatterResolver.GetFormatter<IMultiGetOperation>();

			writer.WriteBeginArray();
			for (var index = 0; index < docs.Count; index++)
			{
				if (index > 0)
					writer.WriteValueSeparator();

				var doc = docs[index];
				if (flatten)
					IdFormatter.Serialize(ref writer, doc.Id, formatterResolver);
				else
					formatter.Serialize(ref writer, doc, formatterResolver);
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}
	}
}
