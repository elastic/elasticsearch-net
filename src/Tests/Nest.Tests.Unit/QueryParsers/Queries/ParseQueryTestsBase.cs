using System;
using System.Diagnostics;
using FluentAssertions;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	public abstract class ParseQueryTestsBase : BaseParserTests
	{
		protected QueryContainer Query1 = Query<object>.Term("w", "x");
		protected QueryContainer Query2 = Query<object>.Term("y", "z");
		protected QueryContainer Query3 = Query<object>.Term("a", "b");
		
		protected FilterContainer Filter1 = Filter<object>.Term("w", "x");
		protected FilterContainer Filter2 = Filter<object>.Term("y", "z");
		protected FilterContainer Filter3 = Filter<object>.Term("a", "b");
		
		protected T SerializeThenDeserialize<T>(
			Func<IQueryContainer, T> queryBaseDescriptor,
			Func<QueryDescriptor<ElasticsearchProject>, QueryContainer> create
			)
			where T : IQuery
		{
			var descriptor = this.GetSearchDescriptorForQuery(s=>s
				.Query(create)
			);
			descriptor.Should().NotBeNull();
			var query = queryBaseDescriptor(descriptor.Query);
			query.Should().NotBeNull();
			//we serialize once more to be sure reusing IQueries in other calls stay the same
			// serialize => deserialize => serialize => deserialize => test properties
			var sw = Stopwatch.StartNew();
			descriptor = this.GetSearchDescriptorForQuery(s => s.Query(descriptor.Query));
			sw.Stop();
			//deserialize/serialize descriptors should be fast for all descriptors 
			sw.ElapsedMilliseconds.Should().BeLessOrEqualTo(20, "(de)?serializing taking a bit too long whats going on here?");
			query = queryBaseDescriptor(descriptor.Query);
			query.Should().NotBeNull();
			return query;
		}
	}
}
