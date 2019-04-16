using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(RangeAggregation))]
	public interface IRangeAggregation : IBucketAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="ranges")]
		IEnumerable<IAggregationRange> Ranges { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	public class RangeAggregation : BucketAggregationBase, IRangeAggregation
	{
		internal RangeAggregation() { }

		public RangeAggregation(string name) : base(name) { }

		public Field Field { get; set; }
		public IEnumerable<IAggregationRange> Ranges { get; set; }
		public IScript Script { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Range = this;
	}

	public class RangeAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<RangeAggregationDescriptor<T>, IRangeAggregation, T>, IRangeAggregation
		where T : class
	{
		Field IRangeAggregation.Field { get; set; }

		IEnumerable<IAggregationRange> IRangeAggregation.Ranges { get; set; }

		IScript IRangeAggregation.Script { get; set; }

		public RangeAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public RangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public RangeAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public RangeAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public RangeAggregationDescriptor<T> Ranges(params Func<AggregationRangeDescriptor, IAggregationRange>[] ranges) =>
			Assign(a => a.Ranges = ranges.Select(r => r(new AggregationRangeDescriptor())));
	}
}
