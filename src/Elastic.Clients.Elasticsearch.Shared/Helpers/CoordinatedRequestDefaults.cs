// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch;

internal static class CoordinatedRequestDefaults
{
	public static int BulkAllBackOffRetriesDefault = 0;
	public static TimeSpan BulkAllBackOffTimeDefault = TimeSpan.FromMinutes(1);
	public static int BulkAllMaxDegreeOfParallelismDefault = 4;
	public static int BulkAllSizeDefault = 1000;
	public static int ReindexBackPressureFactor = 4;
	public static int ReindexScrollSize = 500;
}
