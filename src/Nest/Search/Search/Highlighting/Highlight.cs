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

		[JsonProperty("boundary_max_size")]
		int? BoundaryMaxSize { get; set; }

		[JsonProperty("encoder")]
		string Encoder { get; set; }

		[JsonProperty("order")]
		string Order { get; set; }

		[JsonProperty(PropertyName = "fields")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<Field, IHighlightField> Fields { get; set; }

		[JsonProperty("require_field_match")]
		bool? RequireFieldMatch { get; set; }

		[JsonProperty("boundary_chars")]
		string BoundaryChars { get; set; }

		[JsonProperty("highlight_query")]
		QueryContainer HighlightQuery { get; set; }
	}

	public class Highlight : IHighlight
	{
		public IEnumerable<string> PreTags { get; set; }
		public IEnumerable<string> PostTags { get; set; }
		public int? FragmentSize { get; set; }
		public string TagsSchema { get; set; }
		public int? NumberOfFragments { get; set; }
		public int? FragmentOffset { get; set; }
		public int? BoundaryMaxSize { get; set; }
		public string Encoder { get; set; }
		public string Order { get; set; }
		public Dictionary<Field, IHighlightField> Fields { get; set; }
		public bool? RequireFieldMatch { get; set; }
		public string BoundaryChars { get; set; }
		public QueryContainer HighlightQuery { get; set; }
	}

	public class HighlightDescriptor<T> : IHighlight
		where T : class
	{
		protected IHighlight Self => this;

		private HighlightDescriptor<T> _assign(Action<IHighlight> assigner) => Fluent.Assign(this, assigner);

		IEnumerable<string> IHighlight.PreTags { get; set; }

		IEnumerable<string> IHighlight.PostTags { get; set; }

		int? IHighlight.FragmentSize { get; set; }

		string IHighlight.TagsSchema { get; set; }

		int? IHighlight.NumberOfFragments { get; set; }

		int? IHighlight.FragmentOffset { get; set; }

		int? IHighlight.BoundaryMaxSize { get; set; }

		string IHighlight.Encoder { get; set; }

		string IHighlight.Order { get; set; }

		Dictionary<Field, IHighlightField> IHighlight.Fields { get; set; }

		bool? IHighlight.RequireFieldMatch { get; set; }

		string IHighlight.BoundaryChars { get; set; }

		QueryContainer IHighlight.HighlightQuery { get; set; }

		public HighlightDescriptor<T> Fields(params Func<HighlightFieldDescriptor<T>, IHighlightField>[] fieldHighlighters) =>
			_assign(a => a.Fields = fieldHighlighters?
				.Select(f =>
					f(new HighlightFieldDescriptor<T>())
						.ThrowWhen(p => p.Field == null, "Could not infer key for highlight field descriptor")
				)
				.ToDictionary(k => k.Field, v => v)
				.NullIfNoKeys()
			);

		public HighlightDescriptor<T> TagsSchema(string schema = "styled") => _assign(a => a.TagsSchema = schema);

		public HighlightDescriptor<T> PreTags(string preTags) => this.PreTags(new[] {preTags});

		public HighlightDescriptor<T> PostTags(string postTags)=> this.PostTags(new[] {postTags});

		public HighlightDescriptor<T> PreTags(IEnumerable<string> preTags) => _assign(a => a.PreTags = preTags.ToListOrNullIfEmpty());

		public HighlightDescriptor<T> PostTags(IEnumerable<string> postTags) => _assign(a => a.PostTags = postTags.ToListOrNullIfEmpty());

		public HighlightDescriptor<T> FragmentSize(int fragmentSize) => _assign(a => a.FragmentSize = fragmentSize);

		public HighlightDescriptor<T> NumberOfFragments(int numberOfFragments) => _assign(a => a.NumberOfFragments = numberOfFragments);

		public HighlightDescriptor<T> FragmentOffset(int fragmentOffset) => _assign(a => a.FragmentOffset = fragmentOffset);

		public HighlightDescriptor<T> Encoder(string encoder) => _assign(a => a.Encoder = encoder);

		public HighlightDescriptor<T> Order(string order) => _assign(a => a.Order = order);

		public HighlightDescriptor<T> RequireFieldMatch(bool requireFieldMatch) => _assign(a => a.RequireFieldMatch = requireFieldMatch);

		public HighlightDescriptor<T> BoundaryCharacters(string boundaryCharacters) => _assign(a => a.BoundaryChars = boundaryCharacters);

		public HighlightDescriptor<T> BoundaryMaxSize(int boundaryMaxSize) => _assign(a => a.BoundaryMaxSize = boundaryMaxSize);

		public HighlightDescriptor<T> HighlightQuery(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			_assign(a => a.HighlightQuery = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
