using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IStartRollupJobResponse : IResponse
	{
		[DataMember(Name ="started")]
		bool Started { get; set; }
	}

	public class StartRollupJobResponse : ResponseBase, IStartRollupJobResponse
	{
		public bool Started { get; set; }
	}
}
