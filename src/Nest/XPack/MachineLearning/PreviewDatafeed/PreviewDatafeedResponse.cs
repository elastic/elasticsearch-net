using System.Collections.Generic;

namespace Nest
{
	//TODO T can be anything?
	public interface IPreviewDatafeedResponse<out T> : IResponse
	{
		IReadOnlyCollection<T> Data { get; }
	}

	public class PreviewDatafeedResponse<T> : ResponseBase, IPreviewDatafeedResponse<T>
	{
		public IReadOnlyCollection<T> Data { get; internal set; } = EmptyReadOnly<T>.Collection;
	}
}
