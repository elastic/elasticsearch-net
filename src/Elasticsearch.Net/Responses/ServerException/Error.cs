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
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net
{
	[DataContract]
	[JsonFormatter(typeof(ErrorFormatter))]
	public class Error : ErrorCause
	{
		private static readonly IReadOnlyDictionary<string, string> DefaultHeaders =
			new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(0));

		[DataMember(Name = "headers")]
		public IReadOnlyDictionary<string, string> Headers { get; set; } = DefaultHeaders;

		[DataMember(Name = "root_cause")]
		public IReadOnlyCollection<ErrorCause> RootCause { get; set; }

		public override string ToString() => CausedBy == null
			? $"Type: {Type} Reason: \"{Reason}\""
			: $"Type: {Type} Reason: \"{Reason}\" CausedBy: \"{CausedBy}\"";
	}

	internal class ErrorFormatter : ErrorCauseFormatter<Error>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "headers", 0 },
			{ "root_cause", 1 }
		};

		protected override void Serialize(ref JsonWriter writer, ref int count, Error value, IJsonFormatterResolver formatterResolver)
		{
			if (value.Headers.Any())
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("headers");
				formatterResolver.GetFormatter<IReadOnlyDictionary<string, string>>()
					.Serialize(ref writer, value.Headers, formatterResolver);

				count++;
			}

			if (value.RootCause.Any())
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName("root_cause");
				formatterResolver.GetFormatter<IReadOnlyCollection<ErrorCause>>()
					.Serialize(ref writer, value.RootCause, formatterResolver);

				count++;
			}
		}

		protected override bool Deserialize(ref JsonReader reader, ref ArraySegment<byte> property, Error value, IJsonFormatterResolver formatterResolver)
		{
			if (Fields.TryGetValue(property, out var fieldValue))
			{
				switch (fieldValue)
				{
					case 0:
						value.Headers = formatterResolver.GetFormatter<Dictionary<string, string>>()
							.Deserialize(ref reader, formatterResolver);
						break;
					case 1:
						value.RootCause = formatterResolver.GetFormatter<List<ErrorCause>>()
							.Deserialize(ref reader, formatterResolver);
						break;
				}

				return true;
			}

			return false;
		}
	}
}
