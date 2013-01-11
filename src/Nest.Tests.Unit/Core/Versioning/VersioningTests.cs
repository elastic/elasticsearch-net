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
			var o = new ElasticSearchProject { Id = 1, Name = "Test" };
			var result = this._client.Index(o, new IndexParameters { Version = "1" });
			var status = result.ConnectionStatus;
			StringAssert.Contains("version=1", status.RequestUrl);
		}

        [Test]
        public void IndexOpTypeDefault()
        {
            var o = new ElasticSearchProject { Id = 1, Name = "Test" };
            var result = this._client.Index(o, new IndexParameters());
            var status = result.ConnectionStatus;
            StringAssert.DoesNotContain("op_type=create", status.RequestUrl);
        }

        [Test]
        public void IndexOpTypeNone()
        {
            var o = new ElasticSearchProject { Id = 1, Name = "Test" };
            var result = this._client.Index(o, new IndexParameters { OpType = OpType.None });
            var status = result.ConnectionStatus;
            StringAssert.DoesNotContain("op_type=create", status.RequestUrl);
        }

        [Test]
        public void IndexOpTypeCreate()
        {
            var o = new ElasticSearchProject { Id = 1, Name = "Test" };
            var result = this._client.Index(o, new IndexParameters { OpType = OpType.Create });
            var status = result.ConnectionStatus;
            StringAssert.Contains("op_type=create", status.RequestUrl);
        }
	}
}
