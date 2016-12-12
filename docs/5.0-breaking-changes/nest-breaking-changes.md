# Breaking Changes

Oh my goodness, this looks like a lot of breaking changes! This is true but please take note that this list is very very extensive 
It includes every single *binary* breaking change. In a lot of cases these will not necessarily equate to compiler errors. 

## StatsAggregator renamed to StatsAggregation

`IStatsAggregator` not named correctly all aggregation requests objects need to end with `Aggregation`

**public property Nest.IAggregationContainer.Stats** *Declaration changed (Breaking)*

2.x: `public IStatsAggregator Stats { get; set; }`  
5.x: `public IStatsAggregation Stats { get; set; }` 

**public method Nest.AggregationContainerDescriptor&lt;T&gt;.Stats** *Declaration changed (Breaking)*

2.x: `public AggregationContainerDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, IStatsAggregator> selector)`  
5.x: `public AggregationContainerDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, IStatsAggregation> selector)`  

**public interface Nest.IStatsAggregator** *Removed (Breaking)*


## KeyedBucket is now generic

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

## String Property Mapping is obsolete

See also: https://www.elastic.co/guide/en/elasticsearch/reference/current/breaking_50_mapping_changes.html#_literal_string_literal_fields_replaced_by_literal_text_literal_literal_keyword_literal_fields

This is also reflected in the attachment mappings

**public property Nest.AttachmentProperty.AuthorField** *Declaration changed (Breaking)*

2.x: `public IStringProperty AuthorField { get; set; }`  
5.x: `public ITextProperty AuthorField { get; set; }`  

**public property Nest.AttachmentProperty.ContentTypeField** *Declaration changed (Breaking)*  
**public property Nest.AttachmentProperty.KeywordsField** *Declaration changed (Breaking)*  
**public property Nest.AttachmentProperty.LanguageField** *Declaration changed (Breaking)*  
**public property Nest.AttachmentProperty.NameField** *Declaration changed (Breaking)*  
**public property Nest.AttachmentProperty.TitleField** *Declaration changed (Breaking)*  
**public property Nest.IAttachmentProperty.AuthorField** *Declaration changed (Breaking)*  
**public property Nest.IAttachmentProperty.ContentTypeField** *Declaration changed (Breaking)*  
**public property Nest.IAttachmentProperty.KeywordsField** *Declaration changed (Breaking)*  
**public property Nest.IAttachmentProperty.LanguageField** *Declaration changed (Breaking)*  
**public property Nest.IAttachmentProperty.NameField** *Declaration changed (Breaking)*  
**public property Nest.IAttachmentProperty.TitleField** *Declaration changed (Breaking)*  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.ContentTypeField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> ContentTypeField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> ContentTypeField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.AuthorField** *Declaration changed (Breaking)*  
**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.FileField** *Declaration changed (Breaking)*  
**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.KeywordsField** *Declaration changed (Breaking)*  
**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.LanguageField** *Declaration changed (Breaking)*  
**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.NameField** *Declaration changed (Breaking)*  
**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.TitleField** *Declaration changed (Breaking)*  


## NonStringIndexOption no longer valid

See also: https://www.elastic.co/guide/en/elasticsearch/reference/5.0/breaking_50_mapping_changes.html#_literal_index_literal_property

**public enum Nest.NonStringIndexOption** *Removed (Breaking)*

**public property Nest.BooleanAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public bool Index { get; set; }`  

**public property Nest.BooleanProperty.Index** *Declaration changed (Breaking)*  
**public property Nest.DateAttribute.Index** *Declaration changed (Breaking)*  
**public property Nest.DateProperty.Index** *Declaration changed (Breaking)*  
**public property Nest.IBooleanProperty.Index** *Declaration changed (Breaking)*  
**public property Nest.IDateProperty.Index** *Declaration changed (Breaking)*  
**public property Nest.IIpProperty.Index** *Declaration changed (Breaking)*  
**public property Nest.INumberProperty.Index** *Declaration changed (Breaking)*  
**public property Nest.IpAttribute.Index** *Declaration changed (Breaking)*  
**public property Nest.IpProperty.Index** *Declaration changed (Breaking)*  
**public property Nest.NumberAttribute.Index** *Declaration changed (Breaking)*  
**public property Nest.NumberProperty.Index** *Declaration changed (Breaking)*  

**public method Nest.BooleanPropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public BooleanPropertyDescriptor<T> Index(NonStringIndexOption index = 0)`  
5.x: `public BooleanPropertyDescriptor<T> Index(bool index)`  

**public method Nest.DatePropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*  
**public method Nest.IpPropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*  
**public method Nest.NumberPropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Index** *Declaration changed (Breaking)*  

## Refresh no longer a simple boolean

As it now also accepts a `wait_for` parameter

See also: https://www.elastic.co/guide/en/elasticsearch/reference/5.0/docs-refresh.html

**public method Nest.BulkAllDescriptor&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public BulkAllDescriptor<T> Refresh(bool refresh = True)`  
5.x: `public BulkAllDescriptor<T> Refresh(Refresh refresh)`  

**public method Nest.BulkDescriptor.Refresh** *Declaration changed (Breaking)*  
**public method Nest.DeleteDescriptor&lt;T&gt;.Refresh** *Declaration changed (Breaking)*  
**public method Nest.IndexDescriptor&lt;TDocument&gt;.Refresh** *Declaration changed (Breaking)*  
**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Refresh** *Declaration changed (Breaking)*  

**public property Nest.BulkAllRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool? Refresh { get; set; }`  
5.x: `public Nullable<Refresh> Refresh { get; set; }`  

**public property Nest.BulkRequest.Refresh** *Declaration changed (Breaking)*  
**public property Nest.DeleteRequest.Refresh** *Declaration changed (Breaking)*  
**public property Nest.DeleteRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*  
**public property Nest.IBulkAllRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*  
**public property Nest.IndexRequest&lt;TDocument&gt;.Refresh** *Declaration changed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Refresh** *Declaration changed (Breaking)*  

## Script changes

The default language is now painless! Also we no longer support the `1.x` inline syntax for scripts.
https://www.elastic.co/guide/en/elasticsearch/reference/current/breaking_50_scripting.html#_removed_1_x_script_and_template_syntax

**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(string script)`  
5.x: `public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector)`  

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public string Script { get; set; }`  
5.x: `public IScript Script { get; set; }`  

**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*  
**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*  

In some cases we abused IScript to send template queries this is now fixed

**public property Nest.PhraseSuggestCollate.Query** *Declaration changed (Breaking)*

2.x: `public IScript Query { get; set; }`  
5.x: `public ITemplateQuery Query { get; set; }`  

**public property Nest.IPhraseSuggestCollate.Query** *Declaration changed (Breaking)*  
**public method Nest.PhraseSuggestCollateDescriptor&lt;T&gt;.Query** *Removed (Breaking)*  

**public method Nest.PhraseSuggestCollateDescriptor&lt;T&gt;.Query** *Declaration changed (Breaking)*

2.x: `public PhraseSuggestCollateDescriptor<T> Query(string script)`  
5.x: `public PhraseSuggestCollateDescriptor<T> Query(Func<TemplateQueryDescriptor<T>, ITemplateQuery> selector)`  


Properties supporting the obsoleted and removed syntax have been removed

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


## `I*Operation` on bulk is now `IBulk*Operation`

Impact is low unless you have casting code in your application

**public method Nest.BulkDescriptor.Index&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, IIndexOperation<T>> bulkIndexSelector)`  
5.x: `public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, IBulkIndexOperation<T>> bulkIndexSelector)`  

**public method Nest.BulkDescriptor.IndexMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public BulkDescriptor IndexMany<T>(IEnumerable<T> objects, Func<BulkIndexDescriptor<T>, T, IIndexOperation<T>> bulkIndexSelector)`  
5.x: `public BulkDescriptor IndexMany<T>(IEnumerable<T> objects, Func<BulkIndexDescriptor<T>, T, IBulkIndexOperation<T>> bulkIndexSelector)`  

**public interface Nest.IIndexOperation&lt;T&gt;** *Renamed (Breaking)*


## Cat Threadpool changes

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

## WaitForActiveShards is now a string

See also: https://github.com/elastic/elasticsearch/pull/20186

**public method Nest.ClusterHealthDescriptor.WaitForActiveShards** *Declaration changed (Breaking)*

2.x: `public ClusterHealthDescriptor WaitForActiveShards(long wait_for_active_shards)`  
5.x: `public ClusterHealthDescriptor WaitForActiveShards(string wait_for_active_shards)`  

**public property Nest.ClusterHealthRequest.WaitForActiveShards** *Declaration changed (Breaking)*

2.x: `public long WaitForActiveShards { get; set; }`  
5.x: `public string WaitForActiveShards { get; set; }`  

## AutoExpandReplicas is now an actual type

Binary break only, still implicitly converts from string

**public property Nest.IDynamicIndexSettings.AutoExpandReplicas** *Declaration changed (Breaking)*  
**public property Nest.DynamicIndexSettings.AutoExpandReplicas** *Declaration changed (Breaking)*

2.x: `public string AutoExpandReplicas { get; set; }`  
5.x: `public AutoExpandReplicas AutoExpandReplicas { get; set; }`  

**public method Nest.DynamicIndexSettingsDescriptorBase&lt;TDescriptor, TIndexSettings&gt;.AutoExpandReplicas** *Declaration changed (Breaking)*

2.x: `public TDescriptor AutoExpandReplicas(string AutoExpandReplicas)`  
5.x: `public TDescriptor AutoExpandReplicas(AutoExpandReplicas autoExpandReplicas)`  

## DslPrettyPrintVisitor methods are now virtual

## Nest visitors should be bound to interface

The visitors should be passed interfaces not concrete types see: https://github.com/elastic/elasticsearch-net/pull/2320

## Deprecated queries are now removed

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


## Dynamic mapping now sends true/false

So is now a union of `bool` and `DynamicMapping`

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
**public property Nest.PutMappingRequest.Dynamic** *Declaration changed (Breaking)*  
**public property Nest.PutMappingRequest&lt;T&gt;.Dynamic** *Declaration changed (Breaking)*  
**public property Nest.TypeMapping.Dynamic** *Declaration changed (Breaking)*  
**public property Nest.ObjectProperty.Dynamic** *Declaration changed (Breaking)*  
**public method Nest.ObjectPropertyDescriptorBase&lt;TDescriptor, TInterface, TParent, TChild&gt;.Dynamic** *Declaration changed (Breaking)*

2.x: `public TDescriptor Dynamic(DynamicMapping dynamic)`  
5.x: `public TDescriptor Dynamic(Union<bool, DynamicMapping> dynamic)`  

**public method Nest.PutMappingDescriptor&lt;T&gt;.Dynamic** *Declaration changed (Breaking)*  
**public method Nest.TypeMappingDescriptor&lt;T&gt;.Dynamic** *Declaration changed (Breaking)*  

## CodeStandards changes

Impact low, various binary breaking changes of code that did not adhere to our coding conventions

**public method Nest.BoolQueryDescriptor&lt;T&gt;.DisableCoord** *Declaration changed (Breaking)*

2.x: `public BoolQueryDescriptor<T> DisableCoord()`  
5.x: `public BoolQueryDescriptor<T> DisableCoord(bool? disableCoord = True)`  

**public method Nest.ExtendedStatsBucketAggregationDescriptor.Sigma** *Declaration changed (Breaking)*

2.x: `public ExtendedStatsBucketAggregationDescriptor Sigma(double? sigma)`  
5.x: `public ExtendedStatsBucketAggregationDescriptor Sigma(double sigma)`  

**public method Nest.StringPropertyDescriptor&lt;T&gt;.PositionIncrementGap** *Declaration changed (Breaking)*

2.x: `public StringPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap)`  
5.x: `public StringPropertyDescriptor<T> PositionIncrementGap(int positionIncrementGap)`  

In some cases we exposed `FluentDictionary` as property which is not useful

**public property Nest.TypeMapping.Meta** *Declaration changed (Breaking)*   
**public property Nest.PutMappingRequest&lt;T&gt;.Meta** *Declaration changed (Breaking)*
**public property Nest.ITypeMapping.Meta** *Declaration changed (Breaking)*
**public property Nest.PutMappingRequest.Meta** *Declaration changed (Breaking)*

2.x: `public FluentDictionary<string, object> Meta { get; set; }`  
5.x: `public IDictionary<string, object> Meta { get; set; }`  


## Visibility changes

Impact low, these are types/methods/constructors that were never supposed to be public.

**public class Nest.BucketsPathJsonConverter** *Visibility was changed from public to internal (Breaking)*  
**public class Nest.DictionaryResponseJsonConverter&lt;TResponse, TKey, TValue&gt;** *Visibility was changed from public to internal (Breaking)*  
**public method Nest.CreateIndexRequest..ctor** *Visibility was changed from public to internal (Breaking)*  
**public method Nest.NumberPropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;..ctor** *Visibility was changed from public to protected (Breaking)*  
**public method Nest.ObjectPropertyDescriptorBase&lt;TDescriptor, TInterface, TParent, TChild&gt;..ctor** *Visibility was changed from public to protected (Breaking)*  
**public class Nest.PercentileRanksAggregationJsonConverter** *Visibility was changed from public to internal (Breaking)*  
**public class Nest.PercentilesAggregationJsonConverter** *Visibility was changed from public to internal (Breaking)*  
**public class Nest.PropertyNameExtensions** *Visibility was changed from public to internal (Breaking)*  
**public class Nest.ReindexRoutingJsonConverter** *Visibility was changed from public to internal (Breaking)*  
**public property Nest.ResponseBase.ApiCall** *Visibility was changed from public to protected (Breaking)*  
**public class Nest.ScoreFunctionJsonConverter** *Visibility was changed from public to internal (Breaking)*  
**public class Nest.ScriptJsonConverter** *Visibility was changed from public to internal (Breaking)*  
**public property Nest.SearchResponse&lt;T&gt;.ApiCall** *Visibility was changed from public to protected (Breaking)*  
**public class Nest.SimpleQueryStringFlagsJsonConverter** *Visibility was changed from public to internal (Breaking)*  
**public class Nest.SourceFilterJsonConverter** *Visibility was changed from public to internal (Breaking)*  
**public class Nest.TypeNameExtensions** *Visibility was changed from public to internal (Breaking)*  
**public method Nest.AggregationsHelper..ctor** *Visibility was changed from public to protected (Breaking)*  

## Response properties should not have setters

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


## Setters on interfaces

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
**public property Nest.IUpgradeResponse.Shards** *Declaration changed (Breaking)*  

## No Id type on response

Several response properties were of type `Id` which is not all that useful to consumers. Now `string`.

**public property Nest.ExecuteWatchResponse.Id** *Declaration changed (Breaking)*  
**public property Nest.GetWatchResponse.Id** *Declaration changed (Breaking)*  
**public property Nest.IExecuteWatchResponse.Id** *Declaration changed (Breaking)*  
**public property Nest.IGetWatchResponse.Id** *Declaration changed (Breaking)*  
**public property Nest.IndexActionResult.Id** *Declaration changed (Breaking)*  
**public property Nest.IndexActionResultIndexResponse.Id** *Declaration changed (Breaking)*  
**public property Nest.IPutWatchResponse.Id** *Declaration changed (Breaking)*  
**public property Nest.PutWatchResponse.Id** *Declaration changed (Breaking)*  
**public property Nest.WatchRecord.WatchId** *Declaration changed (Breaking)*  
**public property Nest.WatchRecordQueuedStats.WatchId** *Declaration changed (Breaking)*  
**public property Nest.WatchRecordQueuedStats.WatchRecordId** *Declaration changed (Breaking)*  

## Methods taking Field as string

Some methods were taking a Field as string which should take `Field` instead. impact minimal since string still
implicitly converts to `Field`

**public method Nest.Field.And** *Declaration changed (Breaking)*  
**public method Nest.GeoDistanceAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.GeoHashGridAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.HistogramAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.InnerHitsDescriptor&lt;T&gt;.FielddataFields** *Declaration changed (Breaking)*  
**public method Nest.IpRangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.MetricAggregationDescriptorBase&lt;TMetricAggregation, TMetricAggregationInterface, T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.MissingAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.NestedAggregationDescriptor&lt;T&gt;.Path** *Declaration changed (Breaking)*  
**public method Nest.DateHistogramAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.DateRangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.DecayFunctionDescriptorBase&lt;TDescriptor, TOrigin, TScale, T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.Query&lt;T&gt;.Prefix** *Declaration changed (Breaking)*  
**public method Nest.Query&lt;T&gt;.Term** *Declaration changed (Breaking)*  
**public method Nest.Query&lt;T&gt;.Wildcard** *Declaration changed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.Prefix** *Declaration changed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.Term** *Declaration changed (Breaking)*  
**public method Nest.QueryContainerDescriptor&lt;T&gt;.Wildcard** *Declaration changed (Breaking)*  
**public method Nest.RangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.SignificantTermsAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.TermsAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*  
**public method Nest.ReverseNestedAggregationDescriptor&lt;T&gt;.Path** *Declaration changed (Breaking)*  


## Norms now takes a boolean instead of an object

See: https://github.com/elastic/elasticsearch-net/issues/2004

**public method Nest.GenericPropertyDescriptor&lt;T&gt;.Norms** *Declaration changed (Breaking)*

2.x: `public GenericPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector)`  
5.x: `public GenericPropertyDescriptor<T> Norms(bool enabled = True)`  

**public property Nest.GenericProperty.Norms** *Declaration changed (Breaking)*

2.x: `public INorms Norms { get; set; }`  
5.x: `public bool? Norms { get; set; }`  

**public property Nest.IGenericProperty.Norms** *Declaration changed (Breaking)*  
**public property Nest.IStringProperty.Norms** *Declaration changed (Breaking)*  
**public property Nest.StringProperty.Norms** *Declaration changed (Breaking)*  
**public method Nest.StringPropertyDescriptor&lt;T&gt;.Norms** *Declaration changed (Breaking)*  
**public interface Nest.INorms** *Removed (Breaking)*    
**public class Nest.Norms** *Removed (Breaking)*    
**public class Nest.NormsDescriptor** *Removed (Breaking)*    
**public enum Nest.NormsLoading** *Removed (Breaking)*    

## Score is now nullable on hit

See: https://github.com/elastic/elasticsearch-net/pull/2365

**public property Nest.Hit&lt;T&gt;.Score** *Declaration changed (Breaking)*
**public property Nest.IHit&lt;T&gt;.Score** *Declaration changed (Breaking)*

## Can now take multiple rescores 

2.x can as well but in 5.x we do it in a straightforward way without custom json converters.

**public property Nest.SearchRequest&lt;T&gt;.Rescore** *Declaration changed (Breaking)*  
**public property Nest.SearchRequest.Rescore** *Declaration changed (Breaking)*  
**public property Nest.ISearchRequest.Rescore** *Declaration changed (Breaking)*  

2.x: `public IRescore Rescore { get; set; }`  
5.x: `public IList<IRescore> Rescore { get; set; }`  

**public method Nest.SearchDescriptor&lt;T&gt;.Rescore** *Declaration changed (Breaking)*

2.x: `public SearchDescriptor<T> Rescore(Func<RescoreDescriptor<T>, IRescore> rescoreSelector)`    
5.x: `public SearchDescriptor<T> Rescore(Func<RescoringDescriptor<T>, IPromise<IList<IRescore>>> rescoreSelector)`    

**public class Nest.MultiRescore** *Removed (Breaking)*  
**public class Nest.RescoreConverter** *Removed (Breaking)*  

## Retries on reindex task status now object

Used to return only long, now tells you how bulk and search retries were necessary 

**public property Nest.IReindexOnServerResponse.Retries** *Declaration changed (Breaking)*  
**public property Nest.IUpdateByQueryResponse.Retries** *Declaration changed (Breaking)*  
**public property Nest.ReindexOnServerResponse.Retries** *Declaration changed (Breaking)*  
**public property Nest.ReindexStatus.Retries** *Declaration changed (Breaking)*  
**public property Nest.UpdateByQueryResponse.Retries** *Declaration changed (Breaking)*  

2.x: `public long Retries { get; internal set; }`  
5.x: `public Retries Retries { get; internal set; }`  


## Took should always be a long

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

**public property Nest.IBulkResponse.Took** *Declaration changed (Breaking)*  
**public property Nest.IPercolateCountResponse.Took** *Declaration changed (Breaking)*  
**public property Nest.ISearchResponse&lt;T&gt;.Took** *Declaration changed (Breaking)*  
**public property Nest.TermVectorsResponse.Took** *Declaration changed (Breaking)*  

also the hacks from 2.x have been removed

**public property Nest.BulkResponse.TookAsLong** *Removed (Breaking)*  
**public property Nest.IBulkResponse.TookAsLong** *Removed (Breaking)*  
**public property Nest.IPercolateCountResponse.TookAsLong** *Removed (Breaking)*  
**public property Nest.ISearchResponse&lt;T&gt;.TookAsLong** *Removed (Breaking)*  
**public property Nest.ITermVectorsResponse.TookAsLong** *Removed (Breaking)*  
**public property Nest.PercolateCountResponse.TookAsLong** *Removed (Breaking)*  
**public property Nest.SearchResponse&lt;T&gt;.TookAsLong** *Removed (Breaking)*  
**public property Nest.TermVectorsResponse.TookAsLong** *Removed (Breaking)*  


## Allow source filter to send false

In NEST 2.x we would always send Source.Exclude as `_source: { exclude: [""] }` in 5.x the we use a union of `bool`
`ISourceFiler` so NEST can send and recieve `_source: false`. Which should short circuit some routines on the server

See also: https://github.com/elastic/elasticsearch-net/pull/2200

**public property Nest.InnerHits.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public Union<bool, ISourceFilter> Source { get; set; }`  

**public property Nest.IInnerHits.Source** *Declaration changed (Breaking)*  
**public property Nest.ISearchRequest.Source** *Declaration changed (Breaking)*  
**public property Nest.ITopHitsAggregation.Source** *Declaration changed (Breaking)*  
**public property Nest.SearchRequest.Source** *Declaration changed (Breaking)*  
**public property Nest.SearchRequest&lt;T&gt;.Source** *Declaration changed (Breaking)*  
**public property Nest.TopHitsAggregation.Source** *Declaration changed (Breaking)*  

`Exclude` and `Include` are now plural on `ISourceFilter` in line with the change in Elasticsearch 5.0

**public property Nest.SourceFilter.Disable** *Removed (Breaking)*  
**public property Nest.SourceFilter.Exclude** *Removed (Breaking)*  
**public property Nest.SourceFilter.Include** *Removed (Breaking)*  
**public method Nest.SourceFilterDescriptor&lt;T&gt;.Disable** *Removed (Breaking)*  
**public method Nest.SourceFilterDescriptor&lt;T&gt;.Exclude** *Removed (Breaking)*  
**public method Nest.SourceFilterDescriptor&lt;T&gt;.Include** *Removed (Breaking)*  
**public property Nest.ISourceFilter.Disable** *Removed (Breaking)*  
**public property Nest.ISourceFilter.Exclude** *Removed (Breaking)*  
**public property Nest.ISourceFilter.Include** *Removed (Breaking)*    

## Bulk index failure now returns its metadata

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

## GetAliases API removed 

GetAlias API now returns `GetAlias*` named types not `GetAliases*`

**public class Nest.GetAliasesDescriptor** *Removed (Breaking)*  
**public class Nest.GetAliasesRequest** *Removed (Breaking)*  
**public class Nest.GetAliasesResponse** *Removed (Breaking)*  
**public interface Nest.IGetAliasesRequest** *Removed (Breaking)*  
**public interface Nest.IGetAliasesResponse** *Removed (Breaking)*  
**public method Nest.ElasticClient.GetAliases** *Removed (Breaking)*  
**public method Nest.ElasticClient.GetAliases** *Removed (Breaking)*  
**public method Nest.ElasticClient.GetAliasesAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.GetAliasesAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.GetAliases** *Removed (Breaking)*  
**public method Nest.IElasticClient.GetAliases** *Removed (Breaking)*  
**public method Nest.IElasticClient.GetAliasesAsync** *Removed (Breaking)*  

**public method Nest.ElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(IGetAliasRequest request)`  
5.x: `public IGetAliasResponse GetAlias(IGetAliasRequest request)`  

**public method Nest.ElasticClient.GetAlias** *Declaration changed (Breaking)*  
**public method Nest.IElasticClient.GetAlias** *Declaration changed (Breaking)*  
**public method Nest.IElasticClient.GetAlias** *Declaration changed (Breaking)*  

## Suggest is bound over T

See: https://github.com/elastic/elasticsearch-net/pull/2370

**public method Nest.IElasticClient.Suggest&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  
5.x: `public ISuggestResponse<T> Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  

## IElasticClient changes

Make sure all methods favor types over strings

**public method Nest.DeleteManyExtensions.DeleteMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public static IBulkResponse DeleteMany<T>(IElasticClient client, IEnumerable<T> objects, string index, string type)
```

5.x
```csharp
public static IBulkResponse DeleteMany<T>(IElasticClient client, IEnumerable<T> objects, IndexName index, TypeName type)
```

**public method Nest.GetManyExtensions.GetMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public static IEnumerable<IMultiGetHit<T>> GetMany<T>(IElasticClient client, IEnumerable<long> ids, string index, string type)
```

5.x
```csharp
public static IEnumerable<IMultiGetHit<T>> GetMany<T>(IElasticClient client, IEnumerable<long> ids, IndexName index, TypeName type)
```

**public method Nest.GetManyExtensions.GetMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public static IEnumerable<IMultiGetHit<T>> GetMany<T>(IElasticClient client, IEnumerable<string> ids, string index, string type)
```

5.x
```csharp
public static IEnumerable<IMultiGetHit<T>> GetMany<T>(IElasticClient client, IEnumerable<string> ids, IndexName index, TypeName type)
```

**public method Nest.IElasticClient.DeleteByQuery&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  
5.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  


**public method Nest.IndexManyExtensions.IndexMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public static IBulkResponse IndexMany<T>(IElasticClient client, IEnumerable<T> objects, string index, string type)
```

5.x
```csharp
public static IBulkResponse IndexMany<T>(IElasticClient client, IEnumerable<T> objects, IndexName index, TypeName type)
```

**public method Nest.IndicesPointingToAliasExtensions.GetIndicesPointingToAlias** *Declaration changed (Breaking)*

2.x
```csharp
public static IList<string> GetIndicesPointingToAlias(IElasticClient client, string aliasName)
```

5.x
```csharp
public static IEnumerable<string> GetIndicesPointingToAlias(IElasticClient client, Names alias)
```

**public method Nest.IndicesPointingToAliasExtensions.GetIndicesPointingToAliasAsync** *Declaration changed (Breaking)*

2.x
```csharp
public static Task<IList<string>> GetIndicesPointingToAliasAsync(IElasticClient client, string aliasName)
```

5.x
```csharp
public static Task<IEnumerable<string>> GetIndicesPointingToAliasAsync(IElasticClient client, Names alias)
```

**public method Nest.ElasticClient.DeleteByQuery&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  
5.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  

## Enum Changes

Impact low

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



## Uncategorized

Misc. changes that are yet to be categorized. Please open an issue if you are bit by these and feel it
warrants an explanation.

**public method Nest.DeleteByQueryDescriptor&lt;T&gt;.Routing** *Declaration changed (Breaking)*

2.x: `public DeleteByQueryDescriptor<T> Routing(string routing)`  
5.x: `public DeleteByQueryDescriptor<T> Routing(String[] routing)`  

**public property Nest.DeleteByQueryRequest.Routing** *Declaration changed (Breaking)*

2.x: `public string Routing { get; set; }`  
5.x: `public String[] Routing { get; set; }`  

**public property Nest.DeleteByQueryRequest&lt;T&gt;.Routing** *Declaration changed (Breaking)*

2.x: `public string Routing { get; set; }`  
5.x: `public String[] Routing { get; set; }`  

**public method Nest.Field..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor()`  
5.x: `public  .ctor(string name, double? boost)`  

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

**public class Nest.GeoShapeQueryDescriptorBase&lt;TDescriptor, TInterface, T&gt;** *Declaration changed (Breaking)*

2.x: `public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T> : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IDescriptor, IQuery, IFieldNameQuery, IGeoShapeQuery where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface where TInterface : class, IGeoShapeQuery`  
5.x: `public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T> : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IDescriptor, IQuery, IFieldNameQuery, IGeoShapeQuery where TDescriptor : GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface where TInterface : class, IGeoShapeQuery`  

**public property Nest.HighlightField.Type** *Declaration changed (Breaking)*

2.x: `public Nullable<HighlighterType> Type { get; set; }`  
5.x: `public Union<HighlighterType, string> Type { get; set; }`  

**public method Nest.HighlightFieldDescriptor&lt;T&gt;.PostTags** *Declaration changed (Breaking)*

2.x: `public HighlightFieldDescriptor<T> PostTags(string postTags)`  
5.x: `public HighlightFieldDescriptor<T> PostTags(String[] postTags)`  

**public method Nest.HighlightFieldDescriptor&lt;T&gt;.PreTags** *Declaration changed (Breaking)*

2.x: `public HighlightFieldDescriptor<T> PreTags(string preTags)`  
5.x: `public HighlightFieldDescriptor<T> PreTags(String[] preTags)`  

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

**public method Nest.ReindexRethrottleDescriptor.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public ReindexRethrottleDescriptor RequestsPerSecond(Single requests_per_second)`  
5.x: `public ReindexRethrottleDescriptor RequestsPerSecond(long requests_per_second)`  

**public property Nest.ReindexRethrottleRequest.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public long RequestsPerSecond { get; set; }`  

**public property Nest.Time.Milliseconds** *Declaration changed (Breaking)*

2.x: `public double Milliseconds { get; private set; }`  
5.x: `public double? Milliseconds { get; private set; }`  

**public method Nest.UpdateByQueryDescriptor&lt;T&gt;.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public UpdateByQueryDescriptor<T> RequestsPerSecond(Single requests_per_second)`  
5.x: `public UpdateByQueryDescriptor<T> RequestsPerSecond(long requests_per_second)`  

**public property Nest.UpdateByQueryRequest.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public long RequestsPerSecond { get; set; }`  

**public property Nest.UpdateByQueryRequest&lt;T&gt;.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public long RequestsPerSecond { get; set; }`  

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

# Read only data types on responses.

Responses now favor `IReadOnlyDictionary<TKey, TValue>` and `IReadOnlyCollection<T>` which are initialized as empty.

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

With NEST 2.x async methods, a cancellation tokens could be passed as part of the RequestConfiguration. This was not very discoverable and so each async method can now accept an optional cancellation token as an argument, making the API more async idiomatic.

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

# Removed in 5.0 after being obsoleted in 2.0


These are the things we removed from 5.0 that we gave an advanced warning for in the latest 2.x release of NEST. 

We will make sure to another release of NEST 2.x that flags as many types/methods/properties that are going to be removed that we have not convered yet


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

# Removed in 5.x

This is the complete list of removed types, properties, methods. We will retro actively mark them as obsolete
in an upcomming `2.x` release and update this documentation. 

**public class Nest.AllocateClusterRerouteCommand** *Removed (Breaking)*  
**public class Nest.AllocateClusterRerouteCommandDescriptor** *Removed (Breaking)*  
**public class Nest.AttachData** *Removed (Breaking)*  
**public property Nest.AttachmentProperty.FileField** *Removed (Breaking)*  
**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.LanguageField** *Removed (Breaking)*  
**public property Nest.BulkAllRequest&lt;T&gt;.Consistency** *Removed (Breaking)*  
**public method Nest.BulkDescriptor.Consistency** *Removed (Breaking)*  
**public property Nest.BulkIndexByScrollFailure.CausedBy** *Removed (Breaking)*  
**public property Nest.BulkIndexByScrollFailure.Node** *Removed (Breaking)*  
**public property Nest.BulkIndexByScrollFailure.Reason** *Removed (Breaking)*  
**public property Nest.BulkIndexByScrollFailure.Shard** *Removed (Breaking)*  
**public property Nest.BulkRequest.Consistency** *Removed (Breaking)*  
**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Lang** *Removed (Breaking)*  
**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*  
**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.InferFrom** *Removed (Breaking)*  
**public property Nest.CategorySuggestContext.Default** *Removed (Breaking)*  
**public method Nest.CategorySuggestContextDescriptor&lt;T&gt;.Default** *Removed (Breaking)*  
**public method Nest.CategorySuggestContextDescriptor&lt;T&gt;.Default** *Removed (Breaking)*  
**public property Nest.CatFielddataRecord.FieldSizes** *Removed (Breaking)*  
**public property Nest.CatFielddataRecord.Total** *Removed (Breaking)*  
**public property Nest.CatNodesRecord.Host** *Removed (Breaking)*  
**public property Nest.CatNodesRecord.Load** *Removed (Breaking)*  
**public property Nest.CatRecoveryRecord.TotalTranslog** *Removed (Breaking)*  
**public property Nest.CatRecoveryRecord.Translog** *Removed (Breaking)*  
**public property Nest.CatRecoveryRecord.TranslogPercent** *Removed (Breaking)*  
**public method Nest.ClusterHealthDescriptor.WaitForRelocatingShards** *Removed (Breaking)*  
**public property Nest.ClusterHealthRequest.WaitForRelocatingShards** *Removed (Breaking)*  
**public property Nest.ClusterIndicesStats.Percolate** *Removed (Breaking)*  
**public property Nest.ClusterNodeCount.Client** *Removed (Breaking)*  
**public property Nest.ClusterNodeCount.DataOnly** *Removed (Breaking)*  
**public property Nest.ClusterNodeCount.MasterData** *Removed (Breaking)*  
**public property Nest.ClusterNodeCount.MasterOnly** *Removed (Breaking)*  
**public class Nest.ClusterOperatingSystemMemory** *Removed (Breaking)*  
**public property Nest.ClusterOperatingSystemStats.Memory** *Removed (Breaking)*  
**public method Nest.ClusterRerouteDescriptor.Allocate** *Removed (Breaking)*  
**public property Nest.ClusterRerouteResponse.Version** *Removed (Breaking)*  
**public property Nest.CompletionAttribute.Payloads** *Removed (Breaking)*  
**public class Nest.CompletionField&lt;TPayload&gt;** *Removed (Breaking)*  
**public property Nest.CompletionProperty.Context** *Removed (Breaking)*  
**public property Nest.CompletionProperty.Payloads** *Removed (Breaking)*  
**public method Nest.CompletionPropertyDescriptor&lt;T&gt;.Context** *Removed (Breaking)*  
**public method Nest.CompletionPropertyDescriptor&lt;T&gt;.Payloads** *Removed (Breaking)*  
**public property Nest.CompletionSuggester.Context** *Removed (Breaking)*  
**public method Nest.CompletionSuggesterDescriptor&lt;T&gt;.Context** *Removed (Breaking)*  
**public method Nest.CreateIndexDescriptor.Warmers** *Removed (Breaking)*  
**public property Nest.CreateIndexRequest.Warmers** *Removed (Breaking)*  
**public class Nest.DeleteByQueryIndicesResult** *Removed (Breaking)*  
**public property Nest.DeleteByQueryResponse.Indices** *Removed (Breaking)*  
**public method Nest.DeleteDescriptor&lt;T&gt;.Consistency** *Removed (Breaking)*  
**public property Nest.DeleteRequest.Consistency** *Removed (Breaking)*  
**public property Nest.DeleteRequest&lt;T&gt;.Consistency** *Removed (Breaking)*  
**public method Nest.DeleteScriptDescriptor.Version** *Removed (Breaking)*  
**public method Nest.DeleteScriptDescriptor.VersionType** *Removed (Breaking)*  
**public property Nest.DeleteScriptRequest.Version** *Removed (Breaking)*  
**public property Nest.DeleteScriptRequest.VersionType** *Removed (Breaking)*  
**public method Nest.DeleteSearchTemplateDescriptor.Version** *Removed (Breaking)*  
**public method Nest.DeleteSearchTemplateDescriptor.VersionType** *Removed (Breaking)*  
**public property Nest.DeleteSearchTemplateRequest.Version** *Removed (Breaking)*  
**public property Nest.DeleteSearchTemplateRequest.VersionType** *Removed (Breaking)*  
**public class Nest.DeleteWarmerDescriptor** *Removed (Breaking)*  
**public class Nest.DeleteWarmerRequest** *Removed (Breaking)*  
**public class Nest.DeleteWarmerResponse** *Removed (Breaking)*  
**public method Nest.DeleteWatchDescriptor.Force** *Removed (Breaking)*  
**public property Nest.DeleteWatchRequest.Force** *Removed (Breaking)*  
**public property Nest.DirectGenerator.MinWordLen** *Removed (Breaking)*  
**public property Nest.DirectGenerator.PrefixLen** *Removed (Breaking)*    
**public method Nest.DynamicIndexSettings..ctor** *Removed (Breaking)*  
**public property Nest.DynamicIndexSettings.RequestCacheEnabled** *Removed (Breaking)*  
**public property Nest.DynamicIndexSettings.WarmersEnabled** *Removed (Breaking)*  
**public method Nest.DynamicIndexSettingsDescriptorBase&lt;TDescriptor, TIndexSettings&gt;.RequestCacheEnabled** *Removed (Breaking)*  
**public method Nest.DynamicIndexSettingsDescriptorBase&lt;TDescriptor, TIndexSettings&gt;.WarmersEnabled** *Removed (Breaking)*  
**public method Nest.ElasticClient.DeleteWarmer** *Removed (Breaking)*  
**public method Nest.ElasticClient.DeleteWarmer** *Removed (Breaking)*  
**public method Nest.ElasticClient.DeleteWarmerAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.DeleteWarmerAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.GetWarmer** *Removed (Breaking)*  
**public method Nest.ElasticClient.GetWarmer** *Removed (Breaking)*  
**public method Nest.ElasticClient.GetWarmerAsync** *Removed (Breaking)*    
**public method Nest.ElasticClient.GetWarmerAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.Optimize** *Removed (Breaking)*  
**public method Nest.ElasticClient.Optimize** *Removed (Breaking)*  
**public method Nest.ElasticClient.OptimizeAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.OptimizeAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.PutWarmer** *Removed (Breaking)*  
**public method Nest.ElasticClient.PutWarmer** *Removed (Breaking)*  
**public method Nest.ElasticClient.PutWarmerAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.PutWarmerAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.SearchExists** *Removed (Breaking)*  
**public method Nest.ElasticClient.SearchExists&lt;T&gt;** *Removed (Breaking)*    
**public method Nest.ElasticClient.SearchExistsAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.ElasticClient.Suggest** *Removed (Breaking)*  
**public method Nest.ElasticClient.SuggestAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.TasksCancel** *Removed (Breaking)*  
**public method Nest.ElasticClient.TasksCancel** *Removed (Breaking)*  
**public method Nest.ElasticClient.TasksCancelAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.TasksCancelAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.TasksList** *Removed (Breaking)*  
**public method Nest.ElasticClient.TasksList** *Removed (Breaking)*  
**public method Nest.ElasticClient.TasksListAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.TasksListAsync** *Removed (Breaking)*  
**public method Nest.ElasticClient.UpdateByQuery&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.ElasticClient.UpdateByQueryAsync&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.ElasticClient.WatcherInfo** *Removed (Breaking)*  
**public method Nest.ElasticClient.WatcherInfo** *Removed (Breaking)*    
**public method Nest.ElasticClient.WatcherInfoAsync** *Removed (Breaking)*
**public method Nest.ElasticClient.WatcherInfoAsync** *Removed (Breaking)*  
**public property Nest.ElasticsearchPropertyAttributeBase.DocValues** *Removed (Breaking)*    
**public property Nest.ElasticsearchPropertyAttributeBase.Similarity** *Removed (Breaking)*  
**public property Nest.ElasticsearchPropertyAttributeBase.Store** *Removed (Breaking)*  
**public method Nest.ExplainDescriptor&lt;TDocument&gt;.Fields** *Removed (Breaking)*  
**public method Nest.ExplainDescriptor&lt;TDocument&gt;.Fields** *Removed (Breaking)*  
**public property Nest.ExplainRequest&lt;TDocument&gt;.Fields** *Removed (Breaking)*  
**public property Nest.Field.CacheableExpression** *Removed (Breaking)*    
**public method Nest.Field.Create** *Removed (Breaking)*  
**public method Nest.Field.Create** *Removed (Breaking)*  
**public class Nest.GeoLocationSuggestContext** *Removed (Breaking)*  
**public class Nest.GeoLocationSuggestContextDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.GetDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.GetDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public property Nest.GetMappingResponse.IndexTypeMappings** *Removed (Breaking)*  
**public property Nest.GetRequest.Fields** *Removed (Breaking)*  
**public property Nest.GetRequest&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.GetScriptDescriptor.Version** *Removed (Breaking)*  
**public method Nest.GetScriptDescriptor.VersionType** *Removed (Breaking)*  
**public property Nest.GetScriptRequest.Version** *Removed (Breaking)*  
**public property Nest.GetScriptRequest.VersionType** *Removed (Breaking)*  
**public method Nest.GetSearchTemplateDescriptor.Version** *Removed (Breaking)*  
**public method Nest.GetSearchTemplateDescriptor.VersionType** *Removed (Breaking)*  
**public property Nest.GetSearchTemplateRequest.Version** *Removed (Breaking)*  
**public property Nest.GetSearchTemplateRequest.VersionType** *Removed (Breaking)*  
**public class Nest.GetWarmerDescriptor** *Removed (Breaking)*  
**public class Nest.GetWarmerRequest** *Removed (Breaking)*  
**public class Nest.GetWarmerResponse** *Removed (Breaking)*  
**public class Nest.GlobalInnerHit** *Removed (Breaking)*  
**public class Nest.GlobalInnerHitDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public property Nest.HasParentQuery.ScoreMode** *Removed (Breaking)*  
**public method Nest.HasParentQueryDescriptor&lt;T&gt;.ScoreMode** *Removed (Breaking)*  
**public class Nest.HighlightDocumentDictionary** *Removed (Breaking)*  
**public property Nest.HighlightField.CustomType** *Removed (Breaking)*  
**public method Nest.HighlightFieldDescriptor&lt;T&gt;.OnAll** *Removed (Breaking)*  
**public class Nest.HistogramBucket** *Removed (Breaking)*  
**public property Nest.IAllocateClusterRerouteCommand.AllowPrimary** *Removed (Breaking)*  
**public property Nest.IAttachmentProperty.FileField** *Removed (Breaking)*  
**public property Nest.IBoolQuery.CreatedByBoolDsl** *Removed (Breaking)*  
**public property Nest.IBulkAllRequest&lt;T&gt;.Consistency** *Removed (Breaking)*  
**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.InferFrom** *Removed (Breaking)*  
**public property Nest.ICategorySuggestContext.Default** *Removed (Breaking)*  
**public property Nest.ICompletionProperty.Context** *Removed (Breaking)*  
**public property Nest.ICompletionProperty.Payloads** *Removed (Breaking)*  
**public property Nest.ICompletionSuggester.Context** *Removed (Breaking)*  
**public property Nest.IDeleteByQueryResponse.Indices** *Removed (Breaking)*  
**public interface Nest.IDeleteWarmerRequest** *Removed (Breaking)*  
**public interface Nest.IDeleteWarmerResponse** *Removed (Breaking)*  
**public property Nest.IDirectGenerator.MinWordLen** *Removed (Breaking)*  
**public property Nest.IDirectGenerator.PrefixLen** *Removed (Breaking)*  
**public property Nest.IDynamicIndexSettings.RequestCacheEnabled** *Removed (Breaking)*  
**public property Nest.IDynamicIndexSettings.WarmersEnabled** *Removed (Breaking)*  
**public method Nest.IElasticClient.DeleteWarmer** *Removed (Breaking)*  
**public method Nest.IElasticClient.DeleteWarmer** *Removed (Breaking)*  
**public method Nest.IElasticClient.DeleteWarmerAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.DeleteWarmerAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.GetWarmer** *Removed (Breaking)*  
**public method Nest.IElasticClient.GetWarmer** *Removed (Breaking)*  
**public method Nest.IElasticClient.GetWarmerAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.GetWarmerAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.Optimize** *Removed (Breaking)*  
**public method Nest.IElasticClient.Optimize** *Removed (Breaking)*  
**public method Nest.IElasticClient.OptimizeAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.OptimizeAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.PutWarmer** *Removed (Breaking)*  
**public method Nest.IElasticClient.PutWarmer** *Removed (Breaking)*  
**public method Nest.IElasticClient.PutWarmerAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.PutWarmerAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.SearchExists** *Removed (Breaking)*  
**public method Nest.IElasticClient.SearchExists&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.IElasticClient.SearchExistsAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.IElasticClient.Suggest** *Removed (Breaking)*  
**public method Nest.IElasticClient.SuggestAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.TasksCancel** *Removed (Breaking)*  
**public method Nest.IElasticClient.TasksCancel** *Removed (Breaking)*  
**public method Nest.IElasticClient.TasksCancelAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.TasksCancelAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.TasksList** *Removed (Breaking)*  
**public method Nest.IElasticClient.TasksList** *Removed (Breaking)*  
**public method Nest.IElasticClient.TasksListAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.TasksListAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.UpdateByQuery&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.IElasticClient.UpdateByQueryAsync&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.IElasticClient.WatcherInfo** *Removed (Breaking)*  
**public method Nest.IElasticClient.WatcherInfo** *Removed (Breaking)*  
**public method Nest.IElasticClient.WatcherInfoAsync** *Removed (Breaking)*  
**public method Nest.IElasticClient.WatcherInfoAsync** *Removed (Breaking)*  
**public interface Nest.IGeoLocationSuggestContext** *Removed (Breaking)*  
**public property Nest.IGetMappingResponse.IndexTypeMappings** *Removed (Breaking)*  
**public interface Nest.IGetWarmerRequest** *Removed (Breaking)*  
**public interface Nest.IGetWarmerResponse** *Removed (Breaking)*  
**public interface Nest.IGlobalInnerHit** *Removed (Breaking)*  
**public property Nest.IHasParentQuery.ScoreMode** *Removed (Breaking)*  
**public property Nest.IIndexRequest.UntypedDocument** *Removed (Breaking)*  
**public property Nest.IIndexState.Warmers** *Removed (Breaking)*  
**public property Nest.IIndicesPrivileges.Fields** *Removed (Breaking)*  
**public interface Nest.IInnerHitsContainer** *Removed (Breaking)*  
**public property Nest.IMultiGetOperation.Fields** *Removed (Breaking)*  
**public property Nest.IMultiTermVectorOperation.Fields** *Removed (Breaking)*  
**public interface Nest.INamedInnerHits** *Removed (Breaking)*  
**public method Nest.IndexDescriptor&lt;TDocument&gt;.Consistency** *Removed (Breaking)*  
**public class Nest.IndexFieldMappings** *Removed (Breaking)*  
**public property Nest.IndexRequest&lt;TDocument&gt;.Consistency** *Removed (Breaking)*  
**public method Nest.IndexSettings..ctor** *Removed (Breaking)*  
**public property Nest.IndexState.Warmers** *Removed (Breaking)*  
**public property Nest.IndexStats.Percolate** *Removed (Breaking)*  
**public property Nest.IndexStats.Suggest** *Removed (Breaking)*  
**public property Nest.IndicesPrivileges.Fields** *Removed (Breaking)*  
**public method Nest.IndicesPrivilegesDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.IndicesPrivilegesDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.Infer.Fields&lt;T&gt;** *Removed (Breaking)*  
**public class Nest.InnerHitsContainer** *Removed (Breaking)*  
**public class Nest.InnerHitsContainerDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public interface Nest.IOptimizeRequest** *Removed (Breaking)*  
**public interface Nest.IOptimizeResponse** *Removed (Breaking)*  
**public interface Nest.IPathInnerHit** *Removed (Breaking)*  
**public property Nest.IProperty.CopyTo** *Removed (Breaking)*    
**public property Nest.IProperty.DocValues** *Removed (Breaking)*  
**public property Nest.IProperty.Fields** *Removed (Breaking)*  
**public property Nest.IProperty.Similarity** *Removed (Breaking)*  
**public property Nest.IProperty.Store** *Removed (Breaking)*  
**public interface Nest.IPutWarmerRequest** *Removed (Breaking)*  
**public interface Nest.IPutWarmerResponse** *Removed (Breaking)*  
**public interface Nest.IReindexRequest** *Removed (Breaking)*  
**public interface Nest.IReindexResponse&lt;T&gt;** *Removed (Breaking)*  
**public property Nest.IResponse.ApiCall** *Removed (Breaking)*  
**public property Nest.ISamplerAggregation.Field** *Removed (Breaking)*  
**public interface Nest.ISearchExistsRequest** *Removed (Breaking)*  
**public interface Nest.ISearchExistsRequest&lt;T&gt;** *Removed (Breaking)*  
**public property Nest.ISearchRequest.Fields** *Removed (Breaking)*    
**public property Nest.ISearchRequest.InnerHits** *Removed (Breaking)*  
**public property Nest.ISearchResponse&lt;T&gt;.Highlights** *Removed (Breaking)*  
**public property Nest.ISearchTemplateRequest.Template** *Removed (Breaking)*
**public interface Nest.ISuggestContextMapping** *Removed (Breaking)*  
**public property Nest.ISuggester.ShardSize** *Removed (Breaking)*  
**public property Nest.ISuggester.Text** *Removed (Breaking)*  
**public interface Nest.ISuggestResponse** *Removed (Breaking)*  
**public interface Nest.ITasksCancelRequest** *Removed (Breaking)*  
**public interface Nest.ITasksCancelResponse** *Removed (Breaking)*  
**public interface Nest.ITasksListRequest** *Removed (Breaking)*  
**public interface Nest.ITasksListResponse** *Removed (Breaking)*  
**public property Nest.ITemplateMapping.Warmers** *Removed (Breaking)*  
**public property Nest.ITermsAggregation.ShowTermDocumentCountError** *Removed (Breaking)*  
**public property Nest.ITermSuggester.MinWordLen** *Removed (Breaking)*  
**public property Nest.ITermSuggester.PrefixLen** *Removed (Breaking)*  
**public property Nest.ITermVectorsResponse.Found** *Removed (Breaking)*  
**public property Nest.ITermVectorsResponse.Id** *Removed (Breaking)*    
**public property Nest.ITermVectorsResponse.Index** *Removed (Breaking)*  
**public property Nest.ITermVectorsResponse.TermVectors** *Removed (Breaking)*  
**public property Nest.ITermVectorsResponse.Type** *Removed (Breaking)*  
**public property Nest.ITermVectorsResponse.Version** *Removed (Breaking)*  
**public property Nest.ITranslogFlushSettings.ThresholdOps** *Removed (Breaking)*  
**public property Nest.ITranslogSettings.FileSystemType** *Removed (Breaking)*    
**public interface Nest.ITypeInnerHit** *Removed (Breaking)*  
**public interface Nest.IWarmer** *Removed (Breaking)*    
**public interface Nest.IWarmers** *Removed (Breaking)*  
**public interface Nest.IWatcherInfoRequest** *Removed (Breaking)*  
**public interface Nest.IWatcherInfoResponse** *Removed (Breaking)*  
**public method Nest.MultiGetDescriptor.Fields** *Removed (Breaking)*  
**public method Nest.MultiGetDescriptor.Fields&lt;T&gt;** *Removed (Breaking)*  
**public property Nest.MultiGetOperation&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.MultiGetOperationDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.MultiGetOperationDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public property Nest.MultiGetRequest.Fields** *Removed (Breaking)*  
**public property Nest.MultiTermVectorOperation&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.MultiTermVectorOperationDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.MultiTermVectorOperationDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public class Nest.NamedInnerHits** *Removed (Breaking)*  
**public class Nest.NamedInnerHitsDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public property Nest.NodeInfo.Build** *Removed (Breaking)*  
**public property Nest.NodeInfo.Hostname** *Removed (Breaking)*  
**public property Nest.NodeInfo.HttpAddress** *Removed (Breaking)*  
**public property Nest.NodeProcessInfo.RefreshInterval** *Removed (Breaking)*  
**public enum Nest.NumericResolutionUnit** *Removed (Breaking)*  
**public property Nest.ObjectAttribute.Dynamic** *Removed (Breaking)*  
**public property Nest.OperatingSystemStats.CpuPercent** *Removed (Breaking)*  
**public property Nest.OperatingSystemStats.LoadAverage** *Removed (Breaking)*    
**public class Nest.OptimizeDescriptor** *Removed (Breaking)*  
**public class Nest.OptimizeRequest** *Removed (Breaking)*  
**public class Nest.OptimizeResponse** *Removed (Breaking)*  
**public enum Nest.ParentScoreMode** *Removed (Breaking)*  
**public class Nest.PathInnerHit** *Removed (Breaking)*  
**public class Nest.PathInnerHit&lt;T&gt;** *Removed (Breaking)*  
**public class Nest.PathInnerHitDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public class Nest.PercolateStats** *Removed (Breaking)*  
**public property Nest.PropertyBase.CopyTo** *Removed (Breaking)*  
**public property Nest.PropertyBase.DocValues** *Removed (Breaking)*  
**public property Nest.PropertyBase.Fields** *Removed (Breaking)*  
**public property Nest.PropertyBase.IndexName** *Removed (Breaking)*    
**public property Nest.PropertyBase.Similarity** *Removed (Breaking)*  
**public property Nest.PropertyBase.Store** *Removed (Breaking)*  
**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.CopyTo** *Removed (Breaking)*  
**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.DocValues** *Removed (Breaking)*  
**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Similarity** *Removed (Breaking)*    
**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Similarity** *Removed (Breaking)*  
**public method Nest.PropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Store** *Removed (Breaking)*  
**public method Nest.PutIndexTemplateDescriptor.Warmers** *Removed (Breaking)*    
**public property Nest.PutIndexTemplateRequest.Warmers** *Removed (Breaking)*  
**public method Nest.PutMappingDescriptor&lt;T&gt;.Parent&lt;K&gt;** *Removed (Breaking)*    
**public method Nest.PutScriptDescriptor.OpType** *Removed (Breaking)*  
**public method Nest.PutScriptDescriptor.Version** *Removed (Breaking)*  
**public method Nest.PutScriptDescriptor.VersionType** *Removed (Breaking)*  
**public property Nest.PutScriptRequest.OpType** *Removed (Breaking)*    
**public property Nest.PutScriptRequest.Version** *Removed (Breaking)*  
**public property Nest.PutScriptRequest.VersionType** *Removed (Breaking)*    
**public method Nest.PutSearchTemplateDescriptor.OpType** *Removed (Breaking)*  
**public method Nest.PutSearchTemplateDescriptor.Version** *Removed (Breaking)*    
**public method Nest.PutSearchTemplateDescriptor.VersionType** *Removed (Breaking)*  
**public property Nest.PutSearchTemplateRequest.OpType** *Removed (Breaking)*  
**public property Nest.PutSearchTemplateRequest.Version** *Removed (Breaking)*    
**public property Nest.PutSearchTemplateRequest.VersionType** *Removed (Breaking)*    
**public class Nest.PutWarmerDescriptor** *Removed (Breaking)*  
**public class Nest.PutWarmerRequest** *Removed (Breaking)*  
**public class Nest.PutWarmerResponse** *Removed (Breaking)*  
**public property Nest.QueryProfile.Lucene** *Removed (Breaking)*  
**public property Nest.QueryProfile.QueryType** *Removed (Breaking)*  
**public method Nest.ReindexDescriptor&lt;T&gt;.AllTypes** *Removed (Breaking)*  
**public method Nest.ReindexDescriptor&lt;T&gt;.CreateIndex** *Removed (Breaking)*  
**public method Nest.ReindexDescriptor&lt;T&gt;.Query** *Removed (Breaking)*  
**public method Nest.ReindexDescriptor&lt;T&gt;.Query** *Removed (Breaking)*  
**public method Nest.ReindexDescriptor&lt;T&gt;.Scroll** *Removed (Breaking)*  
**public method Nest.ReindexDescriptor&lt;T&gt;.Size** *Removed (Breaking)*    
**public method Nest.ReindexDescriptor&lt;T&gt;.Take** *Removed (Breaking)*  
**public method Nest.ReindexDescriptor&lt;T&gt;.Type** *Removed (Breaking)*  
**public method Nest.ReindexObservable&lt;T&gt;.IndexSearchResults** *Removed (Breaking)*  
**public method Nest.ReindexOnServerDescriptor.Consistency** *Removed (Breaking)*  
**public property Nest.ReindexOnServerRequest.Consistency** *Removed (Breaking)*  
**public class Nest.ReindexRequest** *Removed (Breaking)*  
**public class Nest.ReindexResponse&lt;T&gt;** *Removed (Breaking)*  
**public class Nest.Role** *Removed (Breaking)*  
**public property Nest.RoutingShard.Version** *Removed (Breaking)*  
**public property Nest.SamplerAggregation.Field** *Removed (Breaking)*  
**public method Nest.SamplerAggregationDescriptor&lt;T&gt;.Field** *Removed (Breaking)*  
**public method Nest.SamplerAggregationDescriptor&lt;T&gt;.Field** *Removed (Breaking)*    
**public method Nest.SearchDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*
**public method Nest.SearchDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.SearchDescriptor&lt;T&gt;.InnerHits** *Removed (Breaking)*  
**public class Nest.SearchExistsDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public class Nest.SearchExistsRequest** *Removed (Breaking)*  
**public class Nest.SearchExistsRequest&lt;T&gt;** *Removed (Breaking)*  
**public property Nest.SearchRequest.Fields** *Removed (Breaking)*  
**public property Nest.SearchRequest.InnerHits** *Removed (Breaking)*    
**public property Nest.SearchRequest&lt;T&gt;.Fields** *Removed (Breaking)*  
**public property Nest.SearchRequest&lt;T&gt;.InnerHits** *Removed (Breaking)*  
**public property Nest.SearchStats.FetchTime** *Removed (Breaking)*  
**public property Nest.SearchStats.QueryTime** *Removed (Breaking)*  
**public property Nest.SearchStats.ScrollTime** *Removed (Breaking)*  
**public method Nest.SearchTemplateDescriptor&lt;T&gt;.Template** *Removed (Breaking)*  
**public property Nest.SearchTemplateRequest.Template** *Removed (Breaking)*  
**public property Nest.ShardStore.Version** *Removed (Breaking)*  
**public class Nest.ShieldNode** *Removed (Breaking)*  
**public class Nest.ShieldNodeStatus** *Removed (Breaking)*  
**public property Nest.Snapshot.ShardFailures** *Removed (Breaking)*  
**public class Nest.Suggest** *Removed (Breaking)*  
**public class Nest.SuggestContextMapping** *Removed (Breaking)*  
**public class Nest.SuggestContextMappingDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public method Nest.SuggestDescriptorBase&lt;TDescriptor, TInterface, T&gt;.ShardSize** *Removed (Breaking)*  
**public method Nest.SuggestDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Text** *Removed (Breaking)*  
**public property Nest.SuggesterBase.ShardSize** *Removed (Breaking)*  
**public property Nest.SuggesterBase.Text** *Removed (Breaking)*  
**public class Nest.SuggestOption** *Removed (Breaking)*  
**public class Nest.SuggestOptionJsonConverter** *Removed (Breaking)*  
**public class Nest.SuggestResponse** *Removed (Breaking)*  
**public class Nest.SuggestStats** *Removed (Breaking)*  
**public class Nest.TasksCancelDescriptor** *Removed (Breaking)*  
**public class Nest.TasksCancelRequest** *Removed (Breaking)*  
**public class Nest.TasksCancelResponse** *Removed (Breaking)*  
**public class Nest.TasksListDescriptor** *Removed (Breaking)*  
**public class Nest.TasksListRequest** *Removed (Breaking)*  
**public class Nest.TasksListResponse** *Removed (Breaking)*    
**public property Nest.TemplateMapping.Warmers** *Removed (Breaking)*  
**public property Nest.TermsAggregation.ShowTermDocumentCountError** *Removed (Breaking)*  
**public method Nest.TermsAggregationDescriptor&lt;T&gt;.ShowTermDocumentCountError** *Removed (Breaking)*  
**public property Nest.TermSuggester.MinWordLen** *Removed (Breaking)*  
**public property Nest.TermSuggester.PrefixLen** *Removed (Breaking)*  
**public method Nest.TermVectorsDescriptor&lt;TDocument&gt;.Dfs** *Removed (Breaking)*  
**public property Nest.TermVectorsRequest&lt;TDocument&gt;.Dfs** *Removed (Breaking)*  
**public property Nest.TranslogFlushSettings.ThresholdOps** *Removed (Breaking)*    
**public method Nest.TranslogFlushSettingsDescriptor.ThresholdOps** *Removed (Breaking)*
**public property Nest.TranslogSettings.FileSystemType** *Removed (Breaking)*  
**public method Nest.TranslogSettingsDescriptor.FileSystemType** *Removed (Breaking)*  
**public enum Nest.TranslogWriteMode** *Removed (Breaking)*  
**public class Nest.TypeInnerHit** *Removed (Breaking)*  
**public class Nest.TypeInnerHit&lt;T&gt;** *Removed (Breaking)*  
**public class Nest.TypeInnerHitDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public field Nest.UpdatableIndexSettings.RequestCacheEnable** *Removed (Breaking)*  
**public field Nest.UpdatableIndexSettings.TranslogFlushTreshHoldOps** *Removed (Breaking)*    
**public field Nest.UpdatableIndexSettings.TranslogFsType** *Removed (Breaking)*  
**public field Nest.UpdatableIndexSettings.TranslogInterval** *Removed (Breaking)*  
**public field Nest.UpdatableIndexSettings.WarmersEnabled** *Removed (Breaking)*  
**public method Nest.UpdateByQueryDescriptor&lt;T&gt;.Consistency** *Removed (Breaking)*  
**public method Nest.UpdateByQueryDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.UpdateByQueryDescriptor&lt;T&gt;.Fields** *Removed (Breaking)*  
**public property Nest.UpdateByQueryRequest.Consistency** *Removed (Breaking)*  
**public property Nest.UpdateByQueryRequest.Fields** *Removed (Breaking)*  
**public property Nest.UpdateByQueryRequest&lt;T&gt;.Consistency** *Removed (Breaking)*  
**public property Nest.UpdateByQueryRequest&lt;T&gt;.Fields** *Removed (Breaking)*  
**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Consistency** *Removed (Breaking)*  
**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Consistency** *Removed (Breaking)*  
**public method Nest.UpgradeDescriptor.AllowNoIndices** *Removed (Breaking)*  
**public property Nest.UpgradeRequest.AllowNoIndices** *Removed (Breaking)*  
**public class Nest.User** *Removed (Breaking)*  
**public class Nest.Warmer** *Removed (Breaking)*  
**public class Nest.WarmerDescriptor&lt;T&gt;** *Removed (Breaking)*  
**public class Nest.Warmers** *Removed (Breaking)*  
**public class Nest.WarmersDescriptor** *Removed (Breaking)*  
**public class Nest.WatcherInfoDescriptor** *Removed (Breaking)*  
**public class Nest.WatcherInfoRequest** *Removed (Breaking)*  
**public class Nest.WatcherInfoResponse** *Removed (Breaking)*  
**public class Nest.WatcherVersion** *Removed (Breaking)*  
  
