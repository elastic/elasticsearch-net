// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Transport.Products.Elasticsearch
{
	public static class ElasticsearchNodeFeatures
	{
		/// <summary>Indicates whether this node holds data, defaults to true when unknown/unspecified</summary>
		public const string HoldsData = "node.data";
		/// <summary>Whether HTTP is enabled on the node or not</summary>
		public const string HttpEnabled = "node.http";
		/// <summary>Indicates whether this node is allowed to run ingest pipelines, defaults to true when unknown/unspecified</summary>
		public const string IngestEnabled = "node.ingest";
		/// <summary>Indicates whether this node is master eligible, defaults to true when unknown/unspecified</summary>
		public const string MasterEligible = "node.master";

		public static readonly IReadOnlyCollection<string> Default = new[] { HoldsData, MasterEligible, IngestEnabled, HttpEnabled }.ToList().AsReadOnly();
		public static readonly IReadOnlyCollection<string> MasterEligableOnly = new[] { MasterEligible, HttpEnabled }.ToList().AsReadOnly();
		public static readonly IReadOnlyCollection<string> NotMasterEligable = Default.Except(new[] { MasterEligible }).ToList().AsReadOnly();

	}

	public class ElasticsearchProductRegistration : IProductRegistration
	{
		public static IProductRegistration Default { get; } = new ElasticsearchProductRegistration();

		public bool SupportsPing { get; } = true;
		public bool SupportsSniff { get; } = true;

		public static string SniffPath => "_nodes/http,settings";

		public RequestData CreateSniffRequestData(Node node, IRequestConfiguration requestConfiguration, IConnectionConfigurationValues settings,
			IMemoryStreamFactory memoryStreamFactory
		)
		{
			var requestParameters = new RequestParameters(HttpMethod.GET, supportsBody: false)
			{
				QueryString = { { "timeout", requestConfiguration.PingTimeout }, { "flat_settings", true }, }
			};
			return new RequestData(HttpMethod.GET, SniffPath, null, settings, requestParameters, memoryStreamFactory) { Node = node };
		}

		public async Task<Tuple<IApiCallDetails, IReadOnlyCollection<Node>>> SniffAsync(IConnection connection, bool forceSsl, RequestData requestData, CancellationToken cancellationToken)
		{
			var response = await connection.RequestAsync<SniffResponse>(requestData, cancellationToken).ConfigureAwait(false);
			var nodes = response.ToNodes(forceSsl);
			return Tuple.Create<IApiCallDetails, IReadOnlyCollection<Node>>(response, new ReadOnlyCollection<Node>(nodes.ToArray()));
		}

		public Tuple<IApiCallDetails, IReadOnlyCollection<Node>> Sniff(IConnection connection, bool forceSsl, RequestData requestData)
		{
			var response = connection.Request<SniffResponse>(requestData);
			var nodes = response.ToNodes(forceSsl);
			return Tuple.Create<IApiCallDetails, IReadOnlyCollection<Node>>(response, new ReadOnlyCollection<Node>(nodes.ToArray()));
		}

		public int SniffOrder(Node node) => node.HasFeature(ElasticsearchNodeFeatures.MasterEligible) ? node.Uri.Port : int.MaxValue;

		public bool NodePredicate(Node node) =>
			// skip master only nodes (holds no data and is master eligable)
			!(node.HasFeature(ElasticsearchNodeFeatures.MasterEligible) && !node.HasFeature(ElasticsearchNodeFeatures.HoldsData));

		public RequestData CreatePingRequestData(Node node, RequestConfiguration requestConfiguration, IConnectionConfigurationValues global,
			IMemoryStreamFactory memoryStreamFactory
		)
		{
			IRequestParameters requestParameters = new RequestParameters(HttpMethod.HEAD, supportsBody: false);
			requestParameters.RequestConfiguration = requestConfiguration;

			var data = new RequestData(HttpMethod.HEAD, string.Empty, null, global, requestParameters, memoryStreamFactory) { Node = node };
			return data;
		}

		public async Task<IApiCallDetails> PingAsync(IConnection connection, RequestData pingData, CancellationToken cancellationToken) =>
			await connection.RequestAsync<VoidResponse>(pingData, cancellationToken).ConfigureAwait(false);

		public IApiCallDetails Ping(IConnection connection, RequestData pingData) =>
			connection.Request<VoidResponse>(pingData);
	}
}
