// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.Serialization;

namespace Tests.Analysis.CharFilters
{
	using FuncTokenizer = Func<string, CharFiltersDescriptor, IPromise<ICharFilters>>;

	public class CharFilterTests
	{
		public class MappingTests : CharFilterAssertionBase<MappingTests>
		{
			public override FuncTokenizer Fluent => (n, cf) => cf.Mapping(n, c => c.Mappings("a=>b"));
			public override ICharFilter Initializer => new MappingCharFilter { Mappings = new[] { "a=>b" } };
			public override object Json => new { mappings = new[] { "a=>b" }, type = "mapping" };
			public override string Name => "mapping";
		}

		public class PatternReplaceTests : CharFilterAssertionBase<PatternReplaceTests>
		{
			public override FuncTokenizer Fluent => (n, cf) => cf.PatternReplace(n, c => c.Flags("CASE_INSENSITIVE").Pattern("x").Replacement("y"));
			public override ICharFilter Initializer => new PatternReplaceCharFilter { Flags = "CASE_INSENSITIVE", Pattern = "x", Replacement = "y" };
			public override object Json => new { flags = "CASE_INSENSITIVE", pattern = "x", replacement = "y", type = "pattern_replace" };
			public override string Name => "pr";
		}

		public class IcuNormalizerTests : CharFilterAssertionBase<IcuNormalizerTests>
		{
			public override FuncTokenizer Fluent => (n, cf) => cf
				.IcuNormalization(n, c => c
					.Mode(IcuNormalizationMode.Compose)
					.Name(IcuNormalizationType.CompatibilityCaseFold)
				);

			public override ICharFilter Initializer =>
				new IcuNormalizationCharFilter
				{
					Mode = IcuNormalizationMode.Compose,
					Name = IcuNormalizationType.CompatibilityCaseFold
				};

			public override object Json => new { mode = "compose", name = "nfkc_cf", type = "icu_normalizer" };
			public override string Name => "icunorm";
		}

		public class KuromojiIterationMarkTests : CharFilterAssertionBase<KuromojiIterationMarkTests>
		{
			public override FuncTokenizer Fluent =>
				(n, cf) => cf.KuromojiIterationMark("kmark", c => c.NormalizeKana().NormalizeKanji());

			public override ICharFilter Initializer =>
				new KuromojiIterationMarkCharFilter { NormalizeKana = true, NormalizeKanji = true };

			public override object Json => new
			{
				normalize_kanji = true,
				normalize_kana = true,
				type = "kuromoji_iteration_mark"
			};

			public override string Name => "kmark";
		}

		public class HtmlStripTests : CharFilterAssertionBase<HtmlStripTests>
		{
			public override FuncTokenizer Fluent => (n, cf) => cf.HtmlStrip(n);
			public override ICharFilter Initializer => new HtmlStripCharFilter();
			public override object Json => new { type = "html_strip" };
			public override string Name => "htmls";
		}

		public class UserDefinedCharFilterTests
		{
			public class UserDefinedCharFilter : CharFilterBase
			{
				public UserDefinedCharFilter(string type) : base(type)
				{
				}

				[DataMember(Name = "string_property")]
				public string StringProperty { get; set; }

				[DataMember(Name = "int_property")]
				public int? IntProperty { get; set; }
			}

			private static string FilterName => "user_defined";

			private static ICharFilter UserDefinedFilter => new UserDefinedCharFilter(FilterName) { StringProperty = "string", IntProperty = 1 };

			[U] public void Fluent() =>
				SerializationTestHelper.Expect(Json).FromRequest(c => c
					.Indices.Create("index", ci => ci
						.Settings(s => s
							.Analysis(a => a
								.CharFilters(ch => ch
									.UserDefined(FilterName, UserDefinedFilter)
								)
							)
						)
					)
				);

			[U] public void Initializer() =>
				SerializationTestHelper.Expect(Json).FromRequest(c => c
					.Indices.Create(new CreateIndexRequest("index")
						{
							Settings = new IndexSettings
							{
								Analysis = new Nest.Analysis
								{
									CharFilters = new Nest.CharFilters()
									{
										{ FilterName, UserDefinedFilter }
									}
								}
							}
						}
					)
				);

			private static object Json => new {
				settings = new {
					analysis = new {
						char_filter = new {
							user_defined = new {
								int_property = 1,
								string_property = "string",
								type = "user_defined"
							}
						}
					}
				}
			};

		}
	}
}
