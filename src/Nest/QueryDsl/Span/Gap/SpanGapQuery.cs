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
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(SpanGapQueryFormatter))]
	public interface ISpanGapQuery : ISpanSubQuery
	{
		Field Field { get; set; }

		int? Width { get; set; }
	}

	public class SpanGapQuery : QueryBase, ISpanGapQuery
	{
		public Field Field { get; set; }
		public int? Width { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal static bool IsConditionless(ISpanGapQuery q) => q?.Width == null || q.Field.IsConditionless();

		internal override void InternalWrapInContainer(IQueryContainer c) =>
			throw new Exception("span_gap may only appear as a span near clause");
	}

	[DataContract]
	public class SpanGapQueryDescriptor<T> : QueryDescriptorBase<SpanGapQueryDescriptor<T>, ISpanGapQuery>, ISpanGapQuery
		where T : class
	{
		protected override bool Conditionless => SpanGapQuery.IsConditionless(this);

		Field ISpanGapQuery.Field { get; set; }

		int? ISpanGapQuery.Width { get; set; }

		public SpanGapQueryDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public SpanGapQueryDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		public SpanGapQueryDescriptor<T> Width(int? width) => Assign(width, (a, v) => a.Width = v);
	}

	internal class SpanGapQueryFormatter : IJsonFormatter<ISpanGapQuery>
	{
		public void Serialize(ref JsonWriter writer, ISpanGapQuery value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null || SpanGapQuery.IsConditionless(value))
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var inferrer = formatterResolver.GetConnectionSettings().Inferrer;
			writer.WritePropertyName(inferrer.Field(value.Field));
			writer.WriteInt32(value.Width.Value);
			writer.WriteEndObject();
		}

		public ISpanGapQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			var count = 0;
			var query = new SpanGapQuery();

			while (reader.ReadIsInObject(ref count))
			{
				if (count > 1)
					continue;

				query.Field = reader.ReadPropertyName();
				query.Width = reader.ReadInt32();
			}

			return query;
		}
	}
}
