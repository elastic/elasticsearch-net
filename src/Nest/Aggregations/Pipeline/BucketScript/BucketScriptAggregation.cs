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
		Script Script { get; set; }
	}

	public class BucketScriptAggregation
		: PipelineAggregationBase, IBucketScriptAggregation
	{
		public Script Script { get; set; }

		internal BucketScriptAggregation () { }

		public BucketScriptAggregation(string name, MultiBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.BucketScript = this;
	}

	public class BucketScriptAggregationDescriptor
		: PipelineAggregationDescriptorBase<BucketScriptAggregationDescriptor, IBucketScriptAggregation, MultiBucketsPath>
		, IBucketScriptAggregation
	{
		Script IBucketScriptAggregation.Script { get; set; }

		public BucketScriptAggregationDescriptor Script(string script) => Assign(a => a.Script = script);

		public BucketScriptAggregationDescriptor Script(Func<ScriptDescriptor, Script> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public BucketScriptAggregationDescriptor BucketsPath(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> bucketsPath) =>
			Assign(a => a.BucketsPath = (MultiBucketsPath)bucketsPath?.Invoke(new FluentDictionary<string, string>()));
	}
}
