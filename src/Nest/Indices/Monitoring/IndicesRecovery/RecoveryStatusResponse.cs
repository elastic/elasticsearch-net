using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRecoveryStatusResponse : IResponse
	{
		IReadOnlyDictionary<string, RecoveryStatus> Indices { get; }
	}

	[JsonObject]
	public class RecoveryStatusResponse : ResponseBase, IRecoveryStatusResponse
	{

		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, RecoveryStatus>))]
		public IReadOnlyDictionary<string, RecoveryStatus> Indices { get; internal set; } = EmptyReadOnly<string, RecoveryStatus>.Dictionary;
	}
}
