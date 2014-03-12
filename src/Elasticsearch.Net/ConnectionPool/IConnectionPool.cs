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
		/// Gets the next live Uri to perform the request on
		/// </summary>
		/// <param name="initialSeed">pass the original seed when retrying, this guarantees that the nodes are walked in a predictable manner when multithreading</param>
		/// <param name="seed">The seed this call started on</param>
		/// <returns></returns>
		Uri GetNext(int? initialSeed, out int seed, out bool shouldPingHint);

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