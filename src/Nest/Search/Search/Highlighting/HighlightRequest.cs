using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HighlightRequest>))]
	public interface IHighlightRequest
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
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		Dictionary<FieldName, IHighlightField> Fields { get; set; }

		[JsonProperty("require_field_match")]
		bool? RequireFieldMatch { get; set; }

		[JsonProperty("boundary_chars")]
		string BoundaryChars { get; set; }

		[JsonProperty("highlight_query")]
		IQueryContainer HighlightQuery { get; set; }
	}

	public class HighlightRequest : IHighlightRequest
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
		public Dictionary<FieldName, IHighlightField> Fields { get; set; }
		public bool? RequireFieldMatch { get; set; }
		public string BoundaryChars { get; set; }
		public IQueryContainer HighlightQuery { get; set; }
	}

	public class HighlightDescriptor<T> : IHighlightRequest
		where T : class
	{
		protected IHighlightRequest Self => this;

		private HighlightDescriptor<T> _assign(Action<IHighlightRequest> assigner) => Fluent.Assign(this, assigner);

		IEnumerable<string> IHighlightRequest.PreTags { get; set; }

		IEnumerable<string> IHighlightRequest.PostTags { get; set; }

		int? IHighlightRequest.FragmentSize { get; set; }

		string IHighlightRequest.TagsSchema { get; set; }

		int? IHighlightRequest.NumberOfFragments { get; set; }

		int? IHighlightRequest.FragmentOffset { get; set; }

		int? IHighlightRequest.BoundaryMaxSize { get; set; }

		string IHighlightRequest.Encoder { get; set; }

		string IHighlightRequest.Order { get; set; }

		Dictionary<FieldName, IHighlightField> IHighlightRequest.Fields { get; set; }

		bool? IHighlightRequest.RequireFieldMatch { get; set; }

		string IHighlightRequest.BoundaryChars { get; set; }

		IQueryContainer IHighlightRequest.HighlightQuery { get; set; }

		public HighlightDescriptor<T> OnFields(params Func<HighlightFieldDescriptor<T>, IHighlightField>[] fieldHighlighters) =>
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
