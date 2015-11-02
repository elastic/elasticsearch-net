using System;
using System.Collections.Generic;
using System.Linq;
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
		HighlighterType Type { get; set; }

		[JsonProperty("force_source")]
		bool? ForceSource { get; set; }

		[JsonProperty("matched_fields")]
		IEnumerable<Field> MatchedFields { get; set; }
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
		public HighlighterType Type { get; set; }
		public bool? ForceSource { get; set; }
		public IEnumerable<Field> MatchedFields { get; set; }
	}

	public class HighlightFieldDescriptor<T> : IHighlightField where T : class
	{
		protected IHighlightField Self => this;

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

		HighlighterType IHighlightField.Type { get; set; }

		bool? IHighlightField.ForceSource { get; set; }

		IEnumerable<Field> IHighlightField.MatchedFields { get; set; }
		
		public HighlightFieldDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}
		public HighlightFieldDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}
		public HighlightFieldDescriptor<T> OnAll()
		{
			return this.OnField("_all");
		}
		public HighlightFieldDescriptor<T> TagsSchema(string schema = "styled")
		{
			Self.TagsSchema = schema;
			return this;
		}
		public HighlightFieldDescriptor<T> ForceSource(bool force = true)
		{
			Self.ForceSource = force;
			return this;
		}
		public HighlightFieldDescriptor<T> Type(HighlighterType type)
		{
			Self.Type = type;
			return this;
		}
		public HighlightFieldDescriptor<T> PreTags(string preTags)
		{
			Self.PreTags = new[] { preTags };
			return this;
		}
		public HighlightFieldDescriptor<T> PostTags(string postTags)
		{
			Self.PostTags = new[] { postTags };
			return this;
		}
		public HighlightFieldDescriptor<T> PreTags(IEnumerable<string> preTags)
		{
			Self.PreTags = preTags;
			return this;
		}
		public HighlightFieldDescriptor<T> PostTags(IEnumerable<string> postTags)
		{
			Self.PostTags = postTags;
			return this;
		}
		public HighlightFieldDescriptor<T> FragmentSize(int fragmentSize)
		{
			Self.FragmentSize = fragmentSize;
			return this;
		}
        public HighlightFieldDescriptor<T> NoMatchSize(int noMatchSize)
        {
            Self.NoMatchSize = noMatchSize;
            return this;
        }
		public HighlightFieldDescriptor<T> NumberOfFragments(int numberOfFragments)
		{
			Self.NumberOfFragments = numberOfFragments;
			return this;
		}
		public HighlightFieldDescriptor<T> FragmentOffset(int fragmentOffset)
		{
			Self.FragmentOffset = fragmentOffset;
			return this;
		}
		public HighlightFieldDescriptor<T> Encoder(string encoder)
		{
			Self.Encoder = encoder;
			return this;
		}
		public HighlightFieldDescriptor<T> Order(string order)
		{
			Self.Order = order;
			return this;
		}
		public HighlightFieldDescriptor<T> RequireFieldMatch(bool requireFieldMatch)
		{
			Self.RequireFieldMatch = requireFieldMatch;
			return this;
		}
		public HighlightFieldDescriptor<T> BoundaryCharacters(string boundaryCharacters)
		{
			Self.BoundaryChars = boundaryCharacters;
			return this;
		}
		public HighlightFieldDescriptor<T> BoundaryMaxSize(int boundaryMaxSize)
		{
			Self.BoundaryMaxSize = boundaryMaxSize;
			return this;
		}
		public HighlightFieldDescriptor<T> MatchedFields(IEnumerable<string> fields)
		{
			Self.MatchedFields = fields.Select(f => (Field)f);
			return this;
		}
		public HighlightFieldDescriptor<T> MatchedFields(params Expression<Func<T, object>>[] objectPaths)
		{
			Self.MatchedFields = objectPaths.Select(f => (Field)f);
			return this;
		}
	}
}
