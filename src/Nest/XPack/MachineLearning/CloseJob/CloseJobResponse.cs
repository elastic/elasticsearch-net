using System.Runtime.Serialization;

namespace Nest
{
	public interface ICloseJobResponse : IResponse
	{
		[DataMember(Name ="closed")]
		bool Closed { get; }
	}

	public class CloseJobResponse : ResponseBase, ICloseJobResponse
	{
		public bool Closed { get; internal set; }
	}
}
