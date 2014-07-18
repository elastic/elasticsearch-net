using NUnit.Framework;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Indices.Analysis.Tokenizers
{
	[TestFixture]
	public class TokenizerTests : BaseAnalysisTests
	{
		[Test]
		public void TestTokenizers()
		{
			var result = this.Analysis(a => a
				.Tokenizers(t => t
					.Add("standard", new StandardTokenizer
					{
						MaximumTokenLength = 90,
						Version = "3.0"
					})
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
	}
}
