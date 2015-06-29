using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<PercentilesAggregator>))]
	public interface IPercentilesAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("percents")]
		IEnumerable<double> Percentages { get; set; }

		[JsonProperty("compression")]
		int? Compression { get; set; }
	}

	public class PercentilesAggregator : BucketAggregator, IPercentilesAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public IEnumerable<double> Percentages { get; set; }
		public int? Compression { get; set; }
	}

	public class PercentilesAggregationDescriptor<T> 
		: BucketAggregationBaseDescriptor<PercentilesAggregationDescriptor<T>, IPercentilesAggregator, T>
			, IPercentilesAggregator 
		where T : class
	{
		PropertyPathMarker IPercentilesAggregator.Field { get; set; }
		
		string IPercentilesAggregator.Script { get; set; }

		IDictionary<string, object> IPercentilesAggregator.Params { get; set; }

		IEnumerable<double> IPercentilesAggregator.Percentages { get; set; }

		int? IPercentilesAggregator.Compression { get; set; }

		public PercentilesAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public PercentilesAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public PercentilesAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public PercentilesAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));

		public PercentilesAggregationDescriptor<T> Percentages(params double[] percentages) => Assign(a => a.Percentages = percentages);

		public PercentilesAggregationDescriptor<T> Compression(int compression) => Assign(a => a.Compression = compression);

	}
}