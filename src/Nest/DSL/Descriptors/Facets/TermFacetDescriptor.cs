using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest.DSL
{
  public interface IFacetDescriptor
  {

  }
  public enum FacetOrder
  {
    count = 0,
    term,
    reverse_count,
    reverse_term
  }
  public enum RegexFlags
  {
    CANNON_EQ,
    CASE_INSENSITIVE,
    COMMENTS,
    DOTALL,
    LITERAL,
    MULTILINE,
    UNICODE_CASE,
    UNIX_LINES
  }

  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class FacetDescriptorsBucket<T> where T : class
  {
    [JsonProperty(PropertyName = "terms")]
    public TermFacetDescriptor<T> Terms { get; set; }
  }

  [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
  public class TermFacetDescriptor<T> : IFacetDescriptor where T : class
  {
    [JsonProperty(PropertyName = "field")]
    internal string _Field { get; set; }
    [JsonProperty(PropertyName = "fields")]
    internal IEnumerable<string> _Fields { get; set; }
    [JsonProperty(PropertyName = "size")]
    internal int _Size { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty(PropertyName = "order")]
    internal FacetOrder? _FacetOrder { get; set; }
    [JsonProperty(PropertyName = "all_terms")]
    internal bool? _AllTerms { get; set; }
    [JsonProperty(PropertyName = "exclude")]
    internal IEnumerable<string> _Exclude { get; set; }

    [JsonProperty(PropertyName = "regex")]
    internal string _Regex { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty(PropertyName = "regex_flags")]
    internal RegexFlags? _RegexFlags { get; set; }

    [JsonProperty(PropertyName = "script")]
    internal string _Script { get; set; }
    [JsonProperty(PropertyName = "script_field")]
    internal string _ScriptField { get; set; }
    //      v
    public TermFacetDescriptor<T> OnField(string field)
    {
      if (this._Fields != null)
        this._Fields = null;
      this._Field = field;
      return this;
    }
    public TermFacetDescriptor<T> OnFields(params string[] fields)
    {
      if (this._Field != null)
        this._Field = null;
      this._Fields = fields;
      return this;
    }
    public TermFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
    {
      var fieldName = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
      return this.OnField(fieldName);
    }
    public TermFacetDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths)
    {
      var fieldNames = objectPaths.Select(o => ElasticClient.PropertyNameResolver.ResolveToSort(o))
        .ToArray();

      return this.OnFields(fieldNames);
    }
    public TermFacetDescriptor<T> Size(int size)
    {
      this._Size = size;
      return this;
    }
    public TermFacetDescriptor<T> Order(FacetOrder order)
    {
      this._FacetOrder = order;
      return this;
    }
    public TermFacetDescriptor<T> Exclude(params string[] args)
    {
      this._Exclude = args;
      return this;
    }
    public TermFacetDescriptor<T> AllTerms()
    {
      this._AllTerms = true;
      return this;
    }
    public TermFacetDescriptor<T> Regex(string regex, RegexFlags? Flags = null)
    {
      this._Regex = regex;
      this._RegexFlags = Flags;
      return this;
    }
    public TermFacetDescriptor<T> Script(string script)
    {
      this._Script = script;
      return this;
    }
    public TermFacetDescriptor<T> ScriptField(string scriptField)
    {
      this._ScriptField = scriptField;
      return this;
    }
  }
}
