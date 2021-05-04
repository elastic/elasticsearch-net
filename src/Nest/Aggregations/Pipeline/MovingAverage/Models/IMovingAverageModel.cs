// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
