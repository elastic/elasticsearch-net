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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ScoreFunctionJsonFormatter))]
	public interface IScoreFunction
	{
		[DataMember(Name ="filter")]
		QueryContainer Filter { get; set; }

		[DataMember(Name ="weight")]
		double? Weight { get; set; }
	}

	public class FunctionScoreFunction : FunctionScoreFunctionBase { }

	public abstract class FunctionScoreFunctionBase : IScoreFunction
	{
		public QueryContainer Filter { get; set; }
		public double? Weight { get; set; }
	}

	public class FunctionScoreFunctionDescriptor<T> : FunctionScoreFunctionDescriptorBase<FunctionScoreFunctionDescriptor<T>, IScoreFunction, T>
		where T : class { }

	public abstract class FunctionScoreFunctionDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, IScoreFunction
		where TDescriptor : FunctionScoreFunctionDescriptorBase<TDescriptor, TInterface, T>, TInterface, IScoreFunction
		where TInterface : class, IScoreFunction
		where T : class
	{
		QueryContainer IScoreFunction.Filter { get; set; }

		double? IScoreFunction.Weight { get; set; }

		public TDescriptor Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filterSelector) =>
			Assign(filterSelector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));

		public TDescriptor Weight(double? weight) => Assign(weight, (a, v) => a.Weight = v);
	}
}
