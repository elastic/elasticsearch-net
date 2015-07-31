using System;
using System.Collections;
using System.Collections.Generic;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.ConnectionPool
{
	public interface IConnectionPool
	{
		/// <summary>
		/// Returns a readonly collection of all the nodes registered. This is meant as a view. 
		/// If you want to update this list call UpdateNodeList(), each IConnectionPool needs to make sure that is threadsafe
		/// GetNext() is also written in a way to deal with intermittent list updates.
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
		bool AcceptsUpdates { get; }

		DateTime? LastUpdate { get; set; }

		/// <summary>
		/// Whether or not SSL is being using
		/// </summary>
		bool UsingSsl { get; }

		/// <summary>
		/// Bookkeeps wheter this connectionpool has seen a sniff on startup. 
		/// </summary>
		bool SniffedOnStartup { get; set; }

		/// <summary>
		/// Gets the next live Uri to perform the request on
		/// </summary>
		/// <param name="initialSeed">pass the original seed when retrying, this guarantees that the nodes are walked in a
		///  predictable manner even when called in a multithreaded context</param>
		/// <param name="seed">The seed this call started on</param>
		/// <returns></returns>
		Node GetNext(int? initialSeed, out int seed);

		/// <summary>
		/// Update the node list manually, usually done by ITransport when sniffing was needed.
		/// </summary>
		/// <param name="newClusterState"></param>
		/// <param name="sniffNode">hint that the node we recieved the sniff from should not be pinged</param>
		void UpdateNodeList(IList<Uri> newClusterState, Uri sniffNode = null);

	}
}