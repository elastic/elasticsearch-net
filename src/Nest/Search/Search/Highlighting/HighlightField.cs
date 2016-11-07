using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HighlightField>))]
	public interface IHighlightField
	{
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
		/// The length of a snippet of text from the beginning of the field to return
		/// when no match for highlighting is found. Default behaviour is to not return anything when a match is not found.
		/// The actual length may be shorter than specified as it tries to break on a word boundary.
		/// </summary>
		/// <remarks>
		/// When using the postings highlighter, it is not possible to control the actual size of the snippet,
		/// therefore the first sentence gets returned whenever no_match_size is greater than 0.
		/// </remarks>
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

		[Obsolete("Use BoundaryMaxScan")]
		int? BoundaryMaxSize { get; set; }

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
		string Encoder { get; set; }

		/// <summary>
		/// The order in which highlighted fragments are sorted
		/// </summary>
		[JsonProperty("order")]
		string Order { get; set; }

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
		string TagsSchema { get; set; }

		/// <summary>
		/// Determines if only fields that hold a query match will be highlighted. Set to <c>false</c>
		/// will cause any field to be highlighted regardless of whether the query matched specifically on them. Default behaviour is <c>true</c>.
		/// </summary>
		[JsonProperty("require_field_match")]
		bool? RequireFieldMatch { get; set; }

		/// <summary>
		/// Defines what constitutes a boundary for highlighting when using the fast vector highlighter.
		/// It's a single string with each boundary character defined in it. It defaults to .,!? \t\n.
		/// </summary>
		[JsonProperty("boundary_chars")]
		string BoundaryChars { get; set; }

		/// <summary>
		/// The type of highlighter to use
		/// </summary>
		HighlighterType? Type { get; set; }

		/// <summary>
		/// The type of highlighter to use
		/// </summary>
		[JsonProperty("type")]
		[Obsolete("This is a temporary binary backwards compatible fix to make sure you can specify any custom highlighter type in 2.0.0. Removed in 5.0.0.")]
		string CustomType { get; set; }

		/// <summary>
		/// Forces the highlighting to highlight fields based on the source even if fields are stored separately.
		/// </summary>
		[JsonProperty("force_source")]
		bool? ForceSource { get; set; }

		/// <summary>
		/// Combine matches on multiple fields to highlight a single field when using the fast vector highighter.
		/// This is most intuitive for multifields that analyze the same string in different ways.
		/// All matched fields must have term_vector set to with_positions_offsets, but only the field to
		/// which the matches are combined is loaded so only that field would benefit from having store set to yes.
		/// </summary>
		[JsonProperty("matched_fields")]
		Fields MatchedFields { get; set; }

		/// <summary>
		/// The query to use for highlighting
		/// </summary>
		[JsonProperty("highlight_query")]
		QueryContainer HighlightQuery { get; set; }
	}

	public class HighlightField : IHighlightField
	{
		/// <summary>
		/// The field on which to perform highlighting.
		/// </summary>
		/// <remarks>
		/// In order to perform highlighting, the actual content of the field is required.
		/// If the field in question is stored (has store set to true in the mapping) it will be used,
		/// otherwise, the actual _source will be loaded and the relevant field will be extracted from it.
		/// </remarks>
		public Field Field { get; set; }

		/// <summary>
		/// Controls the pre tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		public IEnumerable<string> PreTags { get; set; }

		/// <summary>
		/// Controls the post tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		public IEnumerable<string> PostTags { get; set; }

		/// <summary>
		/// The size of the highlighted fragment, in characters. Defaults to 100
		/// </summary>
		public int? FragmentSize { get; set; }

		/// <summary>
		/// The length of a snippet of text from the beginning of the field to return
		/// when no match for highlighting is found. Default behaviour is to not return anything when a match is not found.
		/// The actual length may be shorter than specified as it tries to break on a word boundary.
		/// </summary>
		/// <remarks>
		/// When using the postings highlighter, it is not possible to control the actual size of the snippet,
		/// therefore the first sentence gets returned whenever no_match_size is greater than 0.
		/// </remarks>
		public int? NoMatchSize { get; set; }

		/// <summary>
		/// The maximum number of fragments to return. Defaults to 5.
		/// </summary>
		public int? NumberOfFragments { get; set; }

		/// <summary>
		/// Controls the margin to start highlighting from when using the fast vector highlighter
		/// </summary>
		public int? FragmentOffset { get; set; }

		/// <summary>
		/// Controls how far to look for boundary characters. Defaults to 20.
		/// </summary>
		public int? BoundaryMaxScan { get; set; }

		[Obsolete("Use BoundaryMaxScan")]
		public int? BoundaryMaxSize { get; set; }

		/// <summary>
		/// Define how highlighted text will be encoded.
		/// It can be either default (no encoding) or html (will escape html, if you use html highlighting tags).
		/// </summary>
		public string Encoder { get; set; }

		/// <summary>
		/// The order in which highlighted fragments are sorted
		/// </summary>
		public string Order { get; set; }

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
		public string TagsSchema { get; set; }

		/// <summary>
		/// Determines if only fields that hold a query match will be highlighted. Set to <c>false</c>
		/// will cause any field to be highlighted regardless of whether the query matched specifically on them. Default behaviour is <c>true</c>.
		/// </summary>
		public bool? RequireFieldMatch { get; set; }

		/// <summary>
		/// Defines what constitutes a boundary for highlighting when using the fast vector highlighter.
		/// It's a single string with each boundary character defined in it. It defaults to .,!? \t\n.
		/// </summary>
		public string BoundaryChars { get; set; }

		/// <summary>
		/// The type of highlighter to use.
		/// </summary>
		public HighlighterType? Type
		{
			get { return this.CustomType.ToEnum<HighlighterType>(); }
			set { this.CustomType = value.GetStringValue(); }
		}

		/// <summary>
		/// The type of highlighter to use.
		/// </summary>
		public string CustomType { get; set; }

		/// <summary>
		/// Forces the highlighting to highlight fields based on the source even if fields are stored separately.
		/// </summary>
		public bool? ForceSource { get; set; }

		/// <summary>
		/// Combine matches on multiple fields to highlight a single field when using the fast vector highighter.
		/// This is most intuitive for multifields that analyze the same string in different ways.
		/// All matched fields must have term_vector set to with_positions_offsets, but only the field to
		/// which the matches are combined is loaded so only that field would benefit from having store set to yes.
		/// </summary>
		public Fields MatchedFields { get; set; }

		/// <summary>
		/// The query to use for highlighting
		/// </summary>
		public QueryContainer HighlightQuery { get; set; }
	}

	public class HighlightFieldDescriptor<T> : DescriptorBase<HighlightFieldDescriptor<T>,IHighlightField>, IHighlightField
		where T : class
	{
		Field IHighlightField.Field { get; set; }
		IEnumerable<string> IHighlightField.PreTags { get; set; }
		IEnumerable<string> IHighlightField.PostTags { get; set; }
		int? IHighlightField.FragmentSize { get; set; }
		int? IHighlightField.NoMatchSize { get; set; }
		int? IHighlightField.NumberOfFragments { get; set; }
		int? IHighlightField.FragmentOffset { get; set; }
		int? IHighlightField.BoundaryMaxSize { get; set; }
		int? IHighlightField.BoundaryMaxScan { get; set; }
		string IHighlightField.Encoder { get; set; }
		string IHighlightField.Order { get; set; }
		string IHighlightField.TagsSchema { get; set; }
		bool? IHighlightField.RequireFieldMatch { get; set; }
		string IHighlightField.BoundaryChars { get; set; }
		HighlighterType? IHighlightField.Type
		{
#pragma warning disable CS0618 // Type or member is obsolete
			get { return Self.CustomType.ToEnum<HighlighterType>(); }
			set { Self.CustomType = value.GetStringValue(); }
#pragma warning restore CS0618 // Type or member is obsolete
		}
		string IHighlightField.CustomType { get; set; }
		bool? IHighlightField.ForceSource { get; set; }
		Fields IHighlightField.MatchedFields { get; set; }

		QueryContainer IHighlightField.HighlightQuery { get; set; }

		/// <summary>
		/// The field on which to perform highlighting.
		/// </summary>
		/// <remarks>
		/// In order to perform highlighting, the actual content of the field is required.
		/// If the field in question is stored (has store set to true in the mapping) it will be used,
		/// otherwise, the actual _source will be loaded and the relevant field will be extracted from it.
		/// </remarks>
		public HighlightFieldDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <summary>
		/// The field on which to perform highlighting.
		/// </summary>
		/// <remarks>
		/// In order to perform highlighting, the actual content of the field is required.
		/// If the field in question is stored (has store set to true in the mapping) it will be used,
		/// otherwise, the actual _source will be loaded and the relevant field will be extracted from it.
		/// </remarks>
		public HighlightFieldDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <summary>
		/// Perform highlighting on the _all field.
		/// </summary>
		/// <remarks>
		/// The _all field cannot be extracted from _source,
		/// so it can only be used for highlighting if it mapped to have store set to true.
		/// </remarks>
		public HighlightFieldDescriptor<T> OnAll() => this.Field("_all");

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
		public HighlightFieldDescriptor<T> TagsSchema(string schema = "styled") => Assign(a => a.TagsSchema = schema);

		/// <summary>
		/// Forces the highlighting to highlight fields based on the source even if fields are stored separately.
		/// </summary>
		public HighlightFieldDescriptor<T> ForceSource(bool? force = true) => Assign(a => a.ForceSource = force);

		/// <summary>
		/// The type of highlighter to use
		/// </summary>
		public HighlightFieldDescriptor<T> Type(HighlighterType type) => Assign(a => a.Type = type);

		/// <summary>
		/// The type of highlighter to use
		/// </summary>
#pragma warning disable CS0618 // Type or member is obsolete
		public HighlightFieldDescriptor<T> Type(string type) => Assign(a => a.CustomType = type);
#pragma warning restore CS0618 // Type or member is obsolete

		/// <summary>
		/// Controls the pre tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		public HighlightFieldDescriptor<T> PreTags(string preTags) => Assign(a => a.PreTags = new[] { preTags });

		/// <summary>
		/// Controls the pre tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		public HighlightFieldDescriptor<T> PreTags(IEnumerable<string> preTags) => Assign(a => a.PreTags = preTags);

		/// <summary>
		/// Controls the post tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		public HighlightFieldDescriptor<T> PostTags(IEnumerable<string> postTags) => Assign(a => a.PostTags = postTags);

		/// <summary>
		/// Controls the post tag in which to wrap highights.
		/// By default, the highlighting will wrap highlighted text in &lt;em&gt; and &lt;/em&gt;.
		/// Using the fast vector highlighter, there can be more tags, and the importance is ordered.
		/// </summary>
		public HighlightFieldDescriptor<T> PostTags(string postTags) => Assign(a => a.PostTags = new[] { postTags });

		/// <summary>
		/// The size of the highlighted fragment, in characters. Defaults to 100
		/// </summary>
		public HighlightFieldDescriptor<T> FragmentSize(int? fragmentSize) => Assign(a => a.FragmentSize = fragmentSize);

		/// <summary>
		/// The length of a snippet of text from the beginning of the field to return
		/// when no match for highlighting is found. Default behaviour is to not return anything when a match is not found.
		/// The actual length may be shorter than specified as it tries to break on a word boundary.
		/// </summary>
		/// <remarks>
		/// When using the postings highlighter, it is not possible to control the actual size of the snippet,
		/// therefore the first sentence gets returned whenever no_match_size is greater than 0.
		/// </remarks>
		public HighlightFieldDescriptor<T> NoMatchSize(int? noMatchSize) => Assign(a => a.NoMatchSize = noMatchSize);

		/// <summary>
		/// The maximum number of fragments to return. Defaults to 5.
		/// </summary>
		public HighlightFieldDescriptor<T> NumberOfFragments(int? numberOfFragments) => Assign(a => a.NumberOfFragments = numberOfFragments);

		/// <summary>
		/// Controls the margin to start highlighting from when using the fast vector highlighter
		/// </summary>
		public HighlightFieldDescriptor<T> FragmentOffset(int? fragmentOffset) => Assign(a => a.FragmentOffset = fragmentOffset);

		/// <summary>
		/// Define how highlighted text will be encoded.
		/// It can be either default (no encoding) or html (will escape html, if you use html highlighting tags).
		/// </summary>
		public HighlightFieldDescriptor<T> Encoder(string encoder) => Assign(a => a.Encoder = encoder);

		/// <summary>
		/// The order in which highlighted fragments are sorted
		/// </summary>
		public HighlightFieldDescriptor<T> Order(string order) => Assign(a => a.Order = order);

		/// <summary>
		/// Determines if only fields that hold a query match will be highlighted. Set to <c>false</c>
		/// will cause any field to be highlighted regardless of whether the query matched specifically on them. Default behaviour is <c>true</c>.
		/// </summary>
		public HighlightFieldDescriptor<T> RequireFieldMatch(bool? requireFieldMatch = true) => Assign(a => a.RequireFieldMatch = requireFieldMatch);

		/// <summary>
		/// Defines what constitutes a boundary for highlighting when using the fast vector highlighter.
		/// It's a single string with each boundary character defined in it. It defaults to .,!? \t\n.
		/// </summary>
		public HighlightFieldDescriptor<T> BoundaryCharacters(string boundaryCharacters) => Assign(a => a.BoundaryChars = boundaryCharacters);

		[Obsolete("Use BoundaryMaxScan(int? boundaryMaxScan).")]
		public HighlightFieldDescriptor<T> BoundaryMaxSize(int? boundaryMaxSize) => Assign(a => a.BoundaryMaxSize = boundaryMaxSize);

		/// <summary>
		/// Controls how far to look for boundary characters. Defaults to 20.
		/// </summary>
		public HighlightFieldDescriptor<T> BoundaryMaxScan(int? boundaryMaxScan) => Assign(a => a.BoundaryMaxScan = boundaryMaxScan);

		/// <summary>
		/// Combine matches on multiple fields to highlight a single field when using the fast vector highighter.
		/// This is most intuitive for multifields that analyze the same string in different ways.
		/// All matched fields must have term_vector set to with_positions_offsets, but only the field to
		/// which the matches are combined is loaded so only that field would benefit from having store set to yes.
		/// </summary>
		public HighlightFieldDescriptor<T> MatchedFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.MatchedFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <summary>
		/// The query to use for highlighting
		/// </summary>
		public HighlightFieldDescriptor<T> HighlightQuery(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.HighlightQuery = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
