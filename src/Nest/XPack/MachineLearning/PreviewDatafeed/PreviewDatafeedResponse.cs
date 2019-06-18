using System.Collections.Generic;

namespace Nest
{
	public interface IPreviewDatafeedResponse<out TResult> : IResponse
	{
		IReadOnlyCollection<TResult> Data { get; }
	}

	public class PreviewDatafeedResponse<TResult> : ResponseBase, IPreviewDatafeedResponse<TResult>
	{
		public IReadOnlyCollection<TResult> Data { get; internal set; } = EmptyReadOnly<TResult>.Collection;
	}
}
