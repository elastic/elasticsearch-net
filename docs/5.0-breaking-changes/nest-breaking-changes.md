# Breaking Changes

### StatsAggregator renamed to StatsAggregation

`IStatsAggregator` not named correctly all aggregation requests objects need to end with `Aggregation`

**public property Nest.IAggregationContainer.Stats** *Declaration changed (Breaking)*

2.x: `public IStatsAggregator Stats { get; set; }`  
5.x: `public IStatsAggregation Stats { get; set; }` 

**public method Nest.AggregationContainerDescriptor&lt;T&gt;.Stats** *Declaration changed (Breaking)*

2.x: `public AggregationContainerDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, IStatsAggregator> selector)`  
5.x: `public AggregationContainerDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, IStatsAggregation> selector)`  

**public interface Nest.IStatsAggregator** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[ContractJsonConverterAttribute(Nest.AggregationJsonConverter`1[Nest.StatsAggregation])]
public interface IStatsAggregator : IMetricAggregation, IAggregation
```

### KeyedBucket is now generic

No longer always reads the key as string https://github.com/elastic/elasticsearch-net/issues/2336

**public method Nest.AggregationsHelper.GeoHash** *Declaration changed (Breaking)*

2.x: `public MultiBucketAggregate<KeyedBucket> GeoHash(string key)`  
5.x: `public MultiBucketAggregate<KeyedBucket<string>> GeoHash(string key)`  

**public method Nest.AggregationsHelper.Histogram** *Declaration changed (Breaking)*

2.x: `public MultiBucketAggregate<HistogramBucket> Histogram(string key)`  
5.x: `public MultiBucketAggregate<KeyedBucket<double>> Histogram(string key)`  

**public method Nest.AggregationsHelper.Terms** *Declaration changed (Breaking)*

2.x: `public TermsAggregate Terms(string key)`  
5.x: `public TermsAggregate<string> Terms(string key)`  


**public method Nest.AggregationsHelper..ctor** *Visibility was changed from public to protected (Breaking)*

2.x: `public  .ctor(IDictionary<string, IAggregate> aggregations)`  
5.x: `protected  .ctor(IDictionary<string, IAggregate> aggregations)`  

### String Property Mapping is obsolete

See also: https://www.elastic.co/guide/en/elasticsearch/reference/current/breaking_50_mapping_changes.html#_literal_string_literal_fields_replaced_by_literal_text_literal_literal_keyword_literal_fields

This is also reflected in the attachment mappings

**public property Nest.AttachmentProperty.AuthorField** *Declaration changed (Breaking)*

2.x: `public IStringProperty AuthorField { get; set; }`  
5.x: `public ITextProperty AuthorField { get; set; }`  

**public property Nest.AttachmentProperty.ContentTypeField** *Declaration changed (Breaking)*

2.x: `public IStringProperty ContentTypeField { get; set; }`  
5.x: `public ITextProperty ContentTypeField { get; set; }`  

**public property Nest.AttachmentProperty.KeywordsField** *Declaration changed (Breaking)*

2.x: `public IStringProperty KeywordsField { get; set; }`  
5.x: `public ITextProperty KeywordsField { get; set; }`  

**public property Nest.AttachmentProperty.LanguageField** *Declaration changed (Breaking)*

2.x: `public IStringProperty LanguageField { get; set; }`  
5.x: `public ITextProperty LanguageField { get; set; }`  

**public property Nest.AttachmentProperty.NameField** *Declaration changed (Breaking)*

2.x: `public IStringProperty NameField { get; set; }`  
5.x: `public ITextProperty NameField { get; set; }`  

**public property Nest.AttachmentProperty.TitleField** *Declaration changed (Breaking)*

2.x: `public IStringProperty TitleField { get; set; }`  
5.x: `public ITextProperty TitleField { get; set; }`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.AuthorField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> AuthorField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> AuthorField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.ContentTypeField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> ContentTypeField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> ContentTypeField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.FileField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> FileField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> FileField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.KeywordsField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> KeywordsField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> KeywordsField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.LanguageField** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("Use LanguageField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)")]
public AttachmentPropertyDescriptor<T> LanguageField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector)
```

5.x
```csharp
public AttachmentPropertyDescriptor<T> LanguageField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)
```

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.NameField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> NameField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> NameField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.TitleField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> TitleField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> TitleField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)`  

**public property Nest.IAttachmentProperty.AuthorField** *Declaration changed (Breaking)*

2.x: `public IStringProperty AuthorField { get; set; }`  
5.x: `public ITextProperty AuthorField { get; set; }`  

**public property Nest.IAttachmentProperty.ContentTypeField** *Declaration changed (Breaking)*

2.x: `public IStringProperty ContentTypeField { get; set; }`  
5.x: `public ITextProperty ContentTypeField { get; set; }`  

**public property Nest.IAttachmentProperty.KeywordsField** *Declaration changed (Breaking)*

2.x: `public IStringProperty KeywordsField { get; set; }`  
5.x: `public ITextProperty KeywordsField { get; set; }`  

**public property Nest.IAttachmentProperty.LanguageField** *Declaration changed (Breaking)*

2.x: `public IStringProperty LanguageField { get; set; }`  
5.x: `public ITextProperty LanguageField { get; set; }`  

**public property Nest.IAttachmentProperty.NameField** *Declaration changed (Breaking)*

2.x: `public IStringProperty NameField { get; set; }`  
5.x: `public ITextProperty NameField { get; set; }`  

**public property Nest.IAttachmentProperty.TitleField** *Declaration changed (Breaking)*

2.x: `public IStringProperty TitleField { get; set; }`  
5.x: `public ITextProperty TitleField { get; set; }`  

### NonStringIndexOption no longer valid

See also: https://www.elastic.co/guide/en/elasticsearch/reference/5.0/breaking_50_mapping_changes.html#_literal_index_literal_property

**public enum Nest.NonStringIndexOption** *Removed (Breaking)*

**public property Nest.BooleanAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public bool Index { get; set; }`  

**public property Nest.BooleanProperty.Index** *Declaration changed (Breaking)*

2.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  
5.x: `public bool? Index { get; set; }`  

**public method Nest.BooleanPropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public BooleanPropertyDescriptor<T> Index(NonStringIndexOption index = 0)`  
5.x: `public BooleanPropertyDescriptor<T> Index(bool index)`  

**public property Nest.DateAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public bool Index { get; set; }`  

**public property Nest.DateProperty.Index** *Declaration changed (Breaking)*

2.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  
5.x: `public bool? Index { get; set; }`  

**public method Nest.DatePropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public DatePropertyDescriptor<T> Index(NonStringIndexOption index = 0)`  
5.x: `public DatePropertyDescriptor<T> Index(bool index)`  

**public property Nest.IBooleanProperty.Index** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("index")]
public Nullable<NonStringIndexOption> Index { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("index")]
public bool? Index { get; set; }
```

**public property Nest.IDateProperty.Index** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("index")]
public Nullable<NonStringIndexOption> Index { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("index")]
public bool? Index { get; set; }
```

**public property Nest.IIpProperty.Index** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("index")]
public Nullable<NonStringIndexOption> Index { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("index")]
public bool? Index { get; set; }
```

**public property Nest.INumberProperty.Index** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("index")]
public Nullable<NonStringIndexOption> Index { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("index")]
public bool? Index { get; set; }
```

**public property Nest.IpAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public bool Index { get; set; }`  

**public property Nest.IpProperty.Index** *Declaration changed (Breaking)*

2.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  
5.x: `public bool? Index { get; set; }`  

**public method Nest.IpPropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public IpPropertyDescriptor<T> Index(Nullable<NonStringIndexOption> index = 0)`  
5.x: `public IpPropertyDescriptor<T> Index(bool index)`  

**public property Nest.NumberAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public bool Index { get; set; }`  

**public property Nest.NumberProperty.Index** *Declaration changed (Breaking)*

2.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  
5.x: `public bool? Index { get; set; }`  

**public method Nest.NumberPropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public TDescriptor Index(NonStringIndexOption index = 0)`  
5.x: `public TDescriptor Index(bool index)`  

### Refresh no longer a simple boolean

As it now also accepts a `wait_for` parameter

See also: https://www.elastic.co/guide/en/elasticsearch/reference/5.0/docs-refresh.html

**public method Nest.BulkAllDescriptor&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public BulkAllDescriptor<T> Refresh(bool refresh = True)`  
5.x: `public BulkAllDescriptor<T> Refresh(Refresh refresh)`  

**public property Nest.BulkAllRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool? Refresh { get; set; }`  
5.x: `public Nullable<Refresh> Refresh { get; set; }`  

**public method Nest.BulkDescriptor.Refresh** *Declaration changed (Breaking)*

2.x: `public BulkDescriptor Refresh(bool refresh = True)`  
5.x: `public BulkDescriptor Refresh(Refresh refresh)`  


**public property Nest.BulkRequest.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public Refresh Refresh { get; set; }`  

**public method Nest.DeleteDescriptor&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public DeleteDescriptor<T> Refresh(bool refresh = True)`  
5.x: `public DeleteDescriptor<T> Refresh(Refresh refresh)`  

**public property Nest.DeleteRequest.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public Refresh Refresh { get; set; }`  

**public property Nest.DeleteRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public Refresh Refresh { get; set; }`  

**public property Nest.IBulkAllRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool? Refresh { get; set; }`  
5.x: `public Nullable<Refresh> Refresh { get; set; }`  

**public method Nest.IndexDescriptor&lt;TDocument&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public IndexDescriptor<TDocument> Refresh(bool refresh = True)`  
5.x: `public IndexDescriptor<TDocument> Refresh(Refresh refresh)`  

**public property Nest.IndexRequest&lt;TDocument&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public Refresh Refresh { get; set; }`  

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public UpdateDescriptor<TDocument, TPartialDocument> Refresh(bool refresh = True)`  
5.x: `public UpdateDescriptor<TDocument, TPartialDocument> Refresh(Refresh refresh)`  

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public Refresh Refresh { get; set; }`  

### Script changes

The default language is now painless! Also we no longer support the `1.x` inline syntax for scripts.
https://www.elastic.co/guide/en/elasticsearch/reference/current/breaking_50_scripting.html#_removed_1_x_script_and_template_syntax

**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(string script)`  
5.x: `public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector)`  

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public string Script { get; set; }`  
5.x: `public IScript Script { get; set; }`  

**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public string Script { get; set; }`  
5.x: `public IScript Script { get; set; }`  

**public property Nest.IPhraseSuggestCollate.Query** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IScript Query { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public ITemplateQuery Query { get; set; }
```

**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public string Script { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IScript Script { get; set; }
```

**public property Nest.PhraseSuggestCollate.Query** *Declaration changed (Breaking)*

2.x: `public IScript Query { get; set; }`  
5.x: `public ITemplateQuery Query { get; set; }`  

**public method Nest.PhraseSuggestCollateDescriptor&lt;T&gt;.Query** *Removed (Breaking)*

```csharp
public PhraseSuggestCollateDescriptor<T> Query(Func<ScriptDescriptor, IScript> scriptSelector)
```

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public UpdateDescriptor<TDocument, TPartialDocument> Script(string script)`  
5.x: `public UpdateDescriptor<TDocument, TPartialDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector)`  

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public string Script { get; set; }`  
5.x: `public IScript Script { get; set; }`  

**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*  
**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*  
**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Lang** *Removed (Breaking)*  
**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*  
**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*  
**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*  
**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Lang** *Removed (Breaking)*  
**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*  
**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*  
**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*  
**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.Language** *Removed (Breaking)*  
**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*  
**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*  
**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*  
**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Language** *Removed (Breaking)*  
**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*  
**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptedUpsert** *Removed (Breaking)*  
**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*  
**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*  
**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptQueryString** *Removed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Language** *Removed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptedUpsert** *Removed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptQueryString** *Removed (Breaking)*  


### I*Operation on bulk is now IBulk*Operation

Impact is low unless you have casting code in your application

**public method Nest.BulkDescriptor.Index&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, IIndexOperation<T>> bulkIndexSelector)`  
5.x: `public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, IBulkIndexOperation<T>> bulkIndexSelector)`  

**public method Nest.BulkDescriptor.IndexMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public BulkDescriptor IndexMany<T>(IEnumerable<T> objects, Func<BulkIndexDescriptor<T>, T, IIndexOperation<T>> bulkIndexSelector)`  
5.x: `public BulkDescriptor IndexMany<T>(IEnumerable<T> objects, Func<BulkIndexDescriptor<T>, T, IBulkIndexOperation<T>> bulkIndexSelector)`  

**public interface Nest.IIndexOperation&lt;T&gt;** *Renamed (Breaking)*


### Cat Threadpool changes

Cat threadpool underwent a complete makeover in core: https://github.com/elastic/elasticsearch/pull/19721

**public property Nest.CatThreadPoolRecord.Port** *Declaration changed (Breaking)*

2.x: `public string Port { get; set; }`
5.x: `public int Port { get; set; }`

**public class Nest.CatThreadPool** *Removed (Breaking)*  
**public method Nest.CatThreadPoolDescriptor.FullId** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Bulk** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Flush** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Generic** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Get** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Id** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Index** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Management** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Merge** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Optimize** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Percolate** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Pid** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Refresh** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Search** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Snapshot** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Suggest** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRecord.Warmer** *Removed (Breaking)*  
**public property Nest.CatThreadPoolRequest.FullId** *Removed (Breaking)*  

### WaitForActiveShards is now a string

See also: https://github.com/elastic/elasticsearch/pull/20186

**public method Nest.ClusterHealthDescriptor.WaitForActiveShards** *Declaration changed (Breaking)*

2.x: `public ClusterHealthDescriptor WaitForActiveShards(long wait_for_active_shards)`  
5.x: `public ClusterHealthDescriptor WaitForActiveShards(string wait_for_active_shards)`  

**public property Nest.ClusterHealthRequest.WaitForActiveShards** *Declaration changed (Breaking)*

2.x: `public long WaitForActiveShards { get; set; }`  
5.x: `public string WaitForActiveShards { get; set; }`  

### AutoExpandReplicas is now an actual type

Binary break only, still implicitly converts from string

**public property Nest.DynamicIndexSettings.AutoExpandReplicas** *Declaration changed (Breaking)*

2.x: `public string AutoExpandReplicas { get; set; }`  
5.x: `public AutoExpandReplicas AutoExpandReplicas { get; set; }`  

**public method Nest.DynamicIndexSettingsDescriptorBase&lt;TDescriptor, TIndexSettings&gt;.AutoExpandReplicas** *Declaration changed (Breaking)*

2.x: `public TDescriptor AutoExpandReplicas(string AutoExpandReplicas)`  
5.x: `public TDescriptor AutoExpandReplicas(AutoExpandReplicas autoExpandReplicas)`  

### DslPrerttyPrints methods are now virtual

### Nest visitors should be bound to interface

The visitors should be passed interfaces not concrete types see: https://github.com/elastic/elasticsearch-net/pull/2320

### Deprecated queryies are now removed

See also: https://www.elastic.co/guide/en/elasticsearch/reference/current/breaking_50_search_changes.html#_deprecated_queries_removed

**public property Nest.IQueryContainer.Missing** *Removed (Breaking)*  
**public method Nest.Query&lt;T&gt;.Missing** *Removed (Breaking)*  
**public interface Nest.IMissingQuery** *Removed (Breaking)*  
**public class Nest.MissingQuery** *Removed (Breaking)*  
**public class Nest.MissingQueryDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.Missing** *Removed (Breaking)*  
**public property Nest.IQueryContainer.And** *Removed (Breaking)*  
**public property Nest.IQueryContainer.Filtered** *Removed (Breaking)*  
**public property Nest.IQueryContainer.Limit** *Removed (Breaking)*  
**public property Nest.IQueryContainer.Not** *Removed (Breaking)*  
**public property Nest.IQueryContainer.Or** *Removed (Breaking)*  
**public class Nest.FilteredQuery** *Removed (Breaking)*  
**public class Nest.FilteredQueryDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public interface Nest.IFilteredQuery** *Removed (Breaking)*  
**public method Nest.Query&lt;T&gt;.And** *Removed (Breaking)*  
**public method Nest.Query&lt;T&gt;.Filtered** *Removed (Breaking)*   
**public method Nest.Query&lt;T&gt;.Limit** *Removed (Breaking)*  
**public method Nest.Query&lt;T&gt;.Not** *Removed (Breaking)*  
**public method Nest.Query&lt;T&gt;.Or** *Removed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.And** *Removed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.Filtered** *Removed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.Not** *Removed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.Or** *Removed (Breaking)*  
**public class Nest.AndQuery** *Removed (Breaking)*  
**public class Nest.AndQueryDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public interface Nest.IAndQuery** *Removed (Breaking)*  
**public interface Nest.IOrQuery** *Removed (Breaking)*  
**public class Nest.OrQuery** *Removed (Breaking)*  
**public class Nest.OrQueryDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.Limit** *Removed (Breaking)*  
**public interface Nest.ILimitQuery** *Removed (Breaking)*  
**public class Nest.LimitQuery** *Removed (Breaking)*  
**public class Nest.LimitQueryDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public interface Nest.INotQuery** *Removed (Breaking)*  
**public class Nest.NotQuery** *Removed (Breaking)*  
**public class Nest.NotQueryDescriptor&lt;T&gt;** *Removed (Breaking)*  


### Dynamic mapping now sends true/false

And is now a union of `bool` and `DynamicMapping`

**public enum Nest.DynamicMapping** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Nest.DynamicMappingJsonConverter)]
public enum DynamicMapping
{
     Allow = 0,
     Ignore = 1,
     Strict = 2
}
```

5.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum DynamicMapping
{
     Strict = 0
}
```

### CodeStandards changes

Impact low, various binary breaking changes of code that did not adhere to our coding conventions

**public method Nest.BoolQueryDescriptor&lt;T&gt;.DisableCoord** *Declaration changed (Breaking)*

2.x: `public BoolQueryDescriptor<T> DisableCoord()`  
5.x: `public BoolQueryDescriptor<T> DisableCoord(bool? disableCoord = True)`  

**public method Nest.DateHistogramAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public DateHistogramAggregationDescriptor<T> Field(string field)`  
5.x: `public DateHistogramAggregationDescriptor<T> Field(Field field)`  


**public method Nest.DateRangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public DateRangeAggregationDescriptor<T> Field(string field)`  
5.x: `public DateRangeAggregationDescriptor<T> Field(Field field)`  

**public method Nest.DecayFunctionDescriptorBase&lt;TDescriptor, TOrigin, TScale, T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public TDescriptor Field(string field)`  
5.x: `public TDescriptor Field(Field field)`  

**public method Nest.DeleteManyExtensions.DeleteMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static IBulkResponse DeleteMany<T>(IElasticClient client, IEnumerable<T> objects, string index, string type)
```

5.x
```csharp
[ExtensionAttribute]
public static IBulkResponse DeleteMany<T>(IElasticClient client, IEnumerable<T> objects, IndexName index, TypeName type)
```

**public method Nest.GetManyExtensions.GetMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static IEnumerable<IMultiGetHit<T>> GetMany<T>(IElasticClient client, IEnumerable<long> ids, string index, string type)
```

5.x
```csharp
[ExtensionAttribute]
public static IEnumerable<IMultiGetHit<T>> GetMany<T>(IElasticClient client, IEnumerable<long> ids, IndexName index, TypeName type)
```

**public method Nest.GetManyExtensions.GetMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static IEnumerable<IMultiGetHit<T>> GetMany<T>(IElasticClient client, IEnumerable<string> ids, string index, string type)
```

5.x
```csharp
[ExtensionAttribute]
public static IEnumerable<IMultiGetHit<T>> GetMany<T>(IElasticClient client, IEnumerable<string> ids, IndexName index, TypeName type)
```


### Visibility changes

**public class Nest.BucketsPathJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class BucketsPathJsonConverter : JsonConverter`  
5.x: `internal class BucketsPathJsonConverter : JsonConverter`  

**public class Nest.DictionaryResponseJsonConverter&lt;TResponse, TKey, TValue&gt;** *Visibility was changed from public to internal (Breaking)*

2.x: `public class DictionaryResponseJsonConverter<TResponse, TKey, TValue> : JsonConverter where TResponse : new(), IDictionaryResponse<TKey, TValue>`  
5.x: `internal class DictionaryResponseJsonConverter<TResponse, TKey, TValue> : JsonConverter where TResponse : new(), IDictionaryResponse<TKey, TValue>`  

### Response properties should not have setters

These properties had public setters which made no sense (readonly), impact low.

**public property Nest.ClusterRerouteResponse.State** *Visibility changed (Breaking)*  
**public property Nest.ClusterStatsResponse.ClusterName** *Visibility changed (Breaking)*  
**public property Nest.ClusterStatsResponse.Indices** *Visibility changed (Breaking)*  
**public property Nest.ClusterStatsResponse.Nodes** *Visibility changed (Breaking)*  
**public property Nest.ClusterStatsResponse.Status** *Visibility changed (Breaking)*  
**public property Nest.ClusterStatsResponse.Timestamp** *Visibility changed (Breaking)*  
**public property Nest.FieldMapping.FullName** *Visibility changed (Breaking)*  
**public property Nest.FieldStatsField.Density** *Visibility changed (Breaking)*  
**public property Nest.FieldStatsField.DocCount** *Visibility changed (Breaking)*  
**public property Nest.FieldStatsField.MaxDoc** *Visibility changed (Breaking)*  
**public property Nest.FieldStatsField.MaxValue** *Visibility changed (Breaking)*  
**public property Nest.FieldStatsField.MinValue** *Visibility changed (Breaking)*  
**public property Nest.FieldStatsField.SumDocumentFrequency** *Visibility changed (Breaking)*  
**public property Nest.FieldStatsField.SumTotalTermFrequency** *Visibility changed (Breaking)*  
**public property Nest.FieldStatsResponse.Shards** *Visibility changed (Breaking)*  
**public property Nest.GetSearchTemplateResponse.Template** *Visibility changed (Breaking)*  
**public property Nest.HotThreadInformation.NodeId** *Visibility changed (Breaking)*  
**public property Nest.HotThreadInformation.NodeName** *Visibility changed (Breaking)*  
**public property Nest.IndexHealthStats.ActivePrimaryShards** *Visibility changed (Breaking)*  
**public property Nest.IndexHealthStats.ActiveShards** *Visibility changed (Breaking)*  
**public property Nest.IndexHealthStats.InitializingShards** *Visibility changed (Breaking)*  
**public property Nest.IndexHealthStats.NumberOfReplicas** *Visibility changed (Breaking)*  
**public property Nest.IndexHealthStats.NumberOfShards** *Visibility changed (Breaking)*  
**public property Nest.IndexHealthStats.RelocatingShards** *Visibility changed (Breaking)*  
**public property Nest.IndexHealthStats.Status** *Visibility changed (Breaking)*  
**public property Nest.IndexHealthStats.UnassignedShards** *Visibility changed (Breaking)*  
**public property Nest.IndicesStatsResponse.Stats** *Visibility changed (Breaking)*  
**public property Nest.PendingTask.InsertOrder** *Visibility changed (Breaking)*  
**public property Nest.PendingTask.Priority** *Visibility changed (Breaking)*  
**public property Nest.PendingTask.Source** *Visibility changed (Breaking)*  
**public property Nest.PendingTask.TimeInQueue** *Visibility changed (Breaking)*  
**public property Nest.PendingTask.TimeInQueueMilliseconds** *Visibility changed (Breaking)*  
**public property Nest.PercolateCountResponse.Took** *Declaration changed (Breaking)*  
**public property Nest.PercolatorMatch.Id** *Visibility changed (Breaking)*  
**public property Nest.PercolatorMatch.Index** *Visibility changed (Breaking)*  
**public property Nest.PercolatorMatch.Score** *Visibility changed (Breaking)*  
**public property Nest.InstantGet&lt;T&gt;.Fields** *Visibility changed (Breaking)*  
**public property Nest.SearchNode.Name** *Visibility changed (Breaking)*  
**public property Nest.SearchNode.TransportAddress** *Visibility changed (Breaking)*  
**public property Nest.SearchResponse&lt;T&gt;.Took** *Declaration changed (Breaking)*  
**public property Nest.SearchShard.Index** *Visibility changed (Breaking)*  
**public property Nest.SearchShard.Node** *Visibility changed (Breaking)*   
**public property Nest.SearchShard.Primary** *Visibility changed (Breaking)*  
**public property Nest.SearchShard.RelocatingNode** *Visibility changed (Breaking)*  
**public property Nest.SearchShard.Shard** *Visibility changed (Breaking)*  
**public property Nest.SearchShard.State** *Visibility changed (Breaking)*  
**public property Nest.ShardHealthStats.ActiveShards** *Visibility changed (Breaking)*  
**public property Nest.ShardHealthStats.InitializingShards** *Visibility changed (Breaking)*  
**public property Nest.ShardHealthStats.PrimaryActive** *Visibility changed (Breaking)*  
**public property Nest.ShardHealthStats.RelocatingShards** *Visibility changed (Breaking)*  
**public property Nest.ShardHealthStats.Status** *Visibility changed (Breaking)*  
**public property Nest.ShardHealthStats.UnassignedShards** *Visibility changed (Breaking)*  
**public property Nest.ShardStore.Allocation** *Visibility changed (Breaking)*  
**public property Nest.ShardStore.Id** *Visibility changed (Breaking)  
**public property Nest.ShardStore.Name** *Visibility changed (Breaking)*  
**public property Nest.ShardStore.StoreException** *Visibility changed (Breaking)*  
**public property Nest.ShardStore.TransportAddress** *Visibility changed (Breaking)*  
**public property Nest.ShardStoreException.Reason** *Visibility changed (Breaking)*  
**public property Nest.ShardStoreException.Type** *Visibility changed (Breaking)*  
**public property Nest.UpgradeResponse.Shards** *Visibility changed (Breaking)*  
**public property Nest.UpgradeStatusResponse.SizeInBytes** *Visibility changed (Breaking)*  
**public property Nest.UpgradeStatusResponse.SizeToUpgradeAncientInBytes** *Visibility changed (Breaking)*  
**public property Nest.UpgradeStatusResponse.SizeToUpgradeInBytes** *Visibility changed (Breaking)*  


//getter only TODO (explicit internal set)
**public property Nest.IClusterRerouteResponse.State** *Declaration changed (Breaking)*  
**public property Nest.IClusterStatsResponse.ClusterName** *Declaration changed (Breaking)*  
**public property Nest.IClusterStatsResponse.Indices** *Declaration changed (Breaking)*  
**public property Nest.IClusterStatsResponse.Nodes** *Declaration changed (Breaking)*  
**public property Nest.IClusterStatsResponse.Status** *Declaration changed (Breaking)*  
**public property Nest.IClusterStatsResponse.Timestamp** *Declaration changed (Breaking)*  
**public property Nest.IFieldStatsResponse.Shards** *Declaration changed (Breaking)*  
**public property Nest.IGetSearchTemplateResponse.Template** *Declaration changed (Breaking)*  
**public property Nest.IUpgradeStatusResponse.SizeInBytes** *Declaration changed (Breaking)*  
**public property Nest.IUpgradeStatusResponse.SizeToUpgradeAncientInBytes** *Declaration changed (Breaking)*  
**public property Nest.IUpgradeStatusResponse.SizeToUpgradeInBytes** *Declaration changed (Breaking)*  


### Uncategorized

**public property Nest.BulkIndexByScrollFailure.Cause** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("cause")]
public Throwable Cause { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("cause")]
public BulkIndexFailureCause Cause { get; set; }
```

**public property Nest.BulkResponse.Took** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
[JsonIgnoreAttribute]
public int Took { get; }
```

5.x
```csharp
[JsonPropertyAttribute("took")]
public long Took { get; internal set; }
```

**public method Nest.CreateIndexRequest..ctor** *Visibility was changed from public to internal (Breaking)*

2.x: `public  .ctor()`  
5.x: `internal  .ctor()`  

**public method Nest.DeleteByQueryDescriptor&lt;T&gt;.Routing** *Declaration changed (Breaking)*

2.x: `public DeleteByQueryDescriptor<T> Routing(string routing)`  
5.x: `public DeleteByQueryDescriptor<T> Routing(String[] routing)`  

**public property Nest.DeleteByQueryRequest.Routing** *Declaration changed (Breaking)*

2.x: `public string Routing { get; set; }`  
5.x: `public String[] Routing { get; set; }`  

**public property Nest.DeleteByQueryRequest&lt;T&gt;.Routing** *Declaration changed (Breaking)*

2.x: `public string Routing { get; set; }`  
5.x: `public String[] Routing { get; set; }`  

**public method Nest.ElasticClient.DeleteByQuery&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  
5.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  

**public method Nest.ElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(IGetAliasRequest request)`  
5.x: `public IGetAliasResponse GetAlias(IGetAliasRequest request)`  

**public method Nest.ElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  
5.x: `public IGetAliasResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  

**public method Nest.ElasticClient.Suggest&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  
5.x: `public ISuggestResponse<T> Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  

**public property Nest.ExecuteWatchResponse.Id** *Declaration changed (Breaking)*

2.x: `public Id Id { get; set; }`  
5.x: `public string Id { get; set; }`  

**public method Nest.ExtendedStatsBucketAggregationDescriptor.Sigma** *Declaration changed (Breaking)*

2.x: `public ExtendedStatsBucketAggregationDescriptor Sigma(double? sigma)`  
5.x: `public ExtendedStatsBucketAggregationDescriptor Sigma(double sigma)`  

**public method Nest.Field..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor()`  
5.x: `public  .ctor(string name, double? boost)`  

**public method Nest.Field.And** *Declaration changed (Breaking)*

2.x: `public Fields And(string field)`  
5.x: `public Fields And(Field field)`  

**public method Nest.Field.And&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Fields And<T>(Expression<Func<T, object>> field)`  
5.x: `public Fields And<T>(Expression<Func<T, object>> field, double? boost)`  

**public property Nest.Field.Expression** *Declaration changed (Breaking)*

2.x: `public Expression Expression { get; set; }`  
5.x: `public Expression Expression { get; }`  

**public property Nest.Field.Name** *Declaration changed (Breaking)*

2.x: `public string Name { get; set; }`  
5.x: `public string Name { get; }`  

**public property Nest.Field.Property** *Declaration changed (Breaking)*

2.x: `public PropertyInfo Property { get; set; }`  
5.x: `public PropertyInfo Property { get; }`  

**public property Nest.GenericProperty.Norms** *Declaration changed (Breaking)*

2.x: `public INorms Norms { get; set; }`  
5.x: `public bool? Norms { get; set; }`  

**public method Nest.GenericPropertyDescriptor&lt;T&gt;.Norms** *Declaration changed (Breaking)*

2.x: `public GenericPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector)`  
5.x: `public GenericPropertyDescriptor<T> Norms(bool enabled = True)`  

**public method Nest.GeoDistanceAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public GeoDistanceAggregationDescriptor<T> Field(string field)`  
5.x: `public GeoDistanceAggregationDescriptor<T> Field(Field field)`  

**public method Nest.GeoHashGridAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public GeoHashGridAggregationDescriptor<T> Field(string field)`  
5.x: `public GeoHashGridAggregationDescriptor<T> Field(Field field)`  

**public class Nest.GeoShapeQueryDescriptorBase&lt;TDescriptor, TInterface, T&gt;** *Declaration changed (Breaking)*

2.x: `public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T> : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IDescriptor, IQuery, IFieldNameQuery, IGeoShapeQuery where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface where TInterface : class, IGeoShapeQuery`  
5.x: `public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T> : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IDescriptor, IQuery, IFieldNameQuery, IGeoShapeQuery where TDescriptor : GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface where TInterface : class, IGeoShapeQuery`  

**public property Nest.GetWatchResponse.Id** *Declaration changed (Breaking)*

2.x: `public Id Id { get; internal set; }`  
5.x: `public string Id { get; internal set; }`  

**public property Nest.HighlightField.Type** *Declaration changed (Breaking)*

2.x: `public Nullable<HighlighterType> Type { get; set; }`  
5.x: `public Union<HighlighterType, string> Type { get; set; }`  

**public method Nest.HighlightFieldDescriptor&lt;T&gt;.PostTags** *Declaration changed (Breaking)*

2.x: `public HighlightFieldDescriptor<T> PostTags(string postTags)`  
5.x: `public HighlightFieldDescriptor<T> PostTags(String[] postTags)`  

**public method Nest.HighlightFieldDescriptor&lt;T&gt;.PreTags** *Declaration changed (Breaking)*

2.x: `public HighlightFieldDescriptor<T> PreTags(string preTags)`  
5.x: `public HighlightFieldDescriptor<T> PreTags(String[] preTags)`  

**public method Nest.HistogramAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public HistogramAggregationDescriptor<T> Field(string field)`  
5.x: `public HistogramAggregationDescriptor<T> Field(Field field)`  

**public property Nest.Hit&lt;T&gt;.Score** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_score")]
public double Score { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("_score")]
public double? Score { get; set; }
```



**public property Nest.IBulkResponse.Took** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
public int Took { get; }
```

5.x
```csharp
public long Took { get; }
```

**public property Nest.IDynamicIndexSettings.AutoExpandReplicas** *Declaration changed (Breaking)*

2.x: `public string AutoExpandReplicas { get; set; }`  
5.x: `public AutoExpandReplicas AutoExpandReplicas { get; set; }`  

**public method Nest.IElasticClient.DeleteByQuery&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  
5.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  

**public method Nest.IElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(IGetAliasRequest request)`  
5.x: `public IGetAliasResponse GetAlias(IGetAliasRequest request)`  

**public method Nest.IElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  
5.x: `public IGetAliasResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  

**public method Nest.IElasticClient.Suggest&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  
5.x: `public ISuggestResponse<T> Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  

**public property Nest.IExecuteWatchResponse.Id** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_id")]
public Id Id { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("_id")]
public string Id { get; set; }
```

**public property Nest.IGenericProperty.Norms** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("norms")]
public INorms Norms { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("norms")]
public bool? Norms { get; set; }
```

**public property Nest.IGetWatchResponse.Id** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_id")]
public Id Id { get; }
```

5.x
```csharp
[JsonPropertyAttribute("_id")]
public string Id { get; }
```

**public property Nest.IHighlightField.Type** *Declaration changed (Breaking)*

2.x
```csharp
public Nullable<HighlighterType> Type { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("type")]
public Union<HighlighterType, string> Type { get; set; }
```

**public property Nest.IHit&lt;T&gt;.Score** *Declaration changed (Breaking)*

2.x: `public double Score { get; }`  
5.x: `public double? Score { get; }`  

**public property Nest.IInnerHits.Source** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public ISourceFilter Source { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public Union<bool, ISourceFilter> Source { get; set; }
```



**public property Nest.IndexActionResult.Id** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("id")]
public Id Id { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("id")]
public string Id { get; set; }
```

**public property Nest.IndexActionResultIndexResponse.Id** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("id")]
public Id Id { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("id")]
public string Id { get; set; }
```

**public property Nest.IndexActionResultIndexResponse.Result** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("result")]
public string Result { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("result")]
public Result Result { get; set; }
```

**public method Nest.IndexManyExtensions.IndexMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static IBulkResponse IndexMany<T>(IElasticClient client, IEnumerable<T> objects, string index, string type)
```

5.x
```csharp
[ExtensionAttribute]
public static IBulkResponse IndexMany<T>(IElasticClient client, IEnumerable<T> objects, IndexName index, TypeName type)
```

**public method Nest.IndicesPointingToAliasExtensions.GetIndicesPointingToAlias** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static IList<string> GetIndicesPointingToAlias(IElasticClient client, string aliasName)
```

5.x
```csharp
[ExtensionAttribute]
public static IEnumerable<string> GetIndicesPointingToAlias(IElasticClient client, Names alias)
```

**public method Nest.IndicesPointingToAliasExtensions.GetIndicesPointingToAliasAsync** *Declaration changed (Breaking)*

2.x
```csharp
[AsyncStateMachineAttribute(Nest.IndicesPointingToAliasExtensions+<GetIndicesPointingToAliasAsync>d__1)]
[ExtensionAttribute]
public static Task<IList<string>> GetIndicesPointingToAliasAsync(IElasticClient client, string aliasName)
```

5.x
```csharp
[AsyncStateMachineAttribute(Nest.IndicesPointingToAliasExtensions+<GetIndicesPointingToAliasAsync>d__1)]
[ExtensionAttribute]
public static Task<IEnumerable<string>> GetIndicesPointingToAliasAsync(IElasticClient client, Names alias)
```

**public property Nest.InnerHits.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public Union<bool, ISourceFilter> Source { get; set; }`  

**public method Nest.InnerHitsDescriptor&lt;T&gt;.FielddataFields** *Declaration changed (Breaking)*

2.x: `public InnerHitsDescriptor<T> FielddataFields(String[] fielddataFields)`  
5.x: `public InnerHitsDescriptor<T> FielddataFields(Field[] fielddataFields)`  


**public property Nest.IObjectProperty.Dynamic** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("dynamic")]
public Nullable<DynamicMapping> Dynamic { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("dynamic")]
public Union<bool, DynamicMapping> Dynamic { get; set; }
```

**public property Nest.IPercolateCountResponse.Took** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
public int Took { get; }
```

5.x
```csharp
public long Took { get; }
```


**public method Nest.IpRangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public IpRangeAggregationDescriptor<T> Field(string field)`  
5.x: `public IpRangeAggregationDescriptor<T> Field(Field field)`  

**public property Nest.IPutWatchResponse.Id** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_id")]
public Id Id { get; }
```

5.x
```csharp
[JsonPropertyAttribute("_id")]
public string Id { get; }
```

**public property Nest.IReindexOnServerResponse.Retries** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("retries")]
public long Retries { get; }
```

5.x
```csharp
[JsonPropertyAttribute("retries")]
public Retries Retries { get; }
```

**public property Nest.ISearchRequest.Rescore** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("rescore")]
public IRescore Rescore { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("rescore")]
public IList<IRescore> Rescore { get; set; }
```

**public property Nest.ISearchRequest.Source** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_source")]
public ISourceFilter Source { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("_source")]
public Union<bool, ISourceFilter> Source { get; set; }
```

**public property Nest.ISearchResponse&lt;T&gt;.Took** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
public int Took { get; }
```

5.x
```csharp
public long Took { get; }
```

**public property Nest.IStringProperty.Norms** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("norms")]
public INorms Norms { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("norms")]
public bool? Norms { get; set; }
```

**public property Nest.ITopHitsAggregation.Source** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_source")]
public ISourceFilter Source { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("_source")]
public Union<bool, ISourceFilter> Source { get; set; }
```

**public property Nest.ITypeMapping.Dynamic** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("dynamic")]
public Nullable<DynamicMapping> Dynamic { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("dynamic")]
public Union<bool, DynamicMapping> Dynamic { get; set; }
```

**public property Nest.ITypeMapping.Meta** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_meta")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public FluentDictionary<string, object> Meta { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("_meta")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,System.Object])]
public IDictionary<string, object> Meta { get; set; }
```

**public property Nest.IUpdateByQueryResponse.Retries** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("retries")]
public long Retries { get; }
```

5.x
```csharp
[JsonPropertyAttribute("retries")]
public Retries Retries { get; }
```

**public property Nest.IUpgradeResponse.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_shards")]
public ShardsMetaData Shards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("_shards")]
public ShardsMetaData Shards { get; }
```

**public enum Nest.LicenseStatus** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum LicenseStatus
{
     Active = 0,
     Invalid = 1,
     Expired = 2
}
```

5.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum LicenseStatus
{
     Active = 0,
     Valid = 1,
     Invalid = 2,
     Expired = 3
}
```

**public method Nest.MappingWalker.Accept** *Declaration changed (Breaking)*

2.x: `public void Accept(TypeMapping mapping)`  
5.x: `public void Accept(ITypeMapping mapping)`  

**public method Nest.MetricAggregationDescriptorBase&lt;TMetricAggregation, TMetricAggregationInterface, T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public TMetricAggregation Field(string field)`  
5.x: `public TMetricAggregation Field(Field field)`  

**public method Nest.MissingAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public MissingAggregationDescriptor<T> Field(string field)`  
5.x: `public MissingAggregationDescriptor<T> Field(Field field)`  

**public method Nest.NestedAggregationDescriptor&lt;T&gt;.Path** *Declaration changed (Breaking)*

2.x: `public NestedAggregationDescriptor<T> Path(string path)`  
5.x: `public NestedAggregationDescriptor<T> Path(Field path)`  

**public enum Nest.NestedScoreMode** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NestedScoreMode
{
     Average = 0,
     Total = 1,
     Min = 2,
     Max = 3,
     None = 4
}
```

5.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NestedScoreMode
{
     Average = 0,
     Sum = 1,
     Min = 2,
     Max = 3,
     None = 4
}
```

**public method Nest.NumberPropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;..ctor** *Visibility was changed from public to protected (Breaking)*

2.x: `public  .ctor()`  
5.x: `protected  .ctor()`  

**public enum Nest.NumberType** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NumberType
{
     Default = 0,
     Float = 1,
     Double = 2,
     Integer = 3,
     Long = 4,
     Short = 5,
     Byte = 6
}
```

5.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NumberType
{
     Float = 0,
     HalfFloat = 1,
     ScaledFloat = 2,
     Double = 3,
     Integer = 4,
     Long = 5,
     Short = 6,
     Byte = 7
}
```

**public enum Nest.NumericFielddataFormat** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NumericFielddataFormat
{
     Array = 0,
     DocValues = 1,
     Disabled = 2
}
```

5.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NumericFielddataFormat
{
     Array = 0,
     Disabled = 1
}
```

**public property Nest.ObjectProperty.Dynamic** *Declaration changed (Breaking)*

2.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  
5.x: `public Union<bool, DynamicMapping> Dynamic { get; set; }`  

**public method Nest.ObjectPropertyDescriptorBase&lt;TDescriptor, TInterface, TParent, TChild&gt;..ctor** *Visibility was changed from public to protected (Breaking)*

2.x: `public  .ctor()`  
5.x: `protected  .ctor()`  

**public method Nest.ObjectPropertyDescriptorBase&lt;TDescriptor, TInterface, TParent, TChild&gt;.Dynamic** *Declaration changed (Breaking)*

2.x: `public TDescriptor Dynamic(DynamicMapping dynamic)`  
5.x: `public TDescriptor Dynamic(Union<bool, DynamicMapping> dynamic)`  

**public class Nest.PercentileRanksAggregationJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class PercentileRanksAggregationJsonConverter : PercentilesAggregationJsonConverter`  
5.x: `internal class PercentileRanksAggregationJsonConverter : PercentilesAggregationJsonConverter`  

**public class Nest.PercentilesAggregationJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class PercentilesAggregationJsonConverter : JsonConverter`  
5.x: `internal class PercentilesAggregationJsonConverter : JsonConverter`  

**public method Nest.PhraseSuggestCollateDescriptor&lt;T&gt;.Query** *Declaration changed (Breaking)*

2.x: `public PhraseSuggestCollateDescriptor<T> Query(string script)`  
5.x: `public PhraseSuggestCollateDescriptor<T> Query(Func<TemplateQueryDescriptor<T>, ITemplateQuery> selector)`  

**public method Nest.PropertyName..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor()`  
5.x: `public  .ctor(string name)`  

**public property Nest.PropertyName.Expression** *Declaration changed (Breaking)*

2.x: `public Expression Expression { get; set; }`  
5.x: `public Expression Expression { get; }`  

**public property Nest.PropertyName.Name** *Declaration changed (Breaking)*

2.x: `public string Name { get; set; }`  
5.x: `public string Name { get; }`  

**public property Nest.PropertyName.Property** *Declaration changed (Breaking)*

2.x: `public PropertyInfo Property { get; set; }`  
5.x: `public PropertyInfo Property { get; }`  

**public class Nest.PropertyNameExtensions** *Visibility was changed from public to internal (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static class PropertyNameExtensions
```

5.x
```csharp
[ExtensionAttribute]
internal static class PropertyNameExtensions
```

**public method Nest.PutMappingDescriptor&lt;T&gt;.Dynamic** *Declaration changed (Breaking)*

2.x: `public PutMappingDescriptor<T> Dynamic(DynamicMapping dynamic)`  
5.x: `public PutMappingDescriptor<T> Dynamic(Union<bool, DynamicMapping> dynamic)`  

**public property Nest.PutMappingRequest.Dynamic** *Declaration changed (Breaking)*

2.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  
5.x: `public Union<bool, DynamicMapping> Dynamic { get; set; }`  

**public property Nest.PutMappingRequest.Meta** *Declaration changed (Breaking)*

2.x: `public FluentDictionary<string, object> Meta { get; set; }`  
5.x: `public IDictionary<string, object> Meta { get; set; }`  

**public property Nest.PutMappingRequest&lt;T&gt;.Dynamic** *Declaration changed (Breaking)*

2.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  
5.x: `public Union<bool, DynamicMapping> Dynamic { get; set; }`  

**public property Nest.PutMappingRequest&lt;T&gt;.Meta** *Declaration changed (Breaking)*

2.x: `public FluentDictionary<string, object> Meta { get; set; }`  
5.x: `public IDictionary<string, object> Meta { get; set; }`  

**public property Nest.PutWatchResponse.Id** *Declaration changed (Breaking)*

2.x: `public Id Id { get; internal set; }`  
5.x: `public string Id { get; internal set; }`  

**public method Nest.Query&lt;T&gt;.Prefix** *Declaration changed (Breaking)*

2.x: `public static QueryContainer Prefix(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  
5.x: `public static QueryContainer Prefix(Field field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  

**public method Nest.Query&lt;T&gt;.Term** *Declaration changed (Breaking)*

2.x: `public static QueryContainer Term(string field, object value, double? boost, string name)`  
5.x: `public static QueryContainer Term(Field field, object value, double? boost, string name)`  

**public method Nest.Query&lt;T&gt;.Wildcard** *Declaration changed (Breaking)*

2.x: `public static QueryContainer Wildcard(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  
5.x: `public static QueryContainer Wildcard(Field field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Prefix** *Declaration changed (Breaking)*

2.x: `public QueryContainer Prefix(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  
5.x: `public QueryContainer Prefix(Field field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Term** *Declaration changed (Breaking)*

2.x: `public QueryContainer Term(string field, object value, double? boost, string name)`  
5.x: `public QueryContainer Term(Field field, object value, double? boost, string name)`  

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Wildcard** *Declaration changed (Breaking)*

2.x: `public QueryContainer Wildcard(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  
5.x: `public QueryContainer Wildcard(Field field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  

**public method Nest.RangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public RangeAggregationDescriptor<T> Field(string field)`  
5.x: `public RangeAggregationDescriptor<T> Field(Field field)`  

**public method Nest.ReindexDescriptor&lt;T&gt;..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor(IndexName from, IndexName to)`  
5.x: `public  .ctor()`  

**public method Nest.ReindexObservable&lt;T&gt;.Subscribe** *Declaration changed (Breaking)*

2.x: `public IDisposable Subscribe(IObserver<IReindexResponse<T>> observer)`  
5.x: `public IDisposable Subscribe(ReindexObserver<T> observer)`  

**public method Nest.ReindexObserver&lt;T&gt;..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor(Action<IReindexResponse<T>> onNext, Action<Exception> onError, Action completed)`  
5.x: `public  .ctor(Action<IBulkAllResponse> onNext, Action<Exception> onError, Action onCompleted)`  

**public method Nest.ReindexOnServerDescriptor.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public ReindexOnServerDescriptor RequestsPerSecond(Single requests_per_second)`  
5.x: `public ReindexOnServerDescriptor RequestsPerSecond(long requests_per_second)`  

**public property Nest.ReindexOnServerRequest.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public long RequestsPerSecond { get; set; }`  

**public property Nest.ReindexOnServerResponse.Retries** *Declaration changed (Breaking)*

2.x: `public long Retries { get; internal set; }`  
5.x: `public Retries Retries { get; internal set; }`  

**public method Nest.ReindexRethrottleDescriptor.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public ReindexRethrottleDescriptor RequestsPerSecond(Single requests_per_second)`  
5.x: `public ReindexRethrottleDescriptor RequestsPerSecond(long requests_per_second)`  

**public property Nest.ReindexRethrottleRequest.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public long RequestsPerSecond { get; set; }`  

**public class Nest.ReindexRoutingJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class ReindexRoutingJsonConverter : JsonConverter`  
5.x: `internal class ReindexRoutingJsonConverter : JsonConverter`  

**public property Nest.ReindexStatus.Retries** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("retries")]
public long Retries { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("retries")]
public Retries Retries { get; internal set; }
```

**public property Nest.ResponseBase.ApiCall** *Visibility was changed from public to protected (Breaking)*

2.x: `public IApiCallDetails ApiCall { get; }`  
5.x: `protected IApiCallDetails ApiCall { get; }`  

**public method Nest.ReverseNestedAggregationDescriptor&lt;T&gt;.Path** *Declaration changed (Breaking)*

2.x: `public ReverseNestedAggregationDescriptor<T> Path(string path)`  
5.x: `public ReverseNestedAggregationDescriptor<T> Path(Field path)`  

**public class Nest.ScoreFunctionJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class ScoreFunctionJsonConverter : JsonConverter`  
5.x: `internal class ScoreFunctionJsonConverter : JsonConverter`  

**public enum Nest.ScoreMode** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum ScoreMode
{
     Average = 0,
     First = 1,
     Max = 2,
     Min = 3,
     Multiply = 4,
     Total = 5,
     Sum = 6
}
```

5.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum ScoreMode
{
     Average = 0,
     Max = 1,
     Min = 2,
     Multiply = 3,
     Total = 4
}
```

**public class Nest.ScriptJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class ScriptJsonConverter : JsonConverter`  
5.x: `internal class ScriptJsonConverter : JsonConverter`  

**public method Nest.SearchDescriptor&lt;T&gt;.Rescore** *Declaration changed (Breaking)*

2.x: `public SearchDescriptor<T> Rescore(Func<RescoreDescriptor<T>, IRescore> rescoreSelector)`  
5.x: `public SearchDescriptor<T> Rescore(Func<RescoringDescriptor<T>, IPromise<IList<IRescore>>> rescoreSelector)`  

**public property Nest.SearchRequest.Rescore** *Declaration changed (Breaking)*

2.x: `public IRescore Rescore { get; set; }`  
5.x: `public IList<IRescore> Rescore { get; set; }`  

**public property Nest.SearchRequest.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public Union<bool, ISourceFilter> Source { get; set; }`  

**public property Nest.SearchRequest&lt;T&gt;.Rescore** *Declaration changed (Breaking)*

2.x: `public IRescore Rescore { get; set; }`  
5.x: `public IList<IRescore> Rescore { get; set; }`  

**public property Nest.SearchRequest&lt;T&gt;.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public Union<bool, ISourceFilter> Source { get; set; }`  

**public property Nest.SearchResponse&lt;T&gt;.ApiCall** *Visibility was changed from public to protected (Breaking)*

2.x: `public IApiCallDetails ApiCall { get; }`  
5.x: `protected IApiCallDetails ApiCall { get; }`  

**public method Nest.SignificantTermsAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public SignificantTermsAggregationDescriptor<T> Field(string field)`  
5.x: `public SignificantTermsAggregationDescriptor<T> Field(Field field)`  

**public enum Nest.SimilarityOption** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum SimilarityOption
{
     Default = 0,
     BM25 = 1
}
```

5.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum SimilarityOption
{
     Classic = 0,
     BM25 = 1
}
```

**public class Nest.SimpleQueryStringFlagsJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class SimpleQueryStringFlagsJsonConverter : JsonConverter`  
5.x: `internal class SimpleQueryStringFlagsJsonConverter : JsonConverter`  

**public class Nest.SourceFilterJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class SourceFilterJsonConverter : JsonConverter`  
5.x: `internal class SourceFilterJsonConverter : JsonConverter`  

**public enum Nest.StringFielddataFormat** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum StringFielddataFormat
{
     PagedBytes = 0,
     DocValues = 1,
     Disabled = 2
}
```

5.x
```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum StringFielddataFormat
{
     PagedBytes = 0,
     Disabled = 1
}
```

**public property Nest.StringProperty.Norms** *Declaration changed (Breaking)*

2.x: `public INorms Norms { get; set; }`  
5.x: `public bool? Norms { get; set; }`  

**public method Nest.StringPropertyDescriptor&lt;T&gt;.Norms** *Declaration changed (Breaking)*

2.x: `public StringPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector)`  
5.x: `public StringPropertyDescriptor<T> Norms(bool enabled = True)`  

**public method Nest.StringPropertyDescriptor&lt;T&gt;.PositionIncrementGap** *Declaration changed (Breaking)*

2.x: `public StringPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap)`  
5.x: `public StringPropertyDescriptor<T> PositionIncrementGap(int positionIncrementGap)`  

**public method Nest.TermsAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public TermsAggregationDescriptor<T> Field(string field)`  
5.x: `public TermsAggregationDescriptor<T> Field(Field field)`  

**public property Nest.TermVectorsResponse.Took** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
[JsonIgnoreAttribute]
public int Took { get; }
```

5.x
```csharp
[JsonPropertyAttribute("took")]
public long Took { get; internal set; }
```

**public property Nest.Time.Milliseconds** *Declaration changed (Breaking)*

2.x: `public double Milliseconds { get; private set; }`  
5.x: `public double? Milliseconds { get; private set; }`  

**public property Nest.TopHitsAggregation.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public Union<bool, ISourceFilter> Source { get; set; }`  

**public property Nest.TypeMapping.Dynamic** *Declaration changed (Breaking)*

2.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  
5.x: `public Union<bool, DynamicMapping> Dynamic { get; set; }`  

**public property Nest.TypeMapping.Meta** *Declaration changed (Breaking)*

2.x: `public FluentDictionary<string, object> Meta { get; set; }`  
5.x: `public IDictionary<string, object> Meta { get; set; }`  

**public method Nest.TypeMappingDescriptor&lt;T&gt;.Dynamic** *Declaration changed (Breaking)*

2.x: `public TypeMappingDescriptor<T> Dynamic(DynamicMapping dynamic)`  
5.x: `public TypeMappingDescriptor<T> Dynamic(Union<bool, DynamicMapping> dynamic)`  

**public class Nest.TypeNameExtensions** *Visibility was changed from public to internal (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static class TypeNameExtensions
```

5.x
```csharp
[ExtensionAttribute]
internal static class TypeNameExtensions
```

**public method Nest.UpdateByQueryDescriptor&lt;T&gt;.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public UpdateByQueryDescriptor<T> RequestsPerSecond(Single requests_per_second)`  
5.x: `public UpdateByQueryDescriptor<T> RequestsPerSecond(long requests_per_second)`  

**public property Nest.UpdateByQueryRequest.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public long RequestsPerSecond { get; set; }`  

**public property Nest.UpdateByQueryRequest&lt;T&gt;.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public long RequestsPerSecond { get; set; }`  

**public property Nest.UpdateByQueryResponse.Retries** *Declaration changed (Breaking)*

2.x: `public long Retries { get; internal set; }`  
5.x: `public Retries Retries { get; internal set; }`  

**public property Nest.Watch.Actions** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("actions")]
[JsonConverterAttribute(Nest.ActionsJsonConverter)]
public IDictionary<string, IAction> Actions { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("actions")]
[JsonConverterAttribute(Nest.ActionsJsonConverter)]
public Actions Actions { get; internal set; }
```

**public property Nest.WatchRecord.WatchId** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("watch_id")]
public Id WatchId { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("watch_id")]
public string WatchId { get; set; }
```

**public property Nest.WatchRecordQueuedStats.WatchId** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("watch_id")]
public Id WatchId { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("watch_id")]
public string WatchId { get; internal set; }
```

**public property Nest.WatchRecordQueuedStats.WatchRecordId** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("watch_record_id")]
public Id WatchRecordId { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("watch_record_id")]
public string WatchRecordId { get; internal set; }
```

#RemovedIn5x
**public class Nest.AllocateClusterRerouteCommand** *Removed (Breaking)*

```csharp
public class AllocateClusterRerouteCommand : IAllocateClusterRerouteCommand, IClusterRerouteCommand
```

**public class Nest.AllocateClusterRerouteCommandDescriptor** *Removed (Breaking)*

```csharp
public class AllocateClusterRerouteCommandDescriptor : DescriptorBase<AllocateClusterRerouteCommandDescriptor, IAllocateClusterRerouteCommand>, IDescriptor, IAllocateClusterRerouteCommand, IClusterRerouteCommand
```

**public class Nest.AttachData** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class AttachData
```

**public property Nest.AttachmentProperty.FileField** *Removed (Breaking)*

```csharp
public IStringProperty FileField { get; set; }
```

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.LanguageField** *Removed (Breaking)*

```csharp
public AttachmentPropertyDescriptor<T> LanguageField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)
```

**public property Nest.BulkAllRequest&lt;T&gt;.Consistency** *Removed (Breaking)*

```csharp
public Nullable<Consistency> Consistency { get; set; }
```

**public method Nest.BulkDescriptor.Consistency** *Removed (Breaking)*

```csharp
public BulkDescriptor Consistency(Consistency consistency)
```

**public property Nest.BulkIndexByScrollFailure.CausedBy** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("caused_by")]
public CausedBy CausedBy { get; internal set; }
```

**public property Nest.BulkIndexByScrollFailure.Node** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("node")]
public string Node { get; internal set; }
```

**public property Nest.BulkIndexByScrollFailure.Reason** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("reason")]
public Throwable Reason { get; internal set; }
```

**public property Nest.BulkIndexByScrollFailure.Shard** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("shard")]
public int Shard { get; internal set; }
```

**public property Nest.BulkRequest.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public property Nest.BulkResponse.TookAsLong** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("took")]
public long TookAsLong { get; internal set; }
```

**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Lang** *Removed (Breaking)*

```csharp
public BulkUpdateDescriptor<TDocument, TPartialDocument> Lang(string lang)
```

**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*

```csharp
public BulkUpdateDescriptor<TDocument, TPartialDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
```

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.InferFrom** *Removed (Breaking)*

```csharp
public TDocument InferFrom { get; set; }
```

**public property Nest.CategorySuggestContext.Default** *Removed (Breaking)*

```csharp
public IEnumerable<string> Default { get; set; }
```

**public method Nest.CategorySuggestContextDescriptor&lt;T&gt;.Default** *Removed (Breaking)*

```csharp
public CategorySuggestContextDescriptor<T> Default(IEnumerable<string> defaults)
```

**public method Nest.CategorySuggestContextDescriptor&lt;T&gt;.Default** *Removed (Breaking)*

```csharp
public CategorySuggestContextDescriptor<T> Default(String[] defaults)
```

**public property Nest.CatFielddataRecord.FieldSizes** *Removed (Breaking)*

```csharp
public IDictionary<string, string> FieldSizes { get; set; }
```

**public property Nest.CatFielddataRecord.Total** *Removed (Breaking)*

```csharp
public string Total { get; set; }
```

**public property Nest.CatNodesRecord.Host** *Removed (Breaking)*

```csharp
public string Host { get; }
```

**public property Nest.CatNodesRecord.Load** *Removed (Breaking)*

```csharp
public string Load { get; }
```

**public property Nest.CatRecoveryRecord.TotalTranslog** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("total_translog")]
public long? TotalTranslog { get; set; }
```

**public property Nest.CatRecoveryRecord.Translog** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("translog")]
public long? Translog { get; set; }
```

**public property Nest.CatRecoveryRecord.TranslogPercent** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("translog_percent")]
public string TranslogPercent { get; set; }
```

**public method Nest.ClusterHealthDescriptor.WaitForRelocatingShards** *Removed (Breaking)*

```csharp
public ClusterHealthDescriptor WaitForRelocatingShards(long wait_for_relocating_shards)
```

**public property Nest.ClusterHealthRequest.WaitForRelocatingShards** *Removed (Breaking)*

```csharp
public long WaitForRelocatingShards { get; set; }
```

**public property Nest.ClusterIndicesStats.Percolate** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("percolate")]
public PercolateStats Percolate { get; internal set; }
```

**public property Nest.ClusterNodeCount.Client** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("client")]
public int Client { get; internal set; }
```

**public property Nest.ClusterNodeCount.DataOnly** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("data_only")]
public int DataOnly { get; internal set; }
```

**public property Nest.ClusterNodeCount.MasterData** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("master_data")]
public int MasterData { get; internal set; }
```

**public property Nest.ClusterNodeCount.MasterOnly** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("master_only")]
public int MasterOnly { get; internal set; }
```

**public class Nest.ClusterOperatingSystemMemory** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class ClusterOperatingSystemMemory
```

**public property Nest.ClusterOperatingSystemStats.Memory** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("mem")]
public ClusterOperatingSystemMemory Memory { get; internal set; }
```

**public method Nest.ClusterRerouteDescriptor.Allocate** *Removed (Breaking)*

```csharp
public ClusterRerouteDescriptor Allocate(Func<AllocateClusterRerouteCommandDescriptor, IAllocateClusterRerouteCommand> selector)
```

**public property Nest.ClusterRerouteResponse.Version** *Removed (Breaking)*

```csharp
public int Version { get; set; }
```

**public property Nest.CompletionAttribute.Payloads** *Removed (Breaking)*

```csharp
public bool Payloads { get; set; }
```

**public class Nest.CompletionField&lt;TPayload&gt;** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class CompletionField<TPayload>
```

**public property Nest.CompletionProperty.Context** *Removed (Breaking)*

```csharp
public ISuggestContextMapping Context { get; set; }
```

**public property Nest.CompletionProperty.Payloads** *Removed (Breaking)*

```csharp
public bool? Payloads { get; set; }
```

**public method Nest.CompletionPropertyDescriptor&lt;T&gt;.Context** *Removed (Breaking)*

```csharp
public CompletionPropertyDescriptor<T> Context(Func<SuggestContextMappingDescriptor<T>, IPromise<ISuggestContextMapping>> selector)
```

**public method Nest.CompletionPropertyDescriptor&lt;T&gt;.Payloads** *Removed (Breaking)*

```csharp
public CompletionPropertyDescriptor<T> Payloads(bool payloads = True)
```

**public property Nest.CompletionSuggester.Context** *Removed (Breaking)*

```csharp
public IDictionary<string, object> Context { get; set; }
```

**public method Nest.CompletionSuggesterDescriptor&lt;T&gt;.Context** *Removed (Breaking)*

```csharp
public CompletionSuggesterDescriptor<T> Context(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
```

**public method Nest.CreateIndexDescriptor.Warmers** *Removed (Breaking)*

```csharp
public CreateIndexDescriptor Warmers(Func<WarmersDescriptor, IPromise<IWarmers>> selector)
```

**public property Nest.CreateIndexRequest.Warmers** *Removed (Breaking)*

```csharp
public IWarmers Warmers { get; set; }
```

**public class Nest.DeleteByQueryIndicesResult** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class DeleteByQueryIndicesResult
```

**public property Nest.DeleteByQueryResponse.Indices** *Removed (Breaking)*

```csharp
public IDictionary<string, DeleteByQueryIndicesResult> Indices { get; set; }
```

**public method Nest.DeleteDescriptor&lt;T&gt;.Consistency** *Removed (Breaking)*

```csharp
public DeleteDescriptor<T> Consistency(Consistency consistency)
```

**public property Nest.DeleteRequest.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public property Nest.DeleteRequest&lt;T&gt;.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public method Nest.DeleteScriptDescriptor.Version** *Removed (Breaking)*

```csharp
public DeleteScriptDescriptor Version(long version)
```

**public method Nest.DeleteScriptDescriptor.VersionType** *Removed (Breaking)*

```csharp
public DeleteScriptDescriptor VersionType(VersionType version_type)
```

**public property Nest.DeleteScriptRequest.Version** *Removed (Breaking)*

```csharp
public long Version { get; set; }
```

**public property Nest.DeleteScriptRequest.VersionType** *Removed (Breaking)*

```csharp
public VersionType VersionType { get; set; }
```

**public method Nest.DeleteSearchTemplateDescriptor.Version** *Removed (Breaking)*

```csharp
public DeleteSearchTemplateDescriptor Version(long version)
```

**public method Nest.DeleteSearchTemplateDescriptor.VersionType** *Removed (Breaking)*

```csharp
public DeleteSearchTemplateDescriptor VersionType(VersionType version_type)
```

**public property Nest.DeleteSearchTemplateRequest.Version** *Removed (Breaking)*

```csharp
public long Version { get; set; }
```

**public property Nest.DeleteSearchTemplateRequest.VersionType** *Removed (Breaking)*

```csharp
public VersionType VersionType { get; set; }
```

**public class Nest.DeleteWarmerDescriptor** *Removed (Breaking)*

```csharp
[DescriptorForAttribute("IndicesDeleteWarmer")]
public class DeleteWarmerDescriptor : RequestDescriptorBase<DeleteWarmerDescriptor, DeleteWarmerRequestParameters, IDeleteWarmerRequest>, IRequest<DeleteWarmerRequestParameters>, IRequest, IDescriptor, IDeleteWarmerRequest
```

**public class Nest.DeleteWarmerRequest** *Removed (Breaking)*

```csharp
public class DeleteWarmerRequest : PlainRequestBase<DeleteWarmerRequestParameters>, IRequest<DeleteWarmerRequestParameters>, IRequest, IDeleteWarmerRequest
```

**public class Nest.DeleteWarmerResponse** *Removed (Breaking)*

```csharp
public class DeleteWarmerResponse : AcknowledgedResponseBase, IResponse, IBodyWithApiCallDetails, IAcknowledgedResponse, IDeleteWarmerResponse
```

**public method Nest.DeleteWatchDescriptor.Force** *Removed (Breaking)*

```csharp
public DeleteWatchDescriptor Force(bool force = True)
```

**public property Nest.DeleteWatchRequest.Force** *Removed (Breaking)*

```csharp
public bool Force { get; set; }
```

**public property Nest.DirectGenerator.MinWordLen** *Removed (Breaking)*

```csharp
public int? MinWordLen { get; set; }
```

**public property Nest.DirectGenerator.PrefixLen** *Removed (Breaking)*

```csharp
public int? PrefixLen { get; set; }
```

**public method Nest.DynamicIndexSettings..ctor** *Removed (Breaking)*

```csharp
public  .ctor(Dictionary<string, object> container)
```

**public property Nest.DynamicIndexSettings.RequestCacheEnabled** *Removed (Breaking)*

```csharp
public bool? RequestCacheEnabled { get; set; }
```

**public property Nest.DynamicIndexSettings.WarmersEnabled** *Removed (Breaking)*

```csharp
public bool? WarmersEnabled { get; set; }
```

**public method Nest.DynamicIndexSettingsDescriptorBase&lt;TDescriptor, TIndexSettings&gt;.RequestCacheEnabled** *Removed (Breaking)*

```csharp
public TDescriptor RequestCacheEnabled(bool enabled = True)
```

**public method Nest.DynamicIndexSettingsDescriptorBase&lt;TDescriptor, TIndexSettings&gt;.WarmersEnabled** *Removed (Breaking)*

```csharp
public TDescriptor WarmersEnabled(bool enabled = True)
```

**public method Nest.ElasticClient.DeleteWarmer** *Removed (Breaking)*

```csharp
public IDeleteWarmerResponse DeleteWarmer(IDeleteWarmerRequest request)
```

**public method Nest.ElasticClient.DeleteWarmer** *Removed (Breaking)*

```csharp
public IDeleteWarmerResponse DeleteWarmer(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector)
```

**public method Nest.ElasticClient.DeleteWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IDeleteWarmerResponse> DeleteWarmerAsync(IDeleteWarmerRequest request)
```

**public method Nest.ElasticClient.DeleteWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IDeleteWarmerResponse> DeleteWarmerAsync(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector)
```

**public method Nest.ElasticClient.GetWarmer** *Removed (Breaking)*

```csharp
public IGetWarmerResponse GetWarmer(IGetWarmerRequest request)
```

**public method Nest.ElasticClient.GetWarmer** *Removed (Breaking)*

```csharp
public IGetWarmerResponse GetWarmer(Func<GetWarmerDescriptor, IGetWarmerRequest> selector)
```

**public method Nest.ElasticClient.GetWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IGetWarmerResponse> GetWarmerAsync(IGetWarmerRequest request)
```

**public method Nest.ElasticClient.GetWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IGetWarmerResponse> GetWarmerAsync(Func<GetWarmerDescriptor, IGetWarmerRequest> selector)
```

**public method Nest.ElasticClient.Optimize** *Removed (Breaking)*

```csharp
public IOptimizeResponse Optimize(IOptimizeRequest request)
```

**public method Nest.ElasticClient.Optimize** *Removed (Breaking)*

```csharp
public IOptimizeResponse Optimize(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector)
```

**public method Nest.ElasticClient.OptimizeAsync** *Removed (Breaking)*

```csharp
public Task<IOptimizeResponse> OptimizeAsync(IOptimizeRequest request)
```

**public method Nest.ElasticClient.OptimizeAsync** *Removed (Breaking)*

```csharp
public Task<IOptimizeResponse> OptimizeAsync(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector)
```

**public method Nest.ElasticClient.PutWarmer** *Removed (Breaking)*

```csharp
public IPutWarmerResponse PutWarmer(IPutWarmerRequest request)
```

**public method Nest.ElasticClient.PutWarmer** *Removed (Breaking)*

```csharp
public IPutWarmerResponse PutWarmer(Name name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector)
```

**public method Nest.ElasticClient.PutWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IPutWarmerResponse> PutWarmerAsync(IPutWarmerRequest request)
```

**public method Nest.ElasticClient.PutWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IPutWarmerResponse> PutWarmerAsync(Name name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector)
```

**public method Nest.ElasticClient.SearchExists** *Removed (Breaking)*

```csharp
public IExistsResponse SearchExists(ISearchExistsRequest request)
```

**public method Nest.ElasticClient.SearchExists&lt;T&gt;** *Removed (Breaking)*

```csharp
public IExistsResponse SearchExists<T>(Func<SearchExistsDescriptor<T>, ISearchExistsRequest> selector)
```

**public method Nest.ElasticClient.SearchExistsAsync** *Removed (Breaking)*

```csharp
public Task<IExistsResponse> SearchExistsAsync(ISearchExistsRequest request)
```

**public method Nest.ElasticClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)*

```csharp
public Task<IExistsResponse> SearchExistsAsync<T>(Func<SearchExistsDescriptor<T>, ISearchExistsRequest> selector)
```

**public method Nest.ElasticClient.Suggest** *Removed (Breaking)*

```csharp
public ISuggestResponse Suggest(ISuggestRequest request)
```

**public method Nest.ElasticClient.SuggestAsync** *Removed (Breaking)*

```csharp
public Task<ISuggestResponse> SuggestAsync(ISuggestRequest request)
```

**public method Nest.ElasticClient.TasksCancel** *Removed (Breaking)*

```csharp
public ITasksCancelResponse TasksCancel(ITasksCancelRequest request)
```

**public method Nest.ElasticClient.TasksCancel** *Removed (Breaking)*

```csharp
public ITasksCancelResponse TasksCancel(Func<TasksCancelDescriptor, ITasksCancelRequest> selector)
```

**public method Nest.ElasticClient.TasksCancelAsync** *Removed (Breaking)*

```csharp
public Task<ITasksCancelResponse> TasksCancelAsync(ITasksCancelRequest request)
```

**public method Nest.ElasticClient.TasksCancelAsync** *Removed (Breaking)*

```csharp
public Task<ITasksCancelResponse> TasksCancelAsync(Func<TasksCancelDescriptor, ITasksCancelRequest> selector)
```

**public method Nest.ElasticClient.TasksList** *Removed (Breaking)*

```csharp
public ITasksListResponse TasksList(ITasksListRequest request)
```

**public method Nest.ElasticClient.TasksList** *Removed (Breaking)*

```csharp
public ITasksListResponse TasksList(Func<TasksListDescriptor, ITasksListRequest> selector)
```

**public method Nest.ElasticClient.TasksListAsync** *Removed (Breaking)*

```csharp
public Task<ITasksListResponse> TasksListAsync(ITasksListRequest request)
```

**public method Nest.ElasticClient.TasksListAsync** *Removed (Breaking)*

```csharp
public Task<ITasksListResponse> TasksListAsync(Func<TasksListDescriptor, ITasksListRequest> selector)
```

**public method Nest.ElasticClient.UpdateByQuery&lt;T&gt;** *Removed (Breaking)*

```csharp
public IUpdateByQueryResponse UpdateByQuery<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
```

**public method Nest.ElasticClient.UpdateByQueryAsync&lt;T&gt;** *Removed (Breaking)*

```csharp
public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
```

**public method Nest.ElasticClient.WatcherInfo** *Removed (Breaking)*

```csharp
public IWatcherInfoResponse WatcherInfo(IWatcherInfoRequest request)
```

**public method Nest.ElasticClient.WatcherInfo** *Removed (Breaking)*

```csharp
public IWatcherInfoResponse WatcherInfo(Func<WatcherInfoDescriptor, IWatcherInfoRequest> selector)
```

**public method Nest.ElasticClient.WatcherInfoAsync** *Removed (Breaking)*

```csharp
public Task<IWatcherInfoResponse> WatcherInfoAsync(IWatcherInfoRequest request)
```

**public method Nest.ElasticClient.WatcherInfoAsync** *Removed (Breaking)*

```csharp
public Task<IWatcherInfoResponse> WatcherInfoAsync(Func<WatcherInfoDescriptor, IWatcherInfoRequest> selector)
```

**public property Nest.ElasticsearchPropertyAttributeBase.DocValues** *Removed (Breaking)*

```csharp
public bool DocValues { get; set; }
```

**public property Nest.ElasticsearchPropertyAttributeBase.Similarity** *Removed (Breaking)*

```csharp
public SimilarityOption Similarity { get; set; }
```

**public property Nest.ElasticsearchPropertyAttributeBase.Store** *Removed (Breaking)*

```csharp
public bool Store { get; set; }
```

**public method Nest.ExplainDescriptor&lt;TDocument&gt;.Fields** *Removed (Breaking)*

```csharp
public ExplainDescriptor<TDocument> Fields(Expression`1[] fields)
```

**public method Nest.ExplainDescriptor&lt;TDocument&gt;.Fields** *Removed (Breaking)*

```csharp
public ExplainDescriptor<TDocument> Fields(String[] fields)
```

**public property Nest.ExplainRequest&lt;TDocument&gt;.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public property Nest.Field.CacheableExpression** *Removed (Breaking)*

```csharp
public bool CacheableExpression { get; private set; }
```

**public method Nest.Field.Create** *Removed (Breaking)*

```csharp
public static Field Create(Expression expression, double? boost)
```

**public method Nest.Field.Create** *Removed (Breaking)*

```csharp
public static Field Create(string name, double? boost)
```

**public class Nest.GeoLocationSuggestContext** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class GeoLocationSuggestContext : SuggestContextBase, ISuggestContext, IGeoLocationSuggestContext
```

**public class Nest.GeoLocationSuggestContextDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
public class GeoLocationSuggestContextDescriptor<T> : SuggestContextDescriptorBase<GeoLocationSuggestContextDescriptor<T>, IGeoLocationSuggestContext, T>, IDescriptor, ISuggestContext, IGeoLocationSuggestContext
```

**public class Nest.GetAliasesDescriptor** *Removed (Breaking)*

```csharp
[DescriptorForAttribute("IndicesGetAliases")]
public class GetAliasesDescriptor : RequestDescriptorBase<GetAliasesDescriptor, GetAliasesRequestParameters, IGetAliasesRequest>, IRequest<GetAliasesRequestParameters>, IRequest, IDescriptor, IGetAliasesRequest
```

**public class Nest.GetAliasesRequest** *Removed (Breaking)*

```csharp
public class GetAliasesRequest : PlainRequestBase<GetAliasesRequestParameters>, IRequest<GetAliasesRequestParameters>, IRequest, IGetAliasesRequest
```

**public class Nest.GetAliasesResponse** *Removed (Breaking)*

```csharp
public class GetAliasesResponse : ResponseBase, IResponse, IBodyWithApiCallDetails, IGetAliasesResponse
```

**public method Nest.GetDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public GetDescriptor<T> Fields(Expression`1[] fields)
```

**public method Nest.GetDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public GetDescriptor<T> Fields(String[] fields)
```

**public property Nest.GetMappingResponse.IndexTypeMappings** *Removed (Breaking)*

```csharp
public Dictionary<IndexName, IDictionary<TypeName, TypeMapping>> IndexTypeMappings { get; internal set; }
```

**public property Nest.GetRequest.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public property Nest.GetRequest&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public method Nest.GetScriptDescriptor.Version** *Removed (Breaking)*

```csharp
public GetScriptDescriptor Version(long version)
```

**public method Nest.GetScriptDescriptor.VersionType** *Removed (Breaking)*

```csharp
public GetScriptDescriptor VersionType(VersionType version_type)
```

**public property Nest.GetScriptRequest.Version** *Removed (Breaking)*

```csharp
public long Version { get; set; }
```

**public property Nest.GetScriptRequest.VersionType** *Removed (Breaking)*

```csharp
public VersionType VersionType { get; set; }
```

**public method Nest.GetSearchTemplateDescriptor.Version** *Removed (Breaking)*

```csharp
public GetSearchTemplateDescriptor Version(long version)
```

**public method Nest.GetSearchTemplateDescriptor.VersionType** *Removed (Breaking)*

```csharp
public GetSearchTemplateDescriptor VersionType(VersionType version_type)
```

**public property Nest.GetSearchTemplateRequest.Version** *Removed (Breaking)*

```csharp
public long Version { get; set; }
```

**public property Nest.GetSearchTemplateRequest.VersionType** *Removed (Breaking)*

```csharp
public VersionType VersionType { get; set; }
```

**public class Nest.GetWarmerDescriptor** *Removed (Breaking)*

```csharp
[DescriptorForAttribute("IndicesGetWarmer")]
public class GetWarmerDescriptor : RequestDescriptorBase<GetWarmerDescriptor, GetWarmerRequestParameters, IGetWarmerRequest>, IRequest<GetWarmerRequestParameters>, IRequest, IDescriptor, IGetWarmerRequest
```

**public class Nest.GetWarmerRequest** *Removed (Breaking)*

```csharp
public class GetWarmerRequest : PlainRequestBase<GetWarmerRequestParameters>, IRequest<GetWarmerRequestParameters>, IRequest, IGetWarmerRequest
```

**public class Nest.GetWarmerResponse** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class GetWarmerResponse : ResponseBase, IResponse, IBodyWithApiCallDetails, IGetWarmerResponse
```

**public class Nest.GlobalInnerHit** *Removed (Breaking)*

```csharp
public class GlobalInnerHit : InnerHits, IInnerHits, IGlobalInnerHit
```

**public class Nest.GlobalInnerHitDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
public class GlobalInnerHitDescriptor<T> : DescriptorBase<GlobalInnerHitDescriptor<T>, IGlobalInnerHit>, IDescriptor, IGlobalInnerHit, IInnerHits
```

**public property Nest.HasParentQuery.ScoreMode** *Removed (Breaking)*

```csharp
public Nullable<ParentScoreMode> ScoreMode { get; set; }
```

**public method Nest.HasParentQueryDescriptor&lt;T&gt;.ScoreMode** *Removed (Breaking)*

```csharp
public HasParentQueryDescriptor<T> ScoreMode(Nullable<ParentScoreMode> scoreMode = 1)
```

**public class Nest.HighlightDocumentDictionary** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public class HighlightDocumentDictionary : Dictionary<string, HighlightFieldDictionary>, IDictionary<string, HighlightFieldDictionary>, ICollection<KeyValuePair<string, HighlightFieldDictionary>>, IEnumerable<KeyValuePair<string, HighlightFieldDictionary>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<string, HighlightFieldDictionary>, IReadOnlyCollection<KeyValuePair<string, HighlightFieldDictionary>>, ISerializable, IDeserializationCallback
```

**public property Nest.HighlightField.CustomType** *Removed (Breaking)*

```csharp
public string CustomType { get; set; }
```

**public method Nest.HighlightFieldDescriptor&lt;T&gt;.OnAll** *Removed (Breaking)*

```csharp
public HighlightFieldDescriptor<T> OnAll()
```

**public class Nest.HistogramBucket** *Removed (Breaking)*

```csharp
public class HistogramBucket : BucketBase, IBucket
```

**public property Nest.IAllocateClusterRerouteCommand.AllowPrimary** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("allow_primary")]
public bool? AllowPrimary { get; set; }
```

**public property Nest.IAttachmentProperty.FileField** *Removed (Breaking)*

```csharp
public IStringProperty FileField { get; set; }
```

**public property Nest.IBoolQuery.CreatedByBoolDsl** *Removed (Breaking)*

```csharp
public bool CreatedByBoolDsl { get; }
```

**public property Nest.IBulkAllRequest&lt;T&gt;.Consistency** *Removed (Breaking)*

```csharp
public Nullable<Consistency> Consistency { get; set; }
```

**public property Nest.IBulkResponse.TookAsLong** *Removed (Breaking)*

```csharp
public long TookAsLong { get; }
```

**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.InferFrom** *Removed (Breaking)*

```csharp
public TDocument InferFrom { get; set; }
```

**public property Nest.ICategorySuggestContext.Default** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("default")]
public IEnumerable<string> Default { get; set; }
```

**public property Nest.ICompletionProperty.Context** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("context")]
public ISuggestContextMapping Context { get; set; }
```

**public property Nest.ICompletionProperty.Payloads** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("payloads")]
public bool? Payloads { get; set; }
```

**public property Nest.ICompletionSuggester.Context** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("context")]
public IDictionary<string, object> Context { get; set; }
```

**public property Nest.IDeleteByQueryResponse.Indices** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("_indices")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public IDictionary<string, DeleteByQueryIndicesResult> Indices { get; }
```

**public interface Nest.IDeleteWarmerRequest** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface IDeleteWarmerRequest : IRequest<DeleteWarmerRequestParameters>, IRequest
```

**public interface Nest.IDeleteWarmerResponse** *Removed (Breaking)*

```csharp
public interface IDeleteWarmerResponse : IAcknowledgedResponse, IResponse, IBodyWithApiCallDetails
```

**public property Nest.IDirectGenerator.MinWordLen** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public int? MinWordLen { get; set; }
```

**public property Nest.IDirectGenerator.PrefixLen** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public int? PrefixLen { get; set; }
```

**public property Nest.IDynamicIndexSettings.RequestCacheEnabled** *Removed (Breaking)*

```csharp
public bool? RequestCacheEnabled { get; set; }
```

**public property Nest.IDynamicIndexSettings.WarmersEnabled** *Removed (Breaking)*

```csharp
public bool? WarmersEnabled { get; set; }
```

**public method Nest.IElasticClient.DeleteWarmer** *Removed (Breaking)*

```csharp
public IDeleteWarmerResponse DeleteWarmer(IDeleteWarmerRequest request)
```

**public method Nest.IElasticClient.DeleteWarmer** *Removed (Breaking)*

```csharp
public IDeleteWarmerResponse DeleteWarmer(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector)
```

**public method Nest.IElasticClient.DeleteWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IDeleteWarmerResponse> DeleteWarmerAsync(IDeleteWarmerRequest request)
```

**public method Nest.IElasticClient.DeleteWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IDeleteWarmerResponse> DeleteWarmerAsync(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector)
```

**public method Nest.IElasticClient.GetWarmer** *Removed (Breaking)*

```csharp
public IGetWarmerResponse GetWarmer(IGetWarmerRequest request)
```

**public method Nest.IElasticClient.GetWarmer** *Removed (Breaking)*

```csharp
public IGetWarmerResponse GetWarmer(Func<GetWarmerDescriptor, IGetWarmerRequest> selector)
```

**public method Nest.IElasticClient.GetWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IGetWarmerResponse> GetWarmerAsync(IGetWarmerRequest request)
```

**public method Nest.IElasticClient.GetWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IGetWarmerResponse> GetWarmerAsync(Func<GetWarmerDescriptor, IGetWarmerRequest> selector)
```

**public method Nest.IElasticClient.Optimize** *Removed (Breaking)*

```csharp
public IOptimizeResponse Optimize(IOptimizeRequest request)
```

**public method Nest.IElasticClient.Optimize** *Removed (Breaking)*

```csharp
public IOptimizeResponse Optimize(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector)
```

**public method Nest.IElasticClient.OptimizeAsync** *Removed (Breaking)*

```csharp
public Task<IOptimizeResponse> OptimizeAsync(IOptimizeRequest request)
```

**public method Nest.IElasticClient.OptimizeAsync** *Removed (Breaking)*

```csharp
public Task<IOptimizeResponse> OptimizeAsync(Indices indices, Func<OptimizeDescriptor, IOptimizeRequest> selector)
```

**public method Nest.IElasticClient.PutWarmer** *Removed (Breaking)*

```csharp
public IPutWarmerResponse PutWarmer(IPutWarmerRequest request)
```

**public method Nest.IElasticClient.PutWarmer** *Removed (Breaking)*

```csharp
public IPutWarmerResponse PutWarmer(Name name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector)
```

**public method Nest.IElasticClient.PutWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IPutWarmerResponse> PutWarmerAsync(IPutWarmerRequest request)
```

**public method Nest.IElasticClient.PutWarmerAsync** *Removed (Breaking)*

```csharp
public Task<IPutWarmerResponse> PutWarmerAsync(Name name, Func<PutWarmerDescriptor, IPutWarmerRequest> selector)
```

**public method Nest.IElasticClient.SearchExists** *Removed (Breaking)*

```csharp
public IExistsResponse SearchExists(ISearchExistsRequest request)
```

**public method Nest.IElasticClient.SearchExists&lt;T&gt;** *Removed (Breaking)*

```csharp
public IExistsResponse SearchExists<T>(Func<SearchExistsDescriptor<T>, ISearchExistsRequest> selector)
```

**public method Nest.IElasticClient.SearchExistsAsync** *Removed (Breaking)*

```csharp
public Task<IExistsResponse> SearchExistsAsync(ISearchExistsRequest request)
```

**public method Nest.IElasticClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)*

```csharp
public Task<IExistsResponse> SearchExistsAsync<T>(Func<SearchExistsDescriptor<T>, ISearchExistsRequest> selector)
```

**public method Nest.IElasticClient.Suggest** *Removed (Breaking)*

```csharp
public ISuggestResponse Suggest(ISuggestRequest request)
```

**public method Nest.IElasticClient.SuggestAsync** *Removed (Breaking)*

```csharp
public Task<ISuggestResponse> SuggestAsync(ISuggestRequest request)
```

**public method Nest.IElasticClient.TasksCancel** *Removed (Breaking)*

```csharp
public ITasksCancelResponse TasksCancel(ITasksCancelRequest request)
```

**public method Nest.IElasticClient.TasksCancel** *Removed (Breaking)*

```csharp
public ITasksCancelResponse TasksCancel(Func<TasksCancelDescriptor, ITasksCancelRequest> selector)
```

**public method Nest.IElasticClient.TasksCancelAsync** *Removed (Breaking)*

```csharp
public Task<ITasksCancelResponse> TasksCancelAsync(ITasksCancelRequest request)
```

**public method Nest.IElasticClient.TasksCancelAsync** *Removed (Breaking)*

```csharp
public Task<ITasksCancelResponse> TasksCancelAsync(Func<TasksCancelDescriptor, ITasksCancelRequest> selector)
```

**public method Nest.IElasticClient.TasksList** *Removed (Breaking)*

```csharp
public ITasksListResponse TasksList(ITasksListRequest request)
```

**public method Nest.IElasticClient.TasksList** *Removed (Breaking)*

```csharp
public ITasksListResponse TasksList(Func<TasksListDescriptor, ITasksListRequest> selector)
```

**public method Nest.IElasticClient.TasksListAsync** *Removed (Breaking)*

```csharp
public Task<ITasksListResponse> TasksListAsync(ITasksListRequest request)
```

**public method Nest.IElasticClient.TasksListAsync** *Removed (Breaking)*

```csharp
public Task<ITasksListResponse> TasksListAsync(Func<TasksListDescriptor, ITasksListRequest> selector)
```

**public method Nest.IElasticClient.UpdateByQuery&lt;T&gt;** *Removed (Breaking)*

```csharp
public IUpdateByQueryResponse UpdateByQuery<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
```

**public method Nest.IElasticClient.UpdateByQueryAsync&lt;T&gt;** *Removed (Breaking)*

```csharp
public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)
```

**public method Nest.IElasticClient.WatcherInfo** *Removed (Breaking)*

```csharp
public IWatcherInfoResponse WatcherInfo(IWatcherInfoRequest request)
```

**public method Nest.IElasticClient.WatcherInfo** *Removed (Breaking)*

```csharp
public IWatcherInfoResponse WatcherInfo(Func<WatcherInfoDescriptor, IWatcherInfoRequest> selector)
```

**public method Nest.IElasticClient.WatcherInfoAsync** *Removed (Breaking)*

```csharp
public Task<IWatcherInfoResponse> WatcherInfoAsync(IWatcherInfoRequest request)
```

**public method Nest.IElasticClient.WatcherInfoAsync** *Removed (Breaking)*

```csharp
public Task<IWatcherInfoResponse> WatcherInfoAsync(Func<WatcherInfoDescriptor, IWatcherInfoRequest> selector)
```

**public interface Nest.IGeoLocationSuggestContext** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface IGeoLocationSuggestContext : ISuggestContext
```

**public interface Nest.IGetAliasesRequest** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface IGetAliasesRequest : IRequest<GetAliasesRequestParameters>, IRequest
```

**public interface Nest.IGetAliasesResponse** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.GetAliasResponseConverter)]
public interface IGetAliasesResponse : IResponse, IBodyWithApiCallDetails
```

**public property Nest.IGetMappingResponse.IndexTypeMappings** *Removed (Breaking)*

```csharp
public Dictionary<IndexName, IDictionary<TypeName, TypeMapping>> IndexTypeMappings { get; }
```

**public interface Nest.IGetWarmerRequest** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface IGetWarmerRequest : IRequest<GetWarmerRequestParameters>, IRequest
```

**public interface Nest.IGetWarmerResponse** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.GetWarmerResponseConverter)]
public interface IGetWarmerResponse : IResponse, IBodyWithApiCallDetails
```

**public interface Nest.IGlobalInnerHit** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.GlobalInnerHit])]
public interface IGlobalInnerHit : IInnerHits
```

**public property Nest.IHasParentQuery.ScoreMode** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("score_mode")]
public Nullable<ParentScoreMode> ScoreMode { get; set; }
```


**public property Nest.IIndexRequest.UntypedDocument** *Removed (Breaking)*

```csharp
public object UntypedDocument { get; }
```

**public property Nest.IIndexState.Warmers** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("warmers")]
public IWarmers Warmers { get; set; }
```

**public property Nest.IIndicesPrivileges.Fields** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("fields")]
public Fields Fields { get; set; }
```

**public interface Nest.IInnerHitsContainer** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.InnerHitsContainer])]
public interface IInnerHitsContainer
```

**public property Nest.IMultiGetOperation.Fields** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public Fields Fields { get; set; }
```

**public property Nest.IMultiTermVectorOperation.Fields** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("fields")]
public Fields Fields { get; set; }
```

**public interface Nest.INamedInnerHits** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`3[Nest.NamedInnerHits,System.String,Nest.IInnerHitsContainer])]
public interface INamedInnerHits : IIsADictionary<string, IInnerHitsContainer>, IDictionary<string, IInnerHitsContainer>, ICollection<KeyValuePair<string, IInnerHitsContainer>>, IEnumerable<KeyValuePair<string, IInnerHitsContainer>>, IEnumerable, IDictionary, ICollection, IIsADictionary
```

**public method Nest.IndexDescriptor&lt;TDocument&gt;.Consistency** *Removed (Breaking)*

```csharp
public IndexDescriptor<TDocument> Consistency(Consistency consistency)
```

**public class Nest.IndexFieldMappings** *Removed (Breaking)*

```csharp
public class IndexFieldMappings : Dictionary<string, TypeFieldMappings>, IDictionary<string, TypeFieldMappings>, ICollection<KeyValuePair<string, TypeFieldMappings>>, IEnumerable<KeyValuePair<string, TypeFieldMappings>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<string, TypeFieldMappings>, IReadOnlyCollection<KeyValuePair<string, TypeFieldMappings>>, ISerializable, IDeserializationCallback
```

**public property Nest.IndexRequest&lt;TDocument&gt;.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public method Nest.IndexSettings..ctor** *Removed (Breaking)*

```csharp
public  .ctor(Dictionary<string, object> container)
```

**public property Nest.IndexState.Warmers** *Removed (Breaking)*

```csharp
public IWarmers Warmers { get; set; }
```

**public property Nest.IndexStats.Percolate** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public PercolateStats Percolate { get; set; }
```

**public property Nest.IndexStats.Suggest** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public SuggestStats Suggest { get; set; }
```

**public property Nest.IndicesPrivileges.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public method Nest.IndicesPrivilegesDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public IndicesPrivilegesDescriptor<T> Fields(Fields fields)
```

**public method Nest.IndicesPrivilegesDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public IndicesPrivilegesDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields)
```

**public method Nest.Infer.Fields&lt;T&gt;** *Removed (Breaking)*

```csharp
public static Fields Fields<T>(String[] fields)
```

**public class Nest.InnerHitsContainer** *Removed (Breaking)*

```csharp
public class InnerHitsContainer : IInnerHitsContainer
```

**public class Nest.InnerHitsContainerDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
public class InnerHitsContainerDescriptor<T> : DescriptorBase<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>, IDescriptor, IInnerHitsContainer
```

**public interface Nest.INorms** *Removed (Breaking)*

```csharp
[JsonObjectAttribute(1)]
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.Norms])]
public interface INorms
```

**public interface Nest.IOptimizeRequest** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface IOptimizeRequest : IRequest<OptimizeRequestParameters>, IRequest
```

**public interface Nest.IOptimizeResponse** *Removed (Breaking)*

```csharp
public interface IOptimizeResponse : IShardsOperationResponse, IResponse, IBodyWithApiCallDetails
```

**public interface Nest.IPathInnerHit** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`3[Nest.PathInnerHit,Nest.Field,Nest.IGlobalInnerHit])]
public interface IPathInnerHit : IIsADictionary<Field, IGlobalInnerHit>, IDictionary<Field, IGlobalInnerHit>, ICollection<KeyValuePair<Field, IGlobalInnerHit>>, IEnumerable<KeyValuePair<Field, IGlobalInnerHit>>, IEnumerable, IDictionary, ICollection, IIsADictionary
```

**public property Nest.IPercolateCountResponse.TookAsLong** *Removed (Breaking)*

```csharp
public long TookAsLong { get; }
```

**public property Nest.IProperty.CopyTo** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("copy_to")]
public Fields CopyTo { get; set; }
```

**public property Nest.IProperty.DocValues** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("doc_values")]
public bool? DocValues { get; set; }
```

**public property Nest.IProperty.Fields** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("fields")]
public IProperties Fields { get; set; }
```

**public property Nest.IProperty.Similarity** *Removed (Breaking)*

```csharp
public Nullable<SimilarityOption> Similarity { get; set; }
```

**public property Nest.IProperty.Store** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("store")]
public bool? Store { get; set; }
```

**public interface Nest.IPutWarmerRequest** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.PutWarmerRequestJsonConverter)]
[JsonObjectAttribute]
public interface IPutWarmerRequest : IRequest<PutWarmerRequestParameters>, IRequest
```

**public interface Nest.IPutWarmerResponse** *Removed (Breaking)*

```csharp
public interface IPutWarmerResponse : IAcknowledgedResponse, IResponse, IBodyWithApiCallDetails
```

**public interface Nest.IReindexRequest** *Removed (Breaking)*

```csharp
public interface IReindexRequest
```

**public interface Nest.IReindexResponse&lt;T&gt;** *Removed (Breaking)*

```csharp
public interface IReindexResponse<T>
```

**public property Nest.IResponse.ApiCall** *Removed (Breaking)*

```csharp
[JsonIgnoreAttribute]
public IApiCallDetails ApiCall { get; }
```

**public property Nest.ISamplerAggregation.Field** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("field")]
public Field Field { get; set; }
```

**public interface Nest.ISearchExistsRequest** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.SearchExistsRequest])]
[JsonObjectAttribute]
public interface ISearchExistsRequest : IRequest<SearchExistsRequestParameters>, IRequest
```

**public interface Nest.ISearchExistsRequest&lt;T&gt;** *Removed (Breaking)*

```csharp
public interface ISearchExistsRequest<T> : ISearchExistsRequest, IRequest<SearchExistsRequestParameters>, IRequest
```

**public property Nest.ISearchRequest.Fields** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public Fields Fields { get; set; }
```

**public property Nest.ISearchRequest.InnerHits** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public INamedInnerHits InnerHits { get; set; }
```

**public property Nest.ISearchResponse&lt;T&gt;.Highlights** *Removed (Breaking)*

```csharp
public HighlightDocumentDictionary Highlights { get; }
```

**public property Nest.ISearchResponse&lt;T&gt;.TookAsLong** *Removed (Breaking)*

```csharp
public long TookAsLong { get; }
```

**public property Nest.ISearchTemplateRequest.Template** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string Template { get; set; }
```

**public property Nest.ISourceFilter.Disable** *Removed (Breaking)*

```csharp
[JsonIgnoreAttribute]
public bool Disable { get; set; }
```

**public property Nest.ISourceFilter.Exclude** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("exclude")]
public Fields Exclude { get; set; }
```

**public property Nest.ISourceFilter.Include** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("include")]
public Fields Include { get; set; }
```


**public interface Nest.ISuggestContextMapping** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`3[Nest.SuggestContextMapping,System.String,Nest.ISuggestContext])]
public interface ISuggestContextMapping : IIsADictionary<string, ISuggestContext>, IDictionary<string, ISuggestContext>, ICollection<KeyValuePair<string, ISuggestContext>>, IEnumerable<KeyValuePair<string, ISuggestContext>>, IEnumerable, IDictionary, ICollection, IIsADictionary
```

**public property Nest.ISuggester.ShardSize** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public int? ShardSize { get; set; }
```

**public property Nest.ISuggester.Text** *Removed (Breaking)*

```csharp
public string Text { get; set; }
```

**public interface Nest.ISuggestResponse** *Removed (Breaking)*

```csharp
public interface ISuggestResponse : IResponse, IBodyWithApiCallDetails
```

**public interface Nest.ITasksCancelRequest** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface ITasksCancelRequest : IRequest<TasksCancelRequestParameters>, IRequest
```

**public interface Nest.ITasksCancelResponse** *Removed (Breaking)*

```csharp
[JsonObjectAttribute(1)]
public interface ITasksCancelResponse : IResponse, IBodyWithApiCallDetails
```

**public interface Nest.ITasksListRequest** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface ITasksListRequest : IRequest<TasksListRequestParameters>, IRequest
```

**public interface Nest.ITasksListResponse** *Removed (Breaking)*

```csharp
[JsonObjectAttribute(1)]
public interface ITasksListResponse : IResponse, IBodyWithApiCallDetails
```

**public property Nest.ITemplateMapping.Warmers** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("warmers")]
public IWarmers Warmers { get; set; }
```

**public property Nest.ITermsAggregation.ShowTermDocumentCountError** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("show_term_doc_error_count")]
public bool? ShowTermDocumentCountError { get; set; }
```

**public property Nest.ITermSuggester.MinWordLen** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public int? MinWordLen { get; set; }
```

**public property Nest.ITermSuggester.PrefixLen** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public int? PrefixLen { get; set; }
```

**public property Nest.ITermVectorsResponse.Found** *Removed (Breaking)*

```csharp
public bool Found { get; }
```

**public property Nest.ITermVectorsResponse.Id** *Removed (Breaking)*

```csharp
public string Id { get; }
```

**public property Nest.ITermVectorsResponse.Index** *Removed (Breaking)*

```csharp
public string Index { get; }
```

**public property Nest.ITermVectorsResponse.TermVectors** *Removed (Breaking)*

```csharp
public IDictionary<string, TermVector> TermVectors { get; }
```

**public property Nest.ITermVectorsResponse.TookAsLong** *Removed (Breaking)*

```csharp
public long TookAsLong { get; }
```

**public property Nest.ITermVectorsResponse.Type** *Removed (Breaking)*

```csharp
public string Type { get; }
```

**public property Nest.ITermVectorsResponse.Version** *Removed (Breaking)*

```csharp
public long Version { get; }
```

**public property Nest.ITranslogFlushSettings.ThresholdOps** *Removed (Breaking)*

```csharp
public int? ThresholdOps { get; set; }
```

**public property Nest.ITranslogSettings.FileSystemType** *Removed (Breaking)*

```csharp
public Nullable<TranslogWriteMode> FileSystemType { get; set; }
```

**public interface Nest.ITypeInnerHit** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`3[Nest.TypeInnerHit,Nest.TypeName,Nest.IGlobalInnerHit])]
public interface ITypeInnerHit : IIsADictionary<TypeName, IGlobalInnerHit>, IDictionary<TypeName, IGlobalInnerHit>, ICollection<KeyValuePair<TypeName, IGlobalInnerHit>>, IEnumerable<KeyValuePair<TypeName, IGlobalInnerHit>>, IEnumerable, IDictionary, ICollection, IIsADictionary
```

**public interface Nest.IWarmer** *Removed (Breaking)*

```csharp
public interface IWarmer
```

**public interface Nest.IWarmers** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`3[Nest.Warmers,Nest.TypeName,Nest.IWarmer])]
public interface IWarmers : IIsADictionary<TypeName, IWarmer>, IDictionary<TypeName, IWarmer>, ICollection<KeyValuePair<TypeName, IWarmer>>, IEnumerable<KeyValuePair<TypeName, IWarmer>>, IEnumerable, IDictionary, ICollection, IIsADictionary
```

**public interface Nest.IWatcherInfoRequest** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface IWatcherInfoRequest : IRequest<WatcherInfoRequestParameters>, IRequest
```

**public interface Nest.IWatcherInfoResponse** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public interface IWatcherInfoResponse : IResponse, IBodyWithApiCallDetails
```


**public method Nest.MultiGetDescriptor.Fields** *Removed (Breaking)*

```csharp
public MultiGetDescriptor Fields(String[] fields)
```

**public method Nest.MultiGetDescriptor.Fields&lt;T&gt;** *Removed (Breaking)*

```csharp
public MultiGetDescriptor Fields<T>(Expression`1[] fields)
```

**public property Nest.MultiGetOperation&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public method Nest.MultiGetOperationDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public MultiGetOperationDescriptor<T> Fields(Fields fields)
```

**public method Nest.MultiGetOperationDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public MultiGetOperationDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields)
```

**public property Nest.MultiGetRequest.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public class Nest.MultiRescore** *Removed (Breaking)*

```csharp
public class MultiRescore : IRescore, IEnumerable<IRescore>, IEnumerable
```

**public property Nest.MultiTermVectorOperation&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public method Nest.MultiTermVectorOperationDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public MultiTermVectorOperationDescriptor<T> Fields(Fields fields)
```

**public method Nest.MultiTermVectorOperationDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public MultiTermVectorOperationDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields)
```

**public class Nest.NamedInnerHits** *Removed (Breaking)*

```csharp
public class NamedInnerHits : IsADictionaryBase<string, IInnerHitsContainer>, IIsADictionary<string, IInnerHitsContainer>, IDictionary<string, IInnerHitsContainer>, ICollection<KeyValuePair<string, IInnerHitsContainer>>, IEnumerable<KeyValuePair<string, IInnerHitsContainer>>, IEnumerable, IDictionary, ICollection, IIsADictionary, INamedInnerHits
```

**public class Nest.NamedInnerHitsDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
public class NamedInnerHitsDescriptor<T> : IsADictionaryDescriptorBase<NamedInnerHitsDescriptor<T>, INamedInnerHits, string, IInnerHitsContainer>, IDescriptor, IPromise<INamedInnerHits>
```

**public property Nest.NodeInfo.Build** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string Build { get; internal set; }
```

**public property Nest.NodeInfo.Hostname** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string Hostname { get; internal set; }
```

**public property Nest.NodeInfo.HttpAddress** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string HttpAddress { get; internal set; }
```

**public property Nest.NodeProcessInfo.RefreshInterval** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string RefreshInterval { get; internal set; }
```


```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NonStringIndexOption
{
     NotAnalyzed = 0,
     No = 1
}
```

**public class Nest.Norms** *Removed (Breaking)*

```csharp
public class Norms : INorms
```

**public class Nest.NormsDescriptor** *Removed (Breaking)*

```csharp
public class NormsDescriptor : DescriptorBase<NormsDescriptor, INorms>, IDescriptor, INorms
```

**public enum Nest.NormsLoading** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NormsLoading
{
     Lazy = 0,
     Eager = 1
}
```

**public enum Nest.NumericResolutionUnit** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum NumericResolutionUnit
{
     Milliseconds = 0,
     Seconds = 1
}
```

**public property Nest.ObjectAttribute.Dynamic** *Removed (Breaking)*

```csharp
public DynamicMapping Dynamic { get; set; }
```

**public property Nest.OperatingSystemStats.CpuPercent** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("cpu_percent")]
public int CpuPercent { get; internal set; }
```

**public property Nest.OperatingSystemStats.LoadAverage** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("load_average")]
public Single LoadAverage { get; internal set; }
```

**public class Nest.OptimizeDescriptor** *Removed (Breaking)*

```csharp
[DescriptorForAttribute("IndicesOptimize")]
public class OptimizeDescriptor : RequestDescriptorBase<OptimizeDescriptor, OptimizeRequestParameters, IOptimizeRequest>, IRequest<OptimizeRequestParameters>, IRequest, IDescriptor, IOptimizeRequest
```

**public class Nest.OptimizeRequest** *Removed (Breaking)*

```csharp
public class OptimizeRequest : PlainRequestBase<OptimizeRequestParameters>, IRequest<OptimizeRequestParameters>, IRequest, IOptimizeRequest
```

**public class Nest.OptimizeResponse** *Removed (Breaking)*

```csharp
public class OptimizeResponse : ShardsOperationResponseBase, IResponse, IBodyWithApiCallDetails, IShardsOperationResponse, IOptimizeResponse
```

**public enum Nest.ParentScoreMode** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum ParentScoreMode
{
     None = 0,
     Score = 1
}
```

**public class Nest.PathInnerHit** *Removed (Breaking)*

```csharp
public class PathInnerHit : IsADictionaryBase<Field, IGlobalInnerHit>, IIsADictionary<Field, IGlobalInnerHit>, IDictionary<Field, IGlobalInnerHit>, ICollection<KeyValuePair<Field, IGlobalInnerHit>>, IEnumerable<KeyValuePair<Field, IGlobalInnerHit>>, IEnumerable, IDictionary, ICollection, IIsADictionary, IPathInnerHit
```

**public class Nest.PathInnerHit&lt;T&gt;** *Removed (Breaking)*

```csharp
public class PathInnerHit<T> : PathInnerHit, IIsADictionary<Field, IGlobalInnerHit>, IDictionary<Field, IGlobalInnerHit>, ICollection<KeyValuePair<Field, IGlobalInnerHit>>, IEnumerable<KeyValuePair<Field, IGlobalInnerHit>>, IEnumerable, IDictionary, ICollection, IIsADictionary, IPathInnerHit
```

**public class Nest.PathInnerHitDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
public class PathInnerHitDescriptor<T> : IsADictionaryDescriptorBase<PathInnerHitDescriptor<T>, IPathInnerHit, Field, IGlobalInnerHit>, IDescriptor, IPromise<IPathInnerHit>
```

**public property Nest.PercolateCountResponse.TookAsLong** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("took")]
public long TookAsLong { get; internal set; }
```

**public class Nest.PercolateStats** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class PercolateStats
```

**public property Nest.PropertyBase.CopyTo** *Removed (Breaking)*

```csharp
public Fields CopyTo { get; set; }
```

**public property Nest.PropertyBase.DocValues** *Removed (Breaking)*

```csharp
public bool? DocValues { get; set; }
```

**public property Nest.PropertyBase.Fields** *Removed (Breaking)*

```csharp
public IProperties Fields { get; set; }
```

**public property Nest.PropertyBase.IndexName** *Removed (Breaking)*

```csharp
public string IndexName { get; set; }
```

**public property Nest.PropertyBase.Similarity** *Removed (Breaking)*

```csharp
public Nullable<SimilarityOption> Similarity { get; set; }
```

**public property Nest.PropertyBase.Store** *Removed (Breaking)*

```csharp
public bool? Store { get; set; }
```

**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.CopyTo** *Removed (Breaking)*

```csharp
public TDescriptor CopyTo(Func<FieldsDescriptor<T>, IPromise<Fields>> fields)
```

**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.DocValues** *Removed (Breaking)*

```csharp
public TDescriptor DocValues(bool docValues = True)
```

**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Fields** *Removed (Breaking)*

```csharp
public TDescriptor Fields(Func<PropertiesDescriptor<T>, IPromise<IProperties>> selector)
```

**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Similarity** *Removed (Breaking)*

```csharp
public TDescriptor Similarity(string similarity)
```

**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Similarity** *Removed (Breaking)*

```csharp
public TDescriptor Similarity(SimilarityOption similarity)
```

**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Store** *Removed (Breaking)*

```csharp
public TDescriptor Store(bool store = True)
```

**public method Nest.PutIndexTemplateDescriptor.Warmers** *Removed (Breaking)*

```csharp
public PutIndexTemplateDescriptor Warmers(Func<WarmersDescriptor, IPromise<IWarmers>> warmerSelector)
```

**public property Nest.PutIndexTemplateRequest.Warmers** *Removed (Breaking)*

```csharp
public IWarmers Warmers { get; set; }
```

**public method Nest.PutMappingDescriptor&lt;T&gt;.Parent&lt;K&gt;** *Removed (Breaking)*

```csharp
public PutMappingDescriptor<T> Parent<K>()
```

**public method Nest.PutScriptDescriptor.OpType** *Removed (Breaking)*

```csharp
public PutScriptDescriptor OpType(OpType op_type)
```

**public method Nest.PutScriptDescriptor.Version** *Removed (Breaking)*

```csharp
public PutScriptDescriptor Version(long version)
```

**public method Nest.PutScriptDescriptor.VersionType** *Removed (Breaking)*

```csharp
public PutScriptDescriptor VersionType(VersionType version_type)
```

**public property Nest.PutScriptRequest.OpType** *Removed (Breaking)*

```csharp
public OpType OpType { get; set; }
```

**public property Nest.PutScriptRequest.Version** *Removed (Breaking)*

```csharp
public long Version { get; set; }
```

**public property Nest.PutScriptRequest.VersionType** *Removed (Breaking)*

```csharp
public VersionType VersionType { get; set; }
```

**public method Nest.PutSearchTemplateDescriptor.OpType** *Removed (Breaking)*

```csharp
public PutSearchTemplateDescriptor OpType(OpType op_type)
```

**public method Nest.PutSearchTemplateDescriptor.Version** *Removed (Breaking)*

```csharp
public PutSearchTemplateDescriptor Version(long version)
```

**public method Nest.PutSearchTemplateDescriptor.VersionType** *Removed (Breaking)*

```csharp
public PutSearchTemplateDescriptor VersionType(VersionType version_type)
```

**public property Nest.PutSearchTemplateRequest.OpType** *Removed (Breaking)*

```csharp
public OpType OpType { get; set; }
```

**public property Nest.PutSearchTemplateRequest.Version** *Removed (Breaking)*

```csharp
public long Version { get; set; }
```

**public property Nest.PutSearchTemplateRequest.VersionType** *Removed (Breaking)*

```csharp
public VersionType VersionType { get; set; }
```

**public class Nest.PutWarmerDescriptor** *Removed (Breaking)*

```csharp
[DescriptorForAttribute("IndicesPutWarmer")]
public class PutWarmerDescriptor : RequestDescriptorBase<PutWarmerDescriptor, PutWarmerRequestParameters, IPutWarmerRequest>, IRequest<PutWarmerRequestParameters>, IRequest, IDescriptor, IPutWarmerRequest
```

**public class Nest.PutWarmerRequest** *Removed (Breaking)*

```csharp
public class PutWarmerRequest : PlainRequestBase<PutWarmerRequestParameters>, IRequest<PutWarmerRequestParameters>, IRequest, IPutWarmerRequest
```

**public class Nest.PutWarmerResponse** *Removed (Breaking)*

```csharp
public class PutWarmerResponse : AcknowledgedResponseBase, IResponse, IBodyWithApiCallDetails, IAcknowledgedResponse, IPutWarmerResponse
```

**public property Nest.QueryProfile.Lucene** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("lucene")]
public string Lucene { get; internal set; }
```

**public property Nest.QueryProfile.QueryType** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("query_type")]
public string QueryType { get; internal set; }
```

**public method Nest.ReindexDescriptor&lt;T&gt;.AllTypes** *Removed (Breaking)*

```csharp
public ReindexDescriptor<T> AllTypes()
```

**public method Nest.ReindexDescriptor&lt;T&gt;.CreateIndex** *Removed (Breaking)*

```csharp
public ReindexDescriptor<T> CreateIndex(ICreateIndexRequest createIndexRequest)
```

**public method Nest.ReindexDescriptor&lt;T&gt;.Query** *Removed (Breaking)*

```csharp
public ReindexDescriptor<T> Query(QueryContainer query)
```

**public method Nest.ReindexDescriptor&lt;T&gt;.Query** *Removed (Breaking)*

```csharp
public ReindexDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
```

**public method Nest.ReindexDescriptor&lt;T&gt;.Scroll** *Removed (Breaking)*

```csharp
public ReindexDescriptor<T> Scroll(Time scrollTime)
```

**public method Nest.ReindexDescriptor&lt;T&gt;.Size** *Removed (Breaking)*

```csharp
public ReindexDescriptor<T> Size(int? size)
```

**public method Nest.ReindexDescriptor&lt;T&gt;.Take** *Removed (Breaking)*

```csharp
public ReindexDescriptor<T> Take(int? take)
```

**public method Nest.ReindexDescriptor&lt;T&gt;.Type** *Removed (Breaking)*

```csharp
public ReindexDescriptor<T> Type(Types type)
```

**public method Nest.ReindexObservable&lt;T&gt;.IndexSearchResults** *Removed (Breaking)*

```csharp
public IBulkResponse IndexSearchResults(ISearchResponse<T> searchResult, IObserver<IReindexResponse<T>> observer, IndexName toIndex, int page)
```

**public method Nest.ReindexOnServerDescriptor.Consistency** *Removed (Breaking)*

```csharp
public ReindexOnServerDescriptor Consistency(Consistency consistency)
```

**public property Nest.ReindexOnServerRequest.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public class Nest.ReindexRequest** *Removed (Breaking)*

```csharp
public class ReindexRequest : IReindexRequest
```

**public class Nest.ReindexResponse&lt;T&gt;** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class ReindexResponse<T> : IReindexResponse<T>
```

**public class Nest.RescoreConverter** *Removed (Breaking)*

```csharp
public class RescoreConverter : JsonConverter
```

**public class Nest.Role** *Removed (Breaking)*

```csharp
public class Role
```

**public property Nest.RoutingShard.Version** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("version")]
public long Version { get; internal set; }
```

**public property Nest.SamplerAggregation.Field** *Removed (Breaking)*

```csharp
public Field Field { get; set; }
```

**public method Nest.SamplerAggregationDescriptor&lt;T&gt;.Field** *Removed (Breaking)*

```csharp
public SamplerAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
```

**public method Nest.SamplerAggregationDescriptor&lt;T&gt;.Field** *Removed (Breaking)*

```csharp
public SamplerAggregationDescriptor<T> Field(Field field)
```

**public method Nest.SearchDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public SearchDescriptor<T> Fields(Fields fields)
```

**public method Nest.SearchDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public SearchDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields)
```

**public method Nest.SearchDescriptor&lt;T&gt;.InnerHits** *Removed (Breaking)*

```csharp
public SearchDescriptor<T> InnerHits(Func<NamedInnerHitsDescriptor<T>, IPromise<INamedInnerHits>> selector)
```

**public class Nest.SearchExistsDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
[DescriptorForAttribute("IndicesExists")]
public class SearchExistsDescriptor<T> : RequestDescriptorBase<SearchExistsDescriptor<T>, SearchExistsRequestParameters, ISearchExistsRequest>, IRequest<SearchExistsRequestParameters>, IRequest, IDescriptor, ISearchExistsRequest
```

**public class Nest.SearchExistsRequest** *Removed (Breaking)*

```csharp
public class SearchExistsRequest : PlainRequestBase<SearchExistsRequestParameters>, IRequest<SearchExistsRequestParameters>, IRequest, ISearchExistsRequest
```

**public class Nest.SearchExistsRequest&lt;T&gt;** *Removed (Breaking)*

```csharp
public class SearchExistsRequest<T> : PlainRequestBase<SearchExistsRequestParameters>, IRequest<SearchExistsRequestParameters>, IRequest, ISearchExistsRequest
```

**public property Nest.SearchRequest.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public property Nest.SearchRequest.InnerHits** *Removed (Breaking)*

```csharp
public INamedInnerHits InnerHits { get; set; }
```

**public property Nest.SearchRequest&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public property Nest.SearchRequest&lt;T&gt;.InnerHits** *Removed (Breaking)*

```csharp
public INamedInnerHits InnerHits { get; set; }
```

**public property Nest.SearchResponse&lt;T&gt;.TookAsLong** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("took")]
public long TookAsLong { get; internal set; }
```

**public property Nest.SearchStats.FetchTime** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string FetchTime { get; set; }
```

**public property Nest.SearchStats.QueryTime** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string QueryTime { get; set; }
```

**public property Nest.SearchStats.ScrollTime** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string ScrollTime { get; set; }
```

**public method Nest.SearchTemplateDescriptor&lt;T&gt;.Template** *Removed (Breaking)*

```csharp
public SearchTemplateDescriptor<T> Template(string template)
```

**public property Nest.SearchTemplateRequest.Template** *Removed (Breaking)*

```csharp
public string Template { get; set; }
```

**public property Nest.ShardStore.Version** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("version")]
public long Version { get; set; }
```

**public class Nest.ShieldNode** *Removed (Breaking)*

```csharp
public class ShieldNode
```

**public class Nest.ShieldNodeStatus** *Removed (Breaking)*

```csharp
public class ShieldNodeStatus : Throwable
```

**public property Nest.Snapshot.ShardFailures** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("failures")]
public IEnumerable<SnapshotShardFailure> ShardFailures { get; internal set; }
```

**public property Nest.SourceFilter.Disable** *Removed (Breaking)*

```csharp
public bool Disable { get; set; }
```

**public property Nest.SourceFilter.Exclude** *Removed (Breaking)*

```csharp
public Fields Exclude { get; set; }
```

**public property Nest.SourceFilter.Include** *Removed (Breaking)*

```csharp
public Fields Include { get; set; }
```

**public method Nest.SourceFilterDescriptor&lt;T&gt;.Disable** *Removed (Breaking)*

```csharp
public SourceFilterDescriptor<T> Disable(bool disable = True)
```

**public method Nest.SourceFilterDescriptor&lt;T&gt;.Exclude** *Removed (Breaking)*

```csharp
public SourceFilterDescriptor<T> Exclude(Func<FieldsDescriptor<T>, IPromise<Fields>> fields)
```

**public method Nest.SourceFilterDescriptor&lt;T&gt;.Include** *Removed (Breaking)*

```csharp
public SourceFilterDescriptor<T> Include(Func<FieldsDescriptor<T>, IPromise<Fields>> fields)
```

**public class Nest.Suggest** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class Suggest
```

**public class Nest.SuggestContextMapping** *Removed (Breaking)*

```csharp
public class SuggestContextMapping : IsADictionaryBase<string, ISuggestContext>, IIsADictionary<string, ISuggestContext>, IDictionary<string, ISuggestContext>, ICollection<KeyValuePair<string, ISuggestContext>>, IEnumerable<KeyValuePair<string, ISuggestContext>>, IEnumerable, IDictionary, ICollection, IIsADictionary, ISuggestContextMapping
```

**public class Nest.SuggestContextMappingDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
public class SuggestContextMappingDescriptor<T> : IsADictionaryDescriptorBase<SuggestContextMappingDescriptor<T>, ISuggestContextMapping, string, ISuggestContext>, IDescriptor, IPromise<ISuggestContextMapping>
```

**public method Nest.SuggestDescriptorBase&lt;TDescriptor, TInterface, T&gt;.ShardSize** *Removed (Breaking)*

```csharp
public TDescriptor ShardSize(int? size)
```

**public method Nest.SuggestDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Text** *Removed (Breaking)*

```csharp
public TDescriptor Text(string text)
```

**public property Nest.SuggesterBase.ShardSize** *Removed (Breaking)*

```csharp
public int? ShardSize { get; set; }
```

**public property Nest.SuggesterBase.Text** *Removed (Breaking)*

```csharp
public string Text { get; set; }
```

**public class Nest.SuggestOption** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[JsonConverterAttribute(Nest.SuggestOptionJsonConverter)]
public class SuggestOption
```

**public class Nest.SuggestOptionJsonConverter** *Removed (Breaking)*

```csharp
public class SuggestOptionJsonConverter : JsonConverter
```

**public class Nest.SuggestResponse** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[JsonConverterAttribute(Nest.SuggestResponseJsonConverter)]
public class SuggestResponse : ResponseBase, IResponse, IBodyWithApiCallDetails, ISuggestResponse
```

**public class Nest.SuggestStats** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class SuggestStats
```

**public class Nest.TasksCancelDescriptor** *Removed (Breaking)*

```csharp
public class TasksCancelDescriptor : RequestDescriptorBase<TasksCancelDescriptor, TasksCancelRequestParameters, ITasksCancelRequest>, IRequest<TasksCancelRequestParameters>, IRequest, IDescriptor, ITasksCancelRequest
```

**public class Nest.TasksCancelRequest** *Removed (Breaking)*

```csharp
public class TasksCancelRequest : PlainRequestBase<TasksCancelRequestParameters>, IRequest<TasksCancelRequestParameters>, IRequest, ITasksCancelRequest
```

**public class Nest.TasksCancelResponse** *Removed (Breaking)*

```csharp
public class TasksCancelResponse : ResponseBase, IResponse, IBodyWithApiCallDetails, ITasksCancelResponse
```

**public class Nest.TasksListDescriptor** *Removed (Breaking)*

```csharp
public class TasksListDescriptor : RequestDescriptorBase<TasksListDescriptor, TasksListRequestParameters, ITasksListRequest>, IRequest<TasksListRequestParameters>, IRequest, IDescriptor, ITasksListRequest
```

**public class Nest.TasksListRequest** *Removed (Breaking)*

```csharp
public class TasksListRequest : PlainRequestBase<TasksListRequestParameters>, IRequest<TasksListRequestParameters>, IRequest, ITasksListRequest
```

**public class Nest.TasksListResponse** *Removed (Breaking)*

```csharp
public class TasksListResponse : ResponseBase, IResponse, IBodyWithApiCallDetails, ITasksListResponse
```

**public property Nest.TemplateMapping.Warmers** *Removed (Breaking)*

```csharp
public IWarmers Warmers { get; set; }
```

**public property Nest.TermsAggregation.ShowTermDocumentCountError** *Removed (Breaking)*

```csharp
public bool? ShowTermDocumentCountError { get; set; }
```

**public method Nest.TermsAggregationDescriptor&lt;T&gt;.ShowTermDocumentCountError** *Removed (Breaking)*

```csharp
public TermsAggregationDescriptor<T> ShowTermDocumentCountError(bool showTermDocCountError = True)
```

**public property Nest.TermSuggester.MinWordLen** *Removed (Breaking)*

```csharp
public int? MinWordLen { get; set; }
```

**public property Nest.TermSuggester.PrefixLen** *Removed (Breaking)*

```csharp
public int? PrefixLen { get; set; }
```

**public method Nest.TermVectorsDescriptor&lt;TDocument&gt;.Dfs** *Removed (Breaking)*

```csharp
public TermVectorsDescriptor<TDocument> Dfs(bool dfs = True)
```

**public property Nest.TermVectorsRequest&lt;TDocument&gt;.Dfs** *Removed (Breaking)*

```csharp
public bool Dfs { get; set; }
```

**public property Nest.TermVectorsResponse.TookAsLong** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("took")]
public long TookAsLong { get; internal set; }
```

**public property Nest.TranslogFlushSettings.ThresholdOps** *Removed (Breaking)*

```csharp
public int? ThresholdOps { get; set; }
```

**public method Nest.TranslogFlushSettingsDescriptor.ThresholdOps** *Removed (Breaking)*

```csharp
public TranslogFlushSettingsDescriptor ThresholdOps(int? operations)
```

**public property Nest.TranslogSettings.FileSystemType** *Removed (Breaking)*

```csharp
public Nullable<TranslogWriteMode> FileSystemType { get; set; }
```

**public method Nest.TranslogSettingsDescriptor.FileSystemType** *Removed (Breaking)*

```csharp
public TranslogSettingsDescriptor FileSystemType(Nullable<TranslogWriteMode> writeMode)
```

**public enum Nest.TranslogWriteMode** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Newtonsoft.Json.Converters.StringEnumConverter)]
public enum TranslogWriteMode
{
     Buffered = 0,
     Simple = 1
}
```

**public class Nest.TypeInnerHit** *Removed (Breaking)*

```csharp
public class TypeInnerHit : IsADictionaryBase<TypeName, IGlobalInnerHit>, IIsADictionary<TypeName, IGlobalInnerHit>, IDictionary<TypeName, IGlobalInnerHit>, ICollection<KeyValuePair<TypeName, IGlobalInnerHit>>, IEnumerable<KeyValuePair<TypeName, IGlobalInnerHit>>, IEnumerable, IDictionary, ICollection, IIsADictionary, ITypeInnerHit
```

**public class Nest.TypeInnerHit&lt;T&gt;** *Removed (Breaking)*

```csharp
public class TypeInnerHit<T> : TypeInnerHit, IIsADictionary<TypeName, IGlobalInnerHit>, IDictionary<TypeName, IGlobalInnerHit>, ICollection<KeyValuePair<TypeName, IGlobalInnerHit>>, IEnumerable<KeyValuePair<TypeName, IGlobalInnerHit>>, IEnumerable, IDictionary, ICollection, IIsADictionary, ITypeInnerHit
```

**public class Nest.TypeInnerHitDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
public class TypeInnerHitDescriptor<T> : IsADictionaryDescriptorBase<TypeInnerHitDescriptor<T>, ITypeInnerHit, TypeName, IGlobalInnerHit>, IDescriptor, IPromise<ITypeInnerHit>
```

**public field Nest.UpdatableIndexSettings.RequestCacheEnable** *Removed (Breaking)*

```csharp
public const string RequestCacheEnable = "index.requests.cache.enable"
```

**public field Nest.UpdatableIndexSettings.TranslogFlushTreshHoldOps** *Removed (Breaking)*

```csharp
public const string TranslogFlushTreshHoldOps = "index.translog.flush_threshold_ops"
```

**public field Nest.UpdatableIndexSettings.TranslogFsType** *Removed (Breaking)*

```csharp
public const string TranslogFsType = "index.translog.fs.type"
```

**public field Nest.UpdatableIndexSettings.TranslogInterval** *Removed (Breaking)*

```csharp
public const string TranslogInterval = "index.translog.interval"
```

**public field Nest.UpdatableIndexSettings.WarmersEnabled** *Removed (Breaking)*

```csharp
public const string WarmersEnabled = "index.warmer.enabled"
```

**public method Nest.UpdateByQueryDescriptor&lt;T&gt;.Consistency** *Removed (Breaking)*

```csharp
public UpdateByQueryDescriptor<T> Consistency(Consistency consistency)
```

**public method Nest.UpdateByQueryDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public UpdateByQueryDescriptor<T> Fields(Expression`1[] fields)
```

**public method Nest.UpdateByQueryDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public UpdateByQueryDescriptor<T> Fields(String[] fields)
```

**public property Nest.UpdateByQueryRequest.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public property Nest.UpdateByQueryRequest.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public property Nest.UpdateByQueryRequest&lt;T&gt;.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public property Nest.UpdateByQueryRequest&lt;T&gt;.Fields** *Removed (Breaking)*

```csharp
public Fields Fields { get; set; }
```

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Consistency** *Removed (Breaking)*

```csharp
public UpdateDescriptor<TDocument, TPartialDocument> Consistency(Consistency consistency)
```


**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public method Nest.UpgradeDescriptor.AllowNoIndices** *Removed (Breaking)*

```csharp
public UpgradeDescriptor AllowNoIndices(bool allow_no_indices = True)
```

**public property Nest.UpgradeRequest.AllowNoIndices** *Removed (Breaking)*

```csharp
public bool AllowNoIndices { get; set; }
```

**public class Nest.User** *Removed (Breaking)*

```csharp
public class User
```

**public class Nest.Warmer** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class Warmer : IWarmer
```

**public class Nest.WarmerDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
public class WarmerDescriptor<T> : DescriptorBase<WarmerDescriptor<T>, IWarmer>, IDescriptor, IWarmer
```

**public class Nest.Warmers** *Removed (Breaking)*

```csharp
public class Warmers : IsADictionaryBase<TypeName, IWarmer>, IIsADictionary<TypeName, IWarmer>, IDictionary<TypeName, IWarmer>, ICollection<KeyValuePair<TypeName, IWarmer>>, IEnumerable<KeyValuePair<TypeName, IWarmer>>, IEnumerable, IDictionary, ICollection, IIsADictionary, IWarmers
```

**public class Nest.WarmersDescriptor** *Removed (Breaking)*

```csharp
public class WarmersDescriptor : IsADictionaryDescriptorBase<WarmersDescriptor, IWarmers, TypeName, IWarmer>, IDescriptor, IPromise<IWarmers>
```

**public class Nest.WatcherInfoDescriptor** *Removed (Breaking)*

```csharp
[DescriptorForAttribute("WatcherInfo")]
public class WatcherInfoDescriptor : RequestDescriptorBase<WatcherInfoDescriptor, WatcherInfoRequestParameters, IWatcherInfoRequest>, IRequest<WatcherInfoRequestParameters>, IRequest, IDescriptor, IWatcherInfoRequest
```

**public class Nest.WatcherInfoRequest** *Removed (Breaking)*

```csharp
public class WatcherInfoRequest : PlainRequestBase<WatcherInfoRequestParameters>, IRequest<WatcherInfoRequestParameters>, IRequest, IWatcherInfoRequest
```

**public class Nest.WatcherInfoResponse** *Removed (Breaking)*

```csharp
public class WatcherInfoResponse : ResponseBase, IResponse, IBodyWithApiCallDetails, IWatcherInfoResponse
```

**public class Nest.WatcherVersion** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
public class WatcherVersion
```

#RemovedIn5xBecauseObsoleted
**public property Nest.AnalyzeRequest.CharFilters** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0. Removed in 5.0.0. Use CharFilter instead")]
public String[] CharFilters { get; set; }
```

**public property Nest.AnalyzeRequest.Filters** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0. Removed in 5.0.0. Use Filter instead")]
public String[] Filters { get; set; }
```

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.ContentLengthField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use ContentLengthField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector)")]
public AttachmentPropertyDescriptor<T> ContentLengthField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)
```

**public class Nest.CatNodeattrsDescriptor** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0.  Use CatNodeAttributesDescriptor instead.")]
public class CatNodeattrsDescriptor : RequestDescriptorBase<CatNodeattrsDescriptor, CatNodeattrsRequestParameters, ICatNodeattrsRequest>, IRequest<CatNodeattrsRequestParameters>, IRequest, IDescriptor, ICatNodeattrsRequest
```

**public class Nest.CatNodeattrsRequest** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0.  Use CatNodeAttributesRequest instead.")]
public class CatNodeattrsRequest : PlainRequestBase<CatNodeattrsRequestParameters>, IRequest<CatNodeattrsRequestParameters>, IRequest, ICatNodeattrsRequest
```

**public property Nest.DateAttribute.NumericResolution** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public NumericResolutionUnit NumericResolution { get; set; }
```

**public property Nest.DateAttribute.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public int PrecisionStep { get; set; }
```

**public property Nest.DateHistogramAggregation.Factor** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major version")]
public int? Factor { get; set; }
```

**public method Nest.DateHistogramAggregationDescriptor&lt;T&gt;.Interval** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major version")]
public DateHistogramAggregationDescriptor<T> Interval(int factor)
```

**public property Nest.DateProperty.NumericResolution** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public Nullable<NumericResolutionUnit> NumericResolution { get; set; }
```

**public method Nest.DatePropertyDescriptor&lt;T&gt;.NumericResolution** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public DatePropertyDescriptor<T> NumericResolution(NumericResolutionUnit unit)
```

**public method Nest.DatePropertyDescriptor&lt;T&gt;.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public DatePropertyDescriptor<T> PrecisionStep(int precisionStep)
```

**public method Nest.ElasticClient.GetAliases** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated since 1.0.0, will be removed in 5.0.0. Use GetAlias which accepts multiple aliases and indices")]
public IGetAliasesResponse GetAliases(IGetAliasesRequest request)
```

**public method Nest.ElasticClient.GetAliases** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated since 1.0.0, will be removed in 5.0.0. Use GetAlias which accepts multiple aliases and indices")]
public IGetAliasesResponse GetAliases(Func<GetAliasesDescriptor, IGetAliasesRequest> selector)
```

**public method Nest.ElasticClient.GetAliasesAsync** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated since 1.0.0, will be removed in 5.0.0. Use GetAlias which accepts multiple aliases and indices")]
public Task<IGetAliasesResponse> GetAliasesAsync(IGetAliasesRequest request)
```

**public method Nest.ElasticClient.GetAliasesAsync** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated since 1.0.0, will be removed in 5.0.0. Use GetAlias which accepts multiple aliases and indices")]
public Task<IGetAliasesResponse> GetAliasesAsync(Func<GetAliasesDescriptor, IGetAliasesRequest> selector)
```

**public property Nest.ElasticsearchPropertyAttributeBase.CustomSimilarity** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("This is a temporary binary backwards compatible fix to allow named similarities in 2.0.0. Removed in 5.0.0")]
public string CustomSimilarity { get; set; }
```

**public property Nest.ElasticsearchPropertyAttributeBase.IndexName** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 2.0.0. Use CopyTo instead.")]
public string IndexName { get; set; }
```

**public property Nest.EmailAction.AttachData** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in Watcher 2.3. Use Attachments to set Attachment data")]
public Union<bool?, AttachData> AttachData { get; set; }
```

**public method Nest.EmailActionDescriptor.AttachData** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in Watcher 2.3. Use Attachments to set Attachment data")]
public EmailActionDescriptor AttachData(DataAttachmentFormat format)
```

**public method Nest.EmailActionDescriptor.AttachData** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in Watcher 2.3. Use Attachments to set Attachment data")]
public EmailActionDescriptor AttachData(bool attach = True)
```

**public property Nest.GeoPointAttribute.GeoHash** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public bool GeoHash { get; set; }
```

**public property Nest.GeoPointAttribute.GeoHashPrecision** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public int GeoHashPrecision { get; set; }
```

**public property Nest.GeoPointAttribute.GeoHashPrefix** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public bool GeoHashPrefix { get; set; }
```

**public property Nest.GeoPointAttribute.LatLon** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.3.0 and Removed in 5.0.0")]
public bool LatLon { get; set; }
```

**public property Nest.GeoPointAttribute.Normalize** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public bool Normalize { get; set; }
```

**public property Nest.GeoPointAttribute.NormalizeLatitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public bool NormalizeLatitude { get; set; }
```

**public property Nest.GeoPointAttribute.NormalizeLongitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public bool NormalizeLongitude { get; set; }
```

**public property Nest.GeoPointAttribute.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public int PrecisionStep { get; set; }
```

**public property Nest.GeoPointAttribute.Validate** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool Validate { get; set; }
```

**public property Nest.GeoPointAttribute.ValidateLatitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool ValidateLatitude { get; set; }
```

**public property Nest.GeoPointAttribute.ValidateLongitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool ValidateLongitude { get; set; }
```

**public property Nest.GeoPointProperty.Fielddata** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public IGeoPointFielddata Fielddata { get; set; }
```

**public property Nest.GeoPointProperty.GeoHash** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public bool? GeoHash { get; set; }
```

**public property Nest.GeoPointProperty.GeoHashPrecision** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public int? GeoHashPrecision { get; set; }
```

**public property Nest.GeoPointProperty.GeoHashPrefix** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public bool? GeoHashPrefix { get; set; }
```

**public property Nest.GeoPointProperty.LatLon** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.3.0 and Removed in 5.0.0")]
public bool? LatLon { get; set; }
```

**public property Nest.GeoPointProperty.Normalize** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public bool? Normalize { get; set; }
```

**public property Nest.GeoPointProperty.NormalizeLatitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public bool? NormalizeLatitude { get; set; }
```

**public property Nest.GeoPointProperty.NormalizeLongitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public bool? NormalizeLongitude { get; set; }
```

**public property Nest.GeoPointProperty.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public int? PrecisionStep { get; set; }
```

**public property Nest.GeoPointProperty.Validate** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool? Validate { get; set; }
```

**public property Nest.GeoPointProperty.ValidateLatitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool? ValidateLatitude { get; set; }
```

**public property Nest.GeoPointProperty.ValidateLongitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool? ValidateLongitude { get; set; }
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.Fielddata** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> Fielddata(Func<GeoPointFielddataDescriptor, IGeoPointFielddata> selector)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.GeoHash** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> GeoHash(bool geoHash = True)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.GeoHashPrecision** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> GeoHashPrecision(int geoHashPrecision)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.GeoHashPrefix** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> GeoHashPrefix(bool geoHashPrefix = True)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.LatLon** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.3.0 and Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> LatLon(bool latLon = True)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.Normalize** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> Normalize(bool normalize = True)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.NormalizeLatitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> NormalizeLatitude(bool normalizeLatitude = True)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.NormalizeLongitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> NormalizeLongitude(bool normalizeLongitude = True)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public GeoPointPropertyDescriptor<T> PrecisionStep(int precisionStep)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.Validate** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public GeoPointPropertyDescriptor<T> Validate(bool validate = True)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.ValidateLatitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public GeoPointPropertyDescriptor<T> ValidateLatitude(bool validateLongitude = True)
```

**public method Nest.GeoPointPropertyDescriptor&lt;T&gt;.ValidateLongitude** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public GeoPointPropertyDescriptor<T> ValidateLongitude(bool validateLatitude = True)
```

**public method Nest.GetAliasDescriptor.Alias** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Use Name instead")]
public GetAliasDescriptor Alias(string alias)
```

**public property Nest.GetAliasRequest.Alias** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Use the GetAliasRequest(Names name) constructor instead")]
public string Alias { get; set; }
```

**public property Nest.HighlightField.BoundaryMaxSize** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use BoundaryMaxScan")]
public int? BoundaryMaxSize { get; set; }
```

**public method Nest.HighlightFieldDescriptor&lt;T&gt;.BoundaryMaxSize** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use BoundaryMaxScan(int? boundaryMaxScan).")]
public HighlightFieldDescriptor<T> BoundaryMaxSize(int? boundaryMaxSize)
```

**public property Nest.IAnalyzeRequest.CharFilters** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0. Removed in 5.0.0. Use CharFilter instead")]
[JsonIgnoreAttribute]
public String[] CharFilters { get; set; }
```

**public property Nest.IAnalyzeRequest.Filters** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.4.0. Removed in 5.0.0. Use Filter instead")]
[JsonIgnoreAttribute]
public String[] Filters { get; set; }
```

**public interface Nest.ICatNodeattrsRequest** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0.  Use ICatNodeAttributesRequest instead.")]
[JsonObjectAttribute]
public interface ICatNodeattrsRequest : IRequest<CatNodeattrsRequestParameters>, IRequest
```

**public property Nest.IDateHistogramAggregation.Factor** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major version")]
[JsonPropertyAttribute("factor")]
public int? Factor { get; set; }
```

**public property Nest.IDateProperty.NumericResolution** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("numeric_resolution")]
[ObsoleteAttribute("Removed in 5.0.0")]
public Nullable<NumericResolutionUnit> NumericResolution { get; set; }
```

**public property Nest.IDateProperty.PrecisionStep** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("precision_step")]
[ObsoleteAttribute("Removed in 5.0.0")]
public int? PrecisionStep { get; set; }
```

**public method Nest.IElasticClient.GetAliases** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated since 1.0.0, will be removed in 5.0.0. Use GetAlias which accepts multiple aliases and indices")]
public IGetAliasesResponse GetAliases(IGetAliasesRequest request)
```

**public method Nest.IElasticClient.GetAliases** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated since 1.0.0, will be removed in 5.0.0. Use GetAlias which accepts multiple aliases and indices")]
public IGetAliasesResponse GetAliases(Func<GetAliasesDescriptor, IGetAliasesRequest> selector)
```

**public method Nest.IElasticClient.GetAliasesAsync** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated since 1.0.0, will be removed in 5.0.0. Use GetAlias which accepts multiple aliases and indices")]
public Task<IGetAliasesResponse> GetAliasesAsync(IGetAliasesRequest request)
```

**public method Nest.IElasticClient.GetAliasesAsync** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated since 1.0.0, will be removed in 5.0.0. Use GetAlias which accepts multiple aliases and indices")]
public Task<IGetAliasesResponse> GetAliasesAsync(Func<GetAliasesDescriptor, IGetAliasesRequest> selector)
```

**public property Nest.IEmailAction.AttachData** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("attach_data")]
[ObsoleteAttribute("Deprecated in Watcher 2.3. Use Attachments to set Attachment data")]
public Union<bool?, AttachData> AttachData { get; set; }
```

**public property Nest.IGeoPointProperty.Fielddata** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("fielddata")]
[ObsoleteAttribute("Removed in 5.0.0")]
public IGeoPointFielddata Fielddata { get; set; }
```

**public property Nest.IGeoPointProperty.GeoHash** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("geohash")]
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public bool? GeoHash { get; set; }
```

**public property Nest.IGeoPointProperty.GeoHashPrecision** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("geohash_precision")]
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public int? GeoHashPrecision { get; set; }
```

**public property Nest.IGeoPointProperty.GeoHashPrefix** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("geohash_prefix")]
[ObsoleteAttribute("Deprecated in 2.4.0 and Removed in 5.0.0")]
public bool? GeoHashPrefix { get; set; }
```

**public property Nest.IGeoPointProperty.LatLon** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("lat_lon")]
[ObsoleteAttribute("Deprecated in 2.3.0 and Removed in 5.0.0")]
public bool? LatLon { get; set; }
```

**public property Nest.IGeoPointProperty.Normalize** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("normalize")]
[ObsoleteAttribute("Removed in 5.0.0")]
public bool? Normalize { get; set; }
```

**public property Nest.IGeoPointProperty.NormalizeLatitude** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("normalize_lat")]
[ObsoleteAttribute("Removed in 5.0.0")]
public bool? NormalizeLatitude { get; set; }
```

**public property Nest.IGeoPointProperty.NormalizeLongitude** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("normalize_lon")]
[ObsoleteAttribute("Removed in 5.0.0")]
public bool? NormalizeLongitude { get; set; }
```

**public property Nest.IGeoPointProperty.PrecisionStep** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("precision_step")]
[ObsoleteAttribute("Removed in 5.0.0")]
public int? PrecisionStep { get; set; }
```

**public property Nest.IGeoPointProperty.Validate** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("validate")]
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool? Validate { get; set; }
```

**public property Nest.IGeoPointProperty.ValidateLatitude** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("validate_lat")]
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool? ValidateLatitude { get; set; }
```

**public property Nest.IGeoPointProperty.ValidateLongitude** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("validate_lon")]
[ObsoleteAttribute("Removed in 5.0.0. Use IgnoreMalformed")]
public bool? ValidateLongitude { get; set; }
```

**public property Nest.IGetAliasRequest.Alias** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Use Name instead")]
[JsonIgnoreAttribute]
public string Alias { get; set; }
```

**public property Nest.IHighlightField.BoundaryMaxSize** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use BoundaryMaxScan")]
public int? BoundaryMaxSize { get; set; }
```

**public property Nest.IHighlightField.CustomType** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("type")]
[ObsoleteAttribute("This is a temporary binary backwards compatible fix to make sure you can specify any custom highlighter type in 2.0.0. Removed in 5.0.0.")]
public string CustomType { get; set; }
```

**public property Nest.IIpProperty.PrecisionStep** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("precision_step")]
[ObsoleteAttribute("Removed in 5.0.0")]
public int? PrecisionStep { get; set; }
```

**public interface Nest.IMappingTransform** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.MappingTransform])]
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public interface IMappingTransform
```


**public property Nest.INumberProperty.PrecisionStep** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("precision_step")]
[ObsoleteAttribute("Removed in 5.0.0")]
public int? PrecisionStep { get; set; }
```

**public property Nest.IObjectProperty.Path** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("path")]
[ObsoleteAttribute("Deprecated in 1.0.0 and Removed in 5.0.0. Use CopyTo instead.")]
public string Path { get; set; }
```

**public property Nest.IpAttribute.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public int PrecisionStep { get; set; }
```

**public property Nest.IpProperty.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public int? PrecisionStep { get; set; }
```

**public method Nest.IpPropertyDescriptor&lt;T&gt;.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public IpPropertyDescriptor<T> PrecisionStep(int precisionStep)
```

**public property Nest.IProperty.CustomSimilarity** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("similarity")]
[ObsoleteAttribute("This is a temporary binary backwards compatible fix to allow named similarities in 2.0.0. Removed in 5.0.0")]
public string CustomSimilarity { get; set; }
```

**public property Nest.IProperty.IndexName** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("index_name")]
[ObsoleteAttribute("Removed in 2.0.0. Use CopyTo instead.")]
public string IndexName { get; set; }
```

**public property Nest.IStringProperty.PositionOffsetGap** *Removed (Breaking)*

```csharp
[JsonIgnoreAttribute]
[ObsoleteAttribute("Scheduled to be removed in 5.0.0. Use PositionIncrementGap instead.")]
public int? PositionOffsetGap { get; set; }
```

**public property Nest.ITermsQuery.DisableCoord** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use bool query instead")]
public bool? DisableCoord { get; set; }
```

**public property Nest.ITermsQuery.MinimumShouldMatch** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use bool query instead")]
public MinimumShouldMatch MinimumShouldMatch { get; set; }
```

**public property Nest.ITermVectorsResponse.Took** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
public int Took { get; }
```

**public interface Nest.ITimestampField** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.TimestampField])]
[ObsoleteAttribute("use a normal date field and set its value explicitly")]
public interface ITimestampField : IFieldMapping
```

**public interface Nest.ITtlField** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.TtlField])]
[ObsoleteAttribute("will be replaced with a different implementation in a future version of Elasticsearch")]
public interface ITtlField : IFieldMapping
```

**public property Nest.ITypeMapping.TimestampField** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("_timestamp")]
[ObsoleteAttribute("Use a normal date field and set its value explicitly")]
public ITimestampField TimestampField { get; set; }
```

**public property Nest.ITypeMapping.Transform** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("transform")]
[JsonConverterAttribute(Nest.MappingTransformCollectionJsonConverter)]
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public IList<IMappingTransform> Transform { get; set; }
```

**public property Nest.ITypeMapping.TtlField** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("_ttl")]
[ObsoleteAttribute("Will be replaced with a different implementation in a future version of Elasticsearch")]
public ITtlField TtlField { get; set; }
```

**public class Nest.MappingTransform** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public class MappingTransform : IMappingTransform
```

**public class Nest.MappingTransformDescriptor** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public class MappingTransformDescriptor : DescriptorBase<MappingTransformDescriptor, IMappingTransform>, IDescriptor, IMappingTransform
```

**public class Nest.MappingTransformsDescriptor** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public class MappingTransformsDescriptor : DescriptorPromiseBase<MappingTransformsDescriptor, IList<IMappingTransform>>, IDescriptor, IPromise<IList<IMappingTransform>>
```

**public property Nest.NumberAttribute.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public int PrecisionStep { get; set; }
```

**public property Nest.NumberProperty.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public int? PrecisionStep { get; set; }
```

**public method Nest.NumberPropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.PrecisionStep** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public TDescriptor PrecisionStep(int precisionStep)
```

**public property Nest.ObjectAttribute.Path** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 1.0.0 and Removed in 5.0.0. Use CopyTo instead.")]
public string Path { get; set; }
```

**public property Nest.ObjectProperty.Path** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 1.0.0 and Removed in 5.0.0. Use CopyTo instead.")]
public string Path { get; set; }
```

**public method Nest.ObjectPropertyDescriptorBase&lt;TDescriptor, TInterface, TParent, TChild&gt;.Path** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 1.0.0 and Removed in 5.0.0. Use CopyTo instead.")]
public TDescriptor Path(string path)
```

**public property Nest.PropertyBase.CustomSimilarity** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("This is a temporary binary backwards compatible fix to allow named similarities in 2.0.0. Removed in 5.0.0")]
public string CustomSimilarity { get; set; }
```

**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.IndexName** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 2.0.0. Use CopyTo instead.")]
public TDescriptor IndexName(string indexName)
```

**public method Nest.PutMappingDescriptor&lt;T&gt;.TimestampField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("use a normal date field and set its value explicitly")]
public PutMappingDescriptor<T> TimestampField(Func<TimestampFieldDescriptor<T>, ITimestampField> timestampFieldSelector)
```

**public method Nest.PutMappingDescriptor&lt;T&gt;.Transform** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public PutMappingDescriptor<T> Transform(Func<MappingTransformsDescriptor, IPromise<IList<IMappingTransform>>> selector)
```

**public method Nest.PutMappingDescriptor&lt;T&gt;.Transform** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public PutMappingDescriptor<T> Transform(IEnumerable<IMappingTransform> transforms)
```

**public method Nest.PutMappingDescriptor&lt;T&gt;.TtlField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Will be replaced with a different implementation in a future version of Elasticsearch")]
public PutMappingDescriptor<T> TtlField(Func<TtlFieldDescriptor, ITtlField> ttlFieldSelector)
```

**public property Nest.PutMappingRequest.TimestampField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("use a normal date field and set its value explicitly")]
public ITimestampField TimestampField { get; set; }
```

**public property Nest.PutMappingRequest.Transform** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public IList<IMappingTransform> Transform { get; set; }
```

**public property Nest.PutMappingRequest.TtlField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Will be replaced with a different implementation in a future version of Elasticsearch")]
public ITtlField TtlField { get; set; }
```

**public property Nest.PutMappingRequest&lt;T&gt;.TimestampField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("use a normal date field and set its value explicitly")]
public ITimestampField TimestampField { get; set; }
```

**public property Nest.PutMappingRequest&lt;T&gt;.Transform** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0. Removed in 5.0.0")]
public IList<IMappingTransform> Transform { get; set; }
```

**public property Nest.PutMappingRequest&lt;T&gt;.TtlField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Will be replaced with a different implementation in a future version of Elasticsearch")]
public ITtlField TtlField { get; set; }
```

**public method Nest.Query&lt;T&gt;.Strict** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0.  Setting Strict() at the container level is a noop and must be set on each individual query.")]
public static QueryContainerDescriptor<T> Strict(bool strict = True)
```

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Strict** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0.  Setting Strict() at the container level does is a noop and must be set on each individual query.")]
public QueryContainerDescriptor<T> Strict(bool strict = True)
```

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Verbatim** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0.  Setting Verbatim() at the container level is a noop and must be set on each individual query.")]
public QueryContainerDescriptor<T> Verbatim(bool verbatim = True)
```

**public method Nest.QueryStringQueryDescriptor&lt;T&gt;.Fuziness** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use Fuzziness(Fuzziness fuzziness)")]
public QueryStringQueryDescriptor<T> Fuziness(Fuzziness fuzziness)
```

**public property Nest.SearchResponse&lt;T&gt;.Highlights** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("This highlights by document id dictionary is the wrong abstraction in cases where a search can yield the same ids, for example, different types in the same index or a search across multiple indices. Removed in 5.0.0.")]
public HighlightDocumentDictionary Highlights { get; }
```

**public property Nest.StringAttribute.PositionOffsetGap** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0. Use PositionIncrementGap instead.")]
public int PositionOffsetGap { get; set; }
```

**public property Nest.StringProperty.PositionOffsetGap** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0. Use PositionIncrementGap instead.")]
public int? PositionOffsetGap { get; set; }
```

**public method Nest.StringPropertyDescriptor&lt;T&gt;.PositionOffsetGap** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0. Use PositionIncrementGap() instead.")]
public StringPropertyDescriptor<T> PositionOffsetGap(int positionOffsetGap)
```

**public method Nest.SuggestContextDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Field** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use Path() instead.")]
public TDescriptor Field(Expression<Func<T, object>> objectPath)
```

**public method Nest.SuggestContextDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Field** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use Path() instead.")]
public TDescriptor Field(Field field)
```

**public property Nest.TermsQuery.DisableCoord** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use bool query instead")]
public bool? DisableCoord { get; set; }
```

**public property Nest.TermsQuery.MinimumShouldMatch** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use bool query instead")]
public MinimumShouldMatch MinimumShouldMatch { get; set; }
```

**public method Nest.TermsQueryDescriptor&lt;T&gt;.DisableCoord** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use bool query instead")]
public TermsQueryDescriptor<T> DisableCoord(bool? disable = True)
```

**public method Nest.TermsQueryDescriptor&lt;T&gt;.MinimumShouldMatch** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use bool query instead")]
public TermsQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minMatch)
```

**public class Nest.TimestampField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("use a normal date field and set its value explicitly")]
public class TimestampField : ITimestampField, IFieldMapping
```

**public class Nest.TimestampFieldDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("use a normal date field and set its value explicitly")]
public class TimestampFieldDescriptor<T> : DescriptorBase<TimestampFieldDescriptor<T>, ITimestampField>, IDescriptor, ITimestampField, IFieldMapping
```

**public class Nest.TtlField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("will be replaced with a different implementation in a future version of Elasticsearch")]
public class TtlField : ITtlField, IFieldMapping
```

**public class Nest.TtlFieldDescriptor** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("will be replaced with a different implementation in a future version of Elasticsearch")]
public class TtlFieldDescriptor : DescriptorBase<TtlFieldDescriptor, ITtlField>, IDescriptor, ITtlField, IFieldMapping
```

**public property Nest.TypeMapping.TimestampField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use a normal date field and set its value explicitly")]
public ITimestampField TimestampField { get; set; }
```

**public property Nest.TypeMapping.Transform** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public IList<IMappingTransform> Transform { get; set; }
```

**public property Nest.TypeMapping.TtlField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Will be replaced with a different implementation in a future version of Elasticsearch")]
public ITtlField TtlField { get; set; }
```

**public method Nest.TypeMappingDescriptor&lt;T&gt;.TimestampField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use a normal date field and set its value explicitly")]
public TypeMappingDescriptor<T> TimestampField(Func<TimestampFieldDescriptor<T>, ITimestampField> timestampFieldSelector)
```

**public method Nest.TypeMappingDescriptor&lt;T&gt;.Transform** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public TypeMappingDescriptor<T> Transform(Func<MappingTransformsDescriptor, IPromise<IList<IMappingTransform>>> selector)
```

**public method Nest.TypeMappingDescriptor&lt;T&gt;.Transform** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public TypeMappingDescriptor<T> Transform(IEnumerable<IMappingTransform> transforms)
```

**public method Nest.TypeMappingDescriptor&lt;T&gt;.TtlField** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Will be replaced with a different implementation in a future version of Elasticsearch")]
public TypeMappingDescriptor<T> TtlField(Func<TtlFieldDescriptor, ITtlField> ttlFieldSelector)
```

#ReadOnlyTypeChange

##Now IReadOnlyDictionary
**public property Nest.ActivationStatus.Actions**  
**public property Nest.AggregationsHelper.Aggregations**  
**public property Nest.AuthenticateResponse.Metadata**  
**public property Nest.BucketAggregate.Meta**  
**public property Nest.BucketAggregateBase.Meta**  
**public property Nest.ClearCachedRealmsResponse.Nodes**  
**public property Nest.ClearCachedRolesResponse.Nodes**  
**public property Nest.ClusterGetSettingsResponse.Persistent**  
**public property Nest.ClusterGetSettingsResponse.Transient**  
**public property Nest.ClusterHealthResponse.Indices**  
**public property Nest.ClusterPutSettingsResponse.Persistent**  
**public property Nest.ClusterPutSettingsResponse.Transient**  
**public property Nest.ClusterRerouteState.Nodes**  
**public property Nest.ClusterStateResponse.Nodes**  
**public property Nest.ExecutionResultInput.Payload**  
**public property Nest.FieldMapping.Mapping**  
**public property Nest.FieldStats.Fields**  
**public property Nest.FieldStatsResponse.Indices**  
**public property Nest.GetFieldMappingResponse.Indices**  
**public property Nest.GetIndexResponse.Indices**  
**public property Nest.GetIndexSettingsResponse.Indices**  
**public property Nest.GetIndexTemplateResponse.TemplateMappings**  
**public property Nest.GetMappingResponse.Mappings**  
**public property Nest.GetRepositoryResponse.Repositories**  
**public property Nest.GetRoleResponse.Roles**  
**public property Nest.GetUserResponse.Users**  
**public property Nest.Hit&lt;T&gt;.InnerHits**  
**public property Nest.IAggregate.Meta**  
**public property Nest.IAuthenticateResponse.Metadata**  
**public property Nest.IClearCachedRealmsResponse.Nodes**  
**public property Nest.IClearCachedRolesResponse.Nodes**  
**public property Nest.IClusterGetSettingsResponse.Persistent**  
**public property Nest.IClusterGetSettingsResponse.Transient**  
**public property Nest.IClusterHealthResponse.Indices**  
**public property Nest.IClusterPutSettingsResponse.Persistent**  
**public property Nest.IClusterPutSettingsResponse.Transient**  
**public property Nest.IClusterStateResponse.Nodes**  
**public property Nest.IDictionaryResponse&lt;TKey, TValue&gt;.BackingDictionary**  
**public property Nest.IFieldStatsResponse.Indices**  
**public property Nest.IGetFieldMappingResponse.Indices**  
**public property Nest.IGetIndexResponse.Indices**  
**public property Nest.IGetIndexSettingsResponse.Indices**  
**public property Nest.IGetIndexTemplateResponse.TemplateMappings**  
**public property Nest.IGetMappingResponse.Mappings**  
**public property Nest.IGetRepositoryResponse.Repositories**  
**public property Nest.IGetRoleResponse.Roles**  
**public property Nest.IGetUserResponse.Users**  
**public property Nest.IHit&lt;T&gt;.InnerHits**  
**public property Nest.IIndicesShardStoresResponse.Indices**  
**public property Nest.IIndicesStatsResponse.Indices**  
**public property Nest.IndexHealthStats.Shards**  
**public property Nest.IndexingStats.Types**  
**public property Nest.IndexRoutingTable.Shards**  
**public property Nest.IndexSegment.Shards**  
**public property Nest.IndicesShardStores.Shards**  
**public property Nest.IndicesShardStoresResponse.Indices**  
**public property Nest.IndicesStatsResponse.Indices**  
**public property Nest.INodesInfoResponse.Nodes**  
**public property Nest.INodesStatsResponse.Nodes**  
**public property Nest.IRecoveryStatusResponse.Indices**  
**public property Nest.IReindexRethrottleResponse.Nodes**  
**public property Nest.ISearchResponse&lt;T&gt;.Aggregations**  
**public property Nest.ISearchResponse&lt;T&gt;.Suggest**  
**public property Nest.ISearchShardsResponse.Nodes**  
**public property Nest.ISegmentsResponse.Indices**  
**public property Nest.IUpgradeStatusResponse.Upgrades**  
**public property Nest.IVerifyRepositoryResponse.Nodes**  
**public property Nest.MetadataState.Indices**  
**public property Nest.MetadataState.Templates**  
**public property Nest.MetricAggregateBase.Meta**  
**public property Nest.NodesInfoResponse.Nodes**  
**public property Nest.NodesStatsResponse.Nodes**  
**public property Nest.PercolatorMatch.Highlight**  
**public property Nest.RecoveryStatusResponse.Indices**  
**public property Nest.ReindexNode.Attributes**  
**public property Nest.ReindexNode.Tasks**  
**public property Nest.ReindexRethrottleResponse.Nodes**  
**public property Nest.RoutingNodesState.Nodes**  
**public property Nest.RoutingTableState.Indices**  
**public property Nest.SearchResponse&lt;T&gt;.Aggregations**  
**public property Nest.SearchResponse&lt;T&gt;.Suggest**  
**public property Nest.SearchShardsResponse.Nodes**  
**public property Nest.SegmentsResponse.Indices**  
**public property Nest.ShardsSegment.Segments**  
**public property Nest.ShardStore.Attributes**  
**public property Nest.SnapshotIndexStats.Shards**  
**public property Nest.SnapshotStatus.Indices**  
**public property Nest.TaskExecutingNode.Tasks**  
**public property Nest.TermVector.Terms**  
**public property Nest.TermVectorsResponse.TermVectors**  
**public property Nest.TypeFieldMappings.Mappings**  
**public property Nest.UpgradeStatusResponse.Upgrades**  
**public property Nest.VerifyRepositoryResponse.Nodes**  
**public property Nest.Watch.Meta**  
**public property Nest.WatchRecord.Metadata**  
**public property Nest.WatchStatus.Actions**  


##Now IReadOnlyCollection
**public property Nest.AnalyzeResponse.Tokens**  
**public property Nest.AuthenticateResponse.Roles**  
**public property Nest.BucketAggregate.Items**  
**public property Nest.BulkResponse.Items**  
**public property Nest.CatResponse&lt;TCatRecord&gt;.Records**  
**public property Nest.ClusterJvm.Versions**  
**public property Nest.ClusterNodesStats.Plugins**  
**public property Nest.ClusterNodesStats.Versions**  
**public property Nest.ClusterOperatingSystemStats.Names**  
**public property Nest.ClusterPendingTasksResponse.Tasks**  
**public property Nest.ClusterRerouteResponse.Explanations**  
**public property Nest.Collector.Children**  
**public property Nest.ExecutionResult.Actions**  
**public property Nest.Explanation.Details**  
**public property Nest.ExplanationDetail.Details**  
**public property Nest.GetSnapshotResponse.Snapshots**  
**public property Nest.GraphExploreResponse.Connections**  
**public property Nest.GraphExploreResponse.Failures**  
**public property Nest.GraphExploreResponse.Vertices**  
**public property Nest.HighlightHit.Highlights**  
**public property Nest.Hit&lt;T&gt;.MatchedQueries**  
**public property Nest.Hit&lt;T&gt;.Sorts**  
**public property Nest.HitsMetaData&lt;T&gt;.Hits**  
**public property Nest.HotThreadInformation.Hosts**  
**public property Nest.HotThreadInformation.Threads**  
**public property Nest.IAnalyzeResponse.Tokens**  
**public property Nest.IAuthenticateResponse.Roles**  
**public property Nest.IBulkResponse.Items**  
**public property Nest.ICatResponse&lt;TCatRecord&gt;.Records**  
**public property Nest.IClusterPendingTasksResponse.Tasks**  
**public property Nest.IClusterRerouteResponse.Explanations**  
**public property Nest.IGetSnapshotResponse.Snapshots**  
**public property Nest.IGraphExploreResponse.Connections**  
**public property Nest.IGraphExploreResponse.Failures**  
**public property Nest.IGraphExploreResponse.Vertices**  
**public property Nest.IHit&lt;T&gt;.MatchedQueries**  
**public property Nest.IHit&lt;T&gt;.Sorts**  
**public property Nest.IMultiGetResponse.Documents**  
**public property Nest.IMultiTermVectorsResponse.Documents**  
**public property Nest.INodesHotThreadsResponse.HotThreads**  
**public property Nest.IPercolateResponse.Matches**  
**public property Nest.IReindexOnServerResponse.Failures**  
**public property Nest.ISearchResponse&lt;T&gt;.Documents**  
**public property Nest.ISearchResponse&lt;T&gt;.Fields**  
**public property Nest.ISearchResponse&lt;T&gt;.Hits**  
**public property Nest.ISearchShardsResponse.Shards**  
**public property Nest.ISnapshotStatusResponse.Snapshots**  
**public property Nest.IUpdateByQueryResponse.Failures**  
**public property Nest.IValidateQueryResponse.Explanations**  
**public property Nest.IWatcherStatsResponse.CurrentWatches**  
**public property Nest.IWatcherStatsResponse.QueuedWatches**  
**public property Nest.LicenseAcknowledgement.License**  
**public property Nest.MultiBucketAggregate&lt;TBucket&gt;.Buckets**  
**public property Nest.MultiGetResponse.Documents**  
**public property Nest.MultiTermVectorsResponse.Documents**  
**public property Nest.NodesHotThreadsResponse.HotThreads**  
**public property Nest.PercolateResponse.Matches**  
**public property Nest.Profile.Shards**  
**public property Nest.RecoveryStatus.Shards**  
**public property Nest.ReindexOnServerResponse.Failures**  
**public property Nest.RoutingNodesState.Unassigned**  
**public property Nest.SearchProfile.Collector**  
**public property Nest.SearchProfile.Query**  
**public property Nest.SearchResponse&lt;T&gt;.Documents**  
**public property Nest.SearchResponse&lt;T&gt;.Fields**  
**public property Nest.SearchResponse&lt;T&gt;.Hits**  
**public property Nest.SearchShardsResponse.Shards**  
**public property Nest.ShardProfile.Searches**  
**public property Nest.ShardsMetaData.Failures**  
**public property Nest.ShardStoreWrapper.Stores**  
**public property Nest.Snapshot.Failures**  
**public property Nest.Snapshot.Indices**  
**public property Nest.SnapshotRestore.Indices**  
**public property Nest.SnapshotStatusResponse.Snapshots**  
**public property Nest.TermVectorTerm.Tokens**  
**public method Nest.TopHitsAggregate.Documents&lt;T&gt;**  
**public method Nest.TopHitsAggregate.Hits&lt;T&gt;**  
**public property Nest.UpdateByQueryResponse.Failures**  
**public property Nest.ValidateQueryResponse.Explanations**  
**public property Nest.WatcherStatsResponse.CurrentWatches**  
**public property Nest.WatcherStatsResponse.QueuedWatches**  
**public property Nest.WatchRecord.Messages**  

#CancellationToken
**Nest.BulkAllObservable&lt;T&gt;..ctor**  
**Nest.DeleteManyExtensions.DeleteManyAsync&lt;T&gt;**  
**Nest.ElasticClient.AcknowledgeWatchAsync**  
**Nest.ElasticClient.AcknowledgeWatchAsync**  
**Nest.ElasticClient.ActivateWatchAsync**  
**Nest.ElasticClient.ActivateWatchAsync**  
**Nest.ElasticClient.AliasAsync**  
**Nest.ElasticClient.AliasAsync**  
**Nest.ElasticClient.AliasExistsAsync**  
**Nest.ElasticClient.AliasExistsAsync**  
**Nest.ElasticClient.AnalyzeAsync**  
**Nest.ElasticClient.AnalyzeAsync**  
**Nest.ElasticClient.AuthenticateAsync**  
**Nest.ElasticClient.AuthenticateAsync**  
**Nest.ElasticClient.BulkAsync**  
**Nest.ElasticClient.BulkAsync**  
**Nest.ElasticClient.CatAliasesAsync**  
**Nest.ElasticClient.CatAliasesAsync**  
**Nest.ElasticClient.CatAllocationAsync**  
**Nest.ElasticClient.CatAllocationAsync**  
**Nest.ElasticClient.CatCountAsync**  
**Nest.ElasticClient.CatCountAsync**  
**Nest.ElasticClient.CatFielddataAsync**  
**Nest.ElasticClient.CatFielddataAsync**  
**Nest.ElasticClient.CatHealthAsync**  
**Nest.ElasticClient.CatHealthAsync**  
**Nest.ElasticClient.CatHelpAsync**  
**Nest.ElasticClient.CatHelpAsync**  
**Nest.ElasticClient.CatIndicesAsync**  
**Nest.ElasticClient.CatIndicesAsync**  
**Nest.ElasticClient.CatMasterAsync**  
**Nest.ElasticClient.CatMasterAsync**  
**Nest.ElasticClient.CatNodeAttributesAsync**  
**Nest.ElasticClient.CatNodeAttributesAsync**  
**Nest.ElasticClient.CatNodesAsync**  
**Nest.ElasticClient.CatNodesAsync**  
**Nest.ElasticClient.CatPendingTasksAsync**  
**Nest.ElasticClient.CatPendingTasksAsync**  
**Nest.ElasticClient.CatPluginsAsync**  
**Nest.ElasticClient.CatPluginsAsync**  
**Nest.ElasticClient.CatRecoveryAsync**  
**Nest.ElasticClient.CatRecoveryAsync**  
**Nest.ElasticClient.CatRepositoriesAsync**  
**Nest.ElasticClient.CatRepositoriesAsync**  
**Nest.ElasticClient.CatSegmentsAsync**  
**Nest.ElasticClient.CatSegmentsAsync**  
**Nest.ElasticClient.CatShardsAsync**  
**Nest.ElasticClient.CatShardsAsync**  
**Nest.ElasticClient.CatSnapshotsAsync**  
**Nest.ElasticClient.CatSnapshotsAsync**  
**Nest.ElasticClient.CatThreadPoolAsync**  
**Nest.ElasticClient.CatThreadPoolAsync**  
**Nest.ElasticClient.ClearCacheAsync**  
**Nest.ElasticClient.ClearCacheAsync**  
**Nest.ElasticClient.ClearCachedRealmsAsync**  
**Nest.ElasticClient.ClearCachedRealmsAsync**  
**Nest.ElasticClient.ClearCachedRolesAsync**  
**Nest.ElasticClient.ClearCachedRolesAsync**  
**Nest.ElasticClient.ClearScrollAsync**  
**Nest.ElasticClient.ClearScrollAsync**  
**Nest.ElasticClient.CloseIndexAsync**  
**Nest.ElasticClient.CloseIndexAsync**  
**Nest.ElasticClient.ClusterGetSettingsAsync**  
**Nest.ElasticClient.ClusterGetSettingsAsync**  
**Nest.ElasticClient.ClusterHealthAsync**  
**Nest.ElasticClient.ClusterHealthAsync**  
**Nest.ElasticClient.ClusterPendingTasksAsync**  
**Nest.ElasticClient.ClusterPendingTasksAsync**  
**Nest.ElasticClient.ClusterPutSettingsAsync**  
**Nest.ElasticClient.ClusterPutSettingsAsync**  
**Nest.ElasticClient.ClusterRerouteAsync**  
**Nest.ElasticClient.ClusterRerouteAsync**  
**Nest.ElasticClient.ClusterStateAsync**  
**Nest.ElasticClient.ClusterStateAsync**  
**Nest.ElasticClient.ClusterStatsAsync**  
**Nest.ElasticClient.ClusterStatsAsync**  
**Nest.ElasticClient.CountAsync&lt;T&gt;**  
**Nest.ElasticClient.CountAsync&lt;T&gt;**  
**Nest.ElasticClient.CreateIndexAsync**  
**Nest.ElasticClient.CreateIndexAsync**  
**Nest.ElasticClient.CreateRepositoryAsync**  
**Nest.ElasticClient.CreateRepositoryAsync**  
**Nest.ElasticClient.DeactivateWatchAsync**  
**Nest.ElasticClient.DeactivateWatchAsync**  
**Nest.ElasticClient.DeleteAliasAsync**  
**Nest.ElasticClient.DeleteAliasAsync**  
**Nest.ElasticClient.DeleteAsync**  
**Nest.ElasticClient.DeleteAsync&lt;T&gt;**  
**Nest.ElasticClient.DeleteByQueryAsync**  
**Nest.ElasticClient.DeleteByQueryAsync&lt;T&gt;**  
**Nest.ElasticClient.DeleteIndexAsync**  
**Nest.ElasticClient.DeleteIndexAsync**  
**Nest.ElasticClient.DeleteIndexTemplateAsync**  
**Nest.ElasticClient.DeleteIndexTemplateAsync**  
**Nest.ElasticClient.DeleteLicenseAsync**  
**Nest.ElasticClient.DeleteLicenseAsync**  
**Nest.ElasticClient.DeleteRepositoryAsync**  
**Nest.ElasticClient.DeleteRepositoryAsync**  
**Nest.ElasticClient.DeleteRoleAsync**  
**Nest.ElasticClient.DeleteRoleAsync**  
**Nest.ElasticClient.DeleteScriptAsync**  
**Nest.ElasticClient.DeleteScriptAsync**  
**Nest.ElasticClient.DeleteSearchTemplateAsync**  
**Nest.ElasticClient.DeleteSearchTemplateAsync**  
**Nest.ElasticClient.DeleteSnapshotAsync**  
**Nest.ElasticClient.DeleteSnapshotAsync**  
**Nest.ElasticClient.DeleteUserAsync**  
**Nest.ElasticClient.DeleteUserAsync**  
**Nest.ElasticClient.DeleteWatchAsync**  
**Nest.ElasticClient.DeleteWatchAsync**  
**Nest.ElasticClient.DocumentExistsAsync**  
**Nest.ElasticClient.DocumentExistsAsync&lt;T&gt;**  
**Nest.ElasticClient.ExecuteWatchAsync**  
**Nest.ElasticClient.ExecuteWatchAsync**  
**Nest.ElasticClient.ExplainAsync&lt;T&gt;**  
**Nest.ElasticClient.ExplainAsync&lt;T&gt;**  
**Nest.ElasticClient.FieldStatsAsync**  
**Nest.ElasticClient.FieldStatsAsync**  
**Nest.ElasticClient.FlushAsync**  
**Nest.ElasticClient.FlushAsync**  
**Nest.ElasticClient.ForceMergeAsync**  
**Nest.ElasticClient.ForceMergeAsync**  
**Nest.ElasticClient.GetAliasAsync**  
**Nest.ElasticClient.GetAliasAsync**  
**Nest.ElasticClient.GetAsync&lt;T&gt;**  
**Nest.ElasticClient.GetAsync&lt;T&gt;**  
**Nest.ElasticClient.GetFieldMappingAsync**  
**Nest.ElasticClient.GetFieldMappingAsync&lt;T&gt;**  
**Nest.ElasticClient.GetIndexAsync**  
**Nest.ElasticClient.GetIndexAsync**  
**Nest.ElasticClient.GetIndexSettingsAsync**  
**Nest.ElasticClient.GetIndexSettingsAsync**  
**Nest.ElasticClient.GetIndexTemplateAsync**  
**Nest.ElasticClient.GetIndexTemplateAsync**  
**Nest.ElasticClient.GetLicenseAsync**  
**Nest.ElasticClient.GetLicenseAsync**  
**Nest.ElasticClient.GetMappingAsync**  
**Nest.ElasticClient.GetMappingAsync&lt;T&gt;**  
**Nest.ElasticClient.GetRepositoryAsync**  
**Nest.ElasticClient.GetRepositoryAsync**  
**Nest.ElasticClient.GetRoleAsync**  
**Nest.ElasticClient.GetRoleAsync**  
**Nest.ElasticClient.GetScriptAsync**  
**Nest.ElasticClient.GetScriptAsync**  
**Nest.ElasticClient.GetSearchTemplateAsync**  
**Nest.ElasticClient.GetSearchTemplateAsync**  
**Nest.ElasticClient.GetSnapshotAsync**  
**Nest.ElasticClient.GetSnapshotAsync**  
**Nest.ElasticClient.GetUserAsync**  
**Nest.ElasticClient.GetUserAsync**  
**Nest.ElasticClient.GetWatchAsync**  
**Nest.ElasticClient.GetWatchAsync**  
**Nest.ElasticClient.GraphExploreAsync**  
**Nest.ElasticClient.GraphExploreAsync&lt;T&gt;**  
**Nest.ElasticClient.IndexAsync**  
**Nest.ElasticClient.IndexAsync&lt;T&gt;**  
**Nest.ElasticClient.IndexExistsAsync**  
**Nest.ElasticClient.IndexExistsAsync**  
**Nest.ElasticClient.IndexTemplateExistsAsync**  
**Nest.ElasticClient.IndexTemplateExistsAsync**  
**Nest.ElasticClient.IndicesShardStoresAsync**  
**Nest.ElasticClient.IndicesShardStoresAsync**  
**Nest.ElasticClient.IndicesStatsAsync**  
**Nest.ElasticClient.IndicesStatsAsync**  
**Nest.ElasticClient.MapAsync**  
**Nest.ElasticClient.MapAsync&lt;T&gt;**  
**Nest.ElasticClient.MultiGetAsync**  
**Nest.ElasticClient.MultiGetAsync**  
**Nest.ElasticClient.MultiPercolateAsync**  
**Nest.ElasticClient.MultiPercolateAsync**  
**Nest.ElasticClient.MultiSearchAsync**  
**Nest.ElasticClient.MultiSearchAsync**  
**Nest.ElasticClient.MultiTermVectorsAsync**  
**Nest.ElasticClient.MultiTermVectorsAsync**  
**Nest.ElasticClient.NodesHotThreadsAsync**  
**Nest.ElasticClient.NodesHotThreadsAsync**  
**Nest.ElasticClient.NodesInfoAsync**  
**Nest.ElasticClient.NodesInfoAsync**  
**Nest.ElasticClient.NodesStatsAsync**  
**Nest.ElasticClient.NodesStatsAsync**  
**Nest.ElasticClient.OpenIndexAsync**  
**Nest.ElasticClient.OpenIndexAsync**  
**Nest.ElasticClient.PercolateAsync&lt;T&gt;**  
**Nest.ElasticClient.PercolateAsync&lt;T&gt;**  
**Nest.ElasticClient.PercolateCountAsync&lt;T&gt;**  
**Nest.ElasticClient.PercolateCountAsync&lt;T&gt;**  
**Nest.ElasticClient.PingAsync**  
**Nest.ElasticClient.PingAsync**  
**Nest.ElasticClient.PostLicenseAsync**  
**Nest.ElasticClient.PostLicenseAsync**  
**Nest.ElasticClient.PutAliasAsync**  
**Nest.ElasticClient.PutAliasAsync**  
**Nest.ElasticClient.PutIndexTemplateAsync**  
**Nest.ElasticClient.PutIndexTemplateAsync**  
**Nest.ElasticClient.PutRoleAsync**  
**Nest.ElasticClient.PutRoleAsync**  
**Nest.ElasticClient.PutScriptAsync**  
**Nest.ElasticClient.PutScriptAsync**  
**Nest.ElasticClient.PutSearchTemplateAsync**  
**Nest.ElasticClient.PutSearchTemplateAsync**  
**Nest.ElasticClient.PutUserAsync**  
**Nest.ElasticClient.PutUserAsync**  
**Nest.ElasticClient.PutWatchAsync**  
**Nest.ElasticClient.PutWatchAsync**  
**Nest.ElasticClient.RecoveryStatusAsync**  
**Nest.ElasticClient.RecoveryStatusAsync**  
**Nest.ElasticClient.RefreshAsync**  
**Nest.ElasticClient.RefreshAsync**  
**Nest.ElasticClient.RegisterPercolatorAsync**  
**Nest.ElasticClient.RegisterPercolatorAsync&lt;T&gt;**  
**Nest.ElasticClient.Reindex&lt;T&gt;**  
**Nest.ElasticClient.Reindex&lt;T&gt;**  
**Nest.ElasticClient.ReindexOnServerAsync**  
**Nest.ElasticClient.ReindexOnServerAsync**  
**Nest.ElasticClient.RenderSearchTemplateAsync**  
**Nest.ElasticClient.RenderSearchTemplateAsync**  
**Nest.ElasticClient.RestartWatcherAsync**  
**Nest.ElasticClient.RestartWatcherAsync**  
**Nest.ElasticClient.RestoreAsync**  
**Nest.ElasticClient.RestoreAsync**  
**Nest.ElasticClient.RethrottleAsync**  
**Nest.ElasticClient.RethrottleAsync**  
**Nest.ElasticClient.RootNodeInfoAsync**  
**Nest.ElasticClient.RootNodeInfoAsync**  
**Nest.ElasticClient.ScrollAsync&lt;T&gt;**  
**Nest.ElasticClient.ScrollAsync&lt;T&gt;**  
**Nest.ElasticClient.SearchAsync&lt;T, TResult&gt;**  
**Nest.ElasticClient.SearchAsync&lt;T, TResult&gt;**  
**Nest.ElasticClient.SearchAsync&lt;T&gt;**  
**Nest.ElasticClient.SearchAsync&lt;T&gt;**  
**Nest.ElasticClient.SearchShardsAsync**  
**Nest.ElasticClient.SearchShardsAsync&lt;T&gt;**  
**Nest.ElasticClient.SearchTemplateAsync&lt;T, TResult&gt;**  
**Nest.ElasticClient.SearchTemplateAsync&lt;T, TResult&gt;**  
**Nest.ElasticClient.SearchTemplateAsync&lt;T&gt;**  
**Nest.ElasticClient.SearchTemplateAsync&lt;T&gt;**  
**Nest.ElasticClient.SegmentsAsync**  
**Nest.ElasticClient.SegmentsAsync**  
**Nest.ElasticClient.SnapshotAsync**  
**Nest.ElasticClient.SnapshotAsync**  
**Nest.ElasticClient.SnapshotStatusAsync**  
**Nest.ElasticClient.SnapshotStatusAsync**  
**Nest.ElasticClient.SourceAsync&lt;T&gt;**  
**Nest.ElasticClient.SourceAsync&lt;T&gt;**  
**Nest.ElasticClient.StartWatcherAsync**  
**Nest.ElasticClient.StartWatcherAsync**  
**Nest.ElasticClient.StopWatcherAsync**  
**Nest.ElasticClient.StopWatcherAsync**  
**Nest.ElasticClient.SuggestAsync&lt;T&gt;**  
**Nest.ElasticClient.SyncedFlushAsync**  
**Nest.ElasticClient.SyncedFlushAsync**  
**Nest.ElasticClient.TermVectorsAsync&lt;T&gt;**  
**Nest.ElasticClient.TermVectorsAsync&lt;T&gt;**  
**Nest.ElasticClient.TypeExistsAsync**  
**Nest.ElasticClient.TypeExistsAsync**  
**Nest.ElasticClient.UnregisterPercolatorAsync**  
**Nest.ElasticClient.UnregisterPercolatorAsync&lt;T&gt;**  
**Nest.ElasticClient.UpdateAsync&lt;TDocument, TPartialDocument&gt;**  
**Nest.ElasticClient.UpdateAsync&lt;TDocument, TPartialDocument&gt;**  
**Nest.ElasticClient.UpdateAsync&lt;TDocument&gt;**  
**Nest.ElasticClient.UpdateAsync&lt;TDocument&gt;**  
**Nest.ElasticClient.UpdateByQueryAsync**  
**Nest.ElasticClient.UpdateByQueryAsync&lt;T&gt;**  
**Nest.ElasticClient.UpdateIndexSettingsAsync**  
**Nest.ElasticClient.UpdateIndexSettingsAsync**  
**Nest.ElasticClient.UpgradeAsync**  
**Nest.ElasticClient.UpgradeAsync**  
**Nest.ElasticClient.UpgradeStatusAsync**  
**Nest.ElasticClient.UpgradeStatusAsync**  
**Nest.ElasticClient.ValidateQueryAsync**  
**Nest.ElasticClient.ValidateQueryAsync&lt;T&gt;**  
**Nest.ElasticClient.VerifyRepositoryAsync**  
**Nest.ElasticClient.VerifyRepositoryAsync**  
**Nest.ElasticClient.WatcherStatsAsync**  
**Nest.ElasticClient.WatcherStatsAsync**  
**Nest.GetManyExtensions.GetManyAsync&lt;T&gt;**  
**Nest.GetManyExtensions.GetManyAsync&lt;T&gt;**  
**Nest.IElasticClient.AcknowledgeWatchAsync**  
**Nest.IElasticClient.AcknowledgeWatchAsync**  
**Nest.IElasticClient.ActivateWatchAsync**  
**Nest.IElasticClient.ActivateWatchAsync**  
**Nest.IElasticClient.AliasAsync**  
**Nest.IElasticClient.AliasAsync**  
**Nest.IElasticClient.AliasExistsAsync**  
**Nest.IElasticClient.AliasExistsAsync**  
**Nest.IElasticClient.AnalyzeAsync**  
**Nest.IElasticClient.AnalyzeAsync**  
**Nest.IElasticClient.AuthenticateAsync**  
**Nest.IElasticClient.AuthenticateAsync**  
**Nest.IElasticClient.BulkAsync**  
**Nest.IElasticClient.BulkAsync**  
**Nest.IElasticClient.CatAliasesAsync**  
**Nest.IElasticClient.CatAliasesAsync**  
**Nest.IElasticClient.CatAllocationAsync**  
**Nest.IElasticClient.CatAllocationAsync**  
**Nest.IElasticClient.CatCountAsync**  
**Nest.IElasticClient.CatCountAsync**  
**Nest.IElasticClient.CatFielddataAsync**  
**Nest.IElasticClient.CatFielddataAsync**  
**Nest.IElasticClient.CatHealthAsync**  
**Nest.IElasticClient.CatHealthAsync**  
**Nest.IElasticClient.CatHelpAsync**  
**Nest.IElasticClient.CatHelpAsync**  
**Nest.IElasticClient.CatIndicesAsync**  
**Nest.IElasticClient.CatIndicesAsync**  
**Nest.IElasticClient.CatMasterAsync**  
**Nest.IElasticClient.CatMasterAsync**  
**Nest.IElasticClient.CatNodeAttributesAsync**  
**Nest.IElasticClient.CatNodeAttributesAsync**  
**Nest.IElasticClient.CatNodesAsync**  
**Nest.IElasticClient.CatNodesAsync**  
**Nest.IElasticClient.CatPendingTasksAsync**  
**Nest.IElasticClient.CatPendingTasksAsync**  
**Nest.IElasticClient.CatPluginsAsync**  
**Nest.IElasticClient.CatPluginsAsync**  
**Nest.IElasticClient.CatRecoveryAsync**  
**Nest.IElasticClient.CatRecoveryAsync**  
**Nest.IElasticClient.CatRepositoriesAsync**  
**Nest.IElasticClient.CatRepositoriesAsync**  
**Nest.IElasticClient.CatSegmentsAsync**  
**Nest.IElasticClient.CatSegmentsAsync**  
**Nest.IElasticClient.CatShardsAsync**  
**Nest.IElasticClient.CatShardsAsync**  
**Nest.IElasticClient.CatSnapshotsAsync**  
**Nest.IElasticClient.CatSnapshotsAsync**  
**Nest.IElasticClient.CatThreadPoolAsync**  
**Nest.IElasticClient.CatThreadPoolAsync**  
**Nest.IElasticClient.ClearCacheAsync**  
**Nest.IElasticClient.ClearCacheAsync**  
**Nest.IElasticClient.ClearCachedRealmsAsync**  
**Nest.IElasticClient.ClearCachedRealmsAsync**  
**Nest.IElasticClient.ClearCachedRolesAsync**  
**Nest.IElasticClient.ClearCachedRolesAsync**  
**Nest.IElasticClient.ClearScrollAsync**  
**Nest.IElasticClient.ClearScrollAsync**  
**Nest.IElasticClient.CloseIndexAsync**  
**Nest.IElasticClient.CloseIndexAsync**  
**Nest.IElasticClient.ClusterGetSettingsAsync**  
**Nest.IElasticClient.ClusterGetSettingsAsync**  
**Nest.IElasticClient.ClusterHealthAsync**  
**Nest.IElasticClient.ClusterHealthAsync**  
**Nest.IElasticClient.ClusterPendingTasksAsync**  
**Nest.IElasticClient.ClusterPendingTasksAsync**  
**Nest.IElasticClient.ClusterPutSettingsAsync**  
**Nest.IElasticClient.ClusterPutSettingsAsync**  
**Nest.IElasticClient.ClusterRerouteAsync**  
**Nest.IElasticClient.ClusterRerouteAsync**  
**Nest.IElasticClient.ClusterStateAsync**  
**Nest.IElasticClient.ClusterStateAsync**  
**Nest.IElasticClient.ClusterStatsAsync**  
**Nest.IElasticClient.ClusterStatsAsync**  
**Nest.IElasticClient.CountAsync&lt;T&gt;**  
**Nest.IElasticClient.CountAsync&lt;T&gt;**  
**Nest.IElasticClient.CreateIndexAsync**  
**Nest.IElasticClient.CreateIndexAsync**  
**Nest.IElasticClient.CreateRepositoryAsync**  
**Nest.IElasticClient.CreateRepositoryAsync**  
**Nest.IElasticClient.DeactivateWatchAsync**  
**Nest.IElasticClient.DeactivateWatchAsync**  
**Nest.IElasticClient.DeleteAliasAsync**  
**Nest.IElasticClient.DeleteAliasAsync**  
**Nest.IElasticClient.DeleteAsync**  
**Nest.IElasticClient.DeleteAsync&lt;T&gt;**  
**Nest.IElasticClient.DeleteByQueryAsync**  
**Nest.IElasticClient.DeleteByQueryAsync&lt;T&gt;**  
**Nest.IElasticClient.DeleteIndexAsync**  
**Nest.IElasticClient.DeleteIndexAsync**  
**Nest.IElasticClient.DeleteIndexTemplateAsync**  
**Nest.IElasticClient.DeleteIndexTemplateAsync**  
**Nest.IElasticClient.DeleteLicenseAsync**  
**Nest.IElasticClient.DeleteLicenseAsync**  
**Nest.IElasticClient.DeleteRepositoryAsync**  
**Nest.IElasticClient.DeleteRepositoryAsync**  
**Nest.IElasticClient.DeleteRoleAsync**  
**Nest.IElasticClient.DeleteRoleAsync**  
**Nest.IElasticClient.DeleteScriptAsync**  
**Nest.IElasticClient.DeleteScriptAsync**  
**Nest.IElasticClient.DeleteSearchTemplateAsync**  
**Nest.IElasticClient.DeleteSearchTemplateAsync**  
**Nest.IElasticClient.DeleteSnapshotAsync**  
**Nest.IElasticClient.DeleteSnapshotAsync**  
**Nest.IElasticClient.DeleteUserAsync**  
**Nest.IElasticClient.DeleteUserAsync**  
**Nest.IElasticClient.DeleteWatchAsync**  
**Nest.IElasticClient.DeleteWatchAsync**  
**Nest.IElasticClient.DocumentExistsAsync**  
**Nest.IElasticClient.DocumentExistsAsync&lt;T&gt;**  
**Nest.IElasticClient.ExecuteWatchAsync**  
**Nest.IElasticClient.ExecuteWatchAsync**  
**Nest.IElasticClient.ExplainAsync&lt;T&gt;**  
**Nest.IElasticClient.ExplainAsync&lt;T&gt;**  
**Nest.IElasticClient.FieldStatsAsync**  
**Nest.IElasticClient.FieldStatsAsync**  
**Nest.IElasticClient.FlushAsync**  
**Nest.IElasticClient.FlushAsync**  
**Nest.IElasticClient.ForceMergeAsync**  
**Nest.IElasticClient.ForceMergeAsync**  
**Nest.IElasticClient.GetAliasAsync**  
**Nest.IElasticClient.GetAliasAsync**  
**Nest.IElasticClient.GetAsync&lt;T&gt;**  
**Nest.IElasticClient.GetAsync&lt;T&gt;**  
**Nest.IElasticClient.GetFieldMappingAsync**  
**Nest.IElasticClient.GetFieldMappingAsync&lt;T&gt;**  
**Nest.IElasticClient.GetIndexAsync**  
**Nest.IElasticClient.GetIndexAsync**  
**Nest.IElasticClient.GetIndexSettingsAsync**  
**Nest.IElasticClient.GetIndexSettingsAsync**  
**Nest.IElasticClient.GetIndexTemplateAsync**  
**Nest.IElasticClient.GetIndexTemplateAsync**  
**Nest.IElasticClient.GetLicenseAsync**  
**Nest.IElasticClient.GetLicenseAsync**  
**Nest.IElasticClient.GetMappingAsync**  
**Nest.IElasticClient.GetMappingAsync&lt;T&gt;**  
**Nest.IElasticClient.GetRepositoryAsync**  
**Nest.IElasticClient.GetRepositoryAsync**  
**Nest.IElasticClient.GetRoleAsync**  
**Nest.IElasticClient.GetRoleAsync**  
**Nest.IElasticClient.GetScriptAsync**  
**Nest.IElasticClient.GetScriptAsync**  
**Nest.IElasticClient.GetSearchTemplateAsync**  
**Nest.IElasticClient.GetSearchTemplateAsync**  
**Nest.IElasticClient.GetSnapshotAsync**  
**Nest.IElasticClient.GetSnapshotAsync**  
**Nest.IElasticClient.GetUserAsync**  
**Nest.IElasticClient.GetUserAsync**  
**Nest.IElasticClient.GetWatchAsync**  
**Nest.IElasticClient.GetWatchAsync**  
**Nest.IElasticClient.GraphExploreAsync**  
**Nest.IElasticClient.GraphExploreAsync&lt;T&gt;**  
**Nest.IElasticClient.IndexAsync**  
**Nest.IElasticClient.IndexAsync&lt;T&gt;**  
**Nest.IElasticClient.IndexExistsAsync**  
**Nest.IElasticClient.IndexExistsAsync**  
**Nest.IElasticClient.IndexTemplateExistsAsync**  
**Nest.IElasticClient.IndexTemplateExistsAsync**  
**Nest.IElasticClient.IndicesShardStoresAsync**  
**Nest.IElasticClient.IndicesShardStoresAsync**  
**Nest.IElasticClient.IndicesStatsAsync**  
**Nest.IElasticClient.IndicesStatsAsync**  
**Nest.IElasticClient.MapAsync**  
**Nest.IElasticClient.MapAsync&lt;T&gt;**  
**Nest.IElasticClient.MultiGetAsync**  
**Nest.IElasticClient.MultiGetAsync**  
**Nest.IElasticClient.MultiPercolateAsync**  
**Nest.IElasticClient.MultiPercolateAsync**  
**Nest.IElasticClient.MultiSearchAsync**  
**Nest.IElasticClient.MultiSearchAsync**  
**Nest.IElasticClient.MultiTermVectorsAsync**  
**Nest.IElasticClient.MultiTermVectorsAsync**  
**Nest.IElasticClient.NodesHotThreadsAsync**  
**Nest.IElasticClient.NodesHotThreadsAsync**  
**Nest.IElasticClient.NodesInfoAsync**  
**Nest.IElasticClient.NodesInfoAsync**  
**Nest.IElasticClient.NodesStatsAsync**  
**Nest.IElasticClient.NodesStatsAsync**  
**Nest.IElasticClient.OpenIndexAsync**  
**Nest.IElasticClient.OpenIndexAsync**  
**Nest.IElasticClient.PercolateAsync&lt;T&gt;**  
**Nest.IElasticClient.PercolateAsync&lt;T&gt;**  
**Nest.IElasticClient.PercolateCountAsync&lt;T&gt;**  
**Nest.IElasticClient.PercolateCountAsync&lt;T&gt;**  
**Nest.IElasticClient.PingAsync**  
**Nest.IElasticClient.PingAsync**  
**Nest.IElasticClient.PostLicenseAsync**  
**Nest.IElasticClient.PostLicenseAsync**  
**Nest.IElasticClient.PutAliasAsync**  
**Nest.IElasticClient.PutAliasAsync**  
**Nest.IElasticClient.PutIndexTemplateAsync**  
**Nest.IElasticClient.PutIndexTemplateAsync**  
**Nest.IElasticClient.PutRoleAsync**  
**Nest.IElasticClient.PutRoleAsync**  
**Nest.IElasticClient.PutScriptAsync**  
**Nest.IElasticClient.PutScriptAsync**  
**Nest.IElasticClient.PutSearchTemplateAsync**  
**Nest.IElasticClient.PutSearchTemplateAsync**  
**Nest.IElasticClient.PutUserAsync**  
**Nest.IElasticClient.PutUserAsync**  
**Nest.IElasticClient.PutWatchAsync**  
**Nest.IElasticClient.PutWatchAsync**  
**Nest.IElasticClient.RecoveryStatusAsync**  
**Nest.IElasticClient.RecoveryStatusAsync**  
**Nest.IElasticClient.RefreshAsync**  
**Nest.IElasticClient.RefreshAsync**  
**Nest.IElasticClient.RegisterPercolatorAsync**  
**Nest.IElasticClient.RegisterPercolatorAsync&lt;T&gt;**  
**Nest.IElasticClient.Reindex&lt;T&gt;**  
**Nest.IElasticClient.Reindex&lt;T&gt;**  
**Nest.IElasticClient.ReindexOnServerAsync**  
**Nest.IElasticClient.ReindexOnServerAsync**  
**Nest.IElasticClient.RenderSearchTemplateAsync**  
**Nest.IElasticClient.RenderSearchTemplateAsync**  
**Nest.IElasticClient.RestartWatcherAsync**  
**Nest.IElasticClient.RestartWatcherAsync**  
**Nest.IElasticClient.RestoreAsync**  
**Nest.IElasticClient.RestoreAsync**  
**Nest.IElasticClient.RethrottleAsync**  
**Nest.IElasticClient.RethrottleAsync**  
**Nest.IElasticClient.RootNodeInfoAsync**  
**Nest.IElasticClient.RootNodeInfoAsync**  
**Nest.IElasticClient.ScrollAsync&lt;T&gt;**  
**Nest.IElasticClient.ScrollAsync&lt;T&gt;**  
**Nest.IElasticClient.SearchAsync&lt;T, TResult&gt;**  
**Nest.IElasticClient.SearchAsync&lt;T, TResult&gt;**  
**Nest.IElasticClient.SearchAsync&lt;T&gt;**  
**Nest.IElasticClient.SearchAsync&lt;T&gt;**  
**Nest.IElasticClient.SearchShardsAsync**  
**Nest.IElasticClient.SearchShardsAsync&lt;T&gt;**  
**Nest.IElasticClient.SearchTemplateAsync&lt;T, TResult&gt;**  
**Nest.IElasticClient.SearchTemplateAsync&lt;T, TResult&gt;**  
**Nest.IElasticClient.SearchTemplateAsync&lt;T&gt;**  
**Nest.IElasticClient.SearchTemplateAsync&lt;T&gt;**  
**Nest.IElasticClient.SegmentsAsync**  
**Nest.IElasticClient.SegmentsAsync**  
**Nest.IElasticClient.SnapshotAsync**  
**Nest.IElasticClient.SnapshotAsync**  
**Nest.IElasticClient.SnapshotStatusAsync**  
**Nest.IElasticClient.SnapshotStatusAsync**  
**Nest.IElasticClient.SourceAsync&lt;T&gt;**  
**Nest.IElasticClient.SourceAsync&lt;T&gt;**  
**Nest.IElasticClient.StartWatcherAsync**  
**Nest.IElasticClient.StartWatcherAsync**  
**Nest.IElasticClient.StopWatcherAsync**  
**Nest.IElasticClient.StopWatcherAsync**  
**Nest.IElasticClient.SuggestAsync&lt;T&gt;**  
**Nest.IElasticClient.SyncedFlushAsync**  
**Nest.IElasticClient.SyncedFlushAsync**  
**Nest.IElasticClient.TermVectorsAsync&lt;T&gt;**  
**Nest.IElasticClient.TermVectorsAsync&lt;T&gt;**  
**Nest.IElasticClient.TypeExistsAsync**  
**Nest.IElasticClient.TypeExistsAsync**  
**Nest.IElasticClient.UnregisterPercolatorAsync**  
**Nest.IElasticClient.UnregisterPercolatorAsync&lt;T&gt;**  
**Nest.IElasticClient.UpdateAsync&lt;TDocument, TPartialDocument&gt;**  
**Nest.IElasticClient.UpdateAsync&lt;TDocument, TPartialDocument&gt;**  
**Nest.IElasticClient.UpdateAsync&lt;TDocument&gt;**  
**Nest.IElasticClient.UpdateAsync&lt;TDocument&gt;**  
**Nest.IElasticClient.UpdateByQueryAsync**  
**Nest.IElasticClient.UpdateByQueryAsync&lt;T&gt;**  
**Nest.IElasticClient.UpdateIndexSettingsAsync**  
**Nest.IElasticClient.UpdateIndexSettingsAsync**  
**Nest.IElasticClient.UpgradeAsync**  
**Nest.IElasticClient.UpgradeAsync**  
**Nest.IElasticClient.UpgradeStatusAsync**  
**Nest.IElasticClient.UpgradeStatusAsync**  
**Nest.IElasticClient.ValidateQueryAsync**  
**Nest.IElasticClient.ValidateQueryAsync&lt;T&gt;**  
**Nest.IElasticClient.VerifyRepositoryAsync**  
**Nest.IElasticClient.VerifyRepositoryAsync**  
**Nest.IElasticClient.WatcherStatsAsync**  
**Nest.IElasticClient.WatcherStatsAsync**  
**Nest.IHighLevelToLowLevelDispatcher.DispatchAsync&lt;TRequest, TQueryString, TResponse, TResponseInterface&gt;**  
**Nest.IHighLevelToLowLevelDispatcher.DispatchAsync&lt;TRequest, TQueryString, TResponse, TResponseInterface&gt;**  
**Nest.IndexManyExtensions.IndexManyAsync&lt;T&gt;**  
**Nest.ReindexObservable&lt;T&gt;..ctor**  
**Nest.SourceManyExtensions.SourceManyAsync&lt;T&gt;**  
**Nest.SourceManyExtensions.SourceManyAsync&lt;T&gt;**  
