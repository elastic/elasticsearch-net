using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Fields
{
	[TestFixture]
	public class FieldsTests : BaseJsonTests
	{
		public class BaseClass
		{
			public string Id { get; set; }
			public string Lang { get; set; }
		}

		public class ClassA : BaseClass { }
		public class ClassB : BaseClass { }
		public class ClassC : BaseClass { }
		public class ClassD : BaseClass { }

		[Test]
		public void FieldsSelectionIsCovariantAsWell()
		{
			var settings = new ConnectionSettings(Test.Default.Uri, "default-index")
				.ExposeRawResponse();
			var file = this.GetFileFromMethod(MethodInfo.GetCurrentMethod(), "FixedCovariantSearchResult");
			var jsonResponse = File.ReadAllText(file);
			var connection = new InMemoryConnection(this._settings, jsonResponse);
			var client = new ElasticClient(settings, connection);

			var results = client.Search<BaseClass>(s => s
				.Types(typeof(ClassA),typeof(ClassB),typeof(ClassC),typeof(ClassD))
				.Fields(p=>p.Lang)
			);

			results.Total.Should().Be(1605);

			results.Hits.Should().NotBeNull().And.HaveCount(10);
			var classAHits = results.Hits.OfType<Hit<ClassA>>();
			classAHits.Should().NotBeNull().And.HaveCount(3);

			var classAHit = classAHits.First();
			classAHit.Fields.Should().NotBeNull();
			var lang = classAHit.Fields.FieldValue<ClassA, string>(p => p.Lang).FirstOrDefault();

			
		}

	}
}
