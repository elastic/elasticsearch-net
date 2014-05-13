using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce654Tests : IntegrationTests
	{

		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/654
		/// </summary>
		[Test]
		public async void AsyncMethodsShouldNotThrowOnHttpError()
		{
			Assert.DoesNotThrow(async () =>
			{
				var searchResult = await this._client.DeleteAsync("index", "type", "id");
			});
		}
		[Test]
		public void SyncMethodsShouldNotThrowOnHttpError()
		{
			Assert.DoesNotThrow(() =>
			{
				var searchResult = this._client.Delete("index", "type", "id");
			});
		}
	}
}
