using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ICreateIndexResponse : IAcknowledgedResponse
	{
		[DataMember(Name ="shards_acknowledged")]
		bool ShardsAcknowledged { get; }
	}

	public class CreateIndexResponse : AcknowledgedResponseBase, ICreateIndexResponse
	{
		public bool ShardsAcknowledged { get; set; }
	}
}
