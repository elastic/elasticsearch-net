using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<SmoothingModelContainer, ISmoothingModelContainer>))]
	public interface ISmoothingModelContainer
	{
		[JsonProperty("stupid_backoff")]
		IStupidBackoffSmoothingModel StupidBackoff { get; set; }

		[JsonProperty("laplace")]
		ILaplaceSmoothingModel Laplace { get; set; }

		[JsonProperty("linear_interpolation")]
		ILinearInterpolationSmoothingModel LinearInterpolation { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SmoothingModelContainer : ISmoothingModelContainer, IDescriptor
	{
		internal SmoothingModelContainer() {}

		public SmoothingModelContainer(SmoothingModelBase model)
		{
			model.ThrowIfNull(nameof(model));
			model.WrapInContainer(this);
		}

		IStupidBackoffSmoothingModel ISmoothingModelContainer.StupidBackoff { get; set; }
		ILaplaceSmoothingModel ISmoothingModelContainer.Laplace { get; set; }
		ILinearInterpolationSmoothingModel ISmoothingModelContainer.LinearInterpolation { get; set; }
	}

	public class SmoothingModelContainerDescriptor : SmoothingModelContainer
	{
		private SmoothingModelContainerDescriptor Assign(Action<ISmoothingModelContainer> assigner) => Fluent.Assign(this, assigner);

		public SmoothingModelContainerDescriptor StupidBackoff(Func<StupidBackoffSmoothingModelDescriptor, IStupidBackoffSmoothingModel> selector) =>
			Assign(a => a.StupidBackoff = selector?.InvokeOrDefault(new StupidBackoffSmoothingModelDescriptor()));

		public SmoothingModelContainerDescriptor LinearInterpolation(Func<LinearInterpolationSmoothingModelDescriptor, ILinearInterpolationSmoothingModel> selector) =>
			Assign(a => a.LinearInterpolation = selector?.InvokeOrDefault(new LinearInterpolationSmoothingModelDescriptor()));

		public SmoothingModelContainerDescriptor Laplace(Func<LaplaceSmoothingModelDescriptor, ILaplaceSmoothingModel> selector) =>
			Assign(a => a.Laplace = selector?.InvokeOrDefault(new LaplaceSmoothingModelDescriptor()));
	}
}
