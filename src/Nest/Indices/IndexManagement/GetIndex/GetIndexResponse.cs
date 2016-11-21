using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexResponse : IResponse
	{
		IReadOnlyDictionary<string, IndexState> Indices { get; }
	}

	[JsonObject]
	public class GetIndexResponse : ResponseBase, IGetIndexResponse
	{
		public IReadOnlyDictionary<string, IndexState> Indices { get; internal set; } = EmptyReadOnly<string, IndexState>.Dictionary;
	}
}
