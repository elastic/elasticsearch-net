using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRecoveryStatusResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, RecoveryStatus> Indices { get; }
	}

	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<RecoveryStatusResponse, IndexName, RecoveryStatus>))]
	public class RecoveryStatusResponse : DictionaryResponseBase<IndexName, RecoveryStatus>, IRecoveryStatusResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, RecoveryStatus> Indices => Self.BackingDictionary;
	}
}
