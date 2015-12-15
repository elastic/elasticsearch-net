using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISimpleModel : IMovingAverageModel { }

	public class SimpleModel : ISimpleModel
	{
		string IMovingAverageModel.Name { get; } = "simple";
	}

	public class SimpleModelDescriptor
		: DescriptorBase<SimpleModelDescriptor, ISimpleModel>, ISimpleModel
	{
		string IMovingAverageModel.Name { get; } = "simple";
	}
}
