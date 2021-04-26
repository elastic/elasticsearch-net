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
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Formatters;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	internal class FieldRuleBaseFormatter : IJsonFormatter<FieldRuleBase>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "username", 0 },
			{ "dn", 1 },
			{ "realm.name", 2 },
			{ "groups", 3 }
		};

		private static readonly VerbatimDictionaryInterfaceKeysFormatter<string, object> Formatter =
			new VerbatimDictionaryInterfaceKeysFormatter<string, object>();

		public FieldRuleBase Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			FieldRuleBase fieldRule = null;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							var username = reader.ReadString();
							fieldRule = new UsernameRule(username);
							break;
						case 1:
							if (reader.GetCurrentJsonToken() == JsonToken.BeginArray)
							{
								var fm = formatterResolver.GetFormatter<IEnumerable<string>>();
								var dns = fm.Deserialize(ref reader, formatterResolver);
								fieldRule = new DistinguishedNameRule(dns);
								break;
							}
							var dn = reader.ReadString();
							fieldRule = new DistinguishedNameRule(dn);
							break;
						case 2:
							var realm = reader.ReadString();
							fieldRule = new RealmRule(realm);
							break;
						case 3:
							var formatter = formatterResolver.GetFormatter<IEnumerable<string>>();
							var groups = formatter.Deserialize(ref reader, formatterResolver);
							fieldRule = new GroupsRule(groups);
							break;
					}
				}
				else
				{
					var name = propertyName.Utf8String();
					if (name.StartsWith("metadata."))
					{
						name = name.Replace("metadata.", string.Empty);
						var metadata = formatterResolver.GetFormatter<object>()
							.Deserialize(ref reader, formatterResolver);
						fieldRule = new MetadataRule(name, metadata);
					}
				}
			}

			return fieldRule;
		}

		public void Serialize(ref JsonWriter writer, FieldRuleBase value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			Formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
