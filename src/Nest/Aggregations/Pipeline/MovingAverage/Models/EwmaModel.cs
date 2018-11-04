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
		public float? Alpha { get; set; }
		string IMovingAverageModel.Name { get; } = "ewma";
	}

	public class EwmaModelDescriptor
		: DescriptorBase<EwmaModelDescriptor, IEwmaModel>, IEwmaModel
	{
		float? IEwmaModel.Alpha { get; set; }
		string IMovingAverageModel.Name { get; } = "ewma";

		public EwmaModelDescriptor Alpha(float alpha) => Assign(a => a.Alpha = alpha);
	}
}
