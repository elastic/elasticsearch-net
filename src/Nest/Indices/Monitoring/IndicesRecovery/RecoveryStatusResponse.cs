using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IRecoveryStatusResponse : IResponse
	{
		IReadOnlyDictionary<string, RecoveryStatus> Indices { get; }
	}

	//TODO change to resolvable dictionary response when that PR lands in master and add response assertions
	//in the usage tests for this API
	[JsonConverter(typeof(DictionaryResponseJsonConverter<RecoveryStatusResponse, string, RecoveryStatus>))]
	public class RecoveryStatusResponse : DictionaryResponseBase<string, RecoveryStatus>, IRecoveryStatusResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, RecoveryStatus> Indices => Self.BackingDictionary;

	}
}
