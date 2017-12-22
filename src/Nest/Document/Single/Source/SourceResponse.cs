namespace Nest
{
	public interface ISourceResponse<out T> : IResponse
	{
		T Body { get; }
	}

	public class SourceResponse<T> : ResponseBase, ISourceResponse<T>
	{
		public T Body { get; internal set; }
	}
}
