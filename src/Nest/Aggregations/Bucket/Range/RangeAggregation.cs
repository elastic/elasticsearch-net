using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<RangeAggregation>))]
	public interface IRangeAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("script")]
		IScript Script { get; set; }

		[JsonProperty(PropertyName = "ranges")]
#pragma warning disable 618
		IEnumerable<IRange> Ranges { get; set; }
#pragma warning restore 618
	}

	public class RangeAggregation : BucketAggregationBase, IRangeAggregation
	{
		public Field Field { get; set; }
		public IScript Script { get; set; }
#pragma warning disable 618
		public IEnumerable<IRange> Ranges { get; set; }
#pragma warning restore 618

		internal RangeAggregation() { }

		public RangeAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Range = this;
	}

	public class RangeAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<RangeAggregationDescriptor<T>, IRangeAggregation, T>, IRangeAggregation
		where T : class
	{
		Field IRangeAggregation.Field { get; set; }

		IScript IRangeAggregation.Script { get; set; }

#pragma warning disable 618
		IEnumerable<IRange> IRangeAggregation.Ranges { get; set; }
#pragma warning restore 618

		public RangeAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public RangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public RangeAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public RangeAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

#pragma warning disable 618
		public RangeAggregationDescriptor<T> Ranges(params Func<RangeDescriptor, IRange>[] ranges) =>
			Assign(a => a.Ranges = ranges.Select(r => r(new RangeDescriptor())));
#pragma warning restore 618
	}
}
