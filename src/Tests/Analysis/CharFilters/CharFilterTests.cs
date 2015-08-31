using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Property;

namespace Tests
{
	public class CharFilterTests
	{
		/**
		 */

		public class Usage : GeneralUsageBase<IIndexSettings, IndexSettingsDescriptor, IndexSettings>
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
			protected override Func<IndexSettingsDescriptor, IIndexSettings> Fluent => s => s
				.Analysis(a => a
					.CharFilters(charfilters => charfilters
						.HtmlStrip("stripMe")
						.PatternReplace("patterned", c => c.Pattern("x").Replacement("y"))
						.Mapping("mapped", c => c.Mappings("a=>b"))
					)
				);

			/**
			 */
			protected override IndexSettings Initializer =>
				new IndexSettings
				{
					Analysis = new Analysis
					{
						CharFilters = new CharFilters
						{
							{ "stripMe", new HtmlStripCharFilter { } },
							{ "patterned", new PatternReplaceCharFilter { Pattern = "x", Replacement = "y" } },
							{ "mapped", new MappingCharFilter { Mappings = new [] { "a=>b"} } }
						}
					}
				};
		}
	}
}
