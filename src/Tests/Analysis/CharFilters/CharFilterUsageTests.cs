using System;
using Nest;
using Tests.Framework;

namespace Tests.Analysis.CharFilters
{
	public class CharFilterUsageTests : PromiseUsageTestBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
	{
		protected override object ExpectJson => new
		{
			analysis = new
			{
				char_filter = new
				{
					stripMe = new { type = "html_strip" },
					patterned = new
					{
						pattern = "x",
						replacement = "y",
						type = "pattern_replace"
					},
					mapped = new
					{
						mappings = new[] { "a=>b" },
						type = "mapping"
					}
				}
			}
		};


		/**
		 * 
		 */
		protected override Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> Fluent => FluentExample;
		public static Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> FluentExample => s => s
			.Analysis(a => a
				.CharFilters(charfilters => charfilters
					.HtmlStrip("stripMe")
					.PatternReplace("patterned", c => c.Pattern("x").Replacement("y"))
					.Mapping("mapped", c => c.Mappings("a=>b"))
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
					CharFilters = new Nest.CharFilters
					{
							{ "stripMe", new HtmlStripCharFilter { } },
							{ "patterned", new PatternReplaceCharFilter { Pattern = "x", Replacement = "y" } },
							{ "mapped", new MappingCharFilter { Mappings = new [] { "a=>b"} } }
					}
				}
			};
	}
}
