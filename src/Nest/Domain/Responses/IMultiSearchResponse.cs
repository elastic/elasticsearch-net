using System.Collections.Generic;

namespace Nest
{
	public interface IMultiSearchResponse
	{
		IEnumerable<QueryResponse<T>> GetResponses<T>() where T : class;
		QueryResponse<T> GetResponse<T>(string name) where T : class;
	}
}