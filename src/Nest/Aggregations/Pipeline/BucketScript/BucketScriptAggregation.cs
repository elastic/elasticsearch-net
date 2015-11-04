using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BucketScriptAggregation>))]
	public interface IBucketScriptAggregation : IPipelineAggregation
	{
		[JsonProperty("script")]
		string Script { get; set; }
	}

	public class BucketScriptAggregation
		: PipelineAggregationBase, IBucketScriptAggregation
	{
		public string Script { get; set; }

		internal BucketScriptAggregation () { }

		public BucketScriptAggregation(string name, MultiBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.BucketScript = this;
	}

	public class BucketScriptAggregationDescriptor
		: PipelineAggregationDescriptorBase<BucketScriptAggregationDescriptor, IBucketScriptAggregation, MultiBucketsPath>
		, IBucketScriptAggregation
	{
		string IBucketScriptAggregation.Script { get; set; }

		public BucketScriptAggregationDescriptor Script(string script) => Assign(a => a.Script = script);

		public BucketScriptAggregationDescriptor BucketsPath(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> bucketsPath) =>
			Assign(a => a.BucketsPath = (MultiBucketsPath)bucketsPath?.Invoke(new FluentDictionary<string, string>()));
	}
}
