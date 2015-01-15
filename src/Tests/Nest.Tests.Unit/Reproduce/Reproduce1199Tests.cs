using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FakeItEasy;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce1199Tests : BaseJsonTests
	{
		public class X
		{
			private string _LastName;
			public string FirstName { get; set; }
			public string LastName { get { return _LastName; } set { _LastName = value; }}
		}

		[Test]
		public void Issue1199()
		{
			var result = this._client.CreateIndex(i => i
				.Index("someindex")
				.AddMapping<X>(m=>m
					.MapFromAttributes()
				)
			);
			var mapping = result.ConnectionStatus.Request.Utf8String();
			this.JsonEquals(mapping, MethodBase.GetCurrentMethod(), "Issue1199Mapping");

			var indexResult = this._client.Index(new X {FirstName = "x", LastName = "y"}, i => i.Index("someindex"));
			var index = indexResult.ConnectionStatus.Request.Utf8String();
			this.JsonEquals(index, MethodBase.GetCurrentMethod(), "Issue1199Index");
		}
	}
}
