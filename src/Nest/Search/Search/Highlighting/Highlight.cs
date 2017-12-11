using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	//TODO 6.0 completely revisit how we mapped highlighters
	//this is used in tophits/percolator AND in search highligher as the root
	//Not all of these properties might make sense/valid there
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Highlight>))]
	public interface IHighlight
	{
		/// <summary>
		/// Controls the pre tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		[JsonProperty("pre_tags")]
		IEnumerable<string> PreTags { get; set; }

		/// <summary>
		/// Controls the post tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		[JsonProperty("post_tags")]
		IEnumerable<string> PostTags { get; set; }

		/// <summary>
		/// The size of the highlighted fragment, in characters. Defaults to 100
		/// </summary>
		[JsonProperty("fragment_size")]
		int? FragmentSize { get; set; }

		/// <summary>
		/// In the case where there is no matching fragment to highlight, the default is to not return anything. Instead, we can return a snippet of text from
		/// the beginning of the field by setting no_match_size (default 0) to the length of the text that you want returned. The actual length may be
		/// shorter or longer than specified as it tries to break on a word boundary.
		/// </summary>
		[JsonProperty("no_match_size")]
		int? NoMatchSize { get; set; }

		/// <summary>
		/// The maximum number of fragments to return. Defaults to 5.
		/// </summary>
		[JsonProperty("number_of_fragments")]
		int? NumberOfFragments { get; set; }

		/// <summary>
		/// Controls the margin to start highlighting from when using the fast vector highlighter
		/// </summary>
		[JsonProperty("fragment_offset")]
		int? FragmentOffset { get; set; }

		/// <summary>
		/// Controls how far to look for boundary characters. Defaults to 20.
		/// </summary>
		[JsonProperty("boundary_max_scan")]
		int? BoundaryMaxScan { get; set; }

		/// <summary>
		/// Define how highlighted text will be encoded.
		/// It can be either default (no encoding) or html (will escape html, if you use html highlighting tags).
		/// </summary>
		[JsonProperty("encoder")]
		HighlighterEncoder? Encoder { get; set; }

		/// <summary>
		/// The order in which highlighted fragments are sorted
		/// </summary>
		[JsonProperty("order")]
		HighlighterOrder? Order { get; set; }

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
		[JsonProperty("tags_schema")]
		HighlighterTagsSchema? TagsSchema { get; set; }

		[JsonProperty("fields")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Field, IHighlightField>))]
		Dictionary<Field, IHighlightField> Fields { get; set; }

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
		[JsonProperty("require_field_match")]
		bool? RequireFieldMatch { get; set; }

		/// <summary>
		/// Defines what constitutes a boundary for highlighting when using the fast vector highlighter.
		/// It's a single string with each boundary character defined in it. It defaults to .,!? \t\n.
		/// </summary>
		[JsonProperty("boundary_chars")]
		string BoundaryChars { get; set; }

		[JsonProperty("max_fragment_length")]
		int? MaxFragmentLength { get; set; }

		/// <summary>
		/// When highlighting a field using the unified highlighter or the fast vector highlighter, you can specify how to break the highlighted
		/// fragments using boundary_scanner
		/// </summary>
		[JsonProperty("boundary_scanner")]
		BoundaryScanner? BoundaryScanner { get; set; }

		/// <summary>
		///You can further specify boundary_scanner_locale to control which Locale is used to search the text for these boundaries.
		/// </summary>
		[JsonProperty("boundary_scanner_locale")]
		string BoundaryScannerLocale { get; set; }

		/// <summary>
		/// Fragmenter can control how text should be broken up in highlight snippets. However, this option is
		/// applicable only for the Plain Highlighter
		/// </summary>
		[JsonProperty("fragmenter")]
		HighlighterFragmenter? Fragmenter { get; set; }
	}

	public class Highlight : IHighlight
	{
		// <inheritdoc/>
		public IEnumerable<string> PreTags { get; set; }
		// <inheritdoc/>
		public IEnumerable<string> PostTags { get; set; }
		// <inheritdoc/>
		public int? FragmentSize { get; set; }
		// <inheritdoc/>
		public HighlighterTagsSchema? TagsSchema { get; set; }
		// <inheritdoc/>
		public int? NumberOfFragments { get; set; }
		// <inheritdoc/>
		public int? FragmentOffset { get; set; }
		// <inheritdoc/>
		public int? BoundaryMaxScan { get; set; }
		// <inheritdoc/>
		public HighlighterEncoder? Encoder { get; set; }
		// <inheritdoc/>
		public HighlighterOrder? Order { get; set; }
		// <inheritdoc/>
		public Dictionary<Field, IHighlightField> Fields { get; set; }
		// <inheritdoc/>
		public bool? RequireFieldMatch { get; set; }
		// <inheritdoc/>
		public string BoundaryChars { get; set; }
		// <inheritdoc/>
		public int? MaxFragmentLength { get; set; }
		// <inheritdoc/>
		public int? NoMatchSize { get; set; }
		// <inheritdoc/>
		public BoundaryScanner? BoundaryScanner { get; set; }
		// <inheritdoc/>
		public string BoundaryScannerLocale { get; set; }
		// <inheritdoc/>
		public HighlighterFragmenter? Fragmenter { get; set; }

		public static Highlight Field(Field field) => new Highlight
		{
			Fields = new Dictionary<Field, IHighlightField>
			{
				{field, new HighlightField()}
			}
		};

	}

	public class HighlightDescriptor<T> : DescriptorBase<HighlightDescriptor<T> ,IHighlight>, IHighlight
		where T : class
	{

		IEnumerable<string> IHighlight.PreTags { get; set; }
		IEnumerable<string> IHighlight.PostTags { get; set; }
		int? IHighlight.FragmentSize { get; set; }
		HighlighterTagsSchema? IHighlight.TagsSchema { get; set; }
		int? IHighlight.NumberOfFragments { get; set; }
		int? IHighlight.FragmentOffset { get; set; }
		int? IHighlight.BoundaryMaxScan { get; set; }
		HighlighterEncoder? IHighlight.Encoder { get; set; }
		HighlighterOrder? IHighlight.Order { get; set; }
		Dictionary<Field, IHighlightField> IHighlight.Fields { get; set; }
		bool? IHighlight.RequireFieldMatch { get; set; }
		string IHighlight.BoundaryChars { get; set; }
		int? IHighlight.MaxFragmentLength { get; set; }
		int? IHighlight.NoMatchSize { get; set; }
		BoundaryScanner? IHighlight.BoundaryScanner { get; set; }
		string IHighlight.BoundaryScannerLocale { get; set; }
		HighlighterFragmenter? IHighlight.Fragmenter { get; set; }

		// <inheritdoc/>
		public HighlightDescriptor<T> Fields(params Func<HighlightFieldDescriptor<T>, IHighlightField>[] fieldHighlighters) =>
			Assign(a => a.Fields = fieldHighlighters?
				.Select(f =>
					f(new HighlightFieldDescriptor<T>())
						.ThrowWhen(p => p.Field == null, "Could not infer key for highlight field descriptor")
				)
				.ToDictionary(k => k.Field, v => v)
				.NullIfNoKeys()
			);

		// <inheritdoc/>
		public HighlightDescriptor<T> TagsSchema(HighlighterTagsSchema? schema) => Assign(a => a.TagsSchema = schema);

		// <inheritdoc/>
		public HighlightDescriptor<T> PreTags(string preTags) => this.PreTags(new[] {preTags});

		// <inheritdoc/>
		public HighlightDescriptor<T> PostTags(string postTags) => this.PostTags(new[] {postTags});

		// <inheritdoc/>
		public HighlightDescriptor<T> PreTags(IEnumerable<string> preTags) => Assign(a => a.PreTags = preTags.ToListOrNullIfEmpty());

		// <inheritdoc/>
		public HighlightDescriptor<T> PostTags(IEnumerable<string> postTags) => Assign(a => a.PostTags = postTags.ToListOrNullIfEmpty());

		// <inheritdoc/>
		public HighlightDescriptor<T> FragmentSize(int? fragmentSize) => Assign(a => a.FragmentSize = fragmentSize);

		// <inheritdoc/>
		public HighlightDescriptor<T> NumberOfFragments(int? numberOfFragments) => Assign(a => a.NumberOfFragments = numberOfFragments);

		// <inheritdoc/>
		public HighlightDescriptor<T> FragmentOffset(int? fragmentOffset) => Assign(a => a.FragmentOffset = fragmentOffset);

		// <inheritdoc/>
		public HighlightDescriptor<T> Encoder(HighlighterEncoder? encoder) => Assign(a => a.Encoder = encoder);

		// <inheritdoc/>
		public HighlightDescriptor<T> Order(HighlighterOrder? order) => Assign(a => a.Order = order);

		// <inheritdoc/>
		public HighlightDescriptor<T> RequireFieldMatch(bool? requireFieldMatch = true) => Assign(a => a.RequireFieldMatch = requireFieldMatch);

		// <inheritdoc/>
		public HighlightDescriptor<T> BoundaryCharacters(string boundaryCharacters) => Assign(a => a.BoundaryChars = boundaryCharacters);

		// <inheritdoc/>
		public HighlightDescriptor<T> BoundaryMaxScan(int? boundaryMaxScan) => Assign(a => a.BoundaryMaxScan = boundaryMaxScan);

		// <inheritdoc/>
		public HighlightDescriptor<T> MaxFragmentLength(int? maxFragmentLength) => Assign(a => a.MaxFragmentLength = maxFragmentLength);

		// <inheritdoc/>
		public HighlightDescriptor<T> NoMatchSize(int? noMatchSize) => Assign(a => a.NoMatchSize = noMatchSize);

		// <inheritdoc/>
		public HighlightDescriptor<T> BoundaryScanner(BoundaryScanner? boundaryScanner) => Assign(a => a.BoundaryScanner = boundaryScanner);

		// <inheritdoc/>
		public HighlightDescriptor<T> BoundaryScannerLocale(string locale) => Assign(a => a.BoundaryScannerLocale = locale);

		// <inheritdoc/>
		public HighlightDescriptor<T> Fragmenter(HighlighterFragmenter? fragmenter) => Assign(a => a.Fragmenter = fragmenter);
	}
}
