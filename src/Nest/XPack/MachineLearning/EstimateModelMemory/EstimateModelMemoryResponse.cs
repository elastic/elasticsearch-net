using System.Runtime.Serialization;

namespace Nest
{
	public class EstimateModelMemoryResponse : ResponseBase
	{
		[DataMember(Name ="model_memory_estimate")]
		public string ModelMemoryEstimate { get; internal set;  }
	}
}
