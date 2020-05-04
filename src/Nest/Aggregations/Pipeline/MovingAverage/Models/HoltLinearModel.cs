// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HoltLinearModel))]
	public interface IHoltLinearModel : IMovingAverageModel
	{
		[DataMember(Name ="alpha")]
		float? Alpha { get; set; }

		[DataMember(Name ="beta")]
		float? Beta { get; set; }
	}

	public class HoltLinearModel : IHoltLinearModel
	{
		public float? Alpha { get; set; }
		public float? Beta { get; set; }
		string IMovingAverageModel.Name { get; } = "holt";
	}

	public class HoltLinearModelDescriptor
		: DescriptorBase<HoltLinearModelDescriptor, IHoltLinearModel>, IHoltLinearModel
	{
		float? IHoltLinearModel.Alpha { get; set; }
		float? IHoltLinearModel.Beta { get; set; }
		string IMovingAverageModel.Name { get; } = "holt";

		public HoltLinearModelDescriptor Alpha(float? alpha) => Assign(alpha, (a, v) => a.Alpha = v);

		public HoltLinearModelDescriptor Beta(float? beta) => Assign(beta, (a, v) => a.Beta = v);
	}
}
