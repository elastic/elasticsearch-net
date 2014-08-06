using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Core.Map.Transform
{
	[TestFixture]
	public class MappingTansformTests : BaseJsonTests
	{
		[Test]
		public void SingleTransform()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Transform(t => t
					.Script("if (ctx._source['title']?.startsWith('t')) ctx._source['suggest'] = ctx._source['content']")
					.Params(new Dictionary<string, string> { { "my-variable", "my-value" } })
					.Language("groovy")
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}

		[Test]
		public void MultipleTransformsTest()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.Transform(t => t
					.Script("ctx._source['suggest'] = ctx._source['content']")
				)
				.Transform(t => t
					.Script("ctx._source['foo'] = ctx._source['bar'];")
				)
			);
		}
	}
}
