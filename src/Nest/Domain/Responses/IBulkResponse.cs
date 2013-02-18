using System.Collections.Generic;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		int Took { get; }
		IEnumerable<BulkOperationResponseItem> Items { get; }
	}
}