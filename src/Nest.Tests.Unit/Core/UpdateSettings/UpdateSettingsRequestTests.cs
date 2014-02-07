using System;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.UpdateSettings
{
	[TestFixture]
	public class UpdateSettingsRequestTests : BaseJsonTests
	{

		[Test]
		public void CustomIndexInferred()
		{
			var result = this._client.UpdateSettings(us=>us
				.Index<ElasticsearchProject>()
			);
			Assert.NotNull(result);
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/nest_test_data/_settings", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}

		[Test]
		public void CustomIndex()
		{
			var result = this._client.UpdateSettings(us=>us
				.Index("my_custom_index")
			);
			Assert.NotNull(result);
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/my_custom_index/_settings", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}

		[Test]
		public void AllIndices()
		{
			var result = this._client.UpdateSettings(us=>us
				.AllIndices()
			);
			Assert.NotNull(result);
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/_settings", uri.AbsolutePath);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}

		[Test]
		public void QueryStringOptionsMakeItUntoUrl()
		{
			var result = this._client.UpdateSettings(us=>us
				.AllIndices()
				.MasterTimeout("10s")
			);
			Assert.NotNull(result);
			var status = result.ConnectionStatus;
			var uri = new Uri(status.RequestUrl);
			Assert.AreEqual("/_settings", uri.AbsolutePath);
			Assert.AreEqual("?master_timeout=10s", uri.Query);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
		
	}
}
