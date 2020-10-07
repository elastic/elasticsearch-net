// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;

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
}
