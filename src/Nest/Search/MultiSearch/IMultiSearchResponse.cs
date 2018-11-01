using System.Collections.Generic;

namespace Nest
{
	public interface IMultiSearchResponse : IResponse
	{
		IEnumerable<IResponse> AllResponses { get; }
		int TotalResponses { get; }

		IEnumerable<IResponse> GetInvalidResponses();

		ISearchResponse<T> GetResponse<T>(string name) where T : class;

		IEnumerable<ISearchResponse<T>> GetResponses<T>() where T : class;
	}
}
