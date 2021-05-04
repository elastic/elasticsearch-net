// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Globalization;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Exists 
{
	public class DocumentExistsNotFoundApiTests : DocumentExistsApiTests
	{
		public DocumentExistsNotFoundApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string CallIsolatedValue => int.MaxValue.ToString(CultureInfo.InvariantCulture);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;

		protected override void ExpectResponse(ExistsResponse response) { }
	}
}