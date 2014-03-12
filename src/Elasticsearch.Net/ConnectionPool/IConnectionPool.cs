using System;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.ConnectionPool
{
	public interface IConnectionPool
	{
		/// <summary>
		/// Returns the default maximum retries for the connection pool implementation.
		/// Most implementation default to number of nodes, note that this can be overidden
		/// in the connection settings
		/// </summary>
		int MaxRetries { get; }

		/// <summary>
		/// Get the next available Uri for a live node
		/// </summary>
		Uri GetNext(int? initialSeed, out int seed);

		/// <summary>
		/// Mark the specified Uri as dead
		/// </summary>
		void MarkDead(Uri uri, int? deadTimeout = null, int? maxDeadtimeout = null);

		/// <summary>
		/// Bring the specified uri back to life.
		/// </summary>
		/// <param name="uri"></param>
		void MarkAlive(Uri uri);

		/// <summary>
		/// Instruct the connectionpool to sniff for more nodes
		/// </summary>
		/// <param name="connection">a connection that can be used to call elasticsearch</param>
		/// <param name="fromStartUpHint">hints wheter the sniff was requested from on startup
		/// connection pools should be registered as singletons in the application. The hint prevents new'ing of clients
		/// cause excessive startup sniffs
		/// </param>
		void Sniff(IConnection connection, bool fromStartUpHint = false);
	}
}