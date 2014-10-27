using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class IndexNameResolverTests : BaseJsonTests
	{
		[Test]
		public void Uppercase_Characters_AreNotAllowed()
		{
			var e = Assert.Throws<DslException>(() => this._client.CreateIndex(c => c
				.Index("Hello")
			));
			e.Message.Should().Contain("contains uppercase characters");
		}

		[Test]
		public void SanityCheckCharacters()
		{
			var e = Assert.Throws<DslException>(() => this._client.CreateIndex(c => c
				.Index("rel>lo")
			));
			e.Message.Should().Contain("contains one of");
		}

		[Test]
		public void IndexNameMayNotStartWithUnderscore()
		{
			var e = Assert.Throws<DslException>(() => this._client.CreateIndex(c => c
				.Index("_hello")
			));
			e.Message.Should().Contain("may not start with an underscore");
		}

		[Test]
		public void MaxLengthCheck()
		{
			var indexName = new String('a', 300);

			var e = Assert.Throws<DslException>(() => this._client.CreateIndex(c => c
				.Index(indexName)
			));
			e.Message.Should().Contain(indexName);
			e.Message.Should().Contain("exceeds maximum");
		}

		[Test]
		public void ValidIndexNameShouldNotThrow()
		{
			Assert.DoesNotThrow(() => this._client.CreateIndex(c => c
				.Index("index_name")
			));
		}

	}
}
