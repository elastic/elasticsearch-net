using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
  public class HighlightDescriptor<T> where T : class
  {
    [JsonProperty("pre_tags")]
    internal IEnumerable<string> _PreTags { get; set; }

    [JsonProperty("post_tags")]
    internal IEnumerable<string> _PostTags { get; set; }

    [JsonProperty("fragment_size")]
    internal int? _FragmentSize { get; set; }

	[JsonProperty("tags_schema")]
	internal string _TagsSchema { get; set; }

    [JsonProperty("number_of_fragments")]
    internal int? _NumberOfFragments { get; set; }

    [JsonProperty("fragment_offset")]
    internal int? _FragmentOffset { get; set; }

    [JsonProperty("boundary_max_size")]
    internal int? _BoundaryMaxSize { get; set; }

    [JsonProperty("encoder")]
    internal string _Encoder { get; set; }

    [JsonProperty("order")]
    internal string _Order { get; set; }

    [JsonProperty(PropertyName = "fields")]
	[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
	internal Dictionary<PropertyPathMarker, HighlightFieldDescriptor<T>> _Fields { get; set; }

    [JsonProperty("require_field_match")]
    internal bool? _RequireFieldMatch { get; set; }

    [JsonProperty("boundary_chars")]
    internal string _BoundaryChars { get; set; }

    public HighlightDescriptor<T> OnFields(params Action<HighlightFieldDescriptor<T>>[] fieldHighlighters)
    {
      fieldHighlighters.ThrowIfEmpty("fieldHighlighters");

      var descriptors = new List<HighlightFieldDescriptor<T>>();
      foreach (var selector in fieldHighlighters)
      {
        var filter = new HighlightFieldDescriptor<T>();
        selector(filter);
        descriptors.Add(filter);
      }
      this._Fields = new Dictionary<PropertyPathMarker, HighlightFieldDescriptor<T>>();
      foreach (var d in descriptors)
      {
        var key = d._Field;
        if (key == null)
          throw new DslException("Could not infer key for highlight field descriptor");

        this._Fields.Add(key, d);

      }
      return this;
    }

	public HighlightDescriptor<T> TagsSchema(string schema = "styled")
	{
		this._TagsSchema = schema;
		return this;
	}
    public HighlightDescriptor<T> PreTags(string preTags)
    {
      this._PreTags = new[] { preTags };
      return this;
    }
    public HighlightDescriptor<T> PostTags(string postTags)
    {
      this._PostTags = new[] { postTags };
      return this;
    }
    public HighlightDescriptor<T> PreTags(IEnumerable<string> preTags)
    {
      this._PreTags = preTags;
      return this;
    }
    public HighlightDescriptor<T> PostTags(IEnumerable<string> postTags)
    {
      this._PostTags = postTags;
      return this;
    }
    public HighlightDescriptor<T> FragmentSize(int fragmentSize)
    {
      this._FragmentSize = fragmentSize;
      return this;
    }
    public HighlightDescriptor<T> NumberOfFragments(int numberOfFragments)
    {
      this._NumberOfFragments = numberOfFragments;
      return this;
    }
    public HighlightDescriptor<T> FragmentOffset(int fragmentOffset)
    {
      this._FragmentOffset = fragmentOffset;
      return this;
    }
    public HighlightDescriptor<T> Encoder(string encoder)
    {
      this._Encoder = encoder;
      return this;
    }
    public HighlightDescriptor<T> Order(string order)
    {
      this._Order = order;
      return this;
    }
    public HighlightDescriptor<T> RequireFieldMatch(bool requireFieldMatch)
    {
      this._RequireFieldMatch = requireFieldMatch;
      return this;
    }
    public HighlightDescriptor<T> BoundaryCharacters(string boundaryCharacters)
    {
      this._BoundaryChars = boundaryCharacters;
      return this;
    }
    public HighlightDescriptor<T> BoundaryMaxSize(int boundaryMaxSize)
    {
      this._BoundaryMaxSize = boundaryMaxSize;
      return this;
    }
  }
}
