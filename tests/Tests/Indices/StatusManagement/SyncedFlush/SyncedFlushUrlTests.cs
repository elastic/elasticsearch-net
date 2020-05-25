// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.SyncedFlush
{
	public class SyncedFlushUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_flush/synced")
					.Request(c => c.Indices.SyncedFlush(new SyncedFlushRequest()))
					.RequestAsync(c => c.Indices.SyncedFlushAsync(new SyncedFlushRequest()))
				;

			await POST($"/_all/_flush/synced")
					.Fluent(c => c.Indices.SyncedFlush(All))
					.Request(c => c.Indices.SyncedFlush(new SyncedFlushRequest(All)))
					.FluentAsync(c => c.Indices.SyncedFlushAsync(All))
					.RequestAsync(c => c.Indices.SyncedFlushAsync(new SyncedFlushRequest(All)))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_flush/synced")
					.Fluent(c => c.Indices.SyncedFlush(index))
					.Request(c => c.Indices.SyncedFlush(new SyncedFlushRequest(index)))
					.FluentAsync(c => c.Indices.SyncedFlushAsync(index))
					.RequestAsync(c => c.Indices.SyncedFlushAsync(new SyncedFlushRequest(index)))
				;
		}
	}
}
