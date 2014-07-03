using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentAssertions;
using Nest;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Snapshots
{
	[TestFixture]
	public class RestoreRequestTests : BaseJsonTests
	{
		[Test]
		public void Url()
		{
			var response = this._client.Restore(new RestoreRequest("repos", "snap")
			{
				IgnoreUnavailable = true,
				IncludeGlobalState = true,
				WaitForCompletion = true
			});

			var status = response.ConnectionStatus;
			var url = status.RequestUrl;
			url.Should().EndWith("/_snapshot/repos/snap/_restore?wait_for_completion=true");
			status.RequestMethod.Should().Be("POST");
		}
		
		[Test]
		public void RestoreBody()
		{
			var response = this._client.Restore(new RestoreRequest("repos", "snap")
			{
				IgnoreUnavailable = true,
				IncludeGlobalState = true,
				WaitForCompletion = true,
				RenamePattern = "index_(.+)",
				RenameReplacement = "index_restored_$1"
			});
			var status = response.ConnectionStatus;

			this.JsonEquals(status.Request, MethodBase.GetCurrentMethod());
		}
	}
}
