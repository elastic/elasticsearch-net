namespace Nest
{
	public interface ISourceResponse<out TDocument> : IResponse
	{
		TDocument Body { get; }
	}

	public class SourceResponse<TDocument> : ResponseBase, ISourceResponse<TDocument>
	{
		public TDocument Body { get; internal set; }
	}
}
