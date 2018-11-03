namespace Nest
{
	public interface IRollupSearchResponse<T> : ISearchResponse<T> where T : class { }

	public class RollupSearchResponse<T> : SearchResponse<T>, IRollupSearchResponse<T>
		where T : class { }
}
