using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce876Tests : BaseJsonTests
	{
		[Test]
		public void Issue876()
		{
			var result = new SearchDescriptor<ElasticsearchProject>()
				.Query(q => q
					.Verbatim()
					.Match(m => m
						.OnField(p => p.Name)
						.Query(string.Empty)
					)
				);

			this.JsonEquals(result, MethodInfo.GetCurrentMethod());
		}
	}
}
