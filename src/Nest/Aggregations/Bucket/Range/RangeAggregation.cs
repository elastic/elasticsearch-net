using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RangeAggregation>))]
	public interface IRangeAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		FieldName Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		FluentDictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class RangeAggregation : BucketAggregationBase, IRangeAggregation
	{
		public FieldName Field { get; set; }
		public string Script { get; set; }
		public FluentDictionary<string, object> Params { get; set; }
		public IEnumerable<Range<double>> Ranges { get; set; }

		internal RangeAggregation() { }

		public RangeAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Range = this;
	}

	public class RangeAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<RangeAggregationDescriptor<T>, IRangeAggregation, T>, IRangeAggregation
		where T : class
	{
		FieldName IRangeAggregation.Field { get; set; }

		string IRangeAggregation.Script { get; set; }

		FluentDictionary<string, object> IRangeAggregation.Params { get; set; }

		IEnumerable<Range<double>> IRangeAggregation.Ranges { get; set; }

		public RangeAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public RangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public RangeAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public RangeAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));

		public RangeAggregationDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges) =>
			Assign(a => a.Ranges = (from range in ranges let r = new Range<double>() select range(r)).ToListOrNullIfEmpty());
	}
}