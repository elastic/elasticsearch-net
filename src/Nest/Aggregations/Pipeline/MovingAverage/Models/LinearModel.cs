using Newtonsoft.Json;

namespace Nest
{
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
