using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(MovingAverageAggregationJsonConverter))]
	public interface IMovingAverageAggregation : IPipelineAggregation
	{
		IMovingAverageModel Model { get; set; }

		[JsonProperty("window")]
		int? Window { get; set; }

		[JsonProperty("minimize")]
		bool? Minimize { get; set; }

		[JsonProperty("predict")]
		int? Predict { get; set; }
	}

	public class MovingAverageAggregation : PipelineAggregationBase, IMovingAverageAggregation
	{
		public bool? Minimize { get; set; }
		public IMovingAverageModel Model { get; set; }
		public int? Window { get; set; }
		public int? Predict { get; set; }

		internal MovingAverageAggregation() { }

		public MovingAverageAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath)
		{ }

		internal override void WrapInContainer(AggregationContainer c) => c.MovingAverage = this;
	}

	public class MovingAverageAggregationDescriptor
		: PipelineAggregationDescriptorBase<MovingAverageAggregationDescriptor, IMovingAverageAggregation, SingleBucketsPath>
		, IMovingAverageAggregation
	{
		bool? IMovingAverageAggregation.Minimize { get; set; }
		IMovingAverageModel IMovingAverageAggregation.Model { get; set; }
		int? IMovingAverageAggregation.Window { get; set; }
		int? IMovingAverageAggregation.Predict { get; set; }

		public MovingAverageAggregationDescriptor Minimize(bool minimize) => Assign(a => a.Minimize = minimize);

		public MovingAverageAggregationDescriptor Window(int window) => Assign(a => a.Window = window);

		public MovingAverageAggregationDescriptor Predict(int predict) => Assign(a => a.Predict = predict);

		public MovingAverageAggregationDescriptor Model(Func<MovingAverageModelDescriptor, IMovingAverageModel> modelSelector) =>
			Assign(a => a.Model = modelSelector?.Invoke(new MovingAverageModelDescriptor()));

	}
}
