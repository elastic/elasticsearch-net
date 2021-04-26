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
	[ReadAs(typeof(ChiSquareHeuristic))]
	public interface IChiSquareHeuristic
	{
		[DataMember(Name ="background_is_superset")]
		bool? BackgroundIsSuperSet { get; set; }

		[DataMember(Name ="include_negatives")]
		bool? IncludeNegatives { get; set; }
	}

	public class ChiSquareHeuristic : IChiSquareHeuristic
	{
		public bool? BackgroundIsSuperSet { get; set; }
		public bool? IncludeNegatives { get; set; }
	}

	public class ChiSquareHeuristicDescriptor
		: DescriptorBase<ChiSquareHeuristicDescriptor, IChiSquareHeuristic>, IChiSquareHeuristic
	{
		bool? IChiSquareHeuristic.BackgroundIsSuperSet { get; set; }
		bool? IChiSquareHeuristic.IncludeNegatives { get; set; }

		public ChiSquareHeuristicDescriptor BackgroundIsSuperSet(bool? backgroundIsSuperSet = true) =>
			Assign(backgroundIsSuperSet, (a, v) => a.BackgroundIsSuperSet = v);

		public ChiSquareHeuristicDescriptor IncludeNegatives(bool? includeNegatives = true) =>
			Assign(includeNegatives, (a, v) => a.IncludeNegatives = v);
	}
}
