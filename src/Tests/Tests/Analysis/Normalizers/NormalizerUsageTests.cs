using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.Analysis.Normalizers
{
	/**
	 */

	[SkipVersion("<5.2.0", "Normalizers are a new 5.2.0 feature")]
	public class NormalizerUsageTests : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
	{
		protected override object ExpectJson => new
		{
			analysis = new
			{
				normalizer = new
				{
					myCustom = new
					{
						type = "custom",
						filter = new[] {"lowercase", "asciifolding"},
						char_filter = new[] {"mapped"}
					}
				}
			}
		};

		/**
		 *
		 */
		protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => FluentExample;

		public static Func<IndexSettingsDescriptor, IPromise<IndexSettings>> FluentExample => s => s
			.Analysis(analysis => analysis
				.Normalizers(analyzers => analyzers
					.Custom("myCustom", a => a
						.Filters("lowercase", "asciifolding")
						.CharFilters("mapped")
					)
				)
			);

		/**
		 */
		protected override IndexSettings Initializer => InitializerExample;

		public static IndexSettings InitializerExample =>
			new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Normalizers = new Nest.Normalizers
					{
						{
							"myCustom", new CustomNormalizer
							{
								CharFilter = new[] {"mapped"},
								Filter = new[] {"lowercase", "asciifolding"},
							}
						}
					}
				}
			};
	}
}
