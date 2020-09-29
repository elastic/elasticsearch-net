// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	//TODO 8.0 completely revisit how we mapped highlighters
	//this is used in tophits/percolator AND in search highligher as the root
	//Not all of these properties might make sense/valid there
	[InterfaceDataContract]
	[ReadAs(typeof(Highlight))]
	public interface IHighlight
	{
		/// <summary>
		/// Defines what constitutes a boundary for highlighting when using the fast vector highlighter.
		/// It's a single string with each boundary character defined in it. It defaults to .,!? \t\n.
		/// </summary>
		[DataMember(Name ="boundary_chars")]
		string BoundaryChars { get; set; }

		/// <summary>
		/// Controls how far to look for boundary characters. Defaults to 20.
		/// </summary>
		[DataMember(Name ="boundary_max_scan")]
		int? BoundaryMaxScan { get; set; }

		/// <summary>
		/// When highlighting a field using the unified highlighter or the fast vector highlighter, you can specify how to break the highlighted
		/// fragments using boundary_scanner
		/// </summary>
		[DataMember(Name ="boundary_scanner")]
		BoundaryScanner? BoundaryScanner { get; set; }

		/// <summary>
		/// You can further specify boundary_scanner_locale to control which Locale is used to search the text for these boundaries.
		/// </summary>
		[DataMember(Name ="boundary_scanner_locale")]
		string BoundaryScannerLocale { get; set; }

		/// <summary>
		/// Define how highlighted text will be encoded.
		/// It can be either default (no encoding) or html (will escape html, if you use html highlighting tags).
		/// </summary>
		[DataMember(Name ="encoder")]
		HighlighterEncoder? Encoder { get; set; }

		[DataMember(Name ="fields")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<Field, IHighlightField>))]
		Dictionary<Field, IHighlightField> Fields { get; set; }

		/// <summary>
		/// Fragmenter can control how text should be broken up in highlight snippets. However, this option is
		/// applicable only for the Plain Highlighter
		/// </summary>
		[DataMember(Name ="fragmenter")]
		HighlighterFragmenter? Fragmenter { get; set; }

		/// <summary>
		/// Controls the margin to start highlighting from when using the fast vector highlighter
		/// </summary>
		[DataMember(Name ="fragment_offset")]
		int? FragmentOffset { get; set; }

		/// <summary>
		/// The size of the highlighted fragment, in characters. Defaults to 100
		/// </summary>
		[DataMember(Name ="fragment_size")]
		int? FragmentSize { get; set; }

		/// <summary>
		/// The query to use for highlighting
		/// </summary>
		[DataMember(Name = "highlight_query")]
		QueryContainer HighlightQuery { get; set; }

		[DataMember(Name ="max_fragment_length")]
		int? MaxFragmentLength { get; set; }

		/// <summary>
		/// In the case where there is no matching fragment to highlight, the default is to not return anything. Instead, we can return a snippet of
		/// text from
		/// the beginning of the field by setting no_match_size (default 0) to the length of the text that you want returned. The actual length may be
		/// shorter or longer than specified as it tries to break on a word boundary.
		/// </summary>
		[DataMember(Name ="no_match_size")]
		int? NoMatchSize { get; set; }

		/// <summary>
		/// The maximum number of fragments to return. Defaults to 5.
		/// </summary>
		[DataMember(Name ="number_of_fragments")]
		int? NumberOfFragments { get; set; }

		/// <summary>
		/// The order in which highlighted fragments are sorted
		/// </summary>
		[DataMember(Name ="order")]
		HighlighterOrder? Order { get; set; }

		/// <summary>
		/// Controls the post tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		[DataMember(Name ="post_tags")]
		IEnumerable<string> PostTags { get; set; }

		/// <summary>
		/// Controls the pre tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		[DataMember(Name ="pre_tags")]
		IEnumerable<string> PreTags { get; set; }

		/// <summary>
		/// Use a specific "tag" schemas.
		/// </summary>
		/// <remarks>
		/// Currently a single schema called "styled" with the following pre_tags:
		/// &lt;em class="hlt1"&gt;, &lt;em class="hlt2"&gt;, &lt;em class="hlt3"&gt;,
		/// &lt;em class="hlt4"&gt;, &lt;em class="hlt5"&gt;, &lt;em class="hlt6"&gt;,
		/// &lt;em class="hlt7"&gt;, &lt;em class="hlt8"&gt;, &lt;em class="hlt9"&gt;,
		/// &lt;em class="hlt10"&gt;
		/// </remarks>
		[DataMember(Name ="require_field_match")]
		bool? RequireFieldMatch { get; set; }

		/// <summary>
		/// Use a specific "tag" schemas.
		/// </summary>
		/// <remarks>
		/// Currently a single schema called "styled" with the following pre_tags:
		/// &lt;em class="hlt1"&gt;, &lt;em class="hlt2"&gt;, &lt;em class="hlt3"&gt;,
		/// &lt;em class="hlt4"&gt;, &lt;em class="hlt5"&gt;, &lt;em class="hlt6"&gt;,
		/// &lt;em class="hlt7"&gt;, &lt;em class="hlt8"&gt;, &lt;em class="hlt9"&gt;,
		/// &lt;em class="hlt10"&gt;
		/// </remarks>
		[DataMember(Name ="tags_schema")]
		HighlighterTagsSchema? TagsSchema { get; set; }
	}

	public class Highlight : IHighlight
	{
		/// <inheritdoc/>
		public string BoundaryChars { get; set; }

		/// <inheritdoc/>
		public int? BoundaryMaxScan { get; set; }

		/// <inheritdoc/>
		public BoundaryScanner? BoundaryScanner { get; set; }

		/// <inheritdoc/>
		public string BoundaryScannerLocale { get; set; }

		/// <inheritdoc/>
		public HighlighterEncoder? Encoder { get; set; }

		/// <inheritdoc/>
		public Dictionary<Field, IHighlightField> Fields { get; set; }

		/// <inheritdoc/>
		public HighlighterFragmenter? Fragmenter { get; set; }

		/// <inheritdoc/>
		public int? FragmentOffset { get; set; }

		/// <inheritdoc/>
		public int? FragmentSize { get; set; }

		/// <inheritdoc/>
		public QueryContainer HighlightQuery { get; set; }

		/// <inheritdoc/>
		public int? MaxFragmentLength { get; set; }

		/// <inheritdoc/>
		public int? NoMatchSize { get; set; }

		/// <inheritdoc/>
		public int? NumberOfFragments { get; set; }

		/// <inheritdoc/>
		public HighlighterOrder? Order { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> PostTags { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> PreTags { get; set; }

		/// <inheritdoc/>
		public bool? RequireFieldMatch { get; set; }

		/// <inheritdoc/>
		public HighlighterTagsSchema? TagsSchema { get; set; }

		public static Highlight Field(Field field) => new Highlight
		{
			Fields = new Dictionary<Field, IHighlightField>
			{
				{ field, new HighlightField() }
			}
		};
	}

	public class HighlightDescriptor<T> : DescriptorBase<HighlightDescriptor<T>, IHighlight>, IHighlight
		where T : class
	{
		string IHighlight.BoundaryChars { get; set; }
		int? IHighlight.BoundaryMaxScan { get; set; }
		BoundaryScanner? IHighlight.BoundaryScanner { get; set; }
		string IHighlight.BoundaryScannerLocale { get; set; }
		HighlighterEncoder? IHighlight.Encoder { get; set; }
		Dictionary<Field, IHighlightField> IHighlight.Fields { get; set; }
		HighlighterFragmenter? IHighlight.Fragmenter { get; set; }
		int? IHighlight.FragmentOffset { get; set; }
		int? IHighlight.FragmentSize { get; set; }
		QueryContainer IHighlight.HighlightQuery { get; set; }
		int? IHighlight.MaxFragmentLength { get; set; }
		int? IHighlight.NoMatchSize { get; set; }
		int? IHighlight.NumberOfFragments { get; set; }
		HighlighterOrder? IHighlight.Order { get; set; }
		IEnumerable<string> IHighlight.PostTags { get; set; }

		IEnumerable<string> IHighlight.PreTags { get; set; }
		bool? IHighlight.RequireFieldMatch { get; set; }
		HighlighterTagsSchema? IHighlight.TagsSchema { get; set; }

		/// <inheritdoc cref="IHighlight.Fields" />
		public HighlightDescriptor<T> Fields(params Func<HighlightFieldDescriptor<T>, IHighlightField>[] fieldHighlighters) =>
			Assign(fieldHighlighters, (a, v) => a.Fields = v?
				.Select(f =>
					f(new HighlightFieldDescriptor<T>())
						.ThrowWhen(p => p.Field == null, "Could not infer key for highlight field descriptor")
				)
				.ToDictionary(k => k.Field)
				.NullIfNoKeys()
			);

		/// <inheritdoc cref="IHighlight.TagsSchema" />
		public HighlightDescriptor<T> TagsSchema(HighlighterTagsSchema? schema) => Assign(schema, (a, v) => a.TagsSchema = v);

		/// <inheritdoc cref="IHighlight.PreTags" />
		public HighlightDescriptor<T> PreTags(params string[] preTags) => Assign(preTags.ToListOrNullIfEmpty(), (a, v) => a.PreTags = v);

		/// <inheritdoc cref="IHighlight.PostTags" />
		public HighlightDescriptor<T> PostTags(params string[] postTags) => Assign(postTags.ToListOrNullIfEmpty(), (a, v) => a.PostTags = v);

		/// <inheritdoc cref="IHighlight.PreTags" />
		public HighlightDescriptor<T> PreTags(IEnumerable<string> preTags) => Assign(preTags.ToListOrNullIfEmpty(), (a, v) => a.PreTags = v);

		/// <inheritdoc cref="IHighlight.PostTags" />
		public HighlightDescriptor<T> PostTags(IEnumerable<string> postTags) => Assign(postTags.ToListOrNullIfEmpty(), (a, v) => a.PostTags = v);

		/// <inheritdoc cref="IHighlight.FragmentSize" />
		public HighlightDescriptor<T> FragmentSize(int? fragmentSize) => Assign(fragmentSize, (a, v) => a.FragmentSize = v);

		/// <inheritdoc cref="IHighlight.HighlightQuery" />
		public HighlightDescriptor<T> HighlightQuery(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(query, (a, v) => a.HighlightQuery = v?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="IHighlight.NumberOfFragments" />
		public HighlightDescriptor<T> NumberOfFragments(int? numberOfFragments) => Assign(numberOfFragments, (a, v) => a.NumberOfFragments = v);

		/// <inheritdoc cref="IHighlight.FragmentOffset" />
		public HighlightDescriptor<T> FragmentOffset(int? fragmentOffset) => Assign(fragmentOffset, (a, v) => a.FragmentOffset = v);

		/// <inheritdoc cref="IHighlight.Encoder" />
		public HighlightDescriptor<T> Encoder(HighlighterEncoder? encoder) => Assign(encoder, (a, v) => a.Encoder = v);

		/// <inheritdoc cref="IHighlight.Order" />
		public HighlightDescriptor<T> Order(HighlighterOrder? order) => Assign(order, (a, v) => a.Order = v);

		/// <inheritdoc cref="IHighlight.RequireFieldMatch" />
		public HighlightDescriptor<T> RequireFieldMatch(bool? requireFieldMatch = true) => Assign(requireFieldMatch, (a, v) => a.RequireFieldMatch = v);

		/// <inheritdoc cref="IHighlight.BoundaryChars" />
		public HighlightDescriptor<T> BoundaryChars(string boundaryChars) => Assign(boundaryChars, (a, v) => a.BoundaryChars = v);

		/// <inheritdoc cref="IHighlight.BoundaryMaxScan" />
		public HighlightDescriptor<T> BoundaryMaxScan(int? boundaryMaxScan) => Assign(boundaryMaxScan, (a, v) => a.BoundaryMaxScan = v);

		/// <inheritdoc cref="IHighlight.MaxFragmentLength" />
		public HighlightDescriptor<T> MaxFragmentLength(int? maxFragmentLength) => Assign(maxFragmentLength, (a, v) => a.MaxFragmentLength = v);

		/// <inheritdoc cref="IHighlight.NoMatchSize" />
		public HighlightDescriptor<T> NoMatchSize(int? noMatchSize) => Assign(noMatchSize, (a, v) => a.NoMatchSize = v);

		/// <inheritdoc cref="IHighlight.BoundaryScanner" />
		public HighlightDescriptor<T> BoundaryScanner(BoundaryScanner? boundaryScanner) => Assign(boundaryScanner, (a, v) => a.BoundaryScanner = v);

		/// <inheritdoc cref="IHighlight.BoundaryScannerLocale" />
		public HighlightDescriptor<T> BoundaryScannerLocale(string locale) => Assign(locale, (a, v) => a.BoundaryScannerLocale = v);

		/// <inheritdoc cref="IHighlight.Fragmenter" />
		public HighlightDescriptor<T> Fragmenter(HighlighterFragmenter? fragmenter) => Assign(fragmenter, (a, v) => a.Fragmenter = v);
	}
}
