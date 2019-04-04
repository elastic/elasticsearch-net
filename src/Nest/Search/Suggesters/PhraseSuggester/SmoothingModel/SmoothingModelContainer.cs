using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<SmoothingModelContainer, ISmoothingModelContainer>))]
	public interface ISmoothingModelContainer
	{
		[JsonProperty("laplace")]
		ILaplaceSmoothingModel Laplace { get; set; }

		[JsonProperty("linear_interpolation")]
		ILinearInterpolationSmoothingModel LinearInterpolation { get; set; }

		[JsonProperty("stupid_backoff")]
		IStupidBackoffSmoothingModel StupidBackoff { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
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
