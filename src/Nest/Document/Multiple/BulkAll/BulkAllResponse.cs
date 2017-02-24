using System;
using Newtonsoft.Json;

namespace Nest_5_2_0
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
	}

	/// <inheritdoc />
	[JsonObject]
	public class BulkAllResponse : IBulkAllResponse
	{
		/// <inheritdoc />
		public long Page { get; internal set; }

		/// <inheritdoc />
		public int Retries { get; internal set; }

		/// <inheritdoc />
		public bool IsValid => true;
	}
}
