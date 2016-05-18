using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<BucketSelectorAggregation>))]
	public interface IBucketSelectorAggregation : IPipelineAggregation
	{
		[JsonProperty("script")]
		IScript Script { get; set; }
	}

	public class BucketSelectorAggregation
		: PipelineAggregationBase, IBucketSelectorAggregation
	{
		public override string TypeName => "bucket_selector";

		public IScript Script { get; set; }

		internal BucketSelectorAggregation () { }

		public BucketSelectorAggregation(string name, MultiBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.BucketSelector = this;
	}

	public class BucketSelectorAggregationDescriptor
		: PipelineAggregationDescriptorBase<BucketSelectorAggregationDescriptor, IBucketSelectorAggregation, MultiBucketsPath>
		, IBucketSelectorAggregation
	{
		public override string TypeName => "bucket_selector";

		IScript IBucketSelectorAggregation.Script { get; set; }

		public BucketSelectorAggregationDescriptor Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public BucketSelectorAggregationDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public BucketSelectorAggregationDescriptor BucketsPath(Func<MultiBucketsPathDescriptor, IPromise<IBucketsPath>> selector) =>
			Assign(a => a.BucketsPath = selector?.Invoke(new MultiBucketsPathDescriptor())?.Value);
	}
}
