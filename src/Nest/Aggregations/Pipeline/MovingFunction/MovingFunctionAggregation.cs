// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MovingFunctionAggregation))]
	public interface IMovingFunctionAggregation : IPipelineAggregation
	{
		[DataMember(Name ="script")]
		string Script { get; set; }

		[DataMember(Name ="window")]
		int? Window { get; set; }

		[DataMember(Name ="shift")]
		int? Shift { get; set; }
	}

	public class MovingFunctionAggregation
		: PipelineAggregationBase, IMovingFunctionAggregation
	{
		internal MovingFunctionAggregation() { }

		public MovingFunctionAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public string Script { get; set; }

		public int? Window { get; set; }

		public int? Shift { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.MovingFunction = this;
	}

	public class MovingFunctionAggregationDescriptor
		: PipelineAggregationDescriptorBase<MovingFunctionAggregationDescriptor, IMovingFunctionAggregation, SingleBucketsPath>
			, IMovingFunctionAggregation
	{
		string IMovingFunctionAggregation.Script { get; set; }
		int? IMovingFunctionAggregation.Window { get; set; }
		int? IMovingFunctionAggregation.Shift { get; set; }

		public MovingFunctionAggregationDescriptor Window(int? windowSize) => Assign(windowSize, (a, v) => a.Window = v);

		public MovingFunctionAggregationDescriptor Script(string script) => Assign(script, (a, v) => a.Script = v);

		public MovingFunctionAggregationDescriptor Shift(int? shift) => Assign(shift, (a, v) => a.Shift = v);

	}
}
