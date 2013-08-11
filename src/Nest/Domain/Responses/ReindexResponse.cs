using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// POCO representing the reindex response for a each step
	/// </summary>
	[JsonObject]
	public class ReindexResponse<T> : IReindexResponse<T> where T : class
	{
		public IBulkResponse BulkResponse { get; internal set; }
		public IQueryResponse<T> QueryResponse { get; internal set; }

		public int Scroll { get; internal set; }

		public bool IsValid
		{
			get
			{
				return (this.BulkResponse != null && this.BulkResponse.IsValid
					&& this.QueryResponse != null && this.QueryResponse.IsValid);
			}
		}
	}
}
