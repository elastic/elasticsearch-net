/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

		internal static void AddBulkHelper(this RequestMetaData metaData) => metaData.AddHelper(HelperIdentifiers.BulkHelper);

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
}
