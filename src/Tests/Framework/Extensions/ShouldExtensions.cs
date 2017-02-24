using FluentAssertions;
using Nest_5_2_0;

namespace Tests.Framework
{
	public static class ShouldExtensions
	{
		public static void ShouldHaveExpectedIsValid(this IResponse response, bool expectedIsValid) =>
			response.IsValid.Should().Be(expectedIsValid, "{0}", response.DebugInformation);

		public static void ShouldBeValid(this IResponse response) =>
			response.IsValid.Should().BeTrue("{0}", response.DebugInformation);

		public static void ShouldNotBeValid(this IResponse response) =>
			response.IsValid.Should().BeFalse("{0}", response.DebugInformation);
	}
}
