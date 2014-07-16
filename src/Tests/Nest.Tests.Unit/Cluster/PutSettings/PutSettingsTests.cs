using System;
using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Cluster.PutSettings
{
	[TestFixture]
	public class PutSettingsTests : BaseJsonTests
	{
		[Test]
		public void Url_Should_Be_Correct()
		{
			var r = this._client.ClusterSettings(new ClusterSettingsRequest());
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/settings");
			r.ConnectionStatus.RequestMethod.Should().Contain("PUT");
		}

		[Test]
		public void InitializerJson()
		{
			var r = this._client.ClusterSettings(new ClusterSettingsRequest
			{
				Persistent = new Dictionary<string, object>
				{
					{ "persistent.config.value", 2}
				},
				Transient = new Dictionary<string, object>
				{
					{ "transient.value", "string" }
				}
			});

			this.JsonEquals(r.ConnectionStatus.Request.Utf8String(), MethodBase.GetCurrentMethod(), "PutSettings");
		}

		[Test]
		public void DescriptorJson()
		{
			var r = this._client.ClusterSettings(s => s
				.Persistent(p=>p
					.Add("persistent.config.value", 2)
				)
				.Transient(p=>p
					.Add("transient.value", "string")
				)
			);	
			this.JsonEquals(r.ConnectionStatus.Request.Utf8String(), MethodBase.GetCurrentMethod(), "PutSettings");
		}
		
	}
}