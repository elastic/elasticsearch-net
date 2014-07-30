using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Reproduce
{
	[ElasticType(Name="mypoco", IdProperty="Code")]
	public class MyPoco
	{
		[ElasticProperty(Name="code")]
		public string Code { get; set; }
	}

	[TestFixture]
	public class Reproduce806Tests : BaseJsonTests
	{
		[Test]
		public void IdPropertyAttributeCausesArgumentNullException()
		{
			var r = _client.Update<MyPoco>(d => d.DocAsUpsert().Doc(new MyPoco { Code = "1" }));
		}
	}
}
