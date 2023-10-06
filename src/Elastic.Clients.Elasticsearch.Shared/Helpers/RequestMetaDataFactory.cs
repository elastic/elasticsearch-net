// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal static class RequestMetaDataFactory
{
	internal static RequestMetaData ReindexHelperRequestMetaData()
	{
		var metaData = new RequestMetaData();
		metaData.AddReindexHelper();
		return metaData;
	}

	internal static RequestMetaData ScrollHelperRequestMetaData()
	{
		var metaData = new RequestMetaData();
		metaData.AddScrollHelper();
		return metaData;
	}

	internal static RequestMetaData BulkHelperRequestMetaData()
	{
		var metaData = new RequestMetaData();
		metaData.AddBulkHelper();
		return metaData;
	}

	internal static RequestMetaData SnapshotHelperRequestMetaData()
	{
		var metaData = new RequestMetaData();
		metaData.AddSnapshotHelper();
		return metaData;
	}

	internal static RequestMetaData RestoreHelperRequestMetaData()
	{
		var metaData = new RequestMetaData();
		metaData.AddRestoreHelper();
		return metaData;
	}
}
