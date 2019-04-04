using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(MovingAverageAggregationJsonConverter))]
	public interface IMovingAverageAggregation : IPipelineAggregation
	{
		[JsonProperty("minimize")]
		bool? Minimize { get; set; }

		IMovingAverageModel Model { get; set; }

		[JsonProperty("predict")]
		int? Predict { get; set; }

		[JsonProperty("window")]
		int? Window { get; set; }
	}

	public class MovingAverageAggregation : PipelineAggregationBase, IMovingAverageAggregation
	{
		internal MovingAverageAggregation() { }

		public MovingAverageAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public bool? Minimize { get; set; }
		public IMovingAverageModel Model { get; set; }
		public int? Predict { get; set; }
		public int? Window { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.MovingAverage = this;
	}

	public class MovingAverageAggregationDescriptor
		: PipelineAggregationDescriptorBase<MovingAverageAggregationDescriptor, IMovingAverageAggregation, SingleBucketsPath>
			, IMovingAverageAggregation
	{
		bool? IMovingAverageAggregation.Minimize { get; set; }
		IMovingAverageModel IMovingAverageAggregation.Model { get; set; }
		int? IMovingAverageAggregation.Predict { get; set; }
		int? IMovingAverageAggregation.Window { get; set; }

		public MovingAverageAggregationDescriptor Minimize(bool? minimize = true) => Assign(minimize, (a, v) => a.Minimize = v);

		public MovingAverageAggregationDescriptor Window(int? window) => Assign(window, (a, v) => a.Window = v);

		public MovingAverageAggregationDescriptor Predict(int? predict) => Assign(predict, (a, v) => a.Predict = v);

		public MovingAverageAggregationDescriptor Model(Func<MovingAverageModelDescriptor, IMovingAverageModel> modelSelector) =>
			Assign(modelSelector, (a, v) => a.Model = v?.Invoke(new MovingAverageModelDescriptor()));
	}
}
