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
		public SmoothingModelContainerDescriptor Laplace(Func<LaplaceSmoothingModelDescriptor, ILaplaceSmoothingModel> selector) =>
			Assign(a => a.Laplace = selector?.InvokeOrDefault(new LaplaceSmoothingModelDescriptor()));

		public SmoothingModelContainerDescriptor LinearInterpolation(
			Func<LinearInterpolationSmoothingModelDescriptor, ILinearInterpolationSmoothingModel> selector
		) =>
			Assign(a => a.LinearInterpolation = selector?.InvokeOrDefault(new LinearInterpolationSmoothingModelDescriptor()));

		public SmoothingModelContainerDescriptor StupidBackoff(Func<StupidBackoffSmoothingModelDescriptor, IStupidBackoffSmoothingModel> selector) =>
			Assign(a => a.StupidBackoff = selector?.InvokeOrDefault(new StupidBackoffSmoothingModelDescriptor()));

		private SmoothingModelContainerDescriptor Assign(Action<ISmoothingModelContainer> assigner) => Fluent.Assign(this, assigner);
	}
}
