using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IRecoveryStatusResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, RecoveryStatus> Indices { get; }
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<RecoveryStatusResponse, IndexName, RecoveryStatus>))]
	public class RecoveryStatusResponse : DictionaryResponseBase<IndexName, RecoveryStatus>, IRecoveryStatusResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, RecoveryStatus> Indices => Self.BackingDictionary;
	}
}
