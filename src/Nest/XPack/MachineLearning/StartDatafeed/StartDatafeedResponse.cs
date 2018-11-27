using System.Runtime.Serialization;

namespace Nest
{
	public interface IStartDatafeedResponse : IResponse
	{
		bool Started { get; }
	}

	public class StartDatafeedResponse : ResponseBase, IStartDatafeedResponse
	{
		[DataMember(Name ="started")]
		public bool Started { get; internal set; }
	}
}
