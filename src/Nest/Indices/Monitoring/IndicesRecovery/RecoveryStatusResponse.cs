using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IRecoveryStatusResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, RecoveryStatus> Indices { get; }
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<RecoveryStatusResponse, IndexName, RecoveryStatus>))]
	public class RecoveryStatusResponse : DictionaryResponseBase<IndexName, RecoveryStatus>, IRecoveryStatusResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<IndexName, RecoveryStatus> Indices => Self.BackingDictionary;

	}
}
