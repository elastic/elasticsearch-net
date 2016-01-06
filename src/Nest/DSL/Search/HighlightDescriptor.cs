using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<HighlightRequest>))]
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
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<PropertyPathMarker, IHighlightField> Fields { get; set; }

		[JsonProperty("require_field_match")]
		bool? RequireFieldMatch { get; set; }

		[JsonProperty("boundary_chars")]
		string BoundaryChars { get; set; }

		[JsonProperty("highlight_query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
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
		public Dictionary<PropertyPathMarker, IHighlightField> Fields { get; set; }
		public bool? RequireFieldMatch { get; set; }
		public string BoundaryChars { get; set; }
		public IQueryContainer HighlightQuery { get; set; }
	}

	public class HighlightDescriptor<T> : IHighlightRequest
		where T : class
	{
		protected IHighlightRequest Self { get { return this; } }

		IEnumerable<string> IHighlightRequest.PreTags { get; set; }

		IEnumerable<string> IHighlightRequest.PostTags { get; set; }

		int? IHighlightRequest.FragmentSize { get; set; }

		string IHighlightRequest.TagsSchema { get; set; }

		int? IHighlightRequest.NumberOfFragments { get; set; }

		int? IHighlightRequest.FragmentOffset { get; set; }

		int? IHighlightRequest.BoundaryMaxSize { get; set; }

		string IHighlightRequest.Encoder { get; set; }

		string IHighlightRequest.Order { get; set; }

		Dictionary<PropertyPathMarker, IHighlightField> IHighlightRequest.Fields { get; set; }

		bool? IHighlightRequest.RequireFieldMatch { get; set; }

		string IHighlightRequest.BoundaryChars { get; set; }

		IQueryContainer IHighlightRequest.HighlightQuery { get; set; }

		public HighlightDescriptor<T> OnFields(params Action<HighlightFieldDescriptor<T>>[] fieldHighlighters)
		{
			fieldHighlighters.ThrowIfEmpty("fieldHighlighters");

			var descriptors = new List<IHighlightField>();
			foreach (var selector in fieldHighlighters)
			{
				var filter = new HighlightFieldDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			Self.Fields = new Dictionary<PropertyPathMarker, IHighlightField>();
			foreach (var d in descriptors)
			{
				var key = d.Field;
				if (key == null)
					throw new DslException("Could not infer key for highlight field descriptor");

				Self.Fields.Add(key, d);

			}
			return this;
		}

		public HighlightDescriptor<T> TagsSchema(string schema = "styled")
		{
			Self.TagsSchema = schema;
			return this;
		}
		public HighlightDescriptor<T> PreTags(string preTags)
		{
			Self.PreTags = new[] { preTags };
			return this;
		}
		public HighlightDescriptor<T> PostTags(string postTags)
		{
			Self.PostTags = new[] { postTags };
			return this;
		}
		public HighlightDescriptor<T> PreTags(IEnumerable<string> preTags)
		{
			Self.PreTags = preTags;
			return this;
		}
		public HighlightDescriptor<T> PostTags(IEnumerable<string> postTags)
		{
			Self.PostTags = postTags;
			return this;
		}
		public HighlightDescriptor<T> FragmentSize(int fragmentSize)
		{
			Self.FragmentSize = fragmentSize;
			return this;
		}
		public HighlightDescriptor<T> NumberOfFragments(int numberOfFragments)
		{
			Self.NumberOfFragments = numberOfFragments;
			return this;
		}
		public HighlightDescriptor<T> FragmentOffset(int fragmentOffset)
		{
			Self.FragmentOffset = fragmentOffset;
			return this;
		}
		public HighlightDescriptor<T> Encoder(string encoder)
		{
			Self.Encoder = encoder;
			return this;
		}
		public HighlightDescriptor<T> Order(string order)
		{
			Self.Order = order;
			return this;
		}
		public HighlightDescriptor<T> RequireFieldMatch(bool requireFieldMatch)
		{
			Self.RequireFieldMatch = requireFieldMatch;
			return this;
		}
		public HighlightDescriptor<T> BoundaryCharacters(string boundaryCharacters)
		{
			Self.BoundaryChars = boundaryCharacters;
			return this;
		}
		public HighlightDescriptor<T> BoundaryMaxSize(int boundaryMaxSize)
		{
			Self.BoundaryMaxSize = boundaryMaxSize;
			return this;
		}
		public HighlightDescriptor<T> HighlightQuery(Func<QueryDescriptor<T>, QueryContainer> query)
		{
			Self.HighlightQuery = query(new QueryDescriptor<T>());
			return this;
		}
	}
}
