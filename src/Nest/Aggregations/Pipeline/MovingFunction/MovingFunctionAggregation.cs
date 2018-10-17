using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<MovingFunctionAggregation>))]
	public interface IMovingFunctionAggregation : IPipelineAggregation
	{
		[JsonProperty("window")]
		int? Window { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }
	}

	public class MovingFunctionAggregation
		: PipelineAggregationBase, IMovingFunctionAggregation
	{
		internal MovingFunctionAggregation () { }

		public MovingFunctionAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.MovingFunction = this;

		public int? Window { get; set; }
		public string Script { get; set; }
	}

	public class MovingFunctionAggregationDescriptor
		: PipelineAggregationDescriptorBase<MovingFunctionAggregationDescriptor, IMovingFunctionAggregation, SingleBucketsPath>
		, IMovingFunctionAggregation
	{
		int? IMovingFunctionAggregation.Window { get; set; }
		string IMovingFunctionAggregation.Script { get; set; }

		public MovingFunctionAggregationDescriptor Window(int? windowSize) => Assign(a => a.Window = windowSize);

		public MovingFunctionAggregationDescriptor Script(string script) => Assign(a => a.Script = script);
	}
}
