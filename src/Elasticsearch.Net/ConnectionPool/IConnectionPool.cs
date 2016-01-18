using System;
using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public interface IConnectionPool : IDisposable
	{
		/// <summary>
		/// Returns a readonly constant view of all the nodes in the cluster, this might involve creating copies of the nodes e.g 
		/// if you are using the sniffing connectionpool. If you do not need an isolated copy of the nodes please read `CreateView()` to completion
		/// </summary>
		IReadOnlyCollection<Node> Nodes { get; }

		/// <summary>
		/// Returns the default maximum retries for the connection pool implementation.
		/// Most implementation default to number of nodes, note that this can be overidden
		/// in the connection settings
		/// </summary>
		int MaxRetries { get; }
		
		/// <summary>
		/// Signals that this implemenation can accept new nodes
		/// </summary>
		bool SupportsReseeding { get; }

		bool SupportsPinging { get; }

		DateTime LastUpdate { get; }

		/// <summary>
		/// Whether or not SSL is being using
		/// </summary>
		bool UsingSsl { get; }

		/// <summary>
		/// Bookkeeps wheter this connectionpool has seen a sniff on startup. its up to to the callee to set this in a threadsafe fashion
		/// </summary>
		bool SniffedOnStartup { get; set; }

		/// <summary>
		/// Creates a view with changing starting positions that wraps over on each call
		/// e.g Thread A might get 1,2,3,4,5 and thread B will get 2,3,4,5,1.
		/// if there are no live nodes yields a different dead node to try once
		/// </summary>
		IEnumerable<Node> CreateView(Action<AuditEvent, Node> audit = null);

		/// <summary>
		/// Update the node list, it's the IConnectionPool's responsibility to do so in a threadsafe fashion
		/// </summary>
		void Reseed(IEnumerable<Node> nodes);

	}
}