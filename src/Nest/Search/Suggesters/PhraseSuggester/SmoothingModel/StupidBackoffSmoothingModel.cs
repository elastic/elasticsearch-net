using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<StupidBackoffSmoothingModel>))]
	public interface IStupidBackoffSmoothingModel : ISmoothingModel
	{
		[JsonProperty("discount")]
		double? Discount { get; set; }
	}

	public class StupidBackoffSmoothingModel : SmoothingModelBase, IStupidBackoffSmoothingModel
	{
		public double? Discount { get; set; }

		internal override void WrapInContainer(ISmoothingModelContainer container) => container.StupidBackoff = this;
	}

	public class StupidBackoffSmoothingModelDescriptor : DescriptorBase<StupidBackoffSmoothingModelDescriptor, IStupidBackoffSmoothingModel>, IStupidBackoffSmoothingModel
	{
		double? IStupidBackoffSmoothingModel.Discount{ get; set; }

		public StupidBackoffSmoothingModelDescriptor Discount(double? discount) => Assign(a => a.Discount = discount);
	}
}
