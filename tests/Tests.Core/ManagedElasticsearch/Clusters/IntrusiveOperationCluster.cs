// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using static Elastic.Stack.ArtifactsApi.Products.ElasticsearchPlugin;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	/// <summary>
	/// Use this cluster for heavy API's, either on ES's side or the client (intricate setup etc)
	/// </summary>
	public class IntrusiveOperationCluster : ClientTestClusterBase
	{
		public IntrusiveOperationCluster() : base(new ClientTestClusterConfiguration(IngestGeoIp, IngestAttachment)
		{
			MaxConcurrency = 1
		}) { }
	}
}
