using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce928Tests : BaseJsonTests
	{
		[Test]
		public void Issue928()
		{
			var result = this._client.Search<ElasticsearchProject>(s => s
				.Filter(f => f
					.Bool(bf => bf
						.Must(
							mf => mf.Cache(true).Query(q => q.Term("field1", "value"))
							,mf => mf.Cache(true).Query(q => q.Term("field2", "value"))
						)
					)
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod());
		}

		[Test]
		public void Issue928_And()
		{
			var result = this._client.Search<ElasticsearchProject>(s => s
				.Filter(f => 
					f.Cache(true).Query(q => q.Term("field1", "value"))
					&& f.Cache(true).Query(q => q.Term("field2", "value"))
				)
			);

			this.JsonEquals(result.ConnectionStatus.Request, MethodBase.GetCurrentMethod(), "Issue928");
		}
	}
}
