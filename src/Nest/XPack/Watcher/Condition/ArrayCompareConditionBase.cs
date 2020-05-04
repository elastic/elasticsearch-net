// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ArrayCompareConditionFormatter))]
	public interface IArrayCompareCondition
	{
		string ArrayPath { get; set; }
		string Comparison { get; }
		string Path { get; set; }
		Quantifier? Quantifier { get; set; }
		object Value { get; set; }
	}

	[StringEnum]
	public enum Quantifier
	{
		[EnumMember(Value = "some")]
		Some,

		[EnumMember(Value = "all")]
		All
	}

	public abstract class ArrayCompareConditionBase : ConditionBase, IArrayCompareCondition
	{
		protected ArrayCompareConditionBase(string arrayPath, string path, object value)
		{
			Self.ArrayPath = arrayPath;
			Self.Path = path;
			Self.Value = value;
		}

		public Quantifier? Quantifier { get; set; }

		protected abstract string Comparison { get; }
		string IArrayCompareCondition.ArrayPath { get; set; }
		string IArrayCompareCondition.Comparison => Comparison;

		string IArrayCompareCondition.Path { get; set; }
		private IArrayCompareCondition Self => this;
		object IArrayCompareCondition.Value { get; set; }

		internal override void WrapInContainer(IConditionContainer container) => container.ArrayCompare = this;
	}

	public class ArrayCompareConditionDescriptor : IDescriptor
	{
		public IArrayCompareCondition EqualTo(string arrayPath, string path, object value, Quantifier? quantifier = null) =>
			new EqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };

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
		public EqualArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }

		protected override string Comparison => "eq";
	}

	public class NotEqualArrayCondition : ArrayCompareConditionBase
	{
		public NotEqualArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }

		protected override string Comparison => "not_eq";
	}

	public class GreaterThanArrayCondition : ArrayCompareConditionBase
	{
		public GreaterThanArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }

		protected override string Comparison => "gt";
	}

	public class GreaterThanOrEqualArrayCondition : ArrayCompareConditionBase
	{
		public GreaterThanOrEqualArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }

		protected override string Comparison => "gte";
	}

	public class LowerThanArrayCondition : ArrayCompareConditionBase
	{
		public LowerThanArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }

		protected override string Comparison => "lt";
	}

	public class LowerThanOrEqualArrayCondition : ArrayCompareConditionBase
	{
		public LowerThanOrEqualArrayCondition(string arrayPath, string path, object value)
			: base(arrayPath, path, value) { }

		protected override string Comparison => "lte";
	}

	internal class ArrayCompareConditionFormatter : IJsonFormatter<IArrayCompareCondition>
	{
		private static readonly AutomataDictionary ComparisonProperties = new AutomataDictionary
		{
			{ "quantifier", 0 },
			{ "value", 1 }
		};

		private static readonly AutomataDictionary Comparisons = new AutomataDictionary
		{
			{ "eq", 0 },
			{ "not_eq", 1 },
			{ "gt", 2 },
			{ "gte", 3 },
			{ "lt", 4 },
			{ "lte", 5 },
		};

		public IArrayCompareCondition Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			var count = 0;
			string arrayPath = null;
			string path = null;
			var comparisonValue = -1;
			Quantifier? quantifier = null;
			object value = null;

			while (reader.ReadIsInObject(ref count))
			{
				arrayPath = reader.ReadPropertyName();

				var innerCount = 0;
				while (reader.ReadIsInObject(ref innerCount))
				{
					var property = reader.ReadPropertyNameSegmentRaw();
					if (Comparisons.TryGetValue(property, out comparisonValue))
					{
						var comparisonPropCount = 0;
						while (reader.ReadIsInObject(ref comparisonPropCount))
						{
							var comparisonProperty = reader.ReadPropertyNameSegmentRaw();
							if (ComparisonProperties.TryGetValue(comparisonProperty, out var propValue))
							{
								switch (propValue)
								{
									case 0:
										quantifier = formatterResolver.GetFormatter<Quantifier>()
											.Deserialize(ref reader, formatterResolver);
										break;
									case 1:
										value = formatterResolver.GetFormatter<object>()
											.Deserialize(ref reader, formatterResolver);
										break;
								}
							}
							else
								reader.ReadNextBlock();
						}
					}
					else
						path = reader.ReadString();
				}
			}

			switch (comparisonValue)
			{
				case 0: return new EqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case 1: return new NotEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case 2: return new GreaterThanArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case 3: return new GreaterThanOrEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case 4: return new LowerThanArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				case 5: return new LowerThanOrEqualArrayCondition(arrayPath, path, value) { Quantifier = quantifier };
				default: return null;
			}
		}

		public void Serialize(ref JsonWriter writer, IArrayCompareCondition value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null || value.ArrayPath.IsNullOrEmpty())
				return;

			writer.WriteBeginObject();
			writer.WritePropertyName(value.ArrayPath);
			writer.WriteBeginObject();
			writer.WritePropertyName("path");
			writer.WriteString(value.Path);
			writer.WriteValueSeparator();
			writer.WritePropertyName(value.Comparison);
			writer.WriteBeginObject();

			if (value.Quantifier.HasValue)
			{
				writer.WritePropertyName("quantifier");
				var formatter = formatterResolver.GetFormatter<Quantifier>();
				formatter.Serialize(ref writer, value.Quantifier.Value, formatterResolver);
			}

			writer.WritePropertyName("value");
			var comparisonFormatter = formatterResolver.GetFormatter<object>();
			comparisonFormatter.Serialize(ref writer, value.Value, formatterResolver);

			writer.WriteEndObject();
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}
