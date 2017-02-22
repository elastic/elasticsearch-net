using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
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
