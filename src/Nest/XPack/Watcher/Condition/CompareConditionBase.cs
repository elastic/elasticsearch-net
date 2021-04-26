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

using Nest.Utf8Json;
namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(CompareConditionFormatter))]
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

	internal class CompareConditionFormatter : IJsonFormatter<ICompareCondition>
	{
		private static readonly AutomataDictionary Comparisons = new AutomataDictionary
		{
			{ "eq", 0 },
			{ "not_eq", 1 },
			{ "gt", 2 },
			{ "gte", 3 },
			{ "lt", 4 },
			{ "lte", 5 },
		};

		public ICompareCondition Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			ICompareCondition condition = null;
			while (reader.ReadIsInObject(ref count))
			{
				var path = reader.ReadPropertyName();
				var innerCount = 0;
				while (reader.ReadIsInObject(ref innerCount))
				{
					var comparison = reader.ReadPropertyNameSegmentRaw();
					var formatter = formatterResolver.GetFormatter<object>();
					var comparisonValue = formatter.Deserialize(ref reader, formatterResolver);

					if (Comparisons.TryGetValue(comparison, out var value))
					{
						switch (value)
						{
							case 0:
								condition = new EqualCondition(path, comparisonValue);
								break;
							case 1:
								condition = new NotEqualCondition(path, value);
								break;
							case 2:
								condition = new GreaterThanCondition(path, value);
								break;
							case 3:
								condition = new GreaterThanOrEqualCondition(path, value);
								break;
							case 4:
								condition = new LowerThanCondition(path, value);
								break;
							case 5:
								condition = new LowerThanOrEqualCondition(path, value);
								break;
						}
					}
				}
			}

			return condition;
		}

		public void Serialize(ref JsonWriter writer, ICompareCondition value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null || value.Path.IsNullOrEmpty()) return;

			writer.WriteBeginObject();
			writer.WritePropertyName(value.Path);

			writer.WriteBeginObject();
			writer.WritePropertyName(value.Comparison);

			var formatter = formatterResolver.GetFormatter<object>();
			formatter.Serialize(ref writer, value.Value, formatterResolver);

			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}
