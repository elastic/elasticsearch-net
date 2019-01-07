using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

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

		public SpanGapQueryDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SpanGapQueryDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		public SpanGapQueryDescriptor<T> Width(int? width) => Assign(a => a.Width = width);
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
				return null;

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
