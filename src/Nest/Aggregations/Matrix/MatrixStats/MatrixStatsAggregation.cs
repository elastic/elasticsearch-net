// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MatrixStatsAggregation))]
	public interface IMatrixStatsAggregation : IMatrixAggregation
	{
		[DataMember(Name ="mode")]
		MatrixStatsMode? Mode { get; set; }
	}

	public class MatrixStatsAggregation : MatrixAggregationBase, IMatrixStatsAggregation
	{
		internal MatrixStatsAggregation() { }

		public MatrixStatsAggregation(string name, Fields fields) : base(name, fields) { }

		public MatrixStatsMode? Mode { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.MatrixStats = this;
	}

	public class MatrixStatsAggregationDescriptor<T>
		: MatrixAggregationDescriptorBase<MatrixStatsAggregationDescriptor<T>, IMatrixStatsAggregation, T>
			, IMatrixStatsAggregation
		where T : class
	{
		MatrixStatsMode? IMatrixStatsAggregation.Mode { get; set; }

		public MatrixStatsAggregationDescriptor<T> Mode(MatrixStatsMode? mode) => Assign(mode, (a, v) => a.Mode = v);
	}
}
