using System.Collections.Generic;

namespace Nest
{
	public interface IMultiSearchResponse : IResponse
	{
		IEnumerable<SearchResponse<T>> GetResponses<T>() where T : class;
		SearchResponse<T> GetResponse<T>(string name) where T : class;
		int TotalResponses { get; }
	}
}