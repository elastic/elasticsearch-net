using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;
using System;
using FluentAssertions;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class EscapedFormatTests
	{
		[Test]
		public void ShouldBeAbleToUseUrlsAsId()
		{
			var formattedId = "{0}".EscapedFormat("http://www.gmail.com");
			Assert.AreEqual("http%3A%2F%2Fwww.gmail.com", formattedId);
		}
		[Test]
		public void EscapeInvalidUrlCharacters()
		{
            //NOTE Mono escapes ! using Uri.EscapeDataString .NET does not.
			var formattedId = "{0}".EscapedFormat("../../@#& {}|<>?=/hello");
			Assert.AreEqual("..%2F..%2F%40%23%26%20%7B%7D%7C%3C%3E%3F%3D%2Fhello", formattedId);
		}		
	}
}