using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class ParseQueryTests : BaseParserTests
	{
		private BaseQuery Query1 = Query<object>.Term("w", "x");
		private BaseQuery Query2 = Query<object>.Term("y", "z");
		private BaseQuery Query3 = Query<object>.Term("a", "b");

		[Test]
		public void Term_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.TermQueryDescriptor,
				f=>f.Term(p=>p.Name, "hello world")
			);
			q.Field.Should().Be("name");
			q.Value.Should().Be("hello world");
		}
		
		private T TestBaseQueryProperties<T>(
			Func<IQueryDescriptor, T> queryBaseDescriptor,
			Func<QueryDescriptor<ElasticsearchProject>, BaseQuery> create
			)
			where T : IQuery
		{
			var descriptor = this.GetSearchDescriptorForQuery(s=>s
				.Query(create)
			);
			var query = queryBaseDescriptor(descriptor.Query);
			query.Should().NotBeNull();
			return query;
		}
	}
}
