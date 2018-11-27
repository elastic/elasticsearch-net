using System.Runtime.Serialization;

namespace Nest
{
	public interface IFlushJobResponse : IResponse
	{
		[DataMember(Name ="flushed")]
		bool Flushed { get; }
	}

	public class FlushJobResponse : ResponseBase, IFlushJobResponse
	{
		public bool Flushed { get; internal set; }
	}
}
