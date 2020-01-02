using System;
using Nest;

namespace Tests.Analysis.Normalizers
{
	using FuncTokenizer = Func<string, NormalizersDescriptor, IPromise<INormalizers>>;

	public class NormalizerTests
	{
		public class CustomTests : NormalizerAssertionBase<CustomTests>
		{
			public override FuncTokenizer Fluent => (n, an) => an
				.Custom("myCustom", a => a
					.Filters("lowercase", "asciifolding")
				);

			public override INormalizer Initializer => new CustomNormalizer
			{
				Filter = new[] { "lowercase", "asciifolding" },
			};

			public override object Json => new
			{
				type = "custom",
				filter = new[] { "lowercase", "asciifolding" },
			};

			public override string Name => "myCustom";
		}
	}
}
