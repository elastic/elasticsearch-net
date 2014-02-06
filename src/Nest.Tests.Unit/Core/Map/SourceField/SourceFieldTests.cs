using System;
using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.SourceField
{
	[TestFixture]
	public class SourceFieldTests : BaseJsonTests
	{
		[Test]
		public void SourceFieldSerializesFully()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.SourceField(s => s
					.SetDisabled()
					.SetCompression()
					.SetCompressionTreshold("200b")
					.SetExcludes(new[] { "path1.*" })
					.SetIncludes(new[] { "path2.*" })
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
