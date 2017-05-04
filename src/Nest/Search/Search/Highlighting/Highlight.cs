using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Highlight>))]
	public interface IHighlight
	{
		[JsonProperty("pre_tags")]
		IEnumerable<string> PreTags { get; set; }

		[JsonProperty("post_tags")]
		IEnumerable<string> PostTags { get; set; }

		[JsonProperty("fragment_size")]
		int? FragmentSize { get; set; }

		[JsonProperty("tags_schema")]
		string TagsSchema { get; set; }

		[JsonProperty("number_of_fragments")]
		int? NumberOfFragments { get; set; }

		[JsonProperty("fragment_offset")]
		int? FragmentOffset { get; set; }

		[Obsolete("Bad mapping use BoundaryMaxScan instead")]
		[JsonProperty("boundary_max_size")]
		int? BoundaryMaxSize { get; set; }

		[JsonProperty("boundary_max_scan")]
		int? BoundaryMaxScan { get; set; }

		[JsonProperty("encoder")]
		string Encoder { get; set; }

		[JsonProperty("order")]
		string Order { get; set; }

		[JsonProperty(PropertyName = "fields")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Field, IHighlightField>))]
		Dictionary<Field, IHighlightField> Fields { get; set; }

		[JsonProperty("require_field_match")]
		bool? RequireFieldMatch { get; set; }

		[JsonProperty("boundary_chars")]
		string BoundaryChars { get; set; }

		[JsonProperty("max_fragment_length")]
		int? MaxFragmentLength { get; set; }

		/// <summary>
		/// In the case where there is no matching fragment to highlight, the default is to not return anything. Instead, we can return a snippet of text from
		/// the beginning of the field by setting no_match_size (default 0) to the length of the text that you want returned. The actual length may be
		/// shorter or longer than specified as it tries to break on a word boundary. When using the postings highlighter it is not possible to control the
		/// actual size of the snippet, therefore the first sentence gets returned whenever no_match_size is greater than 0.
		/// </summary>
		[JsonProperty("no_match_size")]
		int? NoMatchSize { get; set; }

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
		public IEnumerable<string> PreTags { get; set; }
		public IEnumerable<string> PostTags { get; set; }
		public int? FragmentSize { get; set; }
		public string TagsSchema { get; set; }
		public int? NumberOfFragments { get; set; }
		public int? FragmentOffset { get; set; }
		[Obsolete("Bad mapping use BoundaryMaxScan instead")]
		public int? BoundaryMaxSize { get; set; }
		public int? BoundaryMaxScan { get; set; }
		public string Encoder { get; set; }
		public string Order { get; set; }
		public Dictionary<Field, IHighlightField> Fields { get; set; }
		public bool? RequireFieldMatch { get; set; }
		public string BoundaryChars { get; set; }
		public int? MaxFragmentLength { get; set; }
		public int? NoMatchSize { get; set; }
		public BoundaryScanner? BoundaryScanner { get; set; }
		public string BoundaryScannerLocale { get; set; }
		public HighlighterFragmenter? Fragmenter { get; set; }
	}

	public class HighlightDescriptor<T> : DescriptorBase<HighlightDescriptor<T> ,IHighlight>, IHighlight
		where T : class
	{

		IEnumerable<string> IHighlight.PreTags { get; set; }
		IEnumerable<string> IHighlight.PostTags { get; set; }
		int? IHighlight.FragmentSize { get; set; }
		string IHighlight.TagsSchema { get; set; }
		int? IHighlight.NumberOfFragments { get; set; }
		int? IHighlight.FragmentOffset { get; set; }
		int? IHighlight.BoundaryMaxSize { get; set; }
		int? IHighlight.BoundaryMaxScan { get; set; }
		string IHighlight.Encoder { get; set; }
		string IHighlight.Order { get; set; }
		Dictionary<Field, IHighlightField> IHighlight.Fields { get; set; }
		bool? IHighlight.RequireFieldMatch { get; set; }
		string IHighlight.BoundaryChars { get; set; }
		int? IHighlight.MaxFragmentLength { get; set; }
		int? IHighlight.NoMatchSize { get; set; }
		BoundaryScanner? IHighlight.BoundaryScanner { get; set; }
		string IHighlight.BoundaryScannerLocale { get; set; }
		HighlighterFragmenter? IHighlight.Fragmenter { get; set; }

		public HighlightDescriptor<T> Fields(params Func<HighlightFieldDescriptor<T>, IHighlightField>[] fieldHighlighters) =>
			Assign(a => a.Fields = fieldHighlighters?
				.Select(f =>
					f(new HighlightFieldDescriptor<T>())
						.ThrowWhen(p => p.Field == null, "Could not infer key for highlight field descriptor")
				)
				.ToDictionary(k => k.Field, v => v)
				.NullIfNoKeys()
			);

		public HighlightDescriptor<T> TagsSchema(string schema = "styled") => Assign(a => a.TagsSchema = schema);

		public HighlightDescriptor<T> PreTags(string preTags) => this.PreTags(new[] {preTags});

		public HighlightDescriptor<T> PostTags(string postTags)=> this.PostTags(new[] {postTags});

		public HighlightDescriptor<T> PreTags(IEnumerable<string> preTags) => Assign(a => a.PreTags = preTags.ToListOrNullIfEmpty());

		public HighlightDescriptor<T> PostTags(IEnumerable<string> postTags) => Assign(a => a.PostTags = postTags.ToListOrNullIfEmpty());

		public HighlightDescriptor<T> FragmentSize(int fragmentSize) => Assign(a => a.FragmentSize = fragmentSize);

		public HighlightDescriptor<T> NumberOfFragments(int numberOfFragments) => Assign(a => a.NumberOfFragments = numberOfFragments);

		public HighlightDescriptor<T> FragmentOffset(int fragmentOffset) => Assign(a => a.FragmentOffset = fragmentOffset);

		public HighlightDescriptor<T> Encoder(string encoder) => Assign(a => a.Encoder = encoder);

		public HighlightDescriptor<T> Order(string order) => Assign(a => a.Order = order);

		public HighlightDescriptor<T> RequireFieldMatch(bool requireFieldMatch) => Assign(a => a.RequireFieldMatch = requireFieldMatch);

		public HighlightDescriptor<T> BoundaryCharacters(string boundaryCharacters) => Assign(a => a.BoundaryChars = boundaryCharacters);

		[Obsolete("Bad mapping use BoundaryMaxScan instead")]
		public HighlightDescriptor<T> BoundaryMaxSize(int boundaryMaxSize) => Assign(a => a.BoundaryMaxSize = boundaryMaxSize);
		public HighlightDescriptor<T> BoundaryMaxScan(int boundaryMaxScan) => Assign(a => a.BoundaryMaxScan = boundaryMaxScan);

		public HighlightDescriptor<T> MaxFragmentLength(int? maxFragmentLength) => Assign(a => a.MaxFragmentLength = maxFragmentLength);

		public HighlightDescriptor<T> NoMatchSize(int? noMatchSize) => Assign(a => a.NoMatchSize = noMatchSize);

		public HighlightDescriptor<T> BoundaryScanner(BoundaryScanner? boundaryScanner) => Assign(a => a.BoundaryScanner = boundaryScanner);

		public HighlightDescriptor<T> BoundaryScannerLocale(string locale) => Assign(a => a.BoundaryScannerLocale = locale);

		public HighlightDescriptor<T> Fragmenter(HighlighterFragmenter? fragmenter) => Assign(a => a.Fragmenter = fragmenter);
	}
}
