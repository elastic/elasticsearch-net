using System.Collections.Generic;

namespace Nest
{
	public interface IMultiSearchResponse : IResponse
	{
		IEnumerable<QueryResponse<T>> GetResponses<T>() where T : class;
		QueryResponse<T> GetResponse<T>(string name) where T : class;
		int TotalResponses { get; }
	}
}