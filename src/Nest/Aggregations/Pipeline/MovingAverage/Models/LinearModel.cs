using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ILinearModel : IMovingAverageModel { }
	
	public class LinearModel : ILinearModel
	{
		string IMovingAverageModel.Name  { get; } = "linear";
	}

	public class LinearModelDescriptor
		: DescriptorBase<LinearModelDescriptor, ILinearModel>, ILinearModel
	{
		string IMovingAverageModel.Name { get; } = "linear";
	}
}
