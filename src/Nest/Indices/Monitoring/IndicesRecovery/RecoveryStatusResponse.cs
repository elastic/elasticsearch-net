using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRecoveryStatusResponse : IResponse
	{
		Dictionary<IndexName, RecoveryStatus> Indices { get; set; }
	}

	[JsonObject]
	public class RecoveryStatusResponse : BaseResponse, IRecoveryStatusResponse
	{

		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<IndexName, RecoveryStatus> Indices { get; set; } 
	}
}