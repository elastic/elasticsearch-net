using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Repository
{
	public class RestoreTests : BaseJsonTests
	{
		[Test]
		public void Restore()
		{
			var restoreResponse = _client.Restore("repository", "snapshotName",
				descriptor => descriptor
					.Index("index")
					.IndexSettings(settingsDescriptor => settingsDescriptor
						.NumberOfReplicas(0)
						.BlocksRead())
					.IgnoreIndexSettings(UpdatableSettings.RefreshInterval, UpdatableSettings.AutoExpandReplicas));

			restoreResponse.ConnectionStatus.RequestUrl.Should().Be("http://localhost:9200/_snapshot/repository/snapshotName/_restore");
			this.JsonEquals(restoreResponse.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Null_IgnoreIndexSettings_ThorwException()
		{
			List<string> ignoreIndexSettings = null;
			_client.Restore("repository", "snapshotName",
				descriptor => descriptor
					.Index("index")
					.IgnoreIndexSettings(ignoreIndexSettings));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Null_IndexSettings_ThorwException()
		{
			Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> settingsSelector = null;

			var restoreResponse = _client.Restore("repository", "snapshotName",
				descriptor => descriptor
					.Index("index")
					.IndexSettings(settingsSelector));
		}
	}
}
