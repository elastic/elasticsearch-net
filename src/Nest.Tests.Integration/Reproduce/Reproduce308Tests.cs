using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce308Tests : IntegrationTests
	{

		/// <summary>
		///	https://github.com/Mpdreamz/NEST/issues/308
		/// </summary>
		[Test]
		public void ShouldBeAbleToSetIndexToReadonly()
		{
			var settings = new IndexSettings();
			settings.Similarity = new SimilaritySettings();
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Add("index.blocks.read_only", "true");

			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var idxRsp = this._client.CreateIndex(indexName, settings);
			Assert.IsTrue(idxRsp.IsValid, idxRsp.ConnectionStatus.ToString());

			var getSettingsResponse = this._client.GetIndexSettings(indexName);
			Assert.IsTrue(getSettingsResponse.IsValid, getSettingsResponse.ConnectionStatus.ToString());

			getSettingsResponse.Settings.Should().ContainKey("index.blocks.read_only");
			getSettingsResponse.Settings["index.blocks.read_only"].Should().Be("true");



		}

	}
}
