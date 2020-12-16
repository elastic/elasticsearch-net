// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net;

namespace Nest
{
	internal static class RequestMetaDataExtensions
	{
		internal static void AddHelper(this RequestMetaData metaData, string helperValue)
		{
			if (!metaData.TryAddMetaData(RequestMetaData.HelperKey, helperValue))
				throw new InvalidOperationException("A helper value has already been added.");
		}

		internal static void AddSnapshotHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.SnapshotHelper);

		internal static void AddScrollHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.ScrollHelper);

		internal static void AddReindexHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.ReindexHelper);

		internal static void AddBulkPushHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.BulkPushHelper);

		internal static void AddRestoreHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.RestoreHelper);
	}

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

		internal static RequestMetaData BulkPushHelperRequestMetaData()
		{
			var metaData = new RequestMetaData();
			metaData.AddBulkPushHelper();
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
}
