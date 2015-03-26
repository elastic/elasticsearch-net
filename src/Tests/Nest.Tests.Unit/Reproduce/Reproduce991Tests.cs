using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce991Tests : BaseJsonTests
	{
		private class MyClass
		{
			public MyEnum MyEnum { get; set; }
		}

		private enum MyEnum
		{
			Value1,
			Value2
		}

		[Test]
		public void EnumQueryDefaultsToInt()
		{
			var query = new SearchDescriptor<MyClass>()
				.Query(q => q.Term(p => p.MyEnum, MyEnum.Value2));
			this.JsonEquals(query, MethodBase.GetCurrentMethod());
		}
	}
}
