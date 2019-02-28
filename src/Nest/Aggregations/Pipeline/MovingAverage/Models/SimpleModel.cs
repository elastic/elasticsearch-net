using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
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
