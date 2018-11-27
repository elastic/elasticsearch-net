using System.Runtime.Serialization;

namespace Nest
{
	public interface IOpenJobResponse : IResponse
	{
		[DataMember(Name ="opened")]
		bool Opened { get; }
	}

	public class OpenJobResponse : ResponseBase, IOpenJobResponse
	{
		public bool Opened { get; internal set; }
	}
}
