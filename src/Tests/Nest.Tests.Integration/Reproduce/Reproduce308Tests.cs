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
			settings.Settings.Add("index.blocks.read_only", "true");

			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var idxRsp = this._client.CreateIndex(indexName, i=>i.InitializeUsing(settings));
			Assert.IsTrue(idxRsp.IsValid, idxRsp.ConnectionStatus.ToString());

			var getSettingsResponse = this._client.GetIndexSettings(i=>i.Index(indexName));
			Assert.IsTrue(getSettingsResponse.IsValid, getSettingsResponse.ConnectionStatus.ToString());

			bool readOnly = getSettingsResponse.IndexSettings._.index.blocks.read_only;
			readOnly.Should().BeTrue();



		}

	}
}
