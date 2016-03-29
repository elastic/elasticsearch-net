using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IEwmaModel : IMovingAverageModel
	{
		[JsonProperty("alpha")]
		float? Alpha { get; set; }
	}

	public class EwmaModel : IEwmaModel
	{
		string IMovingAverageModel.Name { get; } = "ewma";

		public float? Alpha { get; set; }
	}

	public class EwmaModelDescriptor
		: DescriptorBase<EwmaModelDescriptor, IEwmaModel>, IEwmaModel
	{
		string IMovingAverageModel.Name { get; } = "ewma";
		float? IEwmaModel.Alpha { get; set; }

		public EwmaModelDescriptor Alpha(float alpha) => Assign(a => a.Alpha = alpha);
	}
}
