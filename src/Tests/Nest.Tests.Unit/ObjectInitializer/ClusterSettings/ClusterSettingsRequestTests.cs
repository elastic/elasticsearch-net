using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.ClusterSettings
{
	[TestFixture]
	public class ClusterSettingsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public ClusterSettingsRequestTests()
		{
			var request = new ClusterSettingsRequest()
			{
				FlatSettings = true,
				Transient = new Dictionary<string, object>
				{
					{"transient_setting", false}
				},
				Persistent = new Dictionary<string, object>
				{
					{"persistent_setting", 1}
				},
			};
			var response = this._client.ClusterSettings(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_cluster/settings?flat_settings=true");
			this._status.RequestMethod.Should().Be("PUT");
		}
		
		[Test]
		public void ClusterSettingsBody()
		{
			this.JsonEquals(this._status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
