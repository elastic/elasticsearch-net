using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Versioning
{
	[TestFixture]
	public class VersioningTests : BaseJsonTests
	{
		[Test]
		public void IndexSupportsVersioning()
		{
			var o = new ElasticsearchProject { Id = 1, Name = "Test" };
			var result = this._client.Index(o, i=>i.Version(1));
			var status = result.ConnectionStatus;
			StringAssert.Contains("version=1", status.RequestUrl);
		}

        [Test]
        public void IndexOpTypeDefault()
        {
            var o = new ElasticsearchProject { Id = 1, Name = "Test" };
            var result = this._client.Index(o);
            var status = result.ConnectionStatus;
            StringAssert.DoesNotContain("op_type=create", status.RequestUrl);
        }

        [Test]
        public void IndexOpTypeCreate()
        {
            var o = new ElasticsearchProject { Id = 1, Name = "Test" };
            var result = this._client.Index(o, i => i.OpType(OpTypeOptions.Create));
            var status = result.ConnectionStatus;
            StringAssert.Contains("op_type=create", status.RequestUrl);
        }
	}
}
