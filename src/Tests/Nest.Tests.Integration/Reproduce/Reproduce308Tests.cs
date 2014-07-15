using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
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
			settings.NumberOfReplicas = 1;
			settings.NumberOfShards = 5;
			settings.Settings.Add("index.blocks.read_only", "true");

			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var idxRsp = this.Client.CreateIndex(indexName, i=>i.InitializeUsing(settings));
			Assert.IsTrue(idxRsp.IsValid, idxRsp.ConnectionStatus.ToString());

			var getSettingsResponse = this.Client.GetIndexSettings(i=>i.Index(indexName));
			Assert.IsTrue(getSettingsResponse.IsValid, getSettingsResponse.ConnectionStatus.ToString());

			bool readOnly = getSettingsResponse.IndexSettings.AsExpando.index.blocks.read_only;
			readOnly.Should().BeTrue();



		}

	}
}
