using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BucketSelectorAggregation>))]
	public interface IBucketSelectorAggregation : IPipelineAggregation
	{
		[JsonProperty("script")]
		string Script { get; set; }
	}

	public class BucketSelectorAggregation
		: PipelineAggregationBase, IBucketSelectorAggregation
	{
		public string Script { get; set; }

		internal BucketSelectorAggregation () { }

		public BucketSelectorAggregation(string name, MultiBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.BucketSelector = this;
	}

	public class BucketSelectorAggregationDescriptor
		: PipelineAggregationDescriptorBase<BucketSelectorAggregationDescriptor, IBucketSelectorAggregation, MultiBucketsPath>
		, IBucketSelectorAggregation
	{
		string IBucketSelectorAggregation.Script { get; set; }

		public BucketSelectorAggregationDescriptor Script(string script) => Assign(a => a.Script = script);

		public BucketSelectorAggregationDescriptor BucketsPath(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> bucketsPath) =>
			Assign(a => a.BucketsPath = (MultiBucketsPath)bucketsPath?.Invoke(new FluentDictionary<string, string>()));
	}
}
