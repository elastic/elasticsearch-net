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

	public class PercentilesAggregationDescriptor<T> : BucketAggregationBaseDescriptor<PercentilesAggregationDescriptor<T>, T>, IPercentilesAggregator 
		where T : class
	{
		private IPercentilesAggregator Self { get { return this; } }

		PropertyPathMarker IPercentilesAggregator.Field { get; set; }
		
		string IPercentilesAggregator.Script { get; set; }

		IDictionary<string, object> IPercentilesAggregator.Params { get; set; }

		IEnumerable<double> IPercentilesAggregator.Percentages { get; set; }

		int? IPercentilesAggregator.Compression { get; set; }

		public PercentilesAggregationDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public PercentilesAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public PercentilesAggregationDescriptor<T> Script(string script)
		{
			Self.Script = script;
			return this;
		}

		public PercentilesAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			Self.Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		public PercentilesAggregationDescriptor<T> Percentages(params double[] percentages)
		{
			Self.Percentages = percentages;
			return this;
		}
		
		public PercentilesAggregationDescriptor<T> Compression(int compression)
		{
			Self.Compression = compression;
			return this;
		}
	}
}