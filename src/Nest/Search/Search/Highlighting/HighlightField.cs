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
		Field Field { get; set; }

		[JsonProperty("pre_tags")]
		IEnumerable<string> PreTags { get; set; }

		[JsonProperty("post_tags")]
		IEnumerable<string> PostTags { get; set; }

		[JsonProperty("fragment_size")]
		int? FragmentSize { get; set; }

		[JsonProperty("no_match_size")]
		int? NoMatchSize { get; set; }

		[JsonProperty("number_of_fragments")]
		int? NumberOfFragments { get; set; }

		[JsonProperty("fragment_offset")]
		int? FragmentOffset { get; set; }

		[JsonProperty("boundary_max_size")]
		int? BoundaryMaxSize { get; set; }

		[JsonProperty("encoder")]
		string Encoder { get; set; }

		[JsonProperty("order")]
		string Order { get; set; }

		[JsonProperty("tags_schema")]
		string TagsSchema { get; set; }

		[JsonProperty("require_field_match")]
		bool? RequireFieldMatch { get; set; }

		[JsonProperty("boundary_chars")]
		string BoundaryChars { get; set; }

		[JsonProperty("type")]
		HighlighterType? Type { get; set; }

		[JsonProperty("force_source")]
		bool? ForceSource { get; set; }

		[JsonProperty("matched_fields")]
		Fields MatchedFields { get; set; }

		[JsonProperty("highlight_query")]
		QueryContainer HighlightQuery { get; set; }
	}

	public class HighlightField : IHighlightField
	{
		public Field Field { get; set; }
		public IEnumerable<string> PreTags { get; set; }
		public IEnumerable<string> PostTags { get; set; }
		public int? FragmentSize { get; set; }
		public int? NoMatchSize { get; set; }
		public int? NumberOfFragments { get; set; }
		public int? FragmentOffset { get; set; }
		public int? BoundaryMaxSize { get; set; }
		public string Encoder { get; set; }
		public string Order { get; set; }
		public string TagsSchema { get; set; }
		public bool? RequireFieldMatch { get; set; }
		public string BoundaryChars { get; set; }
		public HighlighterType? Type { get; set; }
		public bool? ForceSource { get; set; }
		public Fields MatchedFields { get; set; }
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

		string IHighlightField.Encoder { get; set; }

		string IHighlightField.Order { get; set; }

		string IHighlightField.TagsSchema { get; set; }

		bool? IHighlightField.RequireFieldMatch { get; set; }

		string IHighlightField.BoundaryChars { get; set; }

		HighlighterType? IHighlightField.Type { get; set; }

		bool? IHighlightField.ForceSource { get; set; }

		Fields IHighlightField.MatchedFields { get; set; }

		QueryContainer IHighlightField.HighlightQuery { get; set; }

		public HighlightFieldDescriptor<T> Field(Field field) => Assign(a => a.Field = field);
		public HighlightFieldDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		public HighlightFieldDescriptor<T> OnAll() => this.Field("_all");

		public HighlightFieldDescriptor<T> TagsSchema(string schema = "styled") => Assign(a => a.TagsSchema = schema);

		public HighlightFieldDescriptor<T> ForceSource(bool? force = true) => Assign(a => a.ForceSource = force);

		public HighlightFieldDescriptor<T> Type(HighlighterType type) => Assign(a => a.Type = type);

		public HighlightFieldDescriptor<T> PreTags(string preTags) => Assign(a => a.PreTags = new[] { preTags });

		public HighlightFieldDescriptor<T> PostTags(string postTags) => Assign(a => a.PostTags = new[] { postTags });

		public HighlightFieldDescriptor<T> PreTags(IEnumerable<string> preTags) => Assign(a => a.PreTags = preTags);

		public HighlightFieldDescriptor<T> PostTags(IEnumerable<string> postTags) => Assign(a => a.PostTags = postTags);

		public HighlightFieldDescriptor<T> FragmentSize(int? fragmentSize) => Assign(a => a.FragmentSize = fragmentSize);

		public HighlightFieldDescriptor<T> NoMatchSize(int? noMatchSize) => Assign(a => a.NoMatchSize = noMatchSize);

		public HighlightFieldDescriptor<T> NumberOfFragments(int? numberOfFragments) => Assign(a => a.NumberOfFragments = numberOfFragments);

		public HighlightFieldDescriptor<T> FragmentOffset(int? fragmentOffset) => Assign(a => a.FragmentOffset = fragmentOffset);

		public HighlightFieldDescriptor<T> Encoder(string encoder) => Assign(a => a.Encoder = encoder);

		public HighlightFieldDescriptor<T> Order(string order) => Assign(a => a.Order = order);

		public HighlightFieldDescriptor<T> RequireFieldMatch(bool? requireFieldMatch = true) => Assign(a => a.RequireFieldMatch = requireFieldMatch);

		public HighlightFieldDescriptor<T> BoundaryCharacters(string boundaryCharacters) => Assign(a => a.BoundaryChars = boundaryCharacters);

		public HighlightFieldDescriptor<T> BoundaryMaxSize(int? boundaryMaxSize) => Assign(a => a.BoundaryMaxSize = boundaryMaxSize);
		
		public HighlightFieldDescriptor<T> MatchedFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.MatchedFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public HighlightFieldDescriptor<T> HighlightQuery(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.HighlightQuery = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
