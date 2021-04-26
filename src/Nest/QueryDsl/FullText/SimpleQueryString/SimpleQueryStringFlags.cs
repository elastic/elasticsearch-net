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
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[Flags]
	public enum SimpleQueryStringFlags
	{
		[EnumMember(Value = "NONE")]
		None = 1 << 0,

		[EnumMember(Value = "AND")]
		And = 1 << 1,

		[EnumMember(Value = "OR")]
		Or = 1 << 2,

		[EnumMember(Value = "NOT")]
		Not = 1 << 3,

		[EnumMember(Value = "PREFIX")]
		Prefix = 1 << 4,

		[EnumMember(Value = "PHRASE")]
		Phrase = 1 << 5,

		[EnumMember(Value = "PRECEDENCE")]
		Precedence = 1 << 6,

		[EnumMember(Value = "ESCAPE")]
		Escape = 1 << 7,

		[EnumMember(Value = "WHITESPACE")]
		Whitespace = 1 << 8,

		[EnumMember(Value = "FUZZY")]
		Fuzzy = 1 << 9,

		[EnumMember(Value = "NEAR")]
		Near = 1 << 10,

		[EnumMember(Value = "SLOP")]
		Slop = 1 << 11,

		[EnumMember(Value = "ALL")]
		All = 1 << 12,
	}

	internal class SimpleQueryStringFlagsFormatter : IJsonFormatter<SimpleQueryStringFlags?>
	{
		public void Serialize(ref JsonWriter writer, SimpleQueryStringFlags? value, IJsonFormatterResolver formatterResolver)
		{
			if (!value.HasValue)
			{
				writer.WriteNull();
				return;
			}

			var e = value.Value;
			var list = new List<string>(13);
			if (e.HasFlag(SimpleQueryStringFlags.All)) list.Add("ALL");
			if (e.HasFlag(SimpleQueryStringFlags.None)) list.Add("NONE");
			if (e.HasFlag(SimpleQueryStringFlags.And)) list.Add("AND");
			if (e.HasFlag(SimpleQueryStringFlags.Or)) list.Add("OR");
			if (e.HasFlag(SimpleQueryStringFlags.Not)) list.Add("NOT");
			if (e.HasFlag(SimpleQueryStringFlags.Prefix)) list.Add("PREFIX");
			if (e.HasFlag(SimpleQueryStringFlags.Phrase)) list.Add("PHRASE");
			if (e.HasFlag(SimpleQueryStringFlags.Precedence)) list.Add("PRECEDENCE");
			if (e.HasFlag(SimpleQueryStringFlags.Escape)) list.Add("ESCAPE");
			if (e.HasFlag(SimpleQueryStringFlags.Whitespace)) list.Add("WHITESPACE");
			if (e.HasFlag(SimpleQueryStringFlags.Fuzzy)) list.Add("FUZZY");
			if (e.HasFlag(SimpleQueryStringFlags.Near)) list.Add("NEAR");
			if (e.HasFlag(SimpleQueryStringFlags.Slop)) list.Add("SLOP");
			var flags = string.Join("|", list);
			writer.WriteString(flags);
		}

		public SimpleQueryStringFlags? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var flags = reader.ReadString();
			return flags?.Split('|')
				.Select(flag => flag.ToEnum<SimpleQueryStringFlags>())
				.Where(s => s.HasValue)
				.Aggregate(default(SimpleQueryStringFlags), (current, s) => current | s.Value);
		}
	}
}
