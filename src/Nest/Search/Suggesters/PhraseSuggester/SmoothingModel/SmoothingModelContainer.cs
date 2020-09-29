// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SmoothingModelContainer))]
	public interface ISmoothingModelContainer
	{
		[DataMember(Name = "laplace")]
		ILaplaceSmoothingModel Laplace { get; set; }

		[DataMember(Name = "linear_interpolation")]
		ILinearInterpolationSmoothingModel LinearInterpolation { get; set; }

		[DataMember(Name = "stupid_backoff")]
		IStupidBackoffSmoothingModel StupidBackoff { get; set; }
	}

	[DataContract]
	public class SmoothingModelContainer : ISmoothingModelContainer, IDescriptor
	{
		internal SmoothingModelContainer() { }

		public SmoothingModelContainer(SmoothingModelBase model)
		{
			model.ThrowIfNull(nameof(model));
			model.WrapInContainer(this);
		}

		ILaplaceSmoothingModel ISmoothingModelContainer.Laplace { get; set; }
		ILinearInterpolationSmoothingModel ISmoothingModelContainer.LinearInterpolation { get; set; }
		IStupidBackoffSmoothingModel ISmoothingModelContainer.StupidBackoff { get; set; }
	}

	public class SmoothingModelContainerDescriptor : SmoothingModelContainer
	{
		private SmoothingModelContainerDescriptor Assign<TValue>(TValue value, Action<ISmoothingModelContainer, TValue> assigner) =>
			Fluent.Assign(this, value, assigner);

		public SmoothingModelContainerDescriptor StupidBackoff(Func<StupidBackoffSmoothingModelDescriptor, IStupidBackoffSmoothingModel> selector) =>
			Assign(selector,(a, v) => a.StupidBackoff = v?.InvokeOrDefault(new StupidBackoffSmoothingModelDescriptor()));

		public SmoothingModelContainerDescriptor LinearInterpolation(
			Func<LinearInterpolationSmoothingModelDescriptor, ILinearInterpolationSmoothingModel> selector
		) =>
			Assign(selector, (a, v) => a.LinearInterpolation = v?.InvokeOrDefault(new LinearInterpolationSmoothingModelDescriptor()));

		public SmoothingModelContainerDescriptor Laplace(Func<LaplaceSmoothingModelDescriptor, ILaplaceSmoothingModel> selector) =>
			Assign(selector, (a, v) => a.Laplace = v?.InvokeOrDefault(new LaplaceSmoothingModelDescriptor()));
	}
}
