// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(MovingAverageAggregationFormatter))]
	public interface IMovingAverageAggregation : IPipelineAggregation
	{
		[DataMember(Name ="minimize")]
		bool? Minimize { get; set; }

		IMovingAverageModel Model { get; set; }

		[DataMember(Name ="predict")]
		int? Predict { get; set; }

		[DataMember(Name ="window")]
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
