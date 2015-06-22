using System.Collections.Generic;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		int Took { get; }
		bool Errors { get; }
		IEnumerable<BulkOperationResponseItem> Items { get; }
		IEnumerable<BulkOperationResponseItem> ItemsWithErrors { get; }
	}
}