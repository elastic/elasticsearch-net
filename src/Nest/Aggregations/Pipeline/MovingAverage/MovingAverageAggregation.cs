/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
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
