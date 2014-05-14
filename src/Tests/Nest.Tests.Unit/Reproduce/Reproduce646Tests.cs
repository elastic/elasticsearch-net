using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net.Connection;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce646Tests : BaseJsonTests
	{
		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/646
		/// </summary>
		[Test]
		public void Issue646()
		{
			var client = new ElasticClient(connection: new InMemoryConnection());
			Assert.DoesNotThrow(()=>
			{
				var result = _client.DeleteByQuery<object>(dq => dq
					.Index("my-custom-index")
					.AllTypes()
					.Query(q => q
						.Filtered(descriptor => descriptor
							.Filter(filterDescriptor => filterDescriptor
								.Not(descriptor1 => descriptor1
									.Term("indexProcessId", "1")
								)
							)
						)
					)
				);	
			});
			
		}

	}
}
