using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<LaplaceSmoothingModel>))]
	public interface ILaplaceSmoothingModel : ISmoothingModel
	{
		[JsonProperty("alpha")]
		double? Alpha { get; set; }
	}

	public class LaplaceSmoothingModel : SmoothingModelBase, ILaplaceSmoothingModel
	{
		public double? Alpha { get; set; }

		internal override void WrapInContainer(ISmoothingModelContainer container) => container.Laplace = this;
	}

	public class LaplaceSmoothingModelDescriptor : DescriptorBase<LaplaceSmoothingModelDescriptor, ILaplaceSmoothingModel>, ILaplaceSmoothingModel
	{
		double? ILaplaceSmoothingModel.Alpha{ get; set; }

		public LaplaceSmoothingModelDescriptor Alpha(double? alpha) => Assign(a => a.Alpha = alpha);
	}
}
