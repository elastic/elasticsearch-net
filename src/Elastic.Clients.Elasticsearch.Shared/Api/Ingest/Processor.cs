// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Ingest;
#else
namespace Elastic.Clients.Elasticsearch.Ingest;
#endif

public partial class Processor
{
	// TODO - We should generate this.
	// TODO - We should include a marker interface on the variants to support constraining the generic T here to IProcessor

	public bool TryGet<T>([NotNullWhen(true)] out T? processor) where T : class
	{
		processor = null;

		if (Variant is T variant)
		{
			processor = variant;
			return true;
		}

		return false;
	}
}
