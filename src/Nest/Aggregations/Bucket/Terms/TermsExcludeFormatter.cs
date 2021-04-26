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
using Nest.Utf8Json;

namespace Nest
{
	internal class TermsExcludeFormatter : IJsonFormatter<TermsExclude>
	{
		public TermsExclude Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			TermsExclude termsExclude;
			switch (token)
			{
				case JsonToken.BeginArray:
					var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
					termsExclude = new TermsExclude(formatter.Deserialize(ref reader, formatterResolver));
					break;
				case JsonToken.String:
					termsExclude = new TermsExclude(reader.ReadString());
					break;
				default:
					throw new Exception($"Unexpected token {token} when deserializing {nameof(TermsInclude)}");
			}

			return termsExclude;
		}

		public void Serialize(ref JsonWriter writer, TermsExclude value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else if (value.Values != null)
			{
				var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
				formatter.Serialize(ref writer, value.Values, formatterResolver);
			}
			else
				writer.WriteString(value.Pattern);
		}
	}
}
