using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(SpanGapQueryJsonConverter))]
	public interface ISpanGapQuery : ISpanSubQuery
	{
		Field Field { get; set; }
		int? Width { get; set; }
	}

	public class SpanGapQuery : QueryBase, ISpanGapQuery
	{
		protected override bool Conditionless => SpanGapQuery.IsConditionless(this);

		internal static bool IsConditionless(ISpanGapQuery q) => q?.Width == null || q.Field.IsConditionless();

		public Field Field { get; set; }
		public int? Width { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) =>
			throw new Exception("span_gap may only appear as a span near clause");

	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
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

	internal class SpanGapQueryJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => typeof(ISpanGapQuery).IsAssignableFrom(objectType);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var gapQuery = value as ISpanGapQuery;
			if (value == null || SpanGapQuery.IsConditionless(gapQuery))
			{
				writer.WriteNull();
				return;
			}
			var settings = serializer.GetConnectionSettings();
			var fieldName = settings.Inferrer.Field(gapQuery.Field);
			writer.WriteStartObject();
			writer.WritePropertyName(fieldName);
			writer.WriteValue(gapQuery.Width);
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;
			reader.Read();
			var field = (Field)reader.Value.ToString(); // field
			var width = reader.ReadAsInt32();
			reader.Read();
			return new SpanGapQuery {Field = field, Width = width};
		}

	}
}
