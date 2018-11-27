using System;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(ReserializeJsonConverter<SmoothingModelContainer, ISmoothingModelContainer>))]
	public interface ISmoothingModelContainer
	{
		[DataMember(Name ="laplace")]
		ILaplaceSmoothingModel Laplace { get; set; }

		[DataMember(Name ="linear_interpolation")]
		ILinearInterpolationSmoothingModel LinearInterpolation { get; set; }

		[DataMember(Name ="stupid_backoff")]
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
		private SmoothingModelContainerDescriptor Assign(Action<ISmoothingModelContainer> assigner) => Fluent.Assign(this, assigner);

		public SmoothingModelContainerDescriptor StupidBackoff(Func<StupidBackoffSmoothingModelDescriptor, IStupidBackoffSmoothingModel> selector) =>
			Assign(a => a.StupidBackoff = selector?.InvokeOrDefault(new StupidBackoffSmoothingModelDescriptor()));

		public SmoothingModelContainerDescriptor LinearInterpolation(
			Func<LinearInterpolationSmoothingModelDescriptor, ILinearInterpolationSmoothingModel> selector
		) =>
			Assign(a => a.LinearInterpolation = selector?.InvokeOrDefault(new LinearInterpolationSmoothingModelDescriptor()));

		public SmoothingModelContainerDescriptor Laplace(Func<LaplaceSmoothingModelDescriptor, ILaplaceSmoothingModel> selector) =>
			Assign(a => a.Laplace = selector?.InvokeOrDefault(new LaplaceSmoothingModelDescriptor()));
	}
}
