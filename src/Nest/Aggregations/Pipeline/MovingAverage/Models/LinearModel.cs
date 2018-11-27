using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(LinearModel))]
	public interface ILinearModel : IMovingAverageModel { }

	public class LinearModel : ILinearModel
	{
		string IMovingAverageModel.Name { get; } = "linear";
	}

	public class LinearModelDescriptor
		: DescriptorBase<LinearModelDescriptor, ILinearModel>, ILinearModel
	{
		string IMovingAverageModel.Name { get; } = "linear";
	}
}
