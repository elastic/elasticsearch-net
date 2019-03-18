using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Notification for each bulk response, indicates the page its currently processing and how many retries it took to index this buffer
	/// </summary>
	public interface IBulkAllResponse
	{
		/// <summary>This is the Nth buffer.</summary>
		long Page { get; }

		/// <summary>The number of back off retries were needed to store this document.</summary>
		int Retries { get; }

		/// <summary>The items returned from the bulk response</summary>
		IReadOnlyCollection<IBulkResponseItem> Items { get; }
	}

	/// <inheritdoc />
	[JsonObject]
	public class BulkAllResponse : IBulkAllResponse
	{
		/// <inheritdoc />
		public bool IsValid => true;

		/// <inheritdoc />
		public long Page { get; internal set; }

		/// <inheritdoc />
		public int Retries { get; internal set; }

		/// <inheritdoc />
		public IReadOnlyCollection<IBulkResponseItem> Items { get; internal set; }
	}
}
