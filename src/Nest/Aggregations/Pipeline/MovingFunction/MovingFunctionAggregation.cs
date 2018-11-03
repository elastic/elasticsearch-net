using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<MovingFunctionAggregation>))]
	public interface IMovingFunctionAggregation : IPipelineAggregation
	{
		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("window")]
		int? Window { get; set; }
	}

	public class MovingFunctionAggregation
		: PipelineAggregationBase, IMovingFunctionAggregation
	{
		internal MovingFunctionAggregation() { }

		public MovingFunctionAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public string Script { get; set; }

		public int? Window { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.MovingFunction = this;
	}

	public class MovingFunctionAggregationDescriptor
		: PipelineAggregationDescriptorBase<MovingFunctionAggregationDescriptor, IMovingFunctionAggregation, SingleBucketsPath>
			, IMovingFunctionAggregation
	{
		string IMovingFunctionAggregation.Script { get; set; }
		int? IMovingFunctionAggregation.Window { get; set; }

		public MovingFunctionAggregationDescriptor Window(int? windowSize) => Assign(a => a.Window = windowSize);

		public MovingFunctionAggregationDescriptor Script(string script) => Assign(a => a.Script = script);
	}
}
