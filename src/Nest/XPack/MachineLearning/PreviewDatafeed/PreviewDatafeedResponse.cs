using System.Collections.Generic;

namespace Nest
{
	public interface IPreviewDatafeedResponse<out TResult> : IResponse where TResult : class
	{
		IReadOnlyCollection<TResult> Data { get; }
	}

	public class PreviewDatafeedResponse<TResult> : ResponseBase, IPreviewDatafeedResponse<TResult> where TResult : class
	{
		public IReadOnlyCollection<TResult> Data { get; internal set; } = EmptyReadOnly<TResult>.Collection;
	}
}
