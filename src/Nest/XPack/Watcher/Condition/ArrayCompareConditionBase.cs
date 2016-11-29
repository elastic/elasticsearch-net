using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ArrayCompareConditionConverter))]
	public interface IArrayCompareCondition
	{
		string ArrayPath { get; set; }
		string Path { get; set; }
		string Comparison { get; }
		object Value { get; set; }
		Quantifier? Quantifier { get; set; }
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum Quantifier
	{
		[EnumMember(Value = "some")]
		Some,
		[EnumMember(Value = "all")]
		All
	}

	public abstract class ArrayCompareConditionBase : ConditionBase, IArrayCompareCondition
	{
		private IArrayCompareCondition Self => this;

		string IArrayCompareCondition.Path { get; set; }
		object IArrayCompareCondition.Value { get; set; }
		string IArrayCompareCondition.ArrayPath { get; set; }
		string IArrayCompareCondition.Comparison => this.Comparison;

		protected ArrayCompareConditionBase(string arrayPath, string path, object value)
		{
			Self.ArrayPath = arrayPath;
			Self.Path = path;
			Self.Value = value;
		}

		public Quantifier? Quantifier { get; set; }

		protected abstract string Comparison { get; }

		internal override void WrapInContainer(IConditionContainer container) => container.ArrayCompare = this;
	}

	public class ArrayCompareConditionDescriptor : IDescriptor
	{
		public IArrayCompareCondition EqualTo(string arrayPath, string path, object value, Quantifier? quantifier = null) =>
			new EqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier};

		public IArrayCompareCondition NotEqualTo(string arrayPath, string path, object value, Quantifier? quantifier = null) =>
			new NotEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };

		public IArrayCompareCondition GreaterThan(string arrayPath, string path, object value, Quantifier? quantifier = null) =>
			new GreaterThanArrayCondition(arrayPath, path, value) { Quantifier = quantifier };

		public IArrayCompareCondition GreaterThanOrEqualTo(string arrayPath, string path, object value, Quantifier? quantifier = null) =>
			new GreaterThanOrEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };

		public IArrayCompareCondition LowerThan(string arrayPath, string path, object value, Quantifier? quantifier = null) =>
			new LowerThanArrayCondition(arrayPath, path, value) { Quantifier = quantifier };

		public IArrayCompareCondition LowerThanOrEqualTo(string arrayPath, string path, object value, Quantifier? quantifier = null) =>
			new LowerThanOrEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
	}

	public class EqualArrayCondition : ArrayCompareConditionBase
	{
		protected override string Comparison => "eq";

		public EqualArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }
	}

	public class NotEqualArrayCondition : ArrayCompareConditionBase
	{
		protected override string Comparison => "not_eq";

		public NotEqualArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }
	}

	public class GreaterThanArrayCondition : ArrayCompareConditionBase
	{
		protected override string Comparison => "gt";

		public GreaterThanArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }
	}

	public class GreaterThanOrEqualArrayCondition : ArrayCompareConditionBase
	{
		protected override string Comparison => "gte";

		public GreaterThanOrEqualArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }
	}

	public class LowerThanArrayCondition : ArrayCompareConditionBase
	{
		protected override string Comparison => "lt";

		public LowerThanArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }
	}

	public class LowerThanOrEqualArrayCondition : ArrayCompareConditionBase
	{
		protected override string Comparison => "lte";

		public LowerThanOrEqualArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }
	}

	internal class ArrayCompareConditionConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var s = value as IArrayCompareCondition;
			if (s == null || s.ArrayPath.IsNullOrEmpty()) return;

			writer.WriteStartObject();
			writer.WritePropertyName(s.ArrayPath);
			writer.WriteStartObject();
			writer.WritePropertyName("path");
			writer.WriteValue(s.Path);
			writer.WritePropertyName(s.Comparison);
			writer.WriteStartObject();

			if (s.Quantifier.HasValue)
			{
				writer.WritePropertyName("quantifier");
				writer.WriteValue(s.Quantifier.Value);
			}

			writer.WritePropertyName("value");
			writer.WriteValue(s.Value);
			writer.WriteEndObject();
			writer.WriteEndObject();
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;

			var arrayPath = reader.ReadAsString();
			string path = null;
			Quantifier? quantifier = null;
			string comparison = null;
			object value = null;

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject) break;
				if (reader.TokenType != JsonToken.PropertyName) continue;

				var property = (string)reader.Value;

				if (property == "path")
				{
					path = reader.ReadAsString();
				}
				else
				{
					comparison = property;

					while (reader.Read())
					{
						if (reader.TokenType == JsonToken.EndObject) break;
						if (reader.TokenType != JsonToken.PropertyName) continue;

						var innerProperty = (string)reader.Value;
						switch (innerProperty)
						{
							case "quantifier":
								quantifier = reader.ReadAsString().ToEnum<Quantifier>();
								break;
							case "value":
								reader.Read();
								value = reader.Value;
								break;
						}
					}
				}				
			}

			switch (comparison)
			{
				case "eq": return new EqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case "not_eq": return new NotEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case "gt": return new GreaterThanArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case "gte": return new GreaterThanOrEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case "lt": return new LowerThanArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case "lte": return new LowerThanOrEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				default: return null;
			}
		}

		public override bool CanConvert(Type objectType) => typeof(IArrayCompareCondition).IsAssignableFrom(objectType);
	}
}
