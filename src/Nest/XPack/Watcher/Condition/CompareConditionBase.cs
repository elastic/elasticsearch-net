using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(CompareConditionConverter))]
	public interface ICompareCondition : ICondition
	{
		string Comparison { get; }
		string Path { get; set; }
		object Value { get; set; }
	}

	public abstract class CompareConditionBase : ConditionBase, ICompareCondition
	{
		protected CompareConditionBase(string path, object value)
		{
			Self.Path = path;
			Self.Value = value;
		}

		protected abstract string Comparison { get; }
		string ICompareCondition.Comparison => Comparison;

		string ICompareCondition.Path { get; set; }
		private ICompareCondition Self => this;
		object ICompareCondition.Value { get; set; }

		internal override void WrapInContainer(IConditionContainer container) => container.Compare = this;
	}

	public class CompareConditionDescriptor : IDescriptor
	{
		public ICompareCondition EqualTo(string path, object value) =>
			new EqualCondition(path, value);

		public ICompareCondition NotEqualTo(string path, object value) =>
			new NotEqualCondition(path, value);

		public ICompareCondition GreaterThan(string path, object value) =>
			new GreaterThanCondition(path, value);

		public ICompareCondition GreaterThanOrEqualTo(string path, object value) =>
			new GreaterThanOrEqualCondition(path, value);

		public ICompareCondition LowerThan(string path, object value) =>
			new LowerThanCondition(path, value);

		public ICompareCondition LowerThanOrEqualTo(string path, object value) =>
			new LowerThanOrEqualCondition(path, value);
	}

	public class EqualCondition : CompareConditionBase
	{
		public EqualCondition(string path, object value) : base(path, value) { }

		protected override string Comparison => "eq";
	}

	public class NotEqualCondition : CompareConditionBase
	{
		public NotEqualCondition(string path, object value) : base(path, value) { }

		protected override string Comparison => "not_eq";
	}

	public class GreaterThanCondition : CompareConditionBase
	{
		public GreaterThanCondition(string path, object value) : base(path, value) { }

		protected override string Comparison => "gt";
	}

	public class GreaterThanOrEqualCondition : CompareConditionBase
	{
		public GreaterThanOrEqualCondition(string path, object value) : base(path, value) { }

		protected override string Comparison => "gte";
	}

	public class LowerThanCondition : CompareConditionBase
	{
		public LowerThanCondition(string path, object value) : base(path, value) { }

		protected override string Comparison => "lt";
	}

	public class LowerThanOrEqualCondition : CompareConditionBase
	{
		public LowerThanOrEqualCondition(string path, object value) : base(path, value) { }

		protected override string Comparison => "lte";
	}

	internal class CompareConditionConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;

			var compare = JObject.Load(reader);
			if (compare.Count == 0) return null;

			var pathProperty = compare.Children<JProperty>().First();
			if (pathProperty.Count == 0) return null;

			var path = pathProperty.Name;

			var conditionProperty = pathProperty.Value.Children<JProperty>().First();
			if (conditionProperty.Count == 0) return null;

			var condition = conditionProperty.Name;
			var value = serializer.Deserialize<object>(conditionProperty.Value.CreateReader());

			switch (condition)
			{
				case "eq": return new EqualCondition(path, value);
				case "not_eq": return new NotEqualCondition(path, value);
				case "gt": return new GreaterThanCondition(path, value);
				case "gte": return new GreaterThanOrEqualCondition(path, value);
				case "lt": return new LowerThanCondition(path, value);
				case "lte": return new LowerThanOrEqualCondition(path, value);
				default: return null;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var s = value as ICompareCondition;
			if (s == null || s.Path.IsNullOrEmpty()) return;

			writer.WriteStartObject();
			writer.WritePropertyName(s.Path);
			writer.WriteStartObject();
			writer.WritePropertyName(s.Comparison);
			writer.WriteValue(s.Value);
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}
