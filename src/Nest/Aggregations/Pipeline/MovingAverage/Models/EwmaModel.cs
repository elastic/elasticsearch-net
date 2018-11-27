using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IEwmaModel : IMovingAverageModel
	{
		[DataMember(Name ="alpha")]
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

		public EwmaModelDescriptor Alpha(float? alpha) => Assign(a => a.Alpha = alpha);
	}
}
