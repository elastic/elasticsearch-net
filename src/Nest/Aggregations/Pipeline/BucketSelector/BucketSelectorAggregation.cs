using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<BucketSelectorAggregation>))]
	public interface IBucketSelectorAggregation : IPipelineAggregation
	{
		[JsonProperty("script")]
		IScript Script { get; set; }
	}

	public class BucketSelectorAggregation
		: PipelineAggregationBase, IBucketSelectorAggregation
	{
		internal BucketSelectorAggregation() { }

		public BucketSelectorAggregation(string name, MultiBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public IScript Script { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.BucketSelector = this;
	}

	public class BucketSelectorAggregationDescriptor
		: PipelineAggregationDescriptorBase<BucketSelectorAggregationDescriptor, IBucketSelectorAggregation, MultiBucketsPath>
			, IBucketSelectorAggregation
	{
		IScript IBucketSelectorAggregation.Script { get; set; }

		public BucketSelectorAggregationDescriptor Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public BucketSelectorAggregationDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public BucketSelectorAggregationDescriptor BucketsPath(Func<MultiBucketsPathDescriptor, IPromise<IBucketsPath>> selector) =>
			Assign(selector, (a, v) => a.BucketsPath = v?.Invoke(new MultiBucketsPathDescriptor())?.Value);
	}
}
