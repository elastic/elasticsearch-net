using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(BucketSelectorAggregation))]
	public interface IBucketSelectorAggregation : IPipelineAggregation
	{
		[DataMember(Name ="script")]
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

		public BucketSelectorAggregationDescriptor Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public BucketSelectorAggregationDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public BucketSelectorAggregationDescriptor BucketsPath(Func<MultiBucketsPathDescriptor, IPromise<IBucketsPath>> selector) =>
			Assign(a => a.BucketsPath = selector?.Invoke(new MultiBucketsPathDescriptor())?.Value);
	}
}
