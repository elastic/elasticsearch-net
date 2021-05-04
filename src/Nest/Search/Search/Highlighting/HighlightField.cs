// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HighlightField))]
	public interface IHighlightField
	{
		/// <summary>
		/// Defines what constitutes a boundary for highlighting when using the fast vector highlighter.
		/// It's a single string with each boundary character defined in it. It defaults to .,!? \t\n.
		/// </summary>
		[DataMember(Name = "boundary_chars")]
		string BoundaryChars { get; set; }

		/// <summary>
		/// Controls how far to look for boundary characters. Defaults to 20.
		/// </summary>
		[DataMember(Name = "boundary_max_scan")]
		int? BoundaryMaxScan { get; set; }

		/// <summary>
		/// When highlighting a field using the unified highlighter or the fast vector highlighter, you can specify how to break
		/// the highlighted
		/// fragments using boundary_scanner
		/// </summary>
		[DataMember(Name = "boundary_scanner")]
		BoundaryScanner? BoundaryScanner { get; set; }

		/// <summary>
		/// You can further specify boundary_scanner_locale to control which Locale is used to search the text for these
		/// boundaries.
		/// </summary>
		[DataMember(Name = "boundary_scanner_locale")]
		string BoundaryScannerLocale { get; set; }

		/// <summary>
		/// The field on which to perform highlighting.
		/// </summary>
		/// <remarks>
		/// In order to perform highlighting, the actual content of the field is required.
		/// If the field in question is stored (has store set to true in the mapping) it will be used,
		/// otherwise, the actual _source will be loaded and the relevant field will be extracted from it.
		/// </remarks>
		Field Field { get; set; }

		/// <summary>
		/// Forces the highlighting to highlight fields based on the source even if fields are stored separately.
		/// </summary>
		[DataMember(Name = "force_source")]
		bool? ForceSource { get; set; }

		/// <summary>
		/// Fragmenter can control how text should be broken up in highlight snippets. However, this option is
		/// applicable only for the Plain Highlighter
		/// </summary>
		[DataMember(Name = "fragmenter")]
		HighlighterFragmenter? Fragmenter { get; set; }

		/// <summary>
		/// Controls the margin to start highlighting from when using the fast vector highlighter
		/// </summary>
		[DataMember(Name = "fragment_offset")]
		int? FragmentOffset { get; set; }

		/// <summary>
		/// The size of the highlighted fragment, in characters. Defaults to 100
		/// </summary>
		[DataMember(Name = "fragment_size")]
		int? FragmentSize { get; set; }

		/// <summary>
		/// The query to use for highlighting
		/// </summary>
		[DataMember(Name = "highlight_query")]
		QueryContainer HighlightQuery { get; set; }

		/// <summary>
		/// Combine matches on multiple fields to highlight a single field when using the fast vector highighter.
		/// This is most intuitive for multifields that analyze the same string in different ways.
		/// All matched fields must have term_vector set to with_positions_offsets, but only the field to
		/// which the matches are combined is loaded so only that field would benefit from having store set to yes.
		/// </summary>
		[DataMember(Name = "matched_fields")]
		Fields MatchedFields { get; set; }

		[DataMember(Name = "max_fragment_length")]
		int? MaxFragmentLength { get; set; }

		/// <summary>
		/// The length of a snippet of text from the beginning of the field to return
		/// when no match for highlighting is found. Default behaviour is to not return anything when a match is not found.
		/// The actual length may be shorter than specified as it tries to break on a word boundary.
		/// </summary>
		[DataMember(Name = "no_match_size")]
		int? NoMatchSize { get; set; }

		/// <summary>
		/// The maximum number of fragments to return. Defaults to 5.
		/// </summary>
		[DataMember(Name = "number_of_fragments")]
		int? NumberOfFragments { get; set; }

		/// <summary>
		/// The order in which highlighted fragments are sorted. Only valid for the unified highlighter.
		/// </summary>
		[DataMember(Name = "order")]
		HighlighterOrder? Order { get; set; }

		/// <summary>
		/// Controls the number of matching phrases in a document that are considered. Prevents the
		/// <see cref="HighlighterType.Fvh" /> highlighter from analyzing too many phrases and consuming too much memory.
		/// When using matched_fields, <see cref="PhraseLimit" /> phrases per matched field are considered. Raising the limit
		/// increases query time
		/// and consumes more memory. Only supported by the <see cref="HighlighterType.Fvh" /> highlighter. Defaults to 256.
		/// </summary>
		[DataMember(Name = "phrase_limit")]
		int? PhraseLimit { get; set; }

		/// <summary>
		/// Controls the post tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		[DataMember(Name = "post_tags")]
		IEnumerable<string> PostTags { get; set; }

		/// <summary>
		/// Controls the pre tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		[DataMember(Name = "pre_tags")]
		IEnumerable<string> PreTags { get; set; }

		/// <summary>
		/// Determines if only fields that hold a query match will be highlighted. Set to <c>false</c>
		/// will cause any field to be highlighted regardless of whether the query matched specifically on them. Default behaviour
		/// is <c>true</c>.
		/// </summary>
		[DataMember(Name = "require_field_match")]
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
		[DataMember(Name = "tags_schema")]
		HighlighterTagsSchema? TagsSchema { get; set; }

		/// <summary>
		/// The type of highlighter to use. Can be a defined or custom highlighter
		/// </summary>
		[DataMember(Name = "type")]
		Union<HighlighterType, string> Type { get; set; }
	}

	public class HighlightField : IHighlightField
	{
		/// <inheritdoc />
		public string BoundaryChars { get; set; }

		/// <inheritdoc />
		public int? BoundaryMaxScan { get; set; }

		/// <inheritdoc />
		public BoundaryScanner? BoundaryScanner { get; set; }

		/// <inheritdoc />
		public string BoundaryScannerLocale { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? ForceSource { get; set; }

		/// <inheritdoc />
		public HighlighterFragmenter? Fragmenter { get; set; }

		/// <inheritdoc />
		public int? FragmentOffset { get; set; }

		/// <inheritdoc />
		public int? FragmentSize { get; set; }

		/// <inheritdoc />
		public QueryContainer HighlightQuery { get; set; }

		/// <inheritdoc />
		public Fields MatchedFields { get; set; }

		/// <inheritdoc />
		public int? MaxFragmentLength { get; set; }

		/// <inheritdoc />
		public int? NoMatchSize { get; set; }

		/// <inheritdoc />
		public int? NumberOfFragments { get; set; }

		/// <inheritdoc />
		public HighlighterOrder? Order { get; set; }

		/// <inheritdoc />
		public int? PhraseLimit { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> PostTags { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> PreTags { get; set; }

		/// <inheritdoc />
		public bool? RequireFieldMatch { get; set; }

		/// <inheritdoc />
		public HighlighterTagsSchema? TagsSchema { get; set; }

		/// <inheritdoc />
		public Union<HighlighterType, string> Type { get; set; }
	}

	public class HighlightFieldDescriptor<T> : DescriptorBase<HighlightFieldDescriptor<T>, IHighlightField>, IHighlightField
		where T : class
	{
		string IHighlightField.BoundaryChars { get; set; }
		int? IHighlightField.BoundaryMaxScan { get; set; }
		BoundaryScanner? IHighlightField.BoundaryScanner { get; set; }
		string IHighlightField.BoundaryScannerLocale { get; set; }
		Field IHighlightField.Field { get; set; }
		bool? IHighlightField.ForceSource { get; set; }
		HighlighterFragmenter? IHighlightField.Fragmenter { get; set; }
		int? IHighlightField.FragmentOffset { get; set; }
		int? IHighlightField.FragmentSize { get; set; }
		QueryContainer IHighlightField.HighlightQuery { get; set; }
		Fields IHighlightField.MatchedFields { get; set; }
		int? IHighlightField.MaxFragmentLength { get; set; }
		int? IHighlightField.NoMatchSize { get; set; }
		int? IHighlightField.NumberOfFragments { get; set; }
		HighlighterOrder? IHighlightField.Order { get; set; }
		int? IHighlightField.PhraseLimit { get; set; }
		IEnumerable<string> IHighlightField.PostTags { get; set; }
		IEnumerable<string> IHighlightField.PreTags { get; set; }
		bool? IHighlightField.RequireFieldMatch { get; set; }
		HighlighterTagsSchema? IHighlightField.TagsSchema { get; set; }
		Union<HighlighterType, string> IHighlightField.Type { get; set; }

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> AllField() => Field("_all");

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> TagsSchema(HighlighterTagsSchema? schema) => Assign(schema, (a, v) => a.TagsSchema = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> ForceSource(bool? force = true) => Assign(force, (a, v) => a.ForceSource = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> Type(HighlighterType type) => Assign(type, (a, v) => a.Type = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> Type(string type) => Assign(type, (a, v) => a.Type = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> PreTags(params string[] preTags) => Assign(preTags, (a, v) => a.PreTags = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> PostTags(params string[] postTags) => Assign(postTags, (a, v) => a.PostTags = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> PreTags(IEnumerable<string> preTags) => Assign(preTags, (a, v) => a.PreTags = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> PostTags(IEnumerable<string> postTags) => Assign(postTags, (a, v) => a.PostTags = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> FragmentSize(int? fragmentSize) => Assign(fragmentSize, (a, v) => a.FragmentSize = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> NoMatchSize(int? noMatchSize) => Assign(noMatchSize, (a, v) => a.NoMatchSize = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> NumberOfFragments(int? numberOfFragments) => Assign(numberOfFragments, (a, v) => a.NumberOfFragments = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> FragmentOffset(int? fragmentOffset) => Assign(fragmentOffset, (a, v) => a.FragmentOffset = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> Order(HighlighterOrder? order) => Assign(order, (a, v) => a.Order = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> RequireFieldMatch(bool? requireFieldMatch = true) => Assign(requireFieldMatch, (a, v) => a.RequireFieldMatch = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> BoundaryCharacters(string boundaryCharacters) => Assign(boundaryCharacters, (a, v) => a.BoundaryChars = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> BoundaryMaxScan(int? boundaryMaxSize) => Assign(boundaryMaxSize, (a, v) => a.BoundaryMaxScan = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> MatchedFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.MatchedFields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> HighlightQuery(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.HighlightQuery = v?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> MaxFragmentLength(int? maxFragmentLength) => Assign(maxFragmentLength, (a, v) => a.MaxFragmentLength = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> BoundaryScanner(BoundaryScanner? boundaryScanner) => Assign(boundaryScanner, (a, v) => a.BoundaryScanner = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> BoundaryScannerLocale(string locale) => Assign(locale, (a, v) => a.BoundaryScannerLocale = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> Fragmenter(HighlighterFragmenter? fragmenter) => Assign(fragmenter, (a, v) => a.Fragmenter = v);

		/// <inheritdoc />
		public HighlightFieldDescriptor<T> PhraseLimit(int? phraseLimit) => Assign(phraseLimit, (a, v) => a.PhraseLimit = v);
	}
}
