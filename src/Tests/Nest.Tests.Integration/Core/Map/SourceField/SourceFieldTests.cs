using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.SourceField
{
	[TestFixture]
	public class SourceFieldTests : BaseMappingTests
	{
		[Test]
		public void SourceFieldSerializesFully()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.SourceField(s => s
					.SetDisabled()
					.SetCompression()
					.SetCompressionTreshold("200b")
					.SetExcludes(new[] { "path1.*" })
					.SetIncludes(new[] { "path2.*" })
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
