using System.Collections.Generic;

namespace Nest_5_2_0
{
	public interface IMultiSearchResponse : IResponse
	{
		IEnumerable<IResponse> AllResponses { get; }
		IEnumerable<IResponse> GetInvalidResponses();
		IEnumerable<SearchResponse<T>> GetResponses<T>() where T : class;
		SearchResponse<T> GetResponse<T>(string name) where T : class;
		int TotalResponses { get; }
	}
}
