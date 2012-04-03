using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
  public class HighlightFieldDescriptor<T>  where T : class
  {
    internal string _Field { get; set; }
    [JsonProperty("pre_tags")]
    internal string _PreTags { get; set; }

    [JsonProperty("post_tags")]
    internal string _PostTags { get; set; }

    [JsonProperty("fragment_size")]
    internal int? _FragmentSize { get; set; }

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

    [JsonProperty("require_field_match")]
    internal bool? _RequireFieldMatch { get; set; }

    [JsonProperty("boundary_chars")]
    internal string _BoundaryChars { get; set; }

    public HighlightFieldDescriptor<T> OnField(string field)
    {
      this._Field = field;
      return this;
    }
    public HighlightFieldDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
    {
      var fieldName = ElasticClient.PropertyNameResolver.Resolve(objectPath);
      return this.OnField(fieldName);
    }
    public HighlightFieldDescriptor<T> OnAll()
    {
      return this.OnField("_all");
    }
    public HighlightFieldDescriptor<T> PreTags(string preTags)
    {
      this._PreTags = preTags;
      return this;
    }
    public HighlightFieldDescriptor<T> PostTags(string postTags)
    {
      this._PostTags = postTags;
      return this;
    }
    public HighlightFieldDescriptor<T> FragmentSize(int fragmentSize)
    {
      this._FragmentSize = fragmentSize;
      return this;
    }
    public HighlightFieldDescriptor<T> NumberOfFragments(int numberOfFragments)
    {
      this._NumberOfFragments = numberOfFragments;
      return this;
    }
    public HighlightFieldDescriptor<T> FragmentOffset(int fragmentOffset)
    {
      this._FragmentOffset = fragmentOffset;
      return this;
    }
    public HighlightFieldDescriptor<T> Encoder(string encoder)
    {
      this._Encoder = encoder;
      return this;
    }
    public HighlightFieldDescriptor<T> Order(string order)
    {
      this._Order = order;
      return this;
    }
    public HighlightFieldDescriptor<T> RequireFieldMatch(bool requireFieldMatch)
    {
      this._RequireFieldMatch = requireFieldMatch;
      return this;
    }
    public HighlightFieldDescriptor<T> BoundaryCharacters(string boundaryCharacters)
    {
      this._BoundaryChars = boundaryCharacters;
      return this;
    }
    public HighlightFieldDescriptor<T> BoundaryMaxSize(int boundaryMaxSize)
    {
      this._BoundaryMaxSize = boundaryMaxSize;
      return this;
    }
  }
}
