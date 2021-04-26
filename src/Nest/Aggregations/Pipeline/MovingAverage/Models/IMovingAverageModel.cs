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

namespace Nest
{
	public interface IMovingAverageModel
	{
		[IgnoreDataMember]
		string Name { get; }
	}

	public class MovingAverageModelDescriptor : DescriptorBase<MovingAverageModelDescriptor, IDescriptor>
	{
		public IEwmaModel Ewma(Func<EwmaModelDescriptor, IEwmaModel> ewmaSelector = null) =>
			ewmaSelector.InvokeOrDefault(new EwmaModelDescriptor());

		public IHoltLinearModel HoltLinear(Func<HoltLinearModelDescriptor, IHoltLinearModel> holtSelector = null) =>
			holtSelector.InvokeOrDefault(new HoltLinearModelDescriptor());

		public IHoltWintersModel HoltWinters(Func<HoltWintersModelDescriptor, IHoltWintersModel> holtWintersSelector) =>
			holtWintersSelector?.Invoke(new HoltWintersModelDescriptor());

		public ILinearModel Linear(Func<LinearModelDescriptor, ILinearModel> linearSelector = null) =>
			linearSelector.InvokeOrDefault(new LinearModelDescriptor());

		public ISimpleModel Simple(Func<SimpleModelDescriptor, ISimpleModel> simpleSelector = null) =>
			simpleSelector.InvokeOrDefault(new SimpleModelDescriptor());
	}
}
