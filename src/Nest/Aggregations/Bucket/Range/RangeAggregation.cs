using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RangeAggregator>))]
	public interface IRangeAggregator : IBucketAggregator
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

	public class RangeAggregator : BucketAggregator, IRangeAggregator
	{
		public FieldName Field { get; set; }
		public string Script { get; set; }
		public FluentDictionary<string, object> Params { get; set; }
		public IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class RangeAgg : BucketAgg, IRangeAggregator
	{
		public FieldName Field { get; set; }
		public string Script { get; set; }
		public FluentDictionary<string, object> Params { get; set; }
		public IEnumerable<Range<double>> Ranges { get; set; }

		public RangeAgg(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Range = this;
	}

	public class RangeAggregatorDescriptor<T>
		: BucketAggregatorBaseDescriptor<RangeAggregatorDescriptor<T>, IRangeAggregator, T>, IRangeAggregator
		where T : class
	{
		FieldName IRangeAggregator.Field { get; set; }

		string IRangeAggregator.Script { get; set; }

		FluentDictionary<string, object> IRangeAggregator.Params { get; set; }

		IEnumerable<Range<double>> IRangeAggregator.Ranges { get; set; }

		public RangeAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public RangeAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public RangeAggregatorDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public RangeAggregatorDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));

		public RangeAggregatorDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges) =>
			Assign(a => a.Ranges = (from range in ranges let r = new Range<double>() select range(r)).ToListOrNullIfEmpty());
	}
}