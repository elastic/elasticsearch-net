// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal static class RequestMetaDataExtensions
{
	internal static void AddSnapshotHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.SnapshotHelper);

	internal static void AddScrollHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.ScrollHelper);

	internal static void AddReindexHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.ReindexHelper);

	internal static void AddBulkHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.BulkHelper);

	internal static void AddRestoreHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.RestoreHelper);
}

