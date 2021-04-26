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

using System.Runtime.Serialization;
using Nest.Utf8Json;

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
