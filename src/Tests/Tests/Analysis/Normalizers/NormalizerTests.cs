using System;
using Nest;

namespace Tests.Analysis.Normalizers
{
	using FuncTokenizer = Func<string, NormalizersDescriptor, IPromise<INormalizers>>;

	public class NormalizerTests
	{
		public class CustomTests : NormalizerAssertionBase<CustomTests>
		{
			protected override string Name => "myCustom";

			protected override INormalizer Initializer => new CustomNormalizer
			{
				CharFilter = new[] {"mapped"},
				Filter = new[] {"lowercase", "asciifolding"},
			};

			protected override FuncTokenizer Fluent => (n, an) => an
				.Custom("myCustom", a => a
					.Filters("lowercase", "asciifolding")
					.CharFilters("mapped")
				);

			protected override object Json => new
			{
				type = "custom",
				filter = new[] {"lowercase", "asciifolding"},
				char_filter = new[] {"mapped"}
			};
		}

	}
}
