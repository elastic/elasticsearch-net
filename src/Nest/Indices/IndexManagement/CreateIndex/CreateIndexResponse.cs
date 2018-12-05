using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
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
