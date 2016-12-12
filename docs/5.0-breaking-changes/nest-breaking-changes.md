#Unknown
**public property Nest.AggregationContainer.Stats** *Declaration changed (Breaking)*

2.x: `public IStatsAggregator Stats { get; set; }`  
5.x: `public IStatsAggregator Stats { get; set; }`  

**public method Nest.AggregationContainerDescriptor&lt;T&gt;.Stats** *Declaration changed (Breaking)*

2.x: `public AggregationContainerDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, IStatsAggregator> selector)`  
5.x: `public AggregationContainerDescriptor<T> Stats(string name, Func<StatsAggregationDescriptor<T>, IStatsAggregator> selector)`  

**public method Nest.AggregationsHelper..ctor** *Visibility was changed from public to protected (Breaking)*

2.x: `public  .ctor(IDictionary<string, IAggregate> aggregations)`  
5.x: `public  .ctor(IDictionary<string, IAggregate> aggregations)`  

**public method Nest.AggregationsHelper.GeoHash** *Declaration changed (Breaking)*

2.x: `public MultiBucketAggregate<KeyedBucket> GeoHash(string key)`  
5.x: `public MultiBucketAggregate<KeyedBucket> GeoHash(string key)`  

**public method Nest.AggregationsHelper.Histogram** *Declaration changed (Breaking)*

2.x: `public MultiBucketAggregate<HistogramBucket> Histogram(string key)`  
5.x: `public MultiBucketAggregate<HistogramBucket> Histogram(string key)`  

**public method Nest.AggregationsHelper.Terms** *Declaration changed (Breaking)*

2.x: `public TermsAggregate Terms(string key)`  
5.x: `public TermsAggregate Terms(string key)`  

**public method Nest.AliasPointingToIndexExtensions.GetAliasesPointingToIndex** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static IList<AliasDefinition> GetAliasesPointingToIndex(IElasticClient client, string indexName)
```

5.x
```csharp
[ExtensionAttribute]
public static IEnumerable<AliasDefinition> GetAliasesPointingToIndex(IElasticClient client, Indices indices)
```

**public method Nest.AliasPointingToIndexExtensions.GetAliasesPointingToIndexAsync** *Declaration changed (Breaking)*

2.x
```csharp
[AsyncStateMachineAttribute(Nest.AliasPointingToIndexExtensions+<GetAliasesPointingToIndexAsync>d__1)]
[ExtensionAttribute]
public static Task<IList<AliasDefinition>> GetAliasesPointingToIndexAsync(IElasticClient client, string indexName)
```

5.x
```csharp
[AsyncStateMachineAttribute(Nest.AliasPointingToIndexExtensions+<GetAliasesPointingToIndexAsync>d__1)]
[ExtensionAttribute]
public static Task<IEnumerable<AliasDefinition>> GetAliasesPointingToIndexAsync(IElasticClient client, Indices indices)
```

**public property Nest.AttachmentProperty.AuthorField** *Declaration changed (Breaking)*

2.x: `public IStringProperty AuthorField { get; set; }`  
5.x: `public IStringProperty AuthorField { get; set; }`  

**public property Nest.AttachmentProperty.ContentTypeField** *Declaration changed (Breaking)*

2.x: `public IStringProperty ContentTypeField { get; set; }`  
5.x: `public IStringProperty ContentTypeField { get; set; }`  

**public property Nest.AttachmentProperty.KeywordsField** *Declaration changed (Breaking)*

2.x: `public IStringProperty KeywordsField { get; set; }`  
5.x: `public IStringProperty KeywordsField { get; set; }`  

**public property Nest.AttachmentProperty.LanguageField** *Declaration changed (Breaking)*

2.x: `public IStringProperty LanguageField { get; set; }`  
5.x: `public IStringProperty LanguageField { get; set; }`  

**public property Nest.AttachmentProperty.NameField** *Declaration changed (Breaking)*

2.x: `public IStringProperty NameField { get; set; }`  
5.x: `public IStringProperty NameField { get; set; }`  

**public property Nest.AttachmentProperty.TitleField** *Declaration changed (Breaking)*

2.x: `public IStringProperty TitleField { get; set; }`  
5.x: `public IStringProperty TitleField { get; set; }`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.AuthorField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> AuthorField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> AuthorField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.ContentTypeField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> ContentTypeField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> ContentTypeField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.FileField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> FileField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> FileField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.KeywordsField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> KeywordsField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> KeywordsField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  

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
5.x: `public AttachmentPropertyDescriptor<T> NameField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  

**public method Nest.AttachmentPropertyDescriptor&lt;T&gt;.TitleField** *Declaration changed (Breaking)*

2.x: `public AttachmentPropertyDescriptor<T> TitleField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  
5.x: `public AttachmentPropertyDescriptor<T> TitleField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)`  

**public property Nest.BooleanAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public NonStringIndexOption Index { get; set; }`  

**public property Nest.BooleanProperty.Index** *Declaration changed (Breaking)*

2.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  
5.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  

**public method Nest.BooleanPropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public BooleanPropertyDescriptor<T> Index(NonStringIndexOption index = 0)`  
5.x: `public BooleanPropertyDescriptor<T> Index(NonStringIndexOption index = 0)`  

**public method Nest.BoolQueryDescriptor&lt;T&gt;.DisableCoord** *Declaration changed (Breaking)*

2.x: `public BoolQueryDescriptor<T> DisableCoord()`  
5.x: `public BoolQueryDescriptor<T> DisableCoord()`  

**public class Nest.BucketsPathJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class BucketsPathJsonConverter : JsonConverter`  
5.x: `public class BucketsPathJsonConverter : JsonConverter`  

**public method Nest.BulkAllDescriptor&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public BulkAllDescriptor<T> Refresh(bool refresh = True)`  
5.x: `public BulkAllDescriptor<T> Refresh(bool refresh = True)`  

**public property Nest.BulkAllRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool? Refresh { get; set; }`  
5.x: `public bool? Refresh { get; set; }`  

**public method Nest.BulkDescriptor.Index&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, IIndexOperation<T>> bulkIndexSelector)`  
5.x: `public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, IIndexOperation<T>> bulkIndexSelector)`  

**public method Nest.BulkDescriptor.IndexMany&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public BulkDescriptor IndexMany<T>(IEnumerable<T> objects, Func<BulkIndexDescriptor<T>, T, IIndexOperation<T>> bulkIndexSelector)`  
5.x: `public BulkDescriptor IndexMany<T>(IEnumerable<T> objects, Func<BulkIndexDescriptor<T>, T, IIndexOperation<T>> bulkIndexSelector)`  

**public method Nest.BulkDescriptor.Refresh** *Declaration changed (Breaking)*

2.x: `public BulkDescriptor Refresh(bool refresh = True)`  
5.x: `public BulkDescriptor Refresh(bool refresh = True)`  

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

**public property Nest.BulkRequest.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public bool Refresh { get; set; }`  

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

**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(string script)`  
5.x: `public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(string script)`  

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public string Script { get; set; }`  
5.x: `public string Script { get; set; }`  

**public property Nest.CatThreadPoolRecord.Port** *Declaration changed (Breaking)*

2.x
```csharp
public string Port { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("port")]
public int Port { get; set; }
```

**public method Nest.ClusterHealthDescriptor.WaitForActiveShards** *Declaration changed (Breaking)*

2.x: `public ClusterHealthDescriptor WaitForActiveShards(long wait_for_active_shards)`  
5.x: `public ClusterHealthDescriptor WaitForActiveShards(long wait_for_active_shards)`  

**public property Nest.ClusterHealthRequest.WaitForActiveShards** *Declaration changed (Breaking)*

2.x: `public long WaitForActiveShards { get; set; }`  
5.x: `public long WaitForActiveShards { get; set; }`  

**public property Nest.ClusterRerouteResponse.State** *Visibility changed (Breaking)*

2.x: `public ClusterRerouteState State { get; set; }`  
5.x: `public ClusterRerouteState State { get; set; }`  

**public property Nest.ClusterStatsResponse.ClusterName** *Visibility changed (Breaking)*

2.x: `public string ClusterName { get; set; }`  
5.x: `public string ClusterName { get; set; }`  

**public property Nest.ClusterStatsResponse.Indices** *Visibility changed (Breaking)*

2.x: `public ClusterIndicesStats Indices { get; set; }`  
5.x: `public ClusterIndicesStats Indices { get; set; }`  

**public property Nest.ClusterStatsResponse.Nodes** *Visibility changed (Breaking)*

2.x: `public ClusterNodesStats Nodes { get; set; }`  
5.x: `public ClusterNodesStats Nodes { get; set; }`  

**public property Nest.ClusterStatsResponse.Status** *Visibility changed (Breaking)*

2.x: `public ClusterStatus Status { get; set; }`  
5.x: `public ClusterStatus Status { get; set; }`  

**public property Nest.ClusterStatsResponse.Timestamp** *Visibility changed (Breaking)*

2.x: `public long Timestamp { get; set; }`  
5.x: `public long Timestamp { get; set; }`  

**public method Nest.CreateIndexRequest..ctor** *Visibility was changed from public to internal (Breaking)*

2.x: `public  .ctor()`  
5.x: `public  .ctor()`  

**public property Nest.DateAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public NonStringIndexOption Index { get; set; }`  

**public method Nest.DateHistogramAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public DateHistogramAggregationDescriptor<T> Field(string field)`  
5.x: `public DateHistogramAggregationDescriptor<T> Field(string field)`  

**public property Nest.DateProperty.Index** *Declaration changed (Breaking)*

2.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  
5.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  

**public method Nest.DatePropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public DatePropertyDescriptor<T> Index(NonStringIndexOption index = 0)`  
5.x: `public DatePropertyDescriptor<T> Index(NonStringIndexOption index = 0)`  

**public method Nest.DateRangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public DateRangeAggregationDescriptor<T> Field(string field)`  
5.x: `public DateRangeAggregationDescriptor<T> Field(string field)`  

**public method Nest.DecayFunctionDescriptorBase&lt;TDescriptor, TOrigin, TScale, T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public TDescriptor Field(string field)`  
5.x: `public TDescriptor Field(string field)`  

**public method Nest.DeleteByQueryDescriptor&lt;T&gt;.Routing** *Declaration changed (Breaking)*

2.x: `public DeleteByQueryDescriptor<T> Routing(string routing)`  
5.x: `public DeleteByQueryDescriptor<T> Routing(string routing)`  

**public property Nest.DeleteByQueryRequest.Routing** *Declaration changed (Breaking)*

2.x: `public string Routing { get; set; }`  
5.x: `public string Routing { get; set; }`  

**public property Nest.DeleteByQueryRequest&lt;T&gt;.Routing** *Declaration changed (Breaking)*

2.x: `public string Routing { get; set; }`  
5.x: `public string Routing { get; set; }`  

**public method Nest.DeleteDescriptor&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public DeleteDescriptor<T> Refresh(bool refresh = True)`  
5.x: `public DeleteDescriptor<T> Refresh(bool refresh = True)`  

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

**public property Nest.DeleteRequest.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public bool Refresh { get; set; }`  

**public property Nest.DeleteRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public bool Refresh { get; set; }`  

**public class Nest.DictionaryResponseJsonConverter&lt;TResponse, TKey, TValue&gt;** *Visibility was changed from public to internal (Breaking)*

2.x: `public class DictionaryResponseJsonConverter<TResponse, TKey, TValue> : JsonConverter where TResponse : new(), IDictionaryResponse<TKey, TValue>`  
5.x: `public class DictionaryResponseJsonConverter<TResponse, TKey, TValue> : JsonConverter where TResponse : new(), IDictionaryResponse<TKey, TValue>`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ITemplateQuery query)`  
5.x: `public void Visit(ITemplateQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoShapeMultiPolygonQuery query)`  
5.x: `public void Visit(IGeoShapeMultiPolygonQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoShapePolygonQuery query)`  
5.x: `public void Visit(IGeoShapePolygonQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ISpanMultiTermQuery query)`  
5.x: `public void Visit(ISpanMultiTermQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoShapeMultiPointQuery query)`  
5.x: `public void Visit(IGeoShapeMultiPointQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoShapePointQuery query)`  
5.x: `public void Visit(IGeoShapePointQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ILimitQuery query)`  
5.x: `public void Visit(ILimitQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(IFilteredQuery query)`  
5.x: `public virtual void Visit(IFilteredQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IScriptQuery filter)`  
5.x: `public void Visit(IScriptQuery filter)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IRawQuery filter)`  
5.x: `public void Visit(IRawQuery filter)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(INotQuery query)`  
5.x: `public void Visit(INotQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoShapeEnvelopeQuery query)`  
5.x: `public void Visit(IGeoShapeEnvelopeQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ISpanSubQuery query)`  
5.x: `public void Visit(ISpanSubQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoShapeMultiLineStringQuery query)`  
5.x: `public void Visit(IGeoShapeMultiLineStringQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoShapeLineStringQuery query)`  
5.x: `public void Visit(IGeoShapeLineStringQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ISpanQuery query)`  
5.x: `public void Visit(ISpanQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoShapeCircleQuery query)`  
5.x: `public void Visit(IGeoShapeCircleQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IConditionlessQuery query)`  
5.x: `public void Visit(IConditionlessQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(INumericRangeQuery query)`  
5.x: `public void Visit(INumericRangeQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ITermRangeQuery query)`  
5.x: `public void Visit(ITermRangeQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ISpanWithinQuery query)`  
5.x: `public void Visit(ISpanWithinQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IDateRangeQuery query)`  
5.x: `public void Visit(IDateRangeQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IFuzzyNumericQuery query)`  
5.x: `public void Visit(IFuzzyNumericQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IFuzzyDateQuery query)`  
5.x: `public void Visit(IFuzzyDateQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ISpanContainingQuery query)`  
5.x: `public void Visit(ISpanContainingQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IGeoIndexedShapeQuery query)`  
5.x: `public void Visit(IGeoIndexedShapeQuery query)`  

**public method Nest.DslPrettyPrintVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IFuzzyStringQuery query)`  
5.x: `public void Visit(IFuzzyStringQuery query)`  

**public property Nest.DynamicIndexSettings.AutoExpandReplicas** *Declaration changed (Breaking)*

2.x: `public string AutoExpandReplicas { get; set; }`  
5.x: `public string AutoExpandReplicas { get; set; }`  

**public method Nest.DynamicIndexSettingsDescriptorBase&lt;TDescriptor, TIndexSettings&gt;.AutoExpandReplicas** *Declaration changed (Breaking)*

2.x: `public TDescriptor AutoExpandReplicas(string AutoExpandReplicas)`  
5.x: `public TDescriptor AutoExpandReplicas(string AutoExpandReplicas)`  

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

**public method Nest.ElasticClient.DeleteByQuery&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  
5.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  

**public method Nest.ElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(IGetAliasRequest request)`  
5.x: `public IGetAliasesResponse GetAlias(IGetAliasRequest request)`  

**public method Nest.ElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  
5.x: `public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  

**public method Nest.ElasticClient.Suggest&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  
5.x: `public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  

**public property Nest.ExecuteWatchResponse.Id** *Declaration changed (Breaking)*

2.x: `public Id Id { get; set; }`  
5.x: `public Id Id { get; set; }`  

**public method Nest.ExtendedStatsBucketAggregationDescriptor.Sigma** *Declaration changed (Breaking)*

2.x: `public ExtendedStatsBucketAggregationDescriptor Sigma(double? sigma)`  
5.x: `public ExtendedStatsBucketAggregationDescriptor Sigma(double? sigma)`  

**public method Nest.Field..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor()`  
5.x: `public  .ctor()`  

**public method Nest.Field.And** *Declaration changed (Breaking)*

2.x: `public Fields And(string field)`  
5.x: `public Fields And(string field)`  

**public method Nest.Field.And&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Fields And<T>(Expression<Func<T, object>> field)`  
5.x: `public Fields And<T>(Expression<Func<T, object>> field)`  

**public property Nest.Field.Expression** *Declaration changed (Breaking)*

2.x: `public Expression Expression { get; set; }`  
5.x: `public Expression Expression { get; set; }`  

**public property Nest.Field.Name** *Declaration changed (Breaking)*

2.x: `public string Name { get; set; }`  
5.x: `public string Name { get; set; }`  

**public property Nest.Field.Property** *Declaration changed (Breaking)*

2.x: `public PropertyInfo Property { get; set; }`  
5.x: `public PropertyInfo Property { get; set; }`  

**public property Nest.FieldMapping.FullName** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("full_name")]
public string FullName { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("full_name")]
public string FullName { get; internal set; }
```

**public property Nest.FieldStatsField.Density** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("density")]
public long Density { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("density")]
public long Density { get; internal set; }
```

**public property Nest.FieldStatsField.DocCount** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("doc_count")]
public long DocCount { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("doc_count")]
public long DocCount { get; internal set; }
```

**public property Nest.FieldStatsField.MaxDoc** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("max_doc")]
public long MaxDoc { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("max_doc")]
public long MaxDoc { get; internal set; }
```

**public property Nest.FieldStatsField.MaxValue** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("max_value")]
public string MaxValue { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("max_value")]
public string MaxValue { get; internal set; }
```

**public property Nest.FieldStatsField.MinValue** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("min_value")]
public string MinValue { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("min_value")]
public string MinValue { get; internal set; }
```

**public property Nest.FieldStatsField.SumDocumentFrequency** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("sum_doc_freq")]
public long SumDocumentFrequency { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("sum_doc_freq")]
public long SumDocumentFrequency { get; internal set; }
```

**public property Nest.FieldStatsField.SumTotalTermFrequency** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("sum_total_term_freq")]
public long SumTotalTermFrequency { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("sum_total_term_freq")]
public long SumTotalTermFrequency { get; internal set; }
```

**public property Nest.FieldStatsResponse.Shards** *Visibility changed (Breaking)*

2.x: `public ShardsMetaData Shards { get; set; }`  
5.x: `public ShardsMetaData Shards { get; set; }`  

**public property Nest.GenericProperty.Norms** *Declaration changed (Breaking)*

2.x: `public INorms Norms { get; set; }`  
5.x: `public INorms Norms { get; set; }`  

**public method Nest.GenericPropertyDescriptor&lt;T&gt;.Norms** *Declaration changed (Breaking)*

2.x: `public GenericPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector)`  
5.x: `public GenericPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector)`  

**public method Nest.GeoDistanceAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public GeoDistanceAggregationDescriptor<T> Field(string field)`  
5.x: `public GeoDistanceAggregationDescriptor<T> Field(string field)`  

**public method Nest.GeoHashGridAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public GeoHashGridAggregationDescriptor<T> Field(string field)`  
5.x: `public GeoHashGridAggregationDescriptor<T> Field(string field)`  

**public class Nest.GeoShapeQueryDescriptorBase&lt;TDescriptor, TInterface, T&gt;** *Declaration changed (Breaking)*

2.x: `public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T> : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IDescriptor, IQuery, IFieldNameQuery, IGeoShapeQuery where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface where TInterface : class, IGeoShapeQuery`  
5.x: `public abstract class GeoShapeQueryDescriptorBase<TDescriptor, TInterface, T> : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, IDescriptor, IQuery, IFieldNameQuery, IGeoShapeQuery where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface where TInterface : class, IGeoShapeQuery`  

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

**public property Nest.GetSearchTemplateResponse.Template** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("template")]
public string Template { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("template")]
public string Template { get; internal set; }
```

**public property Nest.GetWatchResponse.Id** *Declaration changed (Breaking)*

2.x: `public Id Id { get; internal set; }`  
5.x: `public Id Id { get; internal set; }`  

**public property Nest.HighlightField.Type** *Declaration changed (Breaking)*

2.x: `public Nullable<HighlighterType> Type { get; set; }`  
5.x: `public Nullable<HighlighterType> Type { get; set; }`  

**public method Nest.HighlightFieldDescriptor&lt;T&gt;.PostTags** *Declaration changed (Breaking)*

2.x: `public HighlightFieldDescriptor<T> PostTags(string postTags)`  
5.x: `public HighlightFieldDescriptor<T> PostTags(string postTags)`  

**public method Nest.HighlightFieldDescriptor&lt;T&gt;.PreTags** *Declaration changed (Breaking)*

2.x: `public HighlightFieldDescriptor<T> PreTags(string preTags)`  
5.x: `public HighlightFieldDescriptor<T> PreTags(string preTags)`  

**public method Nest.HistogramAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public HistogramAggregationDescriptor<T> Field(string field)`  
5.x: `public HistogramAggregationDescriptor<T> Field(string field)`  

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

**public property Nest.HotThreadInformation.NodeId** *Visibility changed (Breaking)*

2.x: `public string NodeId { get; set; }`  
5.x: `public string NodeId { get; set; }`  

**public property Nest.HotThreadInformation.NodeName** *Visibility changed (Breaking)*

2.x: `public string NodeName { get; set; }`  
5.x: `public string NodeName { get; set; }`  

**public property Nest.IAggregationContainer.Stats** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("stats")]
public IStatsAggregator Stats { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("stats")]
public IStatsAggregation Stats { get; set; }
```

**public property Nest.IAttachmentProperty.AuthorField** *Declaration changed (Breaking)*

2.x: `public IStringProperty AuthorField { get; set; }`  
5.x: `public IStringProperty AuthorField { get; set; }`  

**public property Nest.IAttachmentProperty.ContentTypeField** *Declaration changed (Breaking)*

2.x: `public IStringProperty ContentTypeField { get; set; }`  
5.x: `public IStringProperty ContentTypeField { get; set; }`  

**public property Nest.IAttachmentProperty.KeywordsField** *Declaration changed (Breaking)*

2.x: `public IStringProperty KeywordsField { get; set; }`  
5.x: `public IStringProperty KeywordsField { get; set; }`  

**public property Nest.IAttachmentProperty.LanguageField** *Declaration changed (Breaking)*

2.x: `public IStringProperty LanguageField { get; set; }`  
5.x: `public IStringProperty LanguageField { get; set; }`  

**public property Nest.IAttachmentProperty.NameField** *Declaration changed (Breaking)*

2.x: `public IStringProperty NameField { get; set; }`  
5.x: `public IStringProperty NameField { get; set; }`  

**public property Nest.IAttachmentProperty.TitleField** *Declaration changed (Breaking)*

2.x: `public IStringProperty TitleField { get; set; }`  
5.x: `public IStringProperty TitleField { get; set; }`  

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

**public property Nest.IBulkAllRequest&lt;T&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool? Refresh { get; set; }`  
5.x: `public bool? Refresh { get; set; }`  

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

**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public string Script { get; set; }`  
5.x: `public string Script { get; set; }`  

**public property Nest.IClusterRerouteResponse.State** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("state")]
public ClusterRerouteState State { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("state")]
public ClusterRerouteState State { get; }
```

**public property Nest.IClusterStatsResponse.ClusterName** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("cluster_name")]
public string ClusterName { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("cluster_name")]
public string ClusterName { get; }
```

**public property Nest.IClusterStatsResponse.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("indices")]
public ClusterIndicesStats Indices { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("indices")]
public ClusterIndicesStats Indices { get; }
```

**public property Nest.IClusterStatsResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("nodes")]
public ClusterNodesStats Nodes { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("nodes")]
public ClusterNodesStats Nodes { get; }
```

**public property Nest.IClusterStatsResponse.Status** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("status")]
public ClusterStatus Status { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("status")]
public ClusterStatus Status { get; }
```

**public property Nest.IClusterStatsResponse.Timestamp** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("timestamp")]
public long Timestamp { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("timestamp")]
public long Timestamp { get; }
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

**public property Nest.IDynamicIndexSettings.AutoExpandReplicas** *Declaration changed (Breaking)*

2.x: `public string AutoExpandReplicas { get; set; }`  
5.x: `public string AutoExpandReplicas { get; set; }`  

**public method Nest.IElasticClient.DeleteByQuery&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  
5.x: `public IDeleteByQueryResponse DeleteByQuery<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  

**public method Nest.IElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(IGetAliasRequest request)`  
5.x: `public IGetAliasesResponse GetAlias(IGetAliasRequest request)`  

**public method Nest.IElasticClient.GetAlias** *Declaration changed (Breaking)*

2.x: `public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  
5.x: `public IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  

**public method Nest.IElasticClient.Suggest&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  
5.x: `public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  

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

**public property Nest.IFieldStatsResponse.Shards** *Declaration changed (Breaking)*

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

**public property Nest.IGetSearchTemplateResponse.Template** *Declaration changed (Breaking)*

2.x: `public string Template { get; set; }`  
5.x: `public string Template { get; set; }`  

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
5.x: `public double Score { get; }`  

**public property Nest.IIndicesStatsResponse.Stats** *Declaration changed (Breaking)*

2.x: `public IndicesStats Stats { get; set; }`  
5.x: `public IndicesStats Stats { get; set; }`  

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

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(AttachmentProperty mapping)`  
5.x: `public void Visit(AttachmentProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(NumberProperty mapping)`  
5.x: `public void Visit(NumberProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(GeoPointProperty mapping)`  
5.x: `public void Visit(GeoPointProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(GeoShapeProperty mapping)`  
5.x: `public void Visit(GeoShapeProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(TokenCountProperty mapping)`  
5.x: `public void Visit(TokenCountProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(CompletionProperty mapping)`  
5.x: `public void Visit(CompletionProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(Murmur3HashProperty mapping)`  
5.x: `public void Visit(Murmur3HashProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IpProperty mapping)`  
5.x: `public void Visit(IpProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("Use Visit(NumberProperty mapping) for number mappings")]
public void Visit(NumberType mapping)
```

5.x
```csharp
public void Visit(ITextProperty property)
```

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(DateProperty mapping)`  
5.x: `public void Visit(DateProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(TypeMapping mapping)`  
5.x: `public void Visit(TypeMapping mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(StringProperty mapping)`  
5.x: `public void Visit(StringProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ObjectProperty mapping)`  
5.x: `public void Visit(ObjectProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(NestedProperty mapping)`  
5.x: `public void Visit(NestedProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(BooleanProperty mapping)`  
5.x: `public void Visit(BooleanProperty mapping)`  

**public method Nest.IMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(BinaryProperty mapping)`  
5.x: `public void Visit(BinaryProperty mapping)`  

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

**public method Nest.IndexDescriptor&lt;TDocument&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public IndexDescriptor<TDocument> Refresh(bool refresh = True)`  
5.x: `public IndexDescriptor<TDocument> Refresh(bool refresh = True)`  

**public property Nest.IndexHealthStats.ActivePrimaryShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int ActivePrimaryShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int ActivePrimaryShards { get; internal set; }
```

**public property Nest.IndexHealthStats.ActiveShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int ActiveShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int ActiveShards { get; internal set; }
```

**public property Nest.IndexHealthStats.InitializingShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int InitializingShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int InitializingShards { get; internal set; }
```

**public property Nest.IndexHealthStats.NumberOfReplicas** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int NumberOfReplicas { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int NumberOfReplicas { get; internal set; }
```

**public property Nest.IndexHealthStats.NumberOfShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int NumberOfShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int NumberOfShards { get; internal set; }
```

**public property Nest.IndexHealthStats.RelocatingShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int RelocatingShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int RelocatingShards { get; internal set; }
```

**public property Nest.IndexHealthStats.Status** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public string Status { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public string Status { get; internal set; }
```

**public property Nest.IndexHealthStats.UnassignedShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int UnassignedShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int UnassignedShards { get; internal set; }
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

**public property Nest.IndexRequest&lt;TDocument&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public bool Refresh { get; set; }`  

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

**public property Nest.IndicesStatsResponse.Stats** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IndicesStats Stats { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IndicesStats Stats { get; internal set; }
```

**public property Nest.InnerHits.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public ISourceFilter Source { get; set; }`  

**public method Nest.InnerHitsDescriptor&lt;T&gt;.FielddataFields** *Declaration changed (Breaking)*

2.x: `public InnerHitsDescriptor<T> FielddataFields(String[] fielddataFields)`  
5.x: `public InnerHitsDescriptor<T> FielddataFields(String[] fielddataFields)`  

**public property Nest.InstantGet&lt;T&gt;.Fields** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public FieldValues Fields { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public FieldValues Fields { get; internal set; }
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

**public property Nest.IpAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public NonStringIndexOption Index { get; set; }`  

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

**public property Nest.IpProperty.Index** *Declaration changed (Breaking)*

2.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  
5.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  

**public method Nest.IpPropertyDescriptor&lt;T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public IpPropertyDescriptor<T> Index(Nullable<NonStringIndexOption> index = 0)`  
5.x: `public IpPropertyDescriptor<T> Index(Nullable<NonStringIndexOption> index = 0)`  

**public method Nest.IpRangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public IpRangeAggregationDescriptor<T> Field(string field)`  
5.x: `public IpRangeAggregationDescriptor<T> Field(string field)`  

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

**public method Nest.IQueryVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IMissingQuery filter)`  
5.x: `public void Visit(IMissingQuery filter)`  

**public method Nest.IQueryVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(ILimitQuery query)`  
5.x: `public void Visit(ILimitQuery query)`  

**public method Nest.IQueryVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public void Visit(IFilteredQuery query)`  
5.x: `public void Visit(IFilteredQuery query)`  

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

**public property Nest.IUpgradeStatusResponse.SizeInBytes** *Declaration changed (Breaking)*

2.x: `public long SizeInBytes { get; set; }`  
5.x: `public long SizeInBytes { get; set; }`  

**public property Nest.IUpgradeStatusResponse.SizeToUpgradeAncientInBytes** *Declaration changed (Breaking)*

2.x: `public string SizeToUpgradeAncientInBytes { get; set; }`  
5.x: `public string SizeToUpgradeAncientInBytes { get; set; }`  

**public property Nest.IUpgradeStatusResponse.SizeToUpgradeInBytes** *Declaration changed (Breaking)*

2.x: `public string SizeToUpgradeInBytes { get; set; }`  
5.x: `public string SizeToUpgradeInBytes { get; set; }`  

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
5.x: `public void Accept(TypeMapping mapping)`  

**public method Nest.MetricAggregationDescriptorBase&lt;TMetricAggregation, TMetricAggregationInterface, T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public TMetricAggregation Field(string field)`  
5.x: `public TMetricAggregation Field(string field)`  

**public method Nest.MissingAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public MissingAggregationDescriptor<T> Field(string field)`  
5.x: `public MissingAggregationDescriptor<T> Field(string field)`  

**public method Nest.NestedAggregationDescriptor&lt;T&gt;.Path** *Declaration changed (Breaking)*

2.x: `public NestedAggregationDescriptor<T> Path(string path)`  
5.x: `public NestedAggregationDescriptor<T> Path(string path)`  

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

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(AttachmentProperty mapping)`  
5.x: `public virtual void Visit(AttachmentProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(NumberProperty mapping)`  
5.x: `public virtual void Visit(NumberProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(GeoPointProperty mapping)`  
5.x: `public virtual void Visit(GeoPointProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(GeoShapeProperty mapping)`  
5.x: `public virtual void Visit(GeoShapeProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(TokenCountProperty mapping)`  
5.x: `public virtual void Visit(TokenCountProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(CompletionProperty mapping)`  
5.x: `public virtual void Visit(CompletionProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(Murmur3HashProperty mapping)`  
5.x: `public virtual void Visit(Murmur3HashProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(IpProperty mapping)`  
5.x: `public virtual void Visit(IpProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("Use Visit(NumberProperty mapping) for number mappings")]
public virtual void Visit(NumberType mapping)
```

5.x
```csharp
public virtual void Visit(ITextProperty property)
```

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(DateProperty mapping)`  
5.x: `public virtual void Visit(DateProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(TypeMapping mapping)`  
5.x: `public virtual void Visit(TypeMapping mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(StringProperty mapping)`  
5.x: `public virtual void Visit(StringProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(ObjectProperty mapping)`  
5.x: `public virtual void Visit(ObjectProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(NestedProperty mapping)`  
5.x: `public virtual void Visit(NestedProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(BooleanProperty mapping)`  
5.x: `public virtual void Visit(BooleanProperty mapping)`  

**public method Nest.NoopMappingVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(BinaryProperty mapping)`  
5.x: `public virtual void Visit(BinaryProperty mapping)`  

**public property Nest.NumberAttribute.Index** *Declaration changed (Breaking)*

2.x: `public NonStringIndexOption Index { get; set; }`  
5.x: `public NonStringIndexOption Index { get; set; }`  

**public property Nest.NumberProperty.Index** *Declaration changed (Breaking)*

2.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  
5.x: `public Nullable<NonStringIndexOption> Index { get; set; }`  

**public method Nest.NumberPropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;..ctor** *Visibility was changed from public to protected (Breaking)*

2.x: `public  .ctor()`  
5.x: `public  .ctor()`  

**public method Nest.NumberPropertyDescriptorBase&lt;TDescriptor, TInterface, T&gt;.Index** *Declaration changed (Breaking)*

2.x: `public TDescriptor Index(NonStringIndexOption index = 0)`  
5.x: `public TDescriptor Index(NonStringIndexOption index = 0)`  

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
5.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  

**public method Nest.ObjectPropertyDescriptorBase&lt;TDescriptor, TInterface, TParent, TChild&gt;..ctor** *Visibility was changed from public to protected (Breaking)*

2.x: `public  .ctor()`  
5.x: `public  .ctor()`  

**public method Nest.ObjectPropertyDescriptorBase&lt;TDescriptor, TInterface, TParent, TChild&gt;.Dynamic** *Declaration changed (Breaking)*

2.x: `public TDescriptor Dynamic(DynamicMapping dynamic)`  
5.x: `public TDescriptor Dynamic(DynamicMapping dynamic)`  

**public property Nest.PendingTask.InsertOrder** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("insert_order")]
public int InsertOrder { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("insert_order")]
public int InsertOrder { get; internal set; }
```

**public property Nest.PendingTask.Priority** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("priority")]
public string Priority { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("priority")]
public string Priority { get; internal set; }
```

**public property Nest.PendingTask.Source** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("source")]
public string Source { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("source")]
public string Source { get; internal set; }
```

**public property Nest.PendingTask.TimeInQueue** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("time_in_queue")]
public string TimeInQueue { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("time_in_queue")]
public string TimeInQueue { get; internal set; }
```

**public property Nest.PendingTask.TimeInQueueMilliseconds** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("time_in_queue_millis")]
public int TimeInQueueMilliseconds { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("time_in_queue_millis")]
public int TimeInQueueMilliseconds { get; internal set; }
```

**public class Nest.PercentileRanksAggregationJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class PercentileRanksAggregationJsonConverter : PercentilesAggregationJsonConverter`  
5.x: `public class PercentileRanksAggregationJsonConverter : PercentilesAggregationJsonConverter`  

**public class Nest.PercentilesAggregationJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class PercentilesAggregationJsonConverter : JsonConverter`  
5.x: `public class PercentilesAggregationJsonConverter : JsonConverter`  

**public property Nest.PercolateCountResponse.Took** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
[JsonIgnoreAttribute]
public int Took { get; }
```

5.x
```csharp
[JsonPropertyAttribute]
public long Took { get; internal set; }
```

**public property Nest.PercolatorMatch.Id** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_id")]
public string Id { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public string Id { get; internal set; }
```

**public property Nest.PercolatorMatch.Index** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_index")]
public string Index { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public string Index { get; internal set; }
```

**public property Nest.PercolatorMatch.Score** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("_score")]
public double Score { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public double Score { get; internal set; }
```

**public property Nest.PhraseSuggestCollate.Query** *Declaration changed (Breaking)*

2.x: `public IScript Query { get; set; }`  
5.x: `public IScript Query { get; set; }`  

**public method Nest.PhraseSuggestCollateDescriptor&lt;T&gt;.Query** *Declaration changed (Breaking)*

2.x: `public PhraseSuggestCollateDescriptor<T> Query(string script)`  
5.x: `public PhraseSuggestCollateDescriptor<T> Query(string script)`  

**public method Nest.PropertyName..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor()`  
5.x: `public  .ctor()`  

**public property Nest.PropertyName.Expression** *Declaration changed (Breaking)*

2.x: `public Expression Expression { get; set; }`  
5.x: `public Expression Expression { get; set; }`  

**public property Nest.PropertyName.Name** *Declaration changed (Breaking)*

2.x: `public string Name { get; set; }`  
5.x: `public string Name { get; set; }`  

**public property Nest.PropertyName.Property** *Declaration changed (Breaking)*

2.x: `public PropertyInfo Property { get; set; }`  
5.x: `public PropertyInfo Property { get; set; }`  

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
5.x: `public PutMappingDescriptor<T> Dynamic(DynamicMapping dynamic)`  

**public property Nest.PutMappingRequest.Dynamic** *Declaration changed (Breaking)*

2.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  
5.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  

**public property Nest.PutMappingRequest.Meta** *Declaration changed (Breaking)*

2.x: `public FluentDictionary<string, object> Meta { get; set; }`  
5.x: `public FluentDictionary<string, object> Meta { get; set; }`  

**public property Nest.PutMappingRequest&lt;T&gt;.Dynamic** *Declaration changed (Breaking)*

2.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  
5.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  

**public property Nest.PutMappingRequest&lt;T&gt;.Meta** *Declaration changed (Breaking)*

2.x: `public FluentDictionary<string, object> Meta { get; set; }`  
5.x: `public FluentDictionary<string, object> Meta { get; set; }`  

**public property Nest.PutWatchResponse.Id** *Declaration changed (Breaking)*

2.x: `public Id Id { get; internal set; }`  
5.x: `public Id Id { get; internal set; }`  

**public method Nest.Query&lt;T&gt;.Prefix** *Declaration changed (Breaking)*

2.x: `public static QueryContainer Prefix(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  
5.x: `public static QueryContainer Prefix(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  

**public method Nest.Query&lt;T&gt;.Term** *Declaration changed (Breaking)*

2.x: `public static QueryContainer Term(string field, object value, double? boost, string name)`  
5.x: `public static QueryContainer Term(string field, object value, double? boost, string name)`  

**public method Nest.Query&lt;T&gt;.Wildcard** *Declaration changed (Breaking)*

2.x: `public static QueryContainer Wildcard(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  
5.x: `public static QueryContainer Wildcard(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Prefix** *Declaration changed (Breaking)*

2.x: `public QueryContainer Prefix(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  
5.x: `public QueryContainer Prefix(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Term** *Declaration changed (Breaking)*

2.x: `public QueryContainer Term(string field, object value, double? boost, string name)`  
5.x: `public QueryContainer Term(string field, object value, double? boost, string name)`  

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Wildcard** *Declaration changed (Breaking)*

2.x: `public QueryContainer Wildcard(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  
5.x: `public QueryContainer Wildcard(string field, string value, double? boost, Nullable<RewriteMultiTerm> rewrite, string name)`  

**public method Nest.QueryVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(ILimitQuery query)`  
5.x: `public virtual void Visit(ILimitQuery query)`  

**public method Nest.QueryVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(IFilteredQuery query)`  
5.x: `public virtual void Visit(IFilteredQuery query)`  

**public method Nest.QueryVisitor.Visit** *Declaration changed (Breaking)*

2.x: `public virtual void Visit(INotQuery query)`  
5.x: `public virtual void Visit(INotQuery query)`  

**public method Nest.RangeAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public RangeAggregationDescriptor<T> Field(string field)`  
5.x: `public RangeAggregationDescriptor<T> Field(string field)`  

**public method Nest.ReindexDescriptor&lt;T&gt;..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor(IndexName from, IndexName to)`  
5.x: `public  .ctor(IndexName from, IndexName to)`  

**public method Nest.ReindexObservable&lt;T&gt;.Subscribe** *Declaration changed (Breaking)*

2.x: `public IDisposable Subscribe(IObserver<IReindexResponse<T>> observer)`  
5.x: `public IDisposable Subscribe(IObserver<IReindexResponse<T>> observer)`  

**public method Nest.ReindexObserver&lt;T&gt;..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor(Action<IReindexResponse<T>> onNext, Action<Exception> onError, Action completed)`  
5.x: `public  .ctor(Action<IReindexResponse<T>> onNext, Action<Exception> onError, Action completed)`  

**public method Nest.ReindexOnServerDescriptor.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public ReindexOnServerDescriptor RequestsPerSecond(Single requests_per_second)`  
5.x: `public ReindexOnServerDescriptor RequestsPerSecond(Single requests_per_second)`  

**public property Nest.ReindexOnServerRequest.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public Single RequestsPerSecond { get; set; }`  

**public property Nest.ReindexOnServerResponse.Retries** *Declaration changed (Breaking)*

2.x: `public long Retries { get; internal set; }`  
5.x: `public long Retries { get; internal set; }`  

**public method Nest.ReindexRethrottleDescriptor.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public ReindexRethrottleDescriptor RequestsPerSecond(Single requests_per_second)`  
5.x: `public ReindexRethrottleDescriptor RequestsPerSecond(Single requests_per_second)`  

**public property Nest.ReindexRethrottleRequest.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public Single RequestsPerSecond { get; set; }`  

**public class Nest.ReindexRoutingJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class ReindexRoutingJsonConverter : JsonConverter`  
5.x: `public class ReindexRoutingJsonConverter : JsonConverter`  

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
5.x: `public IApiCallDetails ApiCall { get; }`  

**public method Nest.ReverseNestedAggregationDescriptor&lt;T&gt;.Path** *Declaration changed (Breaking)*

2.x: `public ReverseNestedAggregationDescriptor<T> Path(string path)`  
5.x: `public ReverseNestedAggregationDescriptor<T> Path(string path)`  

**public class Nest.ScoreFunctionJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class ScoreFunctionJsonConverter : JsonConverter`  
5.x: `public class ScoreFunctionJsonConverter : JsonConverter`  

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
5.x: `public class ScriptJsonConverter : JsonConverter`  

**public method Nest.SearchDescriptor&lt;T&gt;.Rescore** *Declaration changed (Breaking)*

2.x: `public SearchDescriptor<T> Rescore(Func<RescoreDescriptor<T>, IRescore> rescoreSelector)`  
5.x: `public SearchDescriptor<T> Rescore(Func<RescoreDescriptor<T>, IRescore> rescoreSelector)`  

**public property Nest.SearchNode.Name** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("name")]
public string Name { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("name")]
public string Name { get; internal set; }
```

**public property Nest.SearchNode.TransportAddress** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("transport_address")]
public string TransportAddress { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("transport_address")]
public string TransportAddress { get; internal set; }
```

**public property Nest.SearchRequest.Rescore** *Declaration changed (Breaking)*

2.x: `public IRescore Rescore { get; set; }`  
5.x: `public IRescore Rescore { get; set; }`  

**public property Nest.SearchRequest.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public ISourceFilter Source { get; set; }`  

**public property Nest.SearchRequest&lt;T&gt;.Rescore** *Declaration changed (Breaking)*

2.x: `public IRescore Rescore { get; set; }`  
5.x: `public IRescore Rescore { get; set; }`  

**public property Nest.SearchRequest&lt;T&gt;.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public ISourceFilter Source { get; set; }`  

**public property Nest.SearchResponse&lt;T&gt;.ApiCall** *Visibility was changed from public to protected (Breaking)*

2.x: `public IApiCallDetails ApiCall { get; }`  
5.x: `public IApiCallDetails ApiCall { get; }`  

**public property Nest.SearchResponse&lt;T&gt;.Took** *Declaration changed (Breaking)*

2.x
```csharp
[ObsoleteAttribute("returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
[JsonIgnoreAttribute]
public int Took { get; }
```

5.x
```csharp
[JsonPropertyAttribute]
public long Took { get; internal set; }
```

**public property Nest.SearchShard.Index** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("index")]
public string Index { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("index")]
public string Index { get; internal set; }
```

**public property Nest.SearchShard.Node** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("node")]
public string Node { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("node")]
public string Node { get; internal set; }
```

**public property Nest.SearchShard.Primary** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("primary")]
public bool Primary { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("primary")]
public bool Primary { get; internal set; }
```

**public property Nest.SearchShard.RelocatingNode** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("relocating_node")]
public string RelocatingNode { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("relocating_node")]
public string RelocatingNode { get; internal set; }
```

**public property Nest.SearchShard.Shard** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("shard")]
public int Shard { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("shard")]
public int Shard { get; internal set; }
```

**public property Nest.SearchShard.State** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("state")]
public string State { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("state")]
public string State { get; internal set; }
```

**public property Nest.ShardHealthStats.ActiveShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int ActiveShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int ActiveShards { get; internal set; }
```

**public property Nest.ShardHealthStats.InitializingShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int InitializingShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int InitializingShards { get; internal set; }
```

**public property Nest.ShardHealthStats.PrimaryActive** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public bool PrimaryActive { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public bool PrimaryActive { get; internal set; }
```

**public property Nest.ShardHealthStats.RelocatingShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int RelocatingShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int RelocatingShards { get; internal set; }
```

**public property Nest.ShardHealthStats.Status** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public string Status { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public string Status { get; internal set; }
```

**public property Nest.ShardHealthStats.UnassignedShards** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public int UnassignedShards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public int UnassignedShards { get; internal set; }
```

**public property Nest.ShardStore.Allocation** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("allocation")]
public ShardStoreAllocation Allocation { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("allocation")]
public ShardStoreAllocation Allocation { get; internal set; }
```

**public property Nest.ShardStore.Id** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("id")]
public string Id { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("id")]
public string Id { get; internal set; }
```

**public property Nest.ShardStore.Name** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("name")]
public string Name { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("name")]
public string Name { get; internal set; }
```

**public property Nest.ShardStore.StoreException** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("store_exeption")]
public ShardStoreException StoreException { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("store_exception")]
public ShardStoreException StoreException { get; internal set; }
```

**public property Nest.ShardStore.TransportAddress** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("transport_address")]
public string TransportAddress { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("transport_address")]
public string TransportAddress { get; internal set; }
```

**public property Nest.ShardStoreException.Reason** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("reason")]
public string Reason { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("reason")]
public string Reason { get; internal set; }
```

**public property Nest.ShardStoreException.Type** *Visibility changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("type")]
public string Type { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("type")]
public string Type { get; internal set; }
```

**public method Nest.SignificantTermsAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public SignificantTermsAggregationDescriptor<T> Field(string field)`  
5.x: `public SignificantTermsAggregationDescriptor<T> Field(string field)`  

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
5.x: `public class SimpleQueryStringFlagsJsonConverter : JsonConverter`  

**public class Nest.SourceFilterJsonConverter** *Visibility was changed from public to internal (Breaking)*

2.x: `public class SourceFilterJsonConverter : JsonConverter`  
5.x: `public class SourceFilterJsonConverter : JsonConverter`  

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
5.x: `public INorms Norms { get; set; }`  

**public method Nest.StringPropertyDescriptor&lt;T&gt;.Norms** *Declaration changed (Breaking)*

2.x: `public StringPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector)`  
5.x: `public StringPropertyDescriptor<T> Norms(Func<NormsDescriptor, INorms> selector)`  

**public method Nest.StringPropertyDescriptor&lt;T&gt;.PositionIncrementGap** *Declaration changed (Breaking)*

2.x: `public StringPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap)`  
5.x: `public StringPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap)`  

**public method Nest.TermsAggregationDescriptor&lt;T&gt;.Field** *Declaration changed (Breaking)*

2.x: `public TermsAggregationDescriptor<T> Field(string field)`  
5.x: `public TermsAggregationDescriptor<T> Field(string field)`  

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
5.x: `public double Milliseconds { get; private set; }`  

**public property Nest.TopHitsAggregation.Source** *Declaration changed (Breaking)*

2.x: `public ISourceFilter Source { get; set; }`  
5.x: `public ISourceFilter Source { get; set; }`  

**public property Nest.TypeMapping.Dynamic** *Declaration changed (Breaking)*

2.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  
5.x: `public Nullable<DynamicMapping> Dynamic { get; set; }`  

**public property Nest.TypeMapping.Meta** *Declaration changed (Breaking)*

2.x: `public FluentDictionary<string, object> Meta { get; set; }`  
5.x: `public FluentDictionary<string, object> Meta { get; set; }`  

**public method Nest.TypeMappingDescriptor&lt;T&gt;.Dynamic** *Declaration changed (Breaking)*

2.x: `public TypeMappingDescriptor<T> Dynamic(DynamicMapping dynamic)`  
5.x: `public TypeMappingDescriptor<T> Dynamic(DynamicMapping dynamic)`  

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
5.x: `public UpdateByQueryDescriptor<T> RequestsPerSecond(Single requests_per_second)`  

**public property Nest.UpdateByQueryRequest.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public Single RequestsPerSecond { get; set; }`  

**public property Nest.UpdateByQueryRequest&lt;T&gt;.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public Single RequestsPerSecond { get; set; }`  
5.x: `public Single RequestsPerSecond { get; set; }`  

**public property Nest.UpdateByQueryResponse.Retries** *Declaration changed (Breaking)*

2.x: `public long Retries { get; internal set; }`  
5.x: `public long Retries { get; internal set; }`  

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public UpdateDescriptor<TDocument, TPartialDocument> Refresh(bool refresh = True)`  
5.x: `public UpdateDescriptor<TDocument, TPartialDocument> Refresh(bool refresh = True)`  

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public UpdateDescriptor<TDocument, TPartialDocument> Script(string script)`  
5.x: `public UpdateDescriptor<TDocument, TPartialDocument> Script(string script)`  

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Refresh** *Declaration changed (Breaking)*

2.x: `public bool Refresh { get; set; }`  
5.x: `public bool Refresh { get; set; }`  

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Script** *Declaration changed (Breaking)*

2.x: `public string Script { get; set; }`  
5.x: `public string Script { get; set; }`  

**public property Nest.UpgradeResponse.Shards** *Visibility changed (Breaking)*

2.x: `public ShardsMetaData Shards { get; set; }`  
5.x: `public ShardsMetaData Shards { get; set; }`  

**public property Nest.UpgradeStatusResponse.SizeInBytes** *Visibility changed (Breaking)*

2.x: `public long SizeInBytes { get; set; }`  
5.x: `public long SizeInBytes { get; set; }`  

**public property Nest.UpgradeStatusResponse.SizeToUpgradeAncientInBytes** *Visibility changed (Breaking)*

2.x: `public string SizeToUpgradeAncientInBytes { get; set; }`  
5.x: `public string SizeToUpgradeAncientInBytes { get; set; }`  

**public property Nest.UpgradeStatusResponse.SizeToUpgradeInBytes** *Visibility changed (Breaking)*

2.x: `public string SizeToUpgradeInBytes { get; set; }`  
5.x: `public string SizeToUpgradeInBytes { get; set; }`  

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

**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*

```csharp
public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptFile(string scriptFile)
```

**public method Nest.BulkUpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*

```csharp
public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptId(string scriptId)
```

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.InferFrom** *Removed (Breaking)*

```csharp
public TDocument InferFrom { get; set; }
```

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Lang** *Removed (Breaking)*

```csharp
public string Lang { get; set; }
```

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*

```csharp
public Dictionary<string, object> Params { get; set; }
```

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*

```csharp
public string ScriptFile { get; set; }
```

**public property Nest.BulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*

```csharp
public string ScriptId { get; set; }
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

**public class Nest.CatThreadPool** *Removed (Breaking)*

```csharp
public class CatThreadPool
```

**public method Nest.CatThreadPoolDescriptor.FullId** *Removed (Breaking)*

```csharp
public CatThreadPoolDescriptor FullId(bool full_id = True)
```

**public property Nest.CatThreadPoolRecord.Bulk** *Removed (Breaking)*

```csharp
public CatThreadPool Bulk { get; set; }
```

**public property Nest.CatThreadPoolRecord.Flush** *Removed (Breaking)*

```csharp
public CatThreadPool Flush { get; set; }
```

**public property Nest.CatThreadPoolRecord.Generic** *Removed (Breaking)*

```csharp
public CatThreadPool Generic { get; set; }
```

**public property Nest.CatThreadPoolRecord.Get** *Removed (Breaking)*

```csharp
public CatThreadPool Get { get; set; }
```

**public property Nest.CatThreadPoolRecord.Id** *Removed (Breaking)*

```csharp
public string Id { get; set; }
```

**public property Nest.CatThreadPoolRecord.Index** *Removed (Breaking)*

```csharp
public CatThreadPool Index { get; set; }
```

**public property Nest.CatThreadPoolRecord.Management** *Removed (Breaking)*

```csharp
public CatThreadPool Management { get; set; }
```

**public property Nest.CatThreadPoolRecord.Merge** *Removed (Breaking)*

```csharp
public CatThreadPool Merge { get; set; }
```

**public property Nest.CatThreadPoolRecord.Optimize** *Removed (Breaking)*

```csharp
public CatThreadPool Optimize { get; set; }
```

**public property Nest.CatThreadPoolRecord.Percolate** *Removed (Breaking)*

```csharp
public CatThreadPool Percolate { get; set; }
```

**public property Nest.CatThreadPoolRecord.Pid** *Removed (Breaking)*

```csharp
public string Pid { get; set; }
```

**public property Nest.CatThreadPoolRecord.Refresh** *Removed (Breaking)*

```csharp
public CatThreadPool Refresh { get; set; }
```

**public property Nest.CatThreadPoolRecord.Search** *Removed (Breaking)*

```csharp
public CatThreadPool Search { get; set; }
```

**public property Nest.CatThreadPoolRecord.Snapshot** *Removed (Breaking)*

```csharp
public CatThreadPool Snapshot { get; set; }
```

**public property Nest.CatThreadPoolRecord.Suggest** *Removed (Breaking)*

```csharp
public CatThreadPool Suggest { get; set; }
```

**public property Nest.CatThreadPoolRecord.Warmer** *Removed (Breaking)*

```csharp
public CatThreadPool Warmer { get; set; }
```

**public property Nest.CatThreadPoolRequest.FullId** *Removed (Breaking)*

```csharp
public bool FullId { get; set; }
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

**public method Nest.DslPrettyPrintVisitor.Visit** *Removed (Breaking)*

```csharp
public void Visit(IAndQuery query)
```

**public method Nest.DslPrettyPrintVisitor.Visit** *Removed (Breaking)*

```csharp
public virtual void Visit(IMissingQuery filter)
```

**public method Nest.DslPrettyPrintVisitor.Visit** *Removed (Breaking)*

```csharp
public void Visit(IOrQuery query)
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

**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Lang** *Removed (Breaking)*

```csharp
public string Lang { get; set; }
```

**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*

```csharp
public Dictionary<string, object> Params { get; set; }
```

**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*

```csharp
public string ScriptFile { get; set; }
```

**public property Nest.IBulkUpdateOperation&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*

```csharp
public string ScriptId { get; set; }
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

**public interface Nest.IIndexOperation&lt;T&gt;** *Removed (Breaking)*

```csharp
public interface IIndexOperation<T> : IBulkOperation
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

**public property Nest.IQueryContainer.And** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("and")]
public IAndQuery And { get; set; }
```

**public property Nest.IQueryContainer.Filtered** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("filtered")]
public IFilteredQuery Filtered { get; set; }
```

**public property Nest.IQueryContainer.Limit** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("limit")]
public ILimitQuery Limit { get; set; }
```

**public property Nest.IQueryContainer.Missing** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("missing")]
public IMissingQuery Missing { get; set; }
```

**public property Nest.IQueryContainer.Not** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("not")]
public INotQuery Not { get; set; }
```

**public property Nest.IQueryContainer.Or** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute("or")]
public IOrQuery Or { get; set; }
```

**public method Nest.IQueryVisitor.Visit** *Removed (Breaking)*

```csharp
public void Visit(IOrQuery query)
```

**public method Nest.IQueryVisitor.Visit** *Removed (Breaking)*

```csharp
public void Visit(IAndQuery query)
```

**public method Nest.IQueryVisitor.Visit** *Removed (Breaking)*

```csharp
public void Visit(INotQuery query)
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

**public interface Nest.IStatsAggregator** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[ContractJsonConverterAttribute(Nest.AggregationJsonConverter`1[Nest.StatsAggregation])]
public interface IStatsAggregator : IMetricAggregation, IAggregation
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

**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.Language** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string Language { get; set; }
```

**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, object> Params { get; set; }
```

**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string ScriptFile { get; set; }
```

**public property Nest.IUpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*

```csharp
[JsonPropertyAttribute]
public string ScriptId { get; set; }
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

**public class Nest.KeyedBucket** *Removed (Breaking)*

```csharp
public class KeyedBucket : BucketBase, IBucket
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

**public enum Nest.NonStringIndexOption** *Removed (Breaking)*

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

**public method Nest.PhraseSuggestCollateDescriptor&lt;T&gt;.Query** *Removed (Breaking)*

```csharp
public PhraseSuggestCollateDescriptor<T> Query(Func<ScriptDescriptor, IScript> scriptSelector)
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

**public method Nest.Query&lt;T&gt;.Missing** *Removed (Breaking)*

```csharp
public static QueryContainer Missing(Func<MissingQueryDescriptor<T>, IMissingQuery> selector)
```

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Limit** *Removed (Breaking)*

```csharp
public QueryContainer Limit(Func<LimitQueryDescriptor<T>, ILimitQuery> selector)
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

**public method Nest.QueryVisitor.Visit** *Removed (Breaking)*

```csharp
public virtual void Visit(IAndQuery query)
```

**public method Nest.QueryVisitor.Visit** *Removed (Breaking)*

```csharp
public virtual void Visit(IMissingQuery query)
```

**public method Nest.QueryVisitor.Visit** *Removed (Breaking)*

```csharp
public virtual void Visit(IOrQuery query)
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

**public class Nest.TermsAggregate** *Removed (Breaking)*

```csharp
public class TermsAggregate : MultiBucketAggregate<KeyedBucket>, IAggregate
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

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Language** *Removed (Breaking)*

```csharp
public UpdateDescriptor<TDocument, TPartialDocument> Language(string language)
```

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*

```csharp
public UpdateDescriptor<TDocument, TPartialDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
```

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptedUpsert** *Removed (Breaking)*

```csharp
public UpdateDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool scripted_upsert = True)
```

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*

```csharp
public UpdateDescriptor<TDocument, TPartialDocument> ScriptFile(string scriptFile)
```

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*

```csharp
public UpdateDescriptor<TDocument, TPartialDocument> ScriptId(string script_id)
```

**public method Nest.UpdateDescriptor&lt;TDocument, TPartialDocument&gt;.ScriptQueryString** *Removed (Breaking)*

```csharp
public UpdateDescriptor<TDocument, TPartialDocument> ScriptQueryString(string script)
```

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Consistency** *Removed (Breaking)*

```csharp
public Consistency Consistency { get; set; }
```

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Language** *Removed (Breaking)*

```csharp
public string Language { get; set; }
```

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.Params** *Removed (Breaking)*

```csharp
public Dictionary<string, object> Params { get; set; }
```

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptedUpsert** *Removed (Breaking)*

```csharp
public bool ScriptedUpsert { get; set; }
```

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptFile** *Removed (Breaking)*

```csharp
public string ScriptFile { get; set; }
```

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptId** *Removed (Breaking)*

```csharp
public string ScriptId { get; set; }
```

**public property Nest.UpdateRequest&lt;TDocument, TPartialDocument&gt;.ScriptQueryString** *Removed (Breaking)*

```csharp
public string ScriptQueryString { get; set; }
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

**public class Nest.AndQuery** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query with a must clause query instead. The bool query should not have other clauses to be semantically correct")]
public class AndQuery : QueryBase, IQuery, IAndQuery
```

**public class Nest.AndQueryDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query with a must clause instead. The bool query should not have other clauses to be semantically correct")]
public class AndQueryDescriptor<T> : QueryDescriptorBase<AndQueryDescriptor<T>, IAndQuery>, IDescriptor, IQuery, IAndQuery
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

**public class Nest.FilteredQuery** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query instead with a must clause for the query and a filter clause for the filter.")]
public class FilteredQuery : QueryBase, IQuery, IFilteredQuery
```

**public class Nest.FilteredQueryDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query instead with a must clause for the query and a filter clause for the filter.")]
public class FilteredQueryDescriptor<T> : QueryDescriptorBase<FilteredQueryDescriptor<T>, IFilteredQuery>, IDescriptor, IQuery, IFilteredQuery
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

**public interface Nest.IAndQuery** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.AndQuery])]
[ObsoleteAttribute("Use the bool query with a must clause query instead. The bool query should not have other clauses to be semantically correct")]
public interface IAndQuery : IQuery
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

**public interface Nest.IFilteredQuery** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.FilteredQueryDescriptor`1[System.Object]])]
[ObsoleteAttribute("Use the bool query instead with a must clause for the query and a filter clause for the filter.")]
public interface IFilteredQuery : IQuery
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

**public interface Nest.ILimitQuery** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.LimitQuery])]
[ObsoleteAttribute("Use TerminateAfter (terminate_after parameter) on the request instead")]
public interface ILimitQuery : IQuery
```

**public interface Nest.IMappingTransform** *Removed (Breaking)*

```csharp
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.MappingTransform])]
[ObsoleteAttribute("Deprecated in 2.0.0 Removed in 5.0.0")]
public interface IMappingTransform
```

**public interface Nest.IMissingQuery** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.MissingQuery])]
[ObsoleteAttribute("Removed in 5.0.0. Use an exists query within a bool must_not clause instead.")]
public interface IMissingQuery : IQuery
```

**public interface Nest.INotQuery** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.NotQuery])]
[ObsoleteAttribute("Use the bool query with must_not clause instead")]
public interface INotQuery : IQuery
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

**public interface Nest.IOrQuery** *Removed (Breaking)*

```csharp
[JsonObjectAttribute]
[JsonConverterAttribute(Nest.ReadAsTypeJsonConverter`1[Nest.OrQuery])]
[ObsoleteAttribute("Use the bool query instead with a should clause for the query")]
public interface IOrQuery : IQuery
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

**public class Nest.LimitQuery** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use TerminateAfter (terminate_after parameter) on the request instead")]
public class LimitQuery : QueryBase, IQuery, ILimitQuery
```

**public class Nest.LimitQueryDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use TerminateAfter (terminate_after parameter) on the request instead")]
public class LimitQueryDescriptor<T> : QueryDescriptorBase<LimitQueryDescriptor<T>, ILimitQuery>, IDescriptor, IQuery, ILimitQuery
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

**public class Nest.MissingQuery** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use an exists query within a bool must_not clause instead.")]
public class MissingQuery : QueryBase, IQuery, IMissingQuery
```

**public class Nest.MissingQueryDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use an exists query within a bool must_not clause instead.")]
public class MissingQueryDescriptor<T> : QueryDescriptorBase<MissingQueryDescriptor<T>, IMissingQuery>, IDescriptor, IQuery, IMissingQuery
```

**public class Nest.NotQuery** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query with must_not clause instead")]
public class NotQuery : QueryBase, IQuery, INotQuery
```

**public class Nest.NotQueryDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query with must_not clause instead")]
public class NotQueryDescriptor<T> : QueryDescriptorBase<NotQueryDescriptor<T>, INotQuery>, IDescriptor, IQuery, INotQuery
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

**public class Nest.OrQuery** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query instead with a should clause for the query")]
public class OrQuery : QueryBase, IQuery, IOrQuery
```

**public class Nest.OrQueryDescriptor&lt;T&gt;** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query instead with a should clause for the query")]
public class OrQueryDescriptor<T> : QueryDescriptorBase<OrQueryDescriptor<T>, IOrQuery>, IDescriptor, IQuery, IOrQuery
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

**public method Nest.Query&lt;T&gt;.And** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query with a must clause query instead. The bool query should not have other clauses to be semantically correct")]
public static QueryContainer And(Func<AndQueryDescriptor<T>, IAndQuery> selector)
```

**public method Nest.Query&lt;T&gt;.Filtered** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query instead with a must clause for the query and a filter clause for the filter.")]
public static QueryContainer Filtered(Func<FilteredQueryDescriptor<T>, IFilteredQuery> selector)
```

**public method Nest.Query&lt;T&gt;.Limit** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use TerminateAfter (terminate_after parameter) on the request instead")]
public static QueryContainer Limit(Func<LimitQueryDescriptor<T>, ILimitQuery> selector)
```

**public method Nest.Query&lt;T&gt;.Not** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query with must_not clause instead")]
public static QueryContainer Not(Func<NotQueryDescriptor<T>, INotQuery> selector)
```

**public method Nest.Query&lt;T&gt;.Or** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query instead with a should clause for the query")]
public static QueryContainer Or(Func<OrQueryDescriptor<T>, IOrQuery> selector)
```

**public method Nest.Query&lt;T&gt;.Strict** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Scheduled to be removed in 5.0.0.  Setting Strict() at the container level is a noop and must be set on each individual query.")]
public static QueryContainerDescriptor<T> Strict(bool strict = True)
```

**public method Nest.QueryContainerDescriptor&lt;T&gt;.And** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query instead")]
public QueryContainer And(Func<AndQueryDescriptor<T>, IAndQuery> selector)
```

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Filtered** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query instead with a must clause for the query and a filter clause for the filter.")]
public QueryContainer Filtered(Func<FilteredQueryDescriptor<T>, IFilteredQuery> selector)
```

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Missing** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use an exists query within a bool must_not clause instead.")]
public QueryContainer Missing(Func<MissingQueryDescriptor<T>, IMissingQuery> selector)
```

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Not** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the bool query with must_not clause instead..")]
public QueryContainer Not(Func<NotQueryDescriptor<T>, INotQuery> selector)
```

**public method Nest.QueryContainerDescriptor&lt;T&gt;.Or** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Use the should clause on the bool query instead, note that this bool query should not have other clauses to be semantically correct")]
public QueryContainer Or(Func<OrQueryDescriptor<T>, IOrQuery> selector)
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
**public property Nest.ActivationStatus.Actions** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("actions")]
public Dictionary<string, ActionStatus> Actions { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("actions")]
public IReadOnlyDictionary<string, ActionStatus> Actions { get; set; }
```

**public property Nest.AggregationsHelper.Aggregations** *Declaration changed (Breaking)*

2.x
```csharp
public IDictionary<string, IAggregate> Aggregations
{
     get; protected internal set; 
}
```

5.x
```csharp
public IReadOnlyDictionary<string, IAggregate> Aggregations
{
     get; protected internal set; 
}
```

**public property Nest.AnalyzeResponse.Tokens** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IEnumerable<AnalyzeToken> Tokens { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyCollection<AnalyzeToken> Tokens { get; internal set; }
```

**public property Nest.AuthenticateResponse.Metadata** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Metadata { get; internal set; }`  
5.x: `public IDictionary<string, object> Metadata { get; internal set; }`  

**public property Nest.AuthenticateResponse.Roles** *Declaration changed (Breaking)*

2.x: `public IEnumerable<string> Roles { get; internal set; }`  
5.x: `public IEnumerable<string> Roles { get; internal set; }`  

**public property Nest.BucketAggregate.Items** *Declaration changed (Breaking)*

2.x: `public IEnumerable<IBucket> Items { get; set; }`  
5.x: `public IEnumerable<IBucket> Items { get; set; }`  

**public property Nest.BucketAggregate.Meta** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Meta { get; set; }`  
5.x: `public IDictionary<string, object> Meta { get; set; }`  

**public property Nest.BucketAggregateBase.Meta** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Meta { get; set; }`  
5.x: `public IDictionary<string, object> Meta { get; set; }`  

**public property Nest.BulkResponse.Items** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("items")]
public IEnumerable<BulkResponseItemBase> Items { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("items")]
public IReadOnlyCollection<BulkResponseItemBase> Items { get; internal set; }
```

**public property Nest.CatResponse&lt;TCatRecord&gt;.Records** *Declaration changed (Breaking)*

2.x: `public IEnumerable<TCatRecord> Records { get; internal set; }`  
5.x: `public IEnumerable<TCatRecord> Records { get; internal set; }`  

**public property Nest.ClearCachedRealmsResponse.Nodes** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, ShieldNode> Nodes { get; internal set; }`  
5.x: `public IDictionary<string, ShieldNode> Nodes { get; internal set; }`  

**public property Nest.ClearCachedRolesResponse.Nodes** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, ShieldNode> Nodes { get; internal set; }`  
5.x: `public IDictionary<string, ShieldNode> Nodes { get; internal set; }`  

**public property Nest.ClusterGetSettingsResponse.Persistent** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Persistent { get; set; }`  
5.x: `public IDictionary<string, object> Persistent { get; set; }`  

**public property Nest.ClusterGetSettingsResponse.Transient** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Transient { get; set; }`  
5.x: `public IDictionary<string, object> Transient { get; set; }`  

**public property Nest.ClusterHealthResponse.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, IndexHealthStats> Indices { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.IndexHealthStats])]
public IReadOnlyDictionary<string, IndexHealthStats> Indices { get; internal set; }
```

**public property Nest.ClusterJvm.Versions** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("versions")]
public List<ClusterJvmVersion> Versions { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("versions")]
public IReadOnlyCollection<ClusterJvmVersion> Versions { get; internal set; }
```

**public property Nest.ClusterNodesStats.Plugins** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("plugins")]
public List<PluginStats> Plugins { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("plugins")]
public IReadOnlyCollection<PluginStats> Plugins { get; internal set; }
```

**public property Nest.ClusterNodesStats.Versions** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("versions")]
public List<string> Versions { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("versions")]
public IReadOnlyCollection<string> Versions { get; internal set; }
```

**public property Nest.ClusterOperatingSystemStats.Names** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("names")]
public List<ClusterOperatingSystemName> Names { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("names")]
public IReadOnlyCollection<ClusterOperatingSystemName> Names { get; internal set; }
```

**public property Nest.ClusterPendingTasksResponse.Tasks** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("tasks")]
public IEnumerable<PendingTask> Tasks { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("tasks")]
public IReadOnlyCollection<PendingTask> Tasks { get; internal set; }
```

**public property Nest.ClusterPutSettingsResponse.Persistent** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Persistent { get; set; }`  
5.x: `public IDictionary<string, object> Persistent { get; set; }`  

**public property Nest.ClusterPutSettingsResponse.Transient** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Transient { get; set; }`  
5.x: `public IDictionary<string, object> Transient { get; set; }`  

**public property Nest.ClusterRerouteResponse.Explanations** *Declaration changed (Breaking)*

2.x: `public IEnumerable<ClusterRerouteExplanation> Explanations { get; set; }`  
5.x: `public IEnumerable<ClusterRerouteExplanation> Explanations { get; set; }`  

**public property Nest.ClusterRerouteState.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("nodes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, NodeState> Nodes { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("nodes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.NodeState])]
public IReadOnlyDictionary<string, NodeState> Nodes { get; internal set; }
```

**public property Nest.ClusterStateResponse.Nodes** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, NodeState> Nodes { get; internal set; }`  
5.x: `public Dictionary<string, NodeState> Nodes { get; internal set; }`  

**public property Nest.Collector.Children** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("children")]
public IEnumerable<Collector> Children { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("children")]
public IReadOnlyCollection<Collector> Children { get; internal set; }
```

**public property Nest.ExecutionResult.Actions** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("actions")]
public List<ExecutionResultAction> Actions { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("actions")]
public IReadOnlyCollection<ExecutionResultAction> Actions { get; set; }
```

**public property Nest.ExecutionResultInput.Payload** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("payload")]
public Dictionary<string, object> Payload { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("payload")]
public IReadOnlyDictionary<string, object> Payload { get; set; }
```

**public property Nest.Explanation.Details** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IEnumerable<ExplanationDetail> Details { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyCollection<ExplanationDetail> Details { get; internal set; }
```

**public property Nest.ExplanationDetail.Details** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IEnumerable<ExplanationDetail> Details { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyCollection<ExplanationDetail> Details { get; internal set; }
```

**public property Nest.FieldMapping.Mapping** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("mapping")]
[JsonConverterAttribute(Nest.FieldMappingJsonConverter)]
public Dictionary<string, IFieldMapping> Mapping { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("mapping")]
[JsonConverterAttribute(Nest.FieldMappingJsonConverter)]
public IReadOnlyDictionary<string, IFieldMapping> Mapping { get; internal set; }
```

**public property Nest.FieldStats.Fields** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("fields")]
public Dictionary<string, FieldStatsField> Fields { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("fields")]
public IReadOnlyDictionary<string, FieldStatsField> Fields { get; internal set; }
```

**public property Nest.FieldStatsResponse.Indices** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, FieldStats> Indices { get; set; }`  
5.x: `public Dictionary<string, FieldStats> Indices { get; set; }`  

**public property Nest.GetFieldMappingResponse.Indices** *Declaration changed (Breaking)*

2.x: `public IndexFieldMappings Indices { get; set; }`  
5.x: `public IndexFieldMappings Indices { get; set; }`  

**public property Nest.GetIndexResponse.Indices** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, IndexState> Indices { get; set; }`  
5.x: `public IDictionary<string, IndexState> Indices { get; set; }`  

**public property Nest.GetIndexSettingsResponse.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonIgnoreAttribute]
public IDictionary<string, IIndexState> Indices { get; }
```

5.x
```csharp
[JsonIgnoreAttribute]
public IReadOnlyDictionary<string, IndexState> Indices { get; }
```

**public property Nest.GetIndexTemplateResponse.TemplateMappings** *Declaration changed (Breaking)*

2.x
```csharp
[JsonIgnoreAttribute]
public IDictionary<string, TemplateMapping> TemplateMappings { get; }
```

5.x
```csharp
[JsonIgnoreAttribute]
public IReadOnlyDictionary<string, TemplateMapping> TemplateMappings { get; }
```

**public property Nest.GetMappingResponse.Mappings** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, IList<TypeMapping>> Mappings { get; internal set; }`  
5.x: `public Dictionary<string, IList<TypeMapping>> Mappings { get; internal set; }`  

**public property Nest.GetRepositoryResponse.Repositories** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, ISnapshotRepository> Repositories { get; set; }`  
5.x: `public IDictionary<string, ISnapshotRepository> Repositories { get; set; }`  

**public property Nest.GetRoleResponse.Roles** *Declaration changed (Breaking)*

2.x
```csharp
[JsonIgnoreAttribute]
public IDictionary<string, Role> Roles { get; }
```

5.x
```csharp
[JsonIgnoreAttribute]
public IReadOnlyDictionary<string, XPackRole> Roles { get; }
```

**public property Nest.GetSnapshotResponse.Snapshots** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("snapshots")]
public IEnumerable<Snapshot> Snapshots { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("snapshots")]
public IReadOnlyCollection<Snapshot> Snapshots { get; internal set; }
```

**public property Nest.GetUserResponse.Users** *Declaration changed (Breaking)*

2.x
```csharp
[JsonIgnoreAttribute]
public IDictionary<string, User> Users { get; }
```

5.x
```csharp
[JsonIgnoreAttribute]
public IReadOnlyDictionary<string, XPackUser> Users { get; }
```

**public property Nest.GraphExploreResponse.Connections** *Declaration changed (Breaking)*

2.x: `public IEnumerable<GraphConnection> Connections { get; internal set; }`  
5.x: `public IEnumerable<GraphConnection> Connections { get; internal set; }`  

**public property Nest.GraphExploreResponse.Failures** *Declaration changed (Breaking)*

2.x: `public IEnumerable<ShardFailure> Failures { get; internal set; }`  
5.x: `public IEnumerable<ShardFailure> Failures { get; internal set; }`  

**public property Nest.GraphExploreResponse.Vertices** *Declaration changed (Breaking)*

2.x: `public IEnumerable<GraphVertex> Vertices { get; internal set; }`  
5.x: `public IEnumerable<GraphVertex> Vertices { get; internal set; }`  

**public property Nest.HighlightHit.Highlights** *Declaration changed (Breaking)*

2.x: `public IEnumerable<string> Highlights { get; set; }`  
5.x: `public IEnumerable<string> Highlights { get; set; }`  

**public property Nest.Hit&lt;T&gt;.InnerHits** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("inner_hits")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public IDictionary<string, InnerHitsResult> InnerHits { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("inner_hits")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.InnerHitsResult])]
public IReadOnlyDictionary<string, InnerHitsResult> InnerHits { get; internal set; }
```

**public property Nest.Hit&lt;T&gt;.MatchedQueries** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("matched_queries")]
public IEnumerable<string> MatchedQueries { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("matched_queries")]
public IReadOnlyCollection<string> MatchedQueries { get; internal set; }
```

**public property Nest.Hit&lt;T&gt;.Sorts** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("sort")]
public IEnumerable<object> Sorts { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("sort")]
public IReadOnlyCollection<object> Sorts { get; internal set; }
```

**public property Nest.HitsMetaData&lt;T&gt;.Hits** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("hits")]
public List<IHit<T>> Hits { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("hits")]
public IReadOnlyCollection<IHit<T>> Hits { get; internal set; }
```

**public property Nest.HotThreadInformation.Hosts** *Declaration changed (Breaking)*

2.x: `public IEnumerable<string> Hosts { get; set; }`  
5.x: `public IEnumerable<string> Hosts { get; set; }`  

**public property Nest.HotThreadInformation.Threads** *Declaration changed (Breaking)*

2.x: `public IEnumerable<string> Threads { get; set; }`  
5.x: `public IEnumerable<string> Threads { get; set; }`  

**public property Nest.IAggregate.Meta** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Meta { get; set; }`  
5.x: `public IDictionary<string, object> Meta { get; set; }`  

**public property Nest.IAnalyzeResponse.Tokens** *Declaration changed (Breaking)*

2.x: `public IEnumerable<AnalyzeToken> Tokens { get; }`  
5.x: `public IEnumerable<AnalyzeToken> Tokens { get; }`  

**public property Nest.IAuthenticateResponse.Metadata** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("metadata")]
public IDictionary<string, object> Metadata { get; }
```

5.x
```csharp
[JsonPropertyAttribute("metadata")]
public IReadOnlyDictionary<string, object> Metadata { get; }
```

**public property Nest.IAuthenticateResponse.Roles** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("roles")]
public IEnumerable<string> Roles { get; }
```

5.x
```csharp
[JsonPropertyAttribute("roles")]
public IReadOnlyCollection<string> Roles { get; }
```

**public property Nest.IBulkResponse.Items** *Declaration changed (Breaking)*

2.x: `public IEnumerable<BulkResponseItemBase> Items { get; }`  
5.x: `public IEnumerable<BulkResponseItemBase> Items { get; }`  

**public property Nest.ICatResponse&lt;TCatRecord&gt;.Records** *Declaration changed (Breaking)*

2.x: `public IEnumerable<TCatRecord> Records { get; }`  
5.x: `public IEnumerable<TCatRecord> Records { get; }`  

**public property Nest.IClearCachedRealmsResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("nodes")]
public IDictionary<string, ShieldNode> Nodes { get; }
```

5.x
```csharp
[JsonPropertyAttribute("nodes")]
public IReadOnlyDictionary<string, SecurityNode> Nodes { get; }
```

**public property Nest.IClearCachedRolesResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("nodes")]
public IDictionary<string, ShieldNode> Nodes { get; }
```

5.x
```csharp
[JsonPropertyAttribute("nodes")]
public IReadOnlyDictionary<string, SecurityNode> Nodes { get; }
```

**public property Nest.IClusterGetSettingsResponse.Persistent** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IDictionary<string, object> Persistent { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyDictionary<string, object> Persistent { get; }
```

**public property Nest.IClusterGetSettingsResponse.Transient** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IDictionary<string, object> Transient { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyDictionary<string, object> Transient { get; }
```

**public property Nest.IClusterHealthResponse.Indices** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, IndexHealthStats> Indices { get; }`  
5.x: `public Dictionary<string, IndexHealthStats> Indices { get; }`  

**public property Nest.IClusterPendingTasksResponse.Tasks** *Declaration changed (Breaking)*

2.x: `public IEnumerable<PendingTask> Tasks { get; set; }`  
5.x: `public IEnumerable<PendingTask> Tasks { get; set; }`  

**public property Nest.IClusterPutSettingsResponse.Persistent** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IDictionary<string, object> Persistent { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyDictionary<string, object> Persistent { get; }
```

**public property Nest.IClusterPutSettingsResponse.Transient** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IDictionary<string, object> Transient { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyDictionary<string, object> Transient { get; }
```

**public property Nest.IClusterRerouteResponse.Explanations** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("explanations")]
public IEnumerable<ClusterRerouteExplanation> Explanations { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("explanations")]
public IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; }
```

**public property Nest.IClusterStateResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("nodes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, NodeState> Nodes { get; }
```

5.x
```csharp
[JsonPropertyAttribute("nodes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.NodeState])]
public IReadOnlyDictionary<string, NodeState> Nodes { get; }
```

**public property Nest.IDictionaryResponse&lt;TKey, TValue&gt;.BackingDictionary** *Declaration changed (Breaking)*

2.x: `public IDictionary<TKey, TValue> BackingDictionary { get; set; }`  
5.x: `public IDictionary<TKey, TValue> BackingDictionary { get; set; }`  

**public property Nest.IFieldStatsResponse.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("indices")]
public Dictionary<string, FieldStats> Indices { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("indices")]
public IReadOnlyDictionary<string, FieldStats> Indices { get; }
```

**public property Nest.IGetFieldMappingResponse.Indices** *Declaration changed (Breaking)*

2.x: `public IndexFieldMappings Indices { get; set; }`  
5.x: `public IndexFieldMappings Indices { get; set; }`  

**public property Nest.IGetIndexResponse.Indices** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, IndexState> Indices { get; set; }`  
5.x: `public IDictionary<string, IndexState> Indices { get; set; }`  

**public property Nest.IGetIndexSettingsResponse.Indices** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, IIndexState> Indices { get; }`  
5.x: `public IDictionary<string, IIndexState> Indices { get; }`  

**public property Nest.IGetIndexTemplateResponse.TemplateMappings** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, TemplateMapping> TemplateMappings { get; }`  
5.x: `public IDictionary<string, TemplateMapping> TemplateMappings { get; }`  

**public property Nest.IGetMappingResponse.Mappings** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, IList<TypeMapping>> Mappings { get; }`  
5.x: `public Dictionary<string, IList<TypeMapping>> Mappings { get; }`  

**public property Nest.IGetRepositoryResponse.Repositories** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, ISnapshotRepository> Repositories { get; set; }`  
5.x: `public IDictionary<string, ISnapshotRepository> Repositories { get; set; }`  

**public property Nest.IGetRoleResponse.Roles** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, Role> Roles { get; }`  
5.x: `public IDictionary<string, Role> Roles { get; }`  

**public property Nest.IGetSnapshotResponse.Snapshots** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("snapshots")]
public IEnumerable<Snapshot> Snapshots { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("snapshots")]
public IReadOnlyCollection<Snapshot> Snapshots { get; }
```

**public property Nest.IGetUserResponse.Users** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, User> Users { get; }`  
5.x: `public IDictionary<string, User> Users { get; }`  

**public property Nest.IGraphExploreResponse.Connections** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("connections")]
public IEnumerable<GraphConnection> Connections { get; }
```

5.x
```csharp
[JsonPropertyAttribute("connections")]
public IReadOnlyCollection<GraphConnection> Connections { get; }
```

**public property Nest.IGraphExploreResponse.Failures** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("failures")]
public IEnumerable<ShardFailure> Failures { get; }
```

5.x
```csharp
[JsonPropertyAttribute("failures")]
public IReadOnlyCollection<ShardFailure> Failures { get; }
```

**public property Nest.IGraphExploreResponse.Vertices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("vertices")]
public IEnumerable<GraphVertex> Vertices { get; }
```

5.x
```csharp
[JsonPropertyAttribute("vertices")]
public IReadOnlyCollection<GraphVertex> Vertices { get; }
```

**public property Nest.IHit&lt;T&gt;.InnerHits** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, InnerHitsResult> InnerHits { get; }`  
5.x: `public IDictionary<string, InnerHitsResult> InnerHits { get; }`  

**public property Nest.IHit&lt;T&gt;.MatchedQueries** *Declaration changed (Breaking)*

2.x: `public IEnumerable<string> MatchedQueries { get; }`  
5.x: `public IEnumerable<string> MatchedQueries { get; }`  

**public property Nest.IHit&lt;T&gt;.Sorts** *Declaration changed (Breaking)*

2.x: `public IEnumerable<object> Sorts { get; }`  
5.x: `public IEnumerable<object> Sorts { get; }`  

**public property Nest.IIndicesShardStoresResponse.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, IndicesShardStores> Indices { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.IndicesShardStores])]
public IReadOnlyDictionary<string, IndicesShardStores> Indices { get; }
```

**public property Nest.IIndicesStatsResponse.Indices** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, IndicesStats> Indices { get; set; }`  
5.x: `public Dictionary<string, IndicesStats> Indices { get; set; }`  

**public property Nest.IMultiGetResponse.Documents** *Declaration changed (Breaking)*

2.x: `public IEnumerable<IMultiGetHit<object>> Documents { get; }`  
5.x: `public IEnumerable<IMultiGetHit<object>> Documents { get; }`  

**public property Nest.IMultiTermVectorsResponse.Documents** *Declaration changed (Breaking)*

2.x: `public IEnumerable<TermVectorsResponse> Documents { get; }`  
5.x: `public IEnumerable<TermVectorsResponse> Documents { get; }`  

**public property Nest.IndexHealthStats.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, ShardHealthStats> Shards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.ShardHealthStats])]
public IReadOnlyDictionary<string, ShardHealthStats> Shards { get; internal set; }
```

**public property Nest.IndexingStats.Types** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, IndexingStats> Types { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("types")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.IndexingStats])]
public IReadOnlyDictionary<string, IndexingStats> Types { get; set; }
```

**public property Nest.IndexRoutingTable.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("shards")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, List<RoutingShard>> Shards { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("shards")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,System.Collections.Generic.List`1[Nest.RoutingShard]])]
public IReadOnlyDictionary<string, List<RoutingShard>> Shards { get; internal set; }
```

**public property Nest.IndexSegment.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, ShardsSegment> Shards { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.ShardsSegment])]
public IReadOnlyDictionary<string, ShardsSegment> Shards { get; internal set; }
```

**public property Nest.IndicesShardStores.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, ShardStoreWrapper> Shards { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.ShardStoreWrapper])]
public IReadOnlyDictionary<string, ShardStoreWrapper> Shards { get; internal set; }
```

**public property Nest.IndicesShardStoresResponse.Indices** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, IndicesShardStores> Indices { get; set; }`  
5.x: `public Dictionary<string, IndicesShardStores> Indices { get; set; }`  

**public property Nest.IndicesStatsResponse.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, IndicesStats> Indices { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.IndicesStats])]
public IReadOnlyDictionary<string, IndicesStats> Indices { get; internal set; }
```

**public property Nest.INodesHotThreadsResponse.HotThreads** *Declaration changed (Breaking)*

2.x: `public IEnumerable<HotThreadInformation> HotThreads { get; }`  
5.x: `public IEnumerable<HotThreadInformation> HotThreads { get; }`  

**public property Nest.INodesInfoResponse.Nodes** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, NodeInfo> Nodes { get; }`  
5.x: `public Dictionary<string, NodeInfo> Nodes { get; }`  

**public property Nest.INodesStatsResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, NodeStats> Nodes { get; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.NodeStats])]
public IReadOnlyDictionary<string, NodeStats> Nodes { get; }
```

**public property Nest.IPercolateResponse.Matches** *Declaration changed (Breaking)*

2.x: `public IEnumerable<PercolatorMatch> Matches { get; }`  
5.x: `public IEnumerable<PercolatorMatch> Matches { get; }`  

**public property Nest.IRecoveryStatusResponse.Indices** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, RecoveryStatus> Indices { get; set; }`  
5.x: `public Dictionary<string, RecoveryStatus> Indices { get; set; }`  

**public property Nest.IReindexOnServerResponse.Failures** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("failures")]
public IEnumerable<BulkIndexByScrollFailure> Failures { get; }
```

5.x
```csharp
[JsonPropertyAttribute("failures")]
public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }
```

**public property Nest.IReindexRethrottleResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("nodes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, ReindexNode> Nodes { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("nodes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.ReindexNode])]
public IReadOnlyDictionary<string, ReindexNode> Nodes { get; }
```

**public property Nest.ISearchResponse&lt;T&gt;.Aggregations** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, IAggregate> Aggregations { get; }`  
5.x: `public IDictionary<string, IAggregate> Aggregations { get; }`  

**public property Nest.ISearchResponse&lt;T&gt;.Documents** *Declaration changed (Breaking)*

2.x: `public IEnumerable<T> Documents { get; }`  
5.x: `public IEnumerable<T> Documents { get; }`  

**public property Nest.ISearchResponse&lt;T&gt;.Fields** *Declaration changed (Breaking)*

2.x: `public IEnumerable<FieldValues> Fields { get; }`  
5.x: `public IEnumerable<FieldValues> Fields { get; }`  

**public property Nest.ISearchResponse&lt;T&gt;.Hits** *Declaration changed (Breaking)*

2.x: `public IEnumerable<IHit<T>> Hits { get; }`  
5.x: `public IEnumerable<IHit<T>> Hits { get; }`  

**public property Nest.ISearchResponse&lt;T&gt;.Suggest** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, Suggest[]> Suggest { get; }`  
5.x: `public IDictionary<string, Suggest[]> Suggest { get; }`  

**public property Nest.ISearchShardsResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("nodes")]
public IDictionary<string, SearchNode> Nodes { get; }
```

5.x
```csharp
[JsonPropertyAttribute("nodes")]
public IReadOnlyDictionary<string, SearchNode> Nodes { get; }
```

**public property Nest.ISearchShardsResponse.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("shards")]
public IEnumerable<IEnumerable<SearchShard>> Shards { get; }
```

5.x
```csharp
[JsonPropertyAttribute("shards")]
public IReadOnlyCollection<IReadOnlyCollection<SearchShard>> Shards { get; }
```

**public property Nest.ISegmentsResponse.Indices** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, IndexSegment> Indices { get; set; }`  
5.x: `public Dictionary<string, IndexSegment> Indices { get; set; }`  

**public property Nest.ISnapshotStatusResponse.Snapshots** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("snapshots")]
public IEnumerable<SnapshotStatus> Snapshots { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("snapshots")]
public IReadOnlyCollection<SnapshotStatus> Snapshots { get; }
```

**public property Nest.IUpdateByQueryResponse.Failures** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("failures")]
public IEnumerable<BulkIndexByScrollFailure> Failures { get; }
```

5.x
```csharp
[JsonPropertyAttribute("failures")]
public IReadOnlyCollection<BulkIndexByScrollFailure> Failures { get; }
```

**public property Nest.IUpgradeStatusResponse.Upgrades** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, UpgradeStatus> Upgrades { get; set; }`  
5.x: `public Dictionary<string, UpgradeStatus> Upgrades { get; set; }`  

**public property Nest.IValidateQueryResponse.Explanations** *Declaration changed (Breaking)*

2.x: `public IList<ValidationExplanation> Explanations { get; set; }`  
5.x: `public IList<ValidationExplanation> Explanations { get; set; }`  

**public property Nest.IVerifyRepositoryResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, CompactNodeInfo> Nodes { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.CompactNodeInfo])]
public IReadOnlyDictionary<string, CompactNodeInfo> Nodes { get; }
```

**public property Nest.IWatcherStatsResponse.CurrentWatches** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("current_watches")]
public IEnumerable<WatchRecordStats> CurrentWatches { get; }
```

5.x
```csharp
[JsonPropertyAttribute("current_watches")]
public IReadOnlyCollection<WatchRecordStats> CurrentWatches { get; }
```

**public property Nest.IWatcherStatsResponse.QueuedWatches** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("queued_watches")]
public IEnumerable<WatchRecordQueuedStats> QueuedWatches { get; }
```

5.x
```csharp
[JsonPropertyAttribute("queued_watches")]
public IReadOnlyCollection<WatchRecordQueuedStats> QueuedWatches { get; }
```

**public property Nest.LicenseAcknowledgement.License** *Declaration changed (Breaking)*

2.x: `public String[] License { get; set; }`  
5.x: `public String[] License { get; set; }`  

**public property Nest.MetadataState.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("indices")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, MetadataIndexState> Indices { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("indices")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.MetadataIndexState])]
public IReadOnlyDictionary<string, MetadataIndexState> Indices { get; internal set; }
```

**public property Nest.MetadataState.Templates** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("templates")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public IDictionary<string, TemplateMapping> Templates { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("templates")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.TemplateMapping])]
public IReadOnlyDictionary<string, TemplateMapping> Templates { get; internal set; }
```

**public property Nest.MetricAggregateBase.Meta** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, object> Meta { get; set; }`  
5.x: `public IDictionary<string, object> Meta { get; set; }`  

**public property Nest.MultiBucketAggregate&lt;TBucket&gt;.Buckets** *Declaration changed (Breaking)*

2.x: `public IList<TBucket> Buckets { get; set; }`  
5.x: `public IList<TBucket> Buckets { get; set; }`  

**public property Nest.MultiGetResponse.Documents** *Declaration changed (Breaking)*

2.x: `public IEnumerable<IMultiGetHit<object>> Documents { get; }`  
5.x: `public IEnumerable<IMultiGetHit<object>> Documents { get; }`  

**public property Nest.MultiTermVectorsResponse.Documents** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("docs")]
public IEnumerable<TermVectorsResponse> Documents { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("docs")]
[JsonConverterAttribute(Nest.ReadOnlyCollectionJsonConverter`2[Nest.TermVectorsResult,Nest.ITermVectors])]
public IReadOnlyCollection<ITermVectors> Documents { get; internal set; }
```

**public property Nest.NodesHotThreadsResponse.HotThreads** *Declaration changed (Breaking)*

2.x: `public IEnumerable<HotThreadInformation> HotThreads { get; }`  
5.x: `public IEnumerable<HotThreadInformation> HotThreads { get; }`  

**public property Nest.NodesInfoResponse.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, NodeInfo> Nodes { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.NodeInfo])]
public IReadOnlyDictionary<string, NodeInfo> Nodes { get; internal set; }
```

**public property Nest.NodesStatsResponse.Nodes** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, NodeStats> Nodes { get; set; }`  
5.x: `public Dictionary<string, NodeStats> Nodes { get; set; }`  

**public property Nest.PercolateResponse.Matches** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("matches")]
public IEnumerable<PercolatorMatch> Matches { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyCollection<PercolatorMatch> Matches { get; internal set; }
```

**public property Nest.PercolatorMatch.Highlight** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("highlight")]
public Dictionary<string, IList<string>> Highlight { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyDictionary<string, IList<string>> Highlight { get; internal set; }
```

**public property Nest.Profile.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("shards")]
public IEnumerable<ShardProfile> Shards { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("shards")]
public IReadOnlyCollection<ShardProfile> Shards { get; internal set; }
```

**public property Nest.RecoveryStatus.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("shards")]
public IEnumerable<ShardRecovery> Shards { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("shards")]
public IReadOnlyCollection<ShardRecovery> Shards { get; internal set; }
```

**public property Nest.RecoveryStatusResponse.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, RecoveryStatus> Indices { get; set; }
```

5.x
```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.RecoveryStatus])]
public IReadOnlyDictionary<string, RecoveryStatus> Indices { get; internal set; }
```

**public property Nest.ReindexNode.Attributes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("attributes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, string> Attributes { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("attributes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,System.String])]
public IReadOnlyDictionary<string, string> Attributes { get; internal set; }
```

**public property Nest.ReindexNode.Tasks** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("tasks")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<TaskId, ReindexTask> Tasks { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("tasks")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[Nest.TaskId,Nest.ReindexTask])]
public IReadOnlyDictionary<TaskId, ReindexTask> Tasks { get; internal set; }
```

**public property Nest.ReindexOnServerResponse.Failures** *Declaration changed (Breaking)*

2.x: `public IEnumerable<BulkIndexByScrollFailure> Failures { get; internal set; }`  
5.x: `public IEnumerable<BulkIndexByScrollFailure> Failures { get; internal set; }`  

**public property Nest.ReindexRethrottleResponse.Nodes** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, ReindexNode> Nodes { get; set; }`  
5.x: `public Dictionary<string, ReindexNode> Nodes { get; set; }`  

**public property Nest.RoutingNodesState.Nodes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("nodes")]
public Dictionary<string, List<RoutingShard>> Nodes { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("nodes")]
public IReadOnlyDictionary<string, List<RoutingShard>> Nodes { get; internal set; }
```

**public property Nest.RoutingNodesState.Unassigned** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("unassigned")]
public List<RoutingShard> Unassigned { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("unassigned")]
public IReadOnlyCollection<RoutingShard> Unassigned { get; internal set; }
```

**public property Nest.RoutingTableState.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("indices")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, IndexRoutingTable> Indices { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("indices")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.IndexRoutingTable])]
public IReadOnlyDictionary<string, IndexRoutingTable> Indices { get; internal set; }
```

**public property Nest.SearchProfile.Collector** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("collector")]
public IEnumerable<Collector> Collector { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("collector")]
public IReadOnlyCollection<Collector> Collector { get; internal set; }
```

**public property Nest.SearchProfile.Query** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("query")]
public IEnumerable<QueryProfile> Query { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("query")]
public IReadOnlyCollection<QueryProfile> Query { get; internal set; }
```

**public property Nest.SearchResponse&lt;T&gt;.Aggregations** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public IDictionary<string, IAggregate> Aggregations { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.IAggregate])]
public IReadOnlyDictionary<string, IAggregate> Aggregations { get; internal set; }
```

**public property Nest.SearchResponse&lt;T&gt;.Documents** *Declaration changed (Breaking)*

2.x
```csharp
[JsonIgnoreAttribute]
public IEnumerable<T> Documents { get; }
```

5.x
```csharp
[JsonIgnoreAttribute]
public IReadOnlyCollection<T> Documents { get; }
```

**public property Nest.SearchResponse&lt;T&gt;.Fields** *Declaration changed (Breaking)*

2.x: `public IEnumerable<FieldValues> Fields { get; }`  
5.x: `public IEnumerable<FieldValues> Fields { get; }`  

**public property Nest.SearchResponse&lt;T&gt;.Hits** *Declaration changed (Breaking)*

2.x
```csharp
[JsonIgnoreAttribute]
public IEnumerable<IHit<T>> Hits { get; }
```

5.x
```csharp
[JsonIgnoreAttribute]
public IReadOnlyCollection<IHit<T>> Hits { get; }
```

**public property Nest.SearchResponse&lt;T&gt;.Suggest** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IDictionary<string, Suggest[]> Suggest { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyDictionary<string, Suggest`1[]> Suggest { get; internal set; }
```

**public property Nest.SearchShardsResponse.Nodes** *Declaration changed (Breaking)*

2.x: `public IDictionary<string, SearchNode> Nodes { get; internal set; }`  
5.x: `public IDictionary<string, SearchNode> Nodes { get; internal set; }`  

**public property Nest.SearchShardsResponse.Shards** *Declaration changed (Breaking)*

2.x: `public IEnumerable<IEnumerable<SearchShard>> Shards { get; internal set; }`  
5.x: `public IEnumerable<IEnumerable<SearchShard>> Shards { get; internal set; }`  

**public property Nest.SegmentsResponse.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, IndexSegment> Indices { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.IndexSegment])]
public IReadOnlyDictionary<string, IndexSegment> Indices { get; internal set; }
```

**public property Nest.ShardProfile.Searches** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("searches")]
public IEnumerable<SearchProfile> Searches { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("searches")]
public IReadOnlyCollection<SearchProfile> Searches { get; internal set; }
```

**public property Nest.ShardsMetaData.Failures** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("failures")]
public IEnumerable<ShardFailure> Failures { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("failures")]
public IReadOnlyCollection<ShardFailure> Failures { get; internal set; }
```

**public property Nest.ShardsSegment.Segments** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, Segment> Segments { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.Segment])]
public IReadOnlyDictionary<string, Segment> Segments { get; internal set; }
```

**public property Nest.ShardStore.Attributes** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("attributes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, object> Attributes { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("attributes")]
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,System.Object])]
public IReadOnlyDictionary<string, object> Attributes { get; internal set; }
```

**public property Nest.ShardStoreWrapper.Stores** *Declaration changed (Breaking)*

2.x
```csharp
public IEnumerable<ShardStore> Stores { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyCollection<ShardStore> Stores { get; internal set; }
```

**public property Nest.Snapshot.Failures** *Declaration changed (Breaking)*

2.x
```csharp
[JsonIgnoreAttribute]
public IEnumerable<string> Failures { get; }
```

5.x
```csharp
[JsonPropertyAttribute("failures")]
public IReadOnlyCollection<SnapshotShardFailure> Failures { get; internal set; }
```

**public property Nest.Snapshot.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("indices")]
public IEnumerable<IndexName> Indices { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("indices")]
public IReadOnlyCollection<IndexName> Indices { get; internal set; }
```

**public property Nest.SnapshotIndexStats.Shards** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("shards")]
public IDictionary<string, SnapshotShardsStats> Shards { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("shards")]
public IReadOnlyDictionary<string, SnapshotShardsStats> Shards { get; internal set; }
```

**public property Nest.SnapshotRestore.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("indices")]
public IEnumerable<IndexName> Indices { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("indices")]
public IReadOnlyCollection<IndexName> Indices { get; internal set; }
```

**public property Nest.SnapshotStatus.Indices** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("indices")]
public IDictionary<string, SnapshotIndexStats> Indices { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("indices")]
public IReadOnlyDictionary<string, SnapshotIndexStats> Indices { get; internal set; }
```

**public property Nest.SnapshotStatusResponse.Snapshots** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("snapshots")]
public IEnumerable<SnapshotStatus> Snapshots { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("snapshots")]
public IReadOnlyCollection<SnapshotStatus> Snapshots { get; internal set; }
```

**public property Nest.TaskExecutingNode.Tasks** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("tasks")]
public IDictionary<TaskId, TaskState> Tasks { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("tasks")]
public IReadOnlyDictionary<TaskId, TaskState> Tasks { get; internal set; }
```

**public property Nest.TermVector.Terms** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("terms")]
public IDictionary<string, TermVectorTerm> Terms { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("terms")]
public IReadOnlyDictionary<string, TermVectorTerm> Terms { get; internal set; }
```

**public property Nest.TermVectorsResponse.TermVectors** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("term_vectors")]
public IDictionary<string, TermVector> TermVectors { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("term_vectors")]
public IReadOnlyDictionary<string, TermVector> TermVectors { get; internal set; }
```

**public property Nest.TermVectorTerm.Tokens** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("tokens")]
public IEnumerable<Token> Tokens { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("tokens")]
public IReadOnlyCollection<Token> Tokens { get; internal set; }
```

**public method Nest.TopHitsAggregate.Documents&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IEnumerable<T> Documents<T>(JsonSerializer serializer)`  
5.x: `public IEnumerable<T> Documents<T>(JsonSerializer serializer)`  

**public method Nest.TopHitsAggregate.Hits&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IEnumerable<Hit<T>> Hits<T>(JsonSerializer serializer)`  
5.x: `public IEnumerable<Hit<T>> Hits<T>(JsonSerializer serializer)`  

**public property Nest.TypeFieldMappings.Mappings** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("mappings")]
public Dictionary<string, FieldMappingProperties> Mappings { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("mappings")]
public IReadOnlyDictionary<string, FieldMappingProperties> Mappings { get; internal set; }
```

**public property Nest.UpdateByQueryResponse.Failures** *Declaration changed (Breaking)*

2.x: `public IEnumerable<BulkIndexByScrollFailure> Failures { get; internal set; }`  
5.x: `public IEnumerable<BulkIndexByScrollFailure> Failures { get; internal set; }`  

**public property Nest.UpgradeStatusResponse.Upgrades** *Declaration changed (Breaking)*

2.x
```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter)]
public Dictionary<string, UpgradeStatus> Upgrades { get; set; }
```

5.x
```csharp
[JsonConverterAttribute(Nest.VerbatimDictionaryKeysJsonConverter`2[System.String,Nest.UpgradeStatus])]
public IReadOnlyDictionary<string, UpgradeStatus> Upgrades { get; internal set; }
```

**public property Nest.ValidateQueryResponse.Explanations** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute]
public IList<ValidationExplanation> Explanations { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute]
public IReadOnlyCollection<ValidationExplanation> Explanations { get; internal set; }
```

**public property Nest.VerifyRepositoryResponse.Nodes** *Declaration changed (Breaking)*

2.x: `public Dictionary<string, CompactNodeInfo> Nodes { get; set; }`  
5.x: `public Dictionary<string, CompactNodeInfo> Nodes { get; set; }`  

**public property Nest.Watch.Meta** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("meta")]
public IDictionary<string, object> Meta { get; internal set; }
```

5.x
```csharp
[JsonPropertyAttribute("meta")]
public IReadOnlyDictionary<string, object> Meta { get; internal set; }
```

**public property Nest.WatcherStatsResponse.CurrentWatches** *Declaration changed (Breaking)*

2.x: `public IEnumerable<WatchRecordStats> CurrentWatches { get; internal set; }`  
5.x: `public IEnumerable<WatchRecordStats> CurrentWatches { get; internal set; }`  

**public property Nest.WatcherStatsResponse.QueuedWatches** *Declaration changed (Breaking)*

2.x: `public IEnumerable<WatchRecordQueuedStats> QueuedWatches { get; internal set; }`  
5.x: `public IEnumerable<WatchRecordQueuedStats> QueuedWatches { get; internal set; }`  

**public property Nest.WatchRecord.Messages** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("messages")]
public IEnumerable<string> Messages { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("messages")]
public IReadOnlyCollection<string> Messages { get; set; }
```

**public property Nest.WatchRecord.Metadata** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("metadata")]
public IDictionary<string, object> Metadata { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("metadata")]
public IReadOnlyDictionary<string, object> Metadata { get; set; }
```

**public property Nest.WatchStatus.Actions** *Declaration changed (Breaking)*

2.x
```csharp
[JsonPropertyAttribute("actions")]
public Dictionary<string, ActionStatus> Actions { get; set; }
```

5.x
```csharp
[JsonPropertyAttribute("actions")]
public IReadOnlyDictionary<string, ActionStatus> Actions { get; set; }
```

#CancellationToken
**public method Nest.BulkAllObservable&lt;T&gt;..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor(IElasticClient client, IConnectionSettingsValues connectionSettings, IBulkAllRequest<T> partionedBulkRequest, CancellationToken cancellationToken)`  
5.x: `public  .ctor(IElasticClient client, IConnectionSettingsValues connectionSettings, IBulkAllRequest<T> partionedBulkRequest, CancellationToken cancellationToken)`  

**public method Nest.DeleteManyExtensions.DeleteManyAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static Task<IBulkResponse> DeleteManyAsync<T>(IElasticClient client, IEnumerable<T> objects, string index, string type)
```

5.x
```csharp
[ExtensionAttribute]
public static Task<IBulkResponse> DeleteManyAsync<T>(IElasticClient client, IEnumerable<T> objects, IndexName index, TypeName type, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.AcknowledgeWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(IAcknowledgeWatchRequest request)`  
5.x: `public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(IAcknowledgeWatchRequest request)`  

**public method Nest.ElasticClient.AcknowledgeWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector)`  
5.x: `public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector)`  

**public method Nest.ElasticClient.ActivateWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request)`  
5.x: `public Task<IActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request)`  

**public method Nest.ElasticClient.ActivateWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector)`  
5.x: `public Task<IActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector)`  

**public method Nest.ElasticClient.AliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IBulkAliasResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> selector)`  
5.x: `public Task<IBulkAliasResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> selector)`  

**public method Nest.ElasticClient.AliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IBulkAliasResponse> AliasAsync(IBulkAliasRequest request)`  
5.x: `public Task<IBulkAliasResponse> AliasAsync(IBulkAliasRequest request)`  

**public method Nest.ElasticClient.AliasExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest request)`  
5.x: `public Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest request)`  

**public method Nest.ElasticClient.AliasExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, IAliasExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, IAliasExistsRequest> selector)`  

**public method Nest.ElasticClient.AnalyzeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request)`  
5.x: `public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request)`  

**public method Nest.ElasticClient.AnalyzeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector)`  
5.x: `public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector)`  

**public method Nest.ElasticClient.AuthenticateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request)`  
5.x: `public Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request)`  

**public method Nest.ElasticClient.AuthenticateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector)`  
5.x: `public Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector)`  

**public method Nest.ElasticClient.BulkAsync** *Declaration changed (Breaking)*

2.x: `public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> selector)`  
5.x: `public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> selector)`  

**public method Nest.ElasticClient.BulkAsync** *Declaration changed (Breaking)*

2.x: `public Task<IBulkResponse> BulkAsync(IBulkRequest request)`  
5.x: `public Task<IBulkResponse> BulkAsync(IBulkRequest request)`  

**public method Nest.ElasticClient.CatAliasesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request)`  
5.x: `public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request)`  

**public method Nest.ElasticClient.CatAliasesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector)`  
5.x: `public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector)`  

**public method Nest.ElasticClient.CatAllocationAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request)`  
5.x: `public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request)`  

**public method Nest.ElasticClient.CatAllocationAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, ICatAllocationRequest> selector)`  
5.x: `public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, ICatAllocationRequest> selector)`  

**public method Nest.ElasticClient.CatCountAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request)`  
5.x: `public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request)`  

**public method Nest.ElasticClient.CatCountAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector)`  
5.x: `public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector)`  

**public method Nest.ElasticClient.CatFielddataAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request)`  
5.x: `public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request)`  

**public method Nest.ElasticClient.CatFielddataAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, ICatFielddataRequest> selector)`  
5.x: `public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, ICatFielddataRequest> selector)`  

**public method Nest.ElasticClient.CatHealthAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request)`  
5.x: `public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request)`  

**public method Nest.ElasticClient.CatHealthAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, ICatHealthRequest> selector)`  
5.x: `public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, ICatHealthRequest> selector)`  

**public method Nest.ElasticClient.CatHelpAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request)`  
5.x: `public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request)`  

**public method Nest.ElasticClient.CatHelpAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector)`  
5.x: `public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector)`  

**public method Nest.ElasticClient.CatIndicesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request)`  
5.x: `public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request)`  

**public method Nest.ElasticClient.CatIndicesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, ICatIndicesRequest> selector)`  
5.x: `public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, ICatIndicesRequest> selector)`  

**public method Nest.ElasticClient.CatMasterAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request)`  
5.x: `public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request)`  

**public method Nest.ElasticClient.CatMasterAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, ICatMasterRequest> selector)`  
5.x: `public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, ICatMasterRequest> selector)`  

**public method Nest.ElasticClient.CatNodeAttributesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request)`  
5.x: `public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request)`  

**public method Nest.ElasticClient.CatNodeAttributesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector)`  
5.x: `public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector)`  

**public method Nest.ElasticClient.CatNodesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request)`  
5.x: `public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request)`  

**public method Nest.ElasticClient.CatNodesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector)`  
5.x: `public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector)`  

**public method Nest.ElasticClient.CatPendingTasksAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request)`  
5.x: `public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request)`  

**public method Nest.ElasticClient.CatPendingTasksAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector)`  
5.x: `public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector)`  

**public method Nest.ElasticClient.CatPluginsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request)`  
5.x: `public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request)`  

**public method Nest.ElasticClient.CatPluginsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, ICatPluginsRequest> selector)`  
5.x: `public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, ICatPluginsRequest> selector)`  

**public method Nest.ElasticClient.CatRecoveryAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request)`  
5.x: `public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request)`  

**public method Nest.ElasticClient.CatRecoveryAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector)`  
5.x: `public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector)`  

**public method Nest.ElasticClient.CatRepositoriesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request)`  
5.x: `public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request)`  

**public method Nest.ElasticClient.CatRepositoriesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector)`  
5.x: `public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector)`  

**public method Nest.ElasticClient.CatSegmentsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request)`  
5.x: `public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request)`  

**public method Nest.ElasticClient.CatSegmentsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector)`  
5.x: `public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector)`  

**public method Nest.ElasticClient.CatShardsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request)`  
5.x: `public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request)`  

**public method Nest.ElasticClient.CatShardsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, ICatShardsRequest> selector)`  
5.x: `public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, ICatShardsRequest> selector)`  

**public method Nest.ElasticClient.CatSnapshotsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request)`  
5.x: `public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request)`  

**public method Nest.ElasticClient.CatSnapshotsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector)`  
5.x: `public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector)`  

**public method Nest.ElasticClient.CatThreadPoolAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request)`  
5.x: `public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request)`  

**public method Nest.ElasticClient.CatThreadPoolAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector)`  
5.x: `public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector)`  

**public method Nest.ElasticClient.ClearCacheAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCacheResponse> ClearCacheAsync(IClearCacheRequest request)`  
5.x: `public Task<IClearCacheResponse> ClearCacheAsync(IClearCacheRequest request)`  

**public method Nest.ElasticClient.ClearCacheAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCacheResponse> ClearCacheAsync(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector)`  
5.x: `public Task<IClearCacheResponse> ClearCacheAsync(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector)`  

**public method Nest.ElasticClient.ClearCachedRealmsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request)`  
5.x: `public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request)`  

**public method Nest.ElasticClient.ClearCachedRealmsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector)`  
5.x: `public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector)`  

**public method Nest.ElasticClient.ClearCachedRolesAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request)`  
5.x: `public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request)`  

**public method Nest.ElasticClient.ClearCachedRolesAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector)`  
5.x: `public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector)`  

**public method Nest.ElasticClient.ClearScrollAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearScrollResponse> ClearScrollAsync(IClearScrollRequest request)`  
5.x: `public Task<IClearScrollResponse> ClearScrollAsync(IClearScrollRequest request)`  

**public method Nest.ElasticClient.ClearScrollAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearScrollResponse> ClearScrollAsync(Func<ClearScrollDescriptor, IClearScrollRequest> selector)`  
5.x: `public Task<IClearScrollResponse> ClearScrollAsync(Func<ClearScrollDescriptor, IClearScrollRequest> selector)`  

**public method Nest.ElasticClient.CloseIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICloseIndexResponse> CloseIndexAsync(ICloseIndexRequest request)`  
5.x: `public Task<ICloseIndexResponse> CloseIndexAsync(ICloseIndexRequest request)`  

**public method Nest.ElasticClient.CloseIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICloseIndexResponse> CloseIndexAsync(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector)`  
5.x: `public Task<ICloseIndexResponse> CloseIndexAsync(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector)`  

**public method Nest.ElasticClient.ClusterGetSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request)`  
5.x: `public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request)`  

**public method Nest.ElasticClient.ClusterGetSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector)`  
5.x: `public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector)`  

**public method Nest.ElasticClient.ClusterHealthAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request)`  
5.x: `public Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request)`  

**public method Nest.ElasticClient.ClusterHealthAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector)`  
5.x: `public Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector)`  

**public method Nest.ElasticClient.ClusterPendingTasksAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request)`  
5.x: `public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request)`  

**public method Nest.ElasticClient.ClusterPendingTasksAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector)`  
5.x: `public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector)`  

**public method Nest.ElasticClient.ClusterPutSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request)`  
5.x: `public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request)`  

**public method Nest.ElasticClient.ClusterPutSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector)`  
5.x: `public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector)`  

**public method Nest.ElasticClient.ClusterRerouteAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request)`  
5.x: `public Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request)`  

**public method Nest.ElasticClient.ClusterRerouteAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector)`  
5.x: `public Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector)`  

**public method Nest.ElasticClient.ClusterStateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest request)`  
5.x: `public Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest request)`  

**public method Nest.ElasticClient.ClusterStateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, IClusterStateRequest> selector)`  
5.x: `public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, IClusterStateRequest> selector)`  

**public method Nest.ElasticClient.ClusterStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest request)`  
5.x: `public Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest request)`  

**public method Nest.ElasticClient.ClusterStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector)`  
5.x: `public Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector)`  

**public method Nest.ElasticClient.CountAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ICountResponse> CountAsync<T>(ICountRequest request)`  
5.x: `public Task<ICountResponse> CountAsync<T>(ICountRequest request)`  

**public method Nest.ElasticClient.CountAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, ICountRequest> selector)`  
5.x: `public Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, ICountRequest> selector)`  

**public method Nest.ElasticClient.CreateIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICreateIndexResponse> CreateIndexAsync(ICreateIndexRequest request)`  
5.x: `public Task<ICreateIndexResponse> CreateIndexAsync(ICreateIndexRequest request)`  

**public method Nest.ElasticClient.CreateIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICreateIndexResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector)`  
5.x: `public Task<ICreateIndexResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector)`  

**public method Nest.ElasticClient.CreateRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICreateRepositoryResponse> CreateRepositoryAsync(ICreateRepositoryRequest request)`  
5.x: `public Task<ICreateRepositoryResponse> CreateRepositoryAsync(ICreateRepositoryRequest request)`  

**public method Nest.ElasticClient.CreateRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICreateRepositoryResponse> CreateRepositoryAsync(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector)`  
5.x: `public Task<ICreateRepositoryResponse> CreateRepositoryAsync(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector)`  

**public method Nest.ElasticClient.DeactivateWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request)`  
5.x: `public Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request)`  

**public method Nest.ElasticClient.DeactivateWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector)`  
5.x: `public Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector)`  

**public method Nest.ElasticClient.DeleteAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteAliasResponse> DeleteAliasAsync(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector)`  
5.x: `public Task<IDeleteAliasResponse> DeleteAliasAsync(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector)`  

**public method Nest.ElasticClient.DeleteAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request)`  
5.x: `public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request)`  

**public method Nest.ElasticClient.DeleteAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteResponse> DeleteAsync(IDeleteRequest request)`  
5.x: `public Task<IDeleteResponse> DeleteAsync(IDeleteRequest request)`  

**public method Nest.ElasticClient.DeleteAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteResponse> DeleteAsync<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> selector)`  
5.x: `public Task<IDeleteResponse> DeleteAsync<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> selector)`  

**public method Nest.ElasticClient.DeleteByQueryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request)`  
5.x: `public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request)`  

**public method Nest.ElasticClient.DeleteByQueryAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  
5.x: `public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  

**public method Nest.ElasticClient.DeleteIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteIndexResponse> DeleteIndexAsync(IDeleteIndexRequest request)`  
5.x: `public Task<IDeleteIndexResponse> DeleteIndexAsync(IDeleteIndexRequest request)`  

**public method Nest.ElasticClient.DeleteIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteIndexResponse> DeleteIndexAsync(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector)`  
5.x: `public Task<IDeleteIndexResponse> DeleteIndexAsync(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector)`  

**public method Nest.ElasticClient.DeleteIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request)`  
5.x: `public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request)`  

**public method Nest.ElasticClient.DeleteIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector)`  
5.x: `public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector)`  

**public method Nest.ElasticClient.DeleteLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request)`  
5.x: `public Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request)`  

**public method Nest.ElasticClient.DeleteLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector)`  
5.x: `public Task<IDeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector)`  

**public method Nest.ElasticClient.DeleteRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request)`  
5.x: `public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request)`  

**public method Nest.ElasticClient.DeleteRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector)`  
5.x: `public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector)`  

**public method Nest.ElasticClient.DeleteRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request)`  
5.x: `public Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request)`  

**public method Nest.ElasticClient.DeleteRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector)`  
5.x: `public Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector)`  

**public method Nest.ElasticClient.DeleteScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request)`  
5.x: `public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request)`  

**public method Nest.ElasticClient.DeleteScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteScriptResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector)`  
5.x: `public Task<IDeleteScriptResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector)`  

**public method Nest.ElasticClient.DeleteSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request)`  
5.x: `public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request)`  

**public method Nest.ElasticClient.DeleteSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector)`  
5.x: `public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector)`  

**public method Nest.ElasticClient.DeleteSnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request)`  
5.x: `public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request)`  

**public method Nest.ElasticClient.DeleteSnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector)`  
5.x: `public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector)`  

**public method Nest.ElasticClient.DeleteUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request)`  
5.x: `public Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request)`  

**public method Nest.ElasticClient.DeleteUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector)`  
5.x: `public Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector)`  

**public method Nest.ElasticClient.DeleteWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request)`  
5.x: `public Task<IDeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request)`  

**public method Nest.ElasticClient.DeleteWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteWatchResponse> DeleteWatchAsync(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector)`  
5.x: `public Task<IDeleteWatchResponse> DeleteWatchAsync(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector)`  

**public method Nest.ElasticClient.DocumentExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request)`  
5.x: `public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request)`  

**public method Nest.ElasticClient.DocumentExistsAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> DocumentExistsAsync<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> DocumentExistsAsync<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> selector)`  

**public method Nest.ElasticClient.ExecuteWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request)`  
5.x: `public Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request)`  

**public method Nest.ElasticClient.ExecuteWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector)`  
5.x: `public Task<IExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector)`  

**public method Nest.ElasticClient.ExplainAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest<T> request)`  
5.x: `public Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest<T> request)`  

**public method Nest.ElasticClient.ExplainAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IExplainResponse<T>> ExplainAsync<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> selector)`  
5.x: `public Task<IExplainResponse<T>> ExplainAsync<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> selector)`  

**public method Nest.ElasticClient.FieldStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request)`  
5.x: `public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request)`  

**public method Nest.ElasticClient.FieldStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector)`  
5.x: `public Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector)`  

**public method Nest.ElasticClient.FlushAsync** *Declaration changed (Breaking)*

2.x: `public Task<IFlushResponse> FlushAsync(IFlushRequest request)`  
5.x: `public Task<IFlushResponse> FlushAsync(IFlushRequest request)`  

**public method Nest.ElasticClient.FlushAsync** *Declaration changed (Breaking)*

2.x: `public Task<IFlushResponse> FlushAsync(Indices indices, Func<FlushDescriptor, IFlushRequest> selector)`  
5.x: `public Task<IFlushResponse> FlushAsync(Indices indices, Func<FlushDescriptor, IFlushRequest> selector)`  

**public method Nest.ElasticClient.ForceMergeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request)`  
5.x: `public Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request)`  

**public method Nest.ElasticClient.ForceMergeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector)`  
5.x: `public Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector)`  

**public method Nest.ElasticClient.GetAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest request)`  
5.x: `public Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest request)`  

**public method Nest.ElasticClient.GetAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  
5.x: `public Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  

**public method Nest.ElasticClient.GetAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGetResponse<T>> GetAsync<T>(IGetRequest request)`  
5.x: `public Task<IGetResponse<T>> GetAsync<T>(IGetRequest request)`  

**public method Nest.ElasticClient.GetAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGetResponse<T>> GetAsync<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> selector)`  
5.x: `public Task<IGetResponse<T>> GetAsync<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> selector)`  

**public method Nest.ElasticClient.GetFieldMappingAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request)`  
5.x: `public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request)`  

**public method Nest.ElasticClient.GetFieldMappingAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector)`  
5.x: `public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector)`  

**public method Nest.ElasticClient.GetIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request)`  
5.x: `public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request)`  

**public method Nest.ElasticClient.GetIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector)`  
5.x: `public Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector)`  

**public method Nest.ElasticClient.GetIndexSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request)`  
5.x: `public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request)`  

**public method Nest.ElasticClient.GetIndexSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector)`  
5.x: `public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector)`  

**public method Nest.ElasticClient.GetIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request)`  
5.x: `public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request)`  

**public method Nest.ElasticClient.GetIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector)`  
5.x: `public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector)`  

**public method Nest.ElasticClient.GetLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request)`  
5.x: `public Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request)`  

**public method Nest.ElasticClient.GetLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector)`  
5.x: `public Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector)`  

**public method Nest.ElasticClient.GetMappingAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request)`  
5.x: `public Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request)`  

**public method Nest.ElasticClient.GetMappingAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector)`  
5.x: `public Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector)`  

**public method Nest.ElasticClient.GetRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request)`  
5.x: `public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request)`  

**public method Nest.ElasticClient.GetRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector)`  
5.x: `public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector)`  

**public method Nest.ElasticClient.GetRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request)`  
5.x: `public Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request)`  

**public method Nest.ElasticClient.GetRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector)`  
5.x: `public Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector)`  

**public method Nest.ElasticClient.GetScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest request)`  
5.x: `public Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest request)`  

**public method Nest.ElasticClient.GetScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetScriptResponse> GetScriptAsync(Name language, Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector)`  
5.x: `public Task<IGetScriptResponse> GetScriptAsync(Name language, Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector)`  

**public method Nest.ElasticClient.GetSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request)`  
5.x: `public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request)`  

**public method Nest.ElasticClient.GetSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector)`  
5.x: `public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector)`  

**public method Nest.ElasticClient.GetSnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest request)`  
5.x: `public Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest request)`  

**public method Nest.ElasticClient.GetSnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetSnapshotResponse> GetSnapshotAsync(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector)`  
5.x: `public Task<IGetSnapshotResponse> GetSnapshotAsync(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector)`  

**public method Nest.ElasticClient.GetUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetUserResponse> GetUserAsync(IGetUserRequest request)`  
5.x: `public Task<IGetUserResponse> GetUserAsync(IGetUserRequest request)`  

**public method Nest.ElasticClient.GetUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector)`  
5.x: `public Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector)`  

**public method Nest.ElasticClient.GetWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request)`  
5.x: `public Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request)`  

**public method Nest.ElasticClient.GetWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector)`  
5.x: `public Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector)`  

**public method Nest.ElasticClient.GraphExploreAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request)`  
5.x: `public Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request)`  

**public method Nest.ElasticClient.GraphExploreAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)`  
5.x: `public Task<IGraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)`  

**public method Nest.ElasticClient.IndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndexResponse> IndexAsync(IIndexRequest request)`  
5.x: `public Task<IIndexResponse> IndexAsync(IIndexRequest request)`  

**public method Nest.ElasticClient.IndexAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IIndexResponse> IndexAsync<T>(T object, Func<IndexDescriptor<T>, IIndexRequest> selector)`  
5.x: `public Task<IIndexResponse> IndexAsync<T>(T object, Func<IndexDescriptor<T>, IIndexRequest> selector)`  

**public method Nest.ElasticClient.IndexExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest request)`  
5.x: `public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest request)`  

**public method Nest.ElasticClient.IndexExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector)`  

**public method Nest.ElasticClient.IndexTemplateExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request)`  
5.x: `public Task<IExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request)`  

**public method Nest.ElasticClient.IndexTemplateExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> IndexTemplateExistsAsync(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> IndexTemplateExistsAsync(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector)`  

**public method Nest.ElasticClient.IndicesShardStoresAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndicesShardStoresResponse> IndicesShardStoresAsync(IIndicesShardStoresRequest request)`  
5.x: `public Task<IIndicesShardStoresResponse> IndicesShardStoresAsync(IIndicesShardStoresRequest request)`  

**public method Nest.ElasticClient.IndicesShardStoresAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndicesShardStoresResponse> IndicesShardStoresAsync(Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector)`  
5.x: `public Task<IIndicesShardStoresResponse> IndicesShardStoresAsync(Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector)`  

**public method Nest.ElasticClient.IndicesStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndicesStatsResponse> IndicesStatsAsync(IIndicesStatsRequest request)`  
5.x: `public Task<IIndicesStatsResponse> IndicesStatsAsync(IIndicesStatsRequest request)`  

**public method Nest.ElasticClient.IndicesStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndicesStatsResponse> IndicesStatsAsync(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector)`  
5.x: `public Task<IIndicesStatsResponse> IndicesStatsAsync(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector)`  

**public method Nest.ElasticClient.MapAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutMappingResponse> MapAsync(IPutMappingRequest request)`  
5.x: `public Task<IPutMappingResponse> MapAsync(IPutMappingRequest request)`  

**public method Nest.ElasticClient.MapAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IPutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)`  
5.x: `public Task<IPutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)`  

**public method Nest.ElasticClient.MultiGetAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest request)`  
5.x: `public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest request)`  

**public method Nest.ElasticClient.MultiGetAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, IMultiGetRequest> selector)`  
5.x: `public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, IMultiGetRequest> selector)`  

**public method Nest.ElasticClient.MultiPercolateAsync** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
public Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest request, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.MultiPercolateAsync** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
public Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.MultiSearchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request)`  
5.x: `public Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request)`  

**public method Nest.ElasticClient.MultiSearchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> selector)`  
5.x: `public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> selector)`  

**public method Nest.ElasticClient.MultiTermVectorsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request)`  
5.x: `public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request)`  

**public method Nest.ElasticClient.MultiTermVectorsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector)`  
5.x: `public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector)`  

**public method Nest.ElasticClient.NodesHotThreadsAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest request)`  
5.x: `public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest request)`  

**public method Nest.ElasticClient.NodesHotThreadsAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector)`  
5.x: `public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector)`  

**public method Nest.ElasticClient.NodesInfoAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request)`  
5.x: `public Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request)`  

**public method Nest.ElasticClient.NodesInfoAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector)`  
5.x: `public Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector)`  

**public method Nest.ElasticClient.NodesStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest request)`  
5.x: `public Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest request)`  

**public method Nest.ElasticClient.NodesStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector)`  
5.x: `public Task<INodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector)`  

**public method Nest.ElasticClient.OpenIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IOpenIndexResponse> OpenIndexAsync(IOpenIndexRequest request)`  
5.x: `public Task<IOpenIndexResponse> OpenIndexAsync(IOpenIndexRequest request)`  

**public method Nest.ElasticClient.OpenIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IOpenIndexResponse> OpenIndexAsync(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector)`  
5.x: `public Task<IOpenIndexResponse> OpenIndexAsync(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector)`  

**public method Nest.ElasticClient.PercolateAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
public Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> request, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.PercolateAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
public Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.PercolateCountAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> request, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.PercolateCountAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.PingAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPingResponse> PingAsync(IPingRequest request)`  
5.x: `public Task<IPingResponse> PingAsync(IPingRequest request)`  

**public method Nest.ElasticClient.PingAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector)`  
5.x: `public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector)`  

**public method Nest.ElasticClient.PostLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request)`  
5.x: `public Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request)`  

**public method Nest.ElasticClient.PostLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector)`  
5.x: `public Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector)`  

**public method Nest.ElasticClient.PutAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutAliasResponse> PutAliasAsync(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector)`  
5.x: `public Task<IPutAliasResponse> PutAliasAsync(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector)`  

**public method Nest.ElasticClient.PutAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest request)`  
5.x: `public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest request)`  

**public method Nest.ElasticClient.PutIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request)`  
5.x: `public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request)`  

**public method Nest.ElasticClient.PutIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector)`  
5.x: `public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector)`  

**public method Nest.ElasticClient.PutRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request)`  
5.x: `public Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request)`  

**public method Nest.ElasticClient.PutRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector)`  
5.x: `public Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector)`  

**public method Nest.ElasticClient.PutScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request)`  
5.x: `public Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request)`  

**public method Nest.ElasticClient.PutScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutScriptResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector)`  
5.x: `public Task<IPutScriptResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector)`  

**public method Nest.ElasticClient.PutSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request)`  
5.x: `public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request)`  

**public method Nest.ElasticClient.PutSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector)`  
5.x: `public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector)`  

**public method Nest.ElasticClient.PutUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request)`  
5.x: `public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request)`  

**public method Nest.ElasticClient.PutUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector)`  
5.x: `public Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector)`  

**public method Nest.ElasticClient.PutWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request)`  
5.x: `public Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request)`  

**public method Nest.ElasticClient.PutWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector)`  
5.x: `public Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector)`  

**public method Nest.ElasticClient.RecoveryStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request)`  
5.x: `public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request)`  

**public method Nest.ElasticClient.RecoveryStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector)`  
5.x: `public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector)`  

**public method Nest.ElasticClient.RefreshAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRefreshResponse> RefreshAsync(IRefreshRequest request)`  
5.x: `public Task<IRefreshResponse> RefreshAsync(IRefreshRequest request)`  

**public method Nest.ElasticClient.RefreshAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRefreshResponse> RefreshAsync(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector)`  
5.x: `public Task<IRefreshResponse> RefreshAsync(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector)`  

**public method Nest.ElasticClient.RegisterPercolatorAsync** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest request, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.RegisterPercolatorAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.Reindex&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IObservable<IReindexResponse<T>> Reindex<T>(IReindexRequest request)`  
5.x: `public IObservable<IReindexResponse<T>> Reindex<T>(IReindexRequest request)`  

**public method Nest.ElasticClient.Reindex&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IObservable<IReindexResponse<T>> Reindex<T>(IndexName from, IndexName to, Func<ReindexDescriptor<T>, IReindexRequest> selector)`  
5.x: `public IObservable<IReindexResponse<T>> Reindex<T>(IndexName from, IndexName to, Func<ReindexDescriptor<T>, IReindexRequest> selector)`  

**public method Nest.ElasticClient.ReindexOnServerAsync** *Declaration changed (Breaking)*

2.x: `public Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request)`  
5.x: `public Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request)`  

**public method Nest.ElasticClient.ReindexOnServerAsync** *Declaration changed (Breaking)*

2.x: `public Task<IReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector)`  
5.x: `public Task<IReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector)`  

**public method Nest.ElasticClient.RenderSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request)`  
5.x: `public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request)`  

**public method Nest.ElasticClient.RenderSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector)`  
5.x: `public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector)`  

**public method Nest.ElasticClient.RestartWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRestartWatcherResponse> RestartWatcherAsync(IRestartWatcherRequest request)`  
5.x: `public Task<IRestartWatcherResponse> RestartWatcherAsync(IRestartWatcherRequest request)`  

**public method Nest.ElasticClient.RestartWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRestartWatcherResponse> RestartWatcherAsync(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector)`  
5.x: `public Task<IRestartWatcherResponse> RestartWatcherAsync(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector)`  

**public method Nest.ElasticClient.RestoreAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRestoreResponse> RestoreAsync(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector)`  
5.x: `public Task<IRestoreResponse> RestoreAsync(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector)`  

**public method Nest.ElasticClient.RestoreAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRestoreResponse> RestoreAsync(IRestoreRequest request)`  
5.x: `public Task<IRestoreResponse> RestoreAsync(IRestoreRequest request)`  

**public method Nest.ElasticClient.RethrottleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request)`  
5.x: `public Task<IReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request)`  

**public method Nest.ElasticClient.RethrottleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IReindexRethrottleResponse> RethrottleAsync(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector)`  
5.x: `public Task<IReindexRethrottleResponse> RethrottleAsync(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector)`  

**public method Nest.ElasticClient.RootNodeInfoAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request)`  
5.x: `public Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request)`  

**public method Nest.ElasticClient.RootNodeInfoAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector)`  
5.x: `public Task<IRootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector)`  

**public method Nest.ElasticClient.ScrollAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> ScrollAsync<T>(Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector)`  
5.x: `public Task<ISearchResponse<T>> ScrollAsync<T>(Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector)`  

**public method Nest.ElasticClient.ScrollAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest request)`  
5.x: `public Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest request)`  

**public method Nest.ElasticClient.SearchAsync&lt;T, TResult&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request)`  
5.x: `public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request)`  

**public method Nest.ElasticClient.SearchAsync&lt;T, TResult&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector)`  
5.x: `public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector)`  

**public method Nest.ElasticClient.SearchAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request)`  
5.x: `public Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request)`  

**public method Nest.ElasticClient.SearchAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector)`  
5.x: `public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector)`  

**public method Nest.ElasticClient.SearchShardsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request)`  
5.x: `public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request)`  

**public method Nest.ElasticClient.SearchShardsAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector)`  
5.x: `public Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector)`  

**public method Nest.ElasticClient.SearchTemplateAsync&lt;T, TResult&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)`  
5.x: `public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)`  

**public method Nest.ElasticClient.SearchTemplateAsync&lt;T, TResult&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)`  
5.x: `public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)`  

**public method Nest.ElasticClient.SearchTemplateAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request)`  
5.x: `public Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request)`  

**public method Nest.ElasticClient.SearchTemplateAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)`  
5.x: `public Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)`  

**public method Nest.ElasticClient.SegmentsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest request)`  
5.x: `public Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest request)`  

**public method Nest.ElasticClient.SegmentsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISegmentsResponse> SegmentsAsync(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector)`  
5.x: `public Task<ISegmentsResponse> SegmentsAsync(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector)`  

**public method Nest.ElasticClient.SnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request)`  
5.x: `public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request)`  

**public method Nest.ElasticClient.SnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector)`  
5.x: `public Task<ISnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector)`  

**public method Nest.ElasticClient.SnapshotStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request)`  
5.x: `public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request)`  

**public method Nest.ElasticClient.SnapshotStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector)`  
5.x: `public Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector)`  

**public method Nest.ElasticClient.SourceAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[AsyncStateMachineAttribute(Nest.ElasticClient+<SourceAsync>d__192`1[T])]
public Task<T> SourceAsync<T>(ISourceRequest request)
```

5.x
```csharp
public Task<T> SourceAsync<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.SourceAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<T> SourceAsync<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector)
```

5.x
```csharp
[AsyncStateMachineAttribute(Nest.ElasticClient+<SourceAsync>d__212`1[T])]
public Task<T> SourceAsync<T>(ISourceRequest request, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.StartWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request)`  
5.x: `public Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request)`  

**public method Nest.ElasticClient.StartWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector)`  
5.x: `public Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector)`  

**public method Nest.ElasticClient.StopWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request)`  
5.x: `public Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request)`  

**public method Nest.ElasticClient.StopWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector)`  
5.x: `public Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector)`  

**public method Nest.ElasticClient.SuggestAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  
5.x: `public Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  

**public method Nest.ElasticClient.SyncedFlushAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request)`  
5.x: `public Task<ISyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request)`  

**public method Nest.ElasticClient.SyncedFlushAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector)`  
5.x: `public Task<ISyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector)`  

**public method Nest.ElasticClient.TermVectorsAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ITermVectorsResponse> TermVectorsAsync<T>(ITermVectorsRequest<T> request)`  
5.x: `public Task<ITermVectorsResponse> TermVectorsAsync<T>(ITermVectorsRequest<T> request)`  

**public method Nest.ElasticClient.TermVectorsAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ITermVectorsResponse> TermVectorsAsync<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector)`  
5.x: `public Task<ITermVectorsResponse> TermVectorsAsync<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector)`  

**public method Nest.ElasticClient.TypeExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request)`  
5.x: `public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request)`  

**public method Nest.ElasticClient.TypeExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> TypeExistsAsync(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> TypeExistsAsync(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector)`  

**public method Nest.ElasticClient.UnregisterPercolatorAsync** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest request, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.UnregisterPercolatorAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync<T>(Name name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync<T>(Name name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector, CancellationToken cancellationToken)
```

**public method Nest.ElasticClient.UpdateAsync&lt;TDocument, TPartialDocument&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IUpdateRequest<TDocument, TPartialDocument> request)`  
5.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IUpdateRequest<TDocument, TPartialDocument> request)`  

**public method Nest.ElasticClient.UpdateAsync&lt;TDocument, TPartialDocument&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TPartialDocument>, IUpdateRequest<TDocument, TPartialDocument>> selector)`  
5.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TPartialDocument>, IUpdateRequest<TDocument, TPartialDocument>> selector)`  

**public method Nest.ElasticClient.UpdateAsync&lt;TDocument&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument>(IUpdateRequest<TDocument, TDocument> request)`  
5.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument>(IUpdateRequest<TDocument, TDocument> request)`  

**public method Nest.ElasticClient.UpdateAsync&lt;TDocument&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest<TDocument, TDocument>> selector)`  
5.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest<TDocument, TDocument>> selector)`  

**public method Nest.ElasticClient.UpdateByQueryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request)`  
5.x: `public Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request)`  

**public method Nest.ElasticClient.UpdateByQueryAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)`  
5.x: `public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)`  

**public method Nest.ElasticClient.UpdateIndexSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request)`  
5.x: `public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request)`  

**public method Nest.ElasticClient.UpdateIndexSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector)`  
5.x: `public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector)`  

**public method Nest.ElasticClient.UpgradeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request)`  
5.x: `public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request)`  

**public method Nest.ElasticClient.UpgradeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector)`  
5.x: `public Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector)`  

**public method Nest.ElasticClient.UpgradeStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector)`  
5.x: `public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector)`  

**public method Nest.ElasticClient.UpgradeStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request)`  
5.x: `public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request)`  

**public method Nest.ElasticClient.ValidateQueryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IValidateQueryResponse> ValidateQueryAsync(IValidateQueryRequest request)`  
5.x: `public Task<IValidateQueryResponse> ValidateQueryAsync(IValidateQueryRequest request)`  

**public method Nest.ElasticClient.ValidateQueryAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IValidateQueryResponse> ValidateQueryAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)`  
5.x: `public Task<IValidateQueryResponse> ValidateQueryAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)`  

**public method Nest.ElasticClient.VerifyRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request)`  
5.x: `public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request)`  

**public method Nest.ElasticClient.VerifyRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector)`  
5.x: `public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector)`  

**public method Nest.ElasticClient.WatcherStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IWatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request)`  
5.x: `public Task<IWatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request)`  

**public method Nest.ElasticClient.WatcherStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IWatcherStatsResponse> WatcherStatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector)`  
5.x: `public Task<IWatcherStatsResponse> WatcherStatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector)`  

**public method Nest.GetManyExtensions.GetManyAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static Task<IEnumerable<IMultiGetHit<T>>> GetManyAsync<T>(IElasticClient client, IEnumerable<long> ids, string index, string type)
```

5.x
```csharp
[ExtensionAttribute]
public static Task<IEnumerable<IMultiGetHit<T>>> GetManyAsync<T>(IElasticClient client, IEnumerable<long> ids, IndexName index, TypeName type, CancellationToken cancellationToken)
```

**public method Nest.GetManyExtensions.GetManyAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[AsyncStateMachineAttribute(Nest.GetManyExtensions+<GetManyAsync>d__2`1[T])]
[ExtensionAttribute]
public static Task<IEnumerable<IMultiGetHit<T>>> GetManyAsync<T>(IElasticClient client, IEnumerable<string> ids, string index, string type)
```

5.x
```csharp
[AsyncStateMachineAttribute(Nest.GetManyExtensions+<GetManyAsync>d__2`1[T])]
[ExtensionAttribute]
public static Task<IEnumerable<IMultiGetHit<T>>> GetManyAsync<T>(IElasticClient client, IEnumerable<string> ids, IndexName index, TypeName type, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.AcknowledgeWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(IAcknowledgeWatchRequest request)`  
5.x: `public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(IAcknowledgeWatchRequest request)`  

**public method Nest.IElasticClient.AcknowledgeWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector)`  
5.x: `public Task<IAcknowledgeWatchResponse> AcknowledgeWatchAsync(Id id, Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector)`  

**public method Nest.IElasticClient.ActivateWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request)`  
5.x: `public Task<IActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request)`  

**public method Nest.IElasticClient.ActivateWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector)`  
5.x: `public Task<IActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector)`  

**public method Nest.IElasticClient.AliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IBulkAliasResponse> AliasAsync(IBulkAliasRequest request)`  
5.x: `public Task<IBulkAliasResponse> AliasAsync(IBulkAliasRequest request)`  

**public method Nest.IElasticClient.AliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IBulkAliasResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> selector)`  
5.x: `public Task<IBulkAliasResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> selector)`  

**public method Nest.IElasticClient.AliasExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest request)`  
5.x: `public Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest request)`  

**public method Nest.IElasticClient.AliasExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, IAliasExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, IAliasExistsRequest> selector)`  

**public method Nest.IElasticClient.AnalyzeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request)`  
5.x: `public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request)`  

**public method Nest.IElasticClient.AnalyzeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector)`  
5.x: `public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector)`  

**public method Nest.IElasticClient.AuthenticateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request)`  
5.x: `public Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request)`  

**public method Nest.IElasticClient.AuthenticateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector)`  
5.x: `public Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector)`  

**public method Nest.IElasticClient.BulkAsync** *Declaration changed (Breaking)*

2.x: `public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> selector)`  
5.x: `public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> selector)`  

**public method Nest.IElasticClient.BulkAsync** *Declaration changed (Breaking)*

2.x: `public Task<IBulkResponse> BulkAsync(IBulkRequest request)`  
5.x: `public Task<IBulkResponse> BulkAsync(IBulkRequest request)`  

**public method Nest.IElasticClient.CatAliasesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request)`  
5.x: `public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request)`  

**public method Nest.IElasticClient.CatAliasesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector)`  
5.x: `public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, ICatAliasesRequest> selector)`  

**public method Nest.IElasticClient.CatAllocationAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request)`  
5.x: `public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request)`  

**public method Nest.IElasticClient.CatAllocationAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, ICatAllocationRequest> selector)`  
5.x: `public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, ICatAllocationRequest> selector)`  

**public method Nest.IElasticClient.CatCountAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request)`  
5.x: `public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request)`  

**public method Nest.IElasticClient.CatCountAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector)`  
5.x: `public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector)`  

**public method Nest.IElasticClient.CatFielddataAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request)`  
5.x: `public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request)`  

**public method Nest.IElasticClient.CatFielddataAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, ICatFielddataRequest> selector)`  
5.x: `public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, ICatFielddataRequest> selector)`  

**public method Nest.IElasticClient.CatHealthAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request)`  
5.x: `public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request)`  

**public method Nest.IElasticClient.CatHealthAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, ICatHealthRequest> selector)`  
5.x: `public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, ICatHealthRequest> selector)`  

**public method Nest.IElasticClient.CatHelpAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request)`  
5.x: `public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(ICatHelpRequest request)`  

**public method Nest.IElasticClient.CatHelpAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector)`  
5.x: `public Task<ICatResponse<CatHelpRecord>> CatHelpAsync(Func<CatHelpDescriptor, ICatHelpRequest> selector)`  

**public method Nest.IElasticClient.CatIndicesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request)`  
5.x: `public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request)`  

**public method Nest.IElasticClient.CatIndicesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, ICatIndicesRequest> selector)`  
5.x: `public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, ICatIndicesRequest> selector)`  

**public method Nest.IElasticClient.CatMasterAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request)`  
5.x: `public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request)`  

**public method Nest.IElasticClient.CatMasterAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, ICatMasterRequest> selector)`  
5.x: `public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, ICatMasterRequest> selector)`  

**public method Nest.IElasticClient.CatNodeAttributesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request)`  
5.x: `public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(ICatNodeAttributesRequest request)`  

**public method Nest.IElasticClient.CatNodeAttributesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector)`  
5.x: `public Task<ICatResponse<CatNodeAttributesRecord>> CatNodeAttributesAsync(Func<CatNodeAttributesDescriptor, ICatNodeAttributesRequest> selector)`  

**public method Nest.IElasticClient.CatNodesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request)`  
5.x: `public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request)`  

**public method Nest.IElasticClient.CatNodesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector)`  
5.x: `public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, ICatNodesRequest> selector)`  

**public method Nest.IElasticClient.CatPendingTasksAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request)`  
5.x: `public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request)`  

**public method Nest.IElasticClient.CatPendingTasksAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector)`  
5.x: `public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector)`  

**public method Nest.IElasticClient.CatPluginsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request)`  
5.x: `public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request)`  

**public method Nest.IElasticClient.CatPluginsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, ICatPluginsRequest> selector)`  
5.x: `public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, ICatPluginsRequest> selector)`  

**public method Nest.IElasticClient.CatRecoveryAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request)`  
5.x: `public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request)`  

**public method Nest.IElasticClient.CatRecoveryAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector)`  
5.x: `public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector)`  

**public method Nest.IElasticClient.CatRepositoriesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request)`  
5.x: `public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(ICatRepositoriesRequest request)`  

**public method Nest.IElasticClient.CatRepositoriesAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector)`  
5.x: `public Task<ICatResponse<CatRepositoriesRecord>> CatRepositoriesAsync(Func<CatRepositoriesDescriptor, ICatRepositoriesRequest> selector)`  

**public method Nest.IElasticClient.CatSegmentsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request)`  
5.x: `public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request)`  

**public method Nest.IElasticClient.CatSegmentsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector)`  
5.x: `public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector)`  

**public method Nest.IElasticClient.CatShardsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request)`  
5.x: `public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request)`  

**public method Nest.IElasticClient.CatShardsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, ICatShardsRequest> selector)`  
5.x: `public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, ICatShardsRequest> selector)`  

**public method Nest.IElasticClient.CatSnapshotsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request)`  
5.x: `public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request)`  

**public method Nest.IElasticClient.CatSnapshotsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector)`  
5.x: `public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector)`  

**public method Nest.IElasticClient.CatThreadPoolAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request)`  
5.x: `public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request)`  

**public method Nest.IElasticClient.CatThreadPoolAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector)`  
5.x: `public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector)`  

**public method Nest.IElasticClient.ClearCacheAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCacheResponse> ClearCacheAsync(IClearCacheRequest request)`  
5.x: `public Task<IClearCacheResponse> ClearCacheAsync(IClearCacheRequest request)`  

**public method Nest.IElasticClient.ClearCacheAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCacheResponse> ClearCacheAsync(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector)`  
5.x: `public Task<IClearCacheResponse> ClearCacheAsync(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector)`  

**public method Nest.IElasticClient.ClearCachedRealmsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request)`  
5.x: `public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request)`  

**public method Nest.IElasticClient.ClearCachedRealmsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector)`  
5.x: `public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector)`  

**public method Nest.IElasticClient.ClearCachedRolesAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request)`  
5.x: `public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(IClearCachedRolesRequest request)`  

**public method Nest.IElasticClient.ClearCachedRolesAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector)`  
5.x: `public Task<IClearCachedRolesResponse> ClearCachedRolesAsync(Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector)`  

**public method Nest.IElasticClient.ClearScrollAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearScrollResponse> ClearScrollAsync(IClearScrollRequest request)`  
5.x: `public Task<IClearScrollResponse> ClearScrollAsync(IClearScrollRequest request)`  

**public method Nest.IElasticClient.ClearScrollAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClearScrollResponse> ClearScrollAsync(Func<ClearScrollDescriptor, IClearScrollRequest> selector)`  
5.x: `public Task<IClearScrollResponse> ClearScrollAsync(Func<ClearScrollDescriptor, IClearScrollRequest> selector)`  

**public method Nest.IElasticClient.CloseIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICloseIndexResponse> CloseIndexAsync(ICloseIndexRequest request)`  
5.x: `public Task<ICloseIndexResponse> CloseIndexAsync(ICloseIndexRequest request)`  

**public method Nest.IElasticClient.CloseIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICloseIndexResponse> CloseIndexAsync(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector)`  
5.x: `public Task<ICloseIndexResponse> CloseIndexAsync(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector)`  

**public method Nest.IElasticClient.ClusterGetSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request)`  
5.x: `public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request)`  

**public method Nest.IElasticClient.ClusterGetSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector)`  
5.x: `public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector)`  

**public method Nest.IElasticClient.ClusterHealthAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request)`  
5.x: `public Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest request)`  

**public method Nest.IElasticClient.ClusterHealthAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector)`  
5.x: `public Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector)`  

**public method Nest.IElasticClient.ClusterPendingTasksAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request)`  
5.x: `public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request)`  

**public method Nest.IElasticClient.ClusterPendingTasksAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector)`  
5.x: `public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector)`  

**public method Nest.IElasticClient.ClusterPutSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request)`  
5.x: `public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request)`  

**public method Nest.IElasticClient.ClusterPutSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector)`  
5.x: `public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector)`  

**public method Nest.IElasticClient.ClusterRerouteAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request)`  
5.x: `public Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest request)`  

**public method Nest.IElasticClient.ClusterRerouteAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector)`  
5.x: `public Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector)`  

**public method Nest.IElasticClient.ClusterStateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest request)`  
5.x: `public Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest request)`  

**public method Nest.IElasticClient.ClusterStateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, IClusterStateRequest> selector)`  
5.x: `public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, IClusterStateRequest> selector)`  

**public method Nest.IElasticClient.ClusterStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest request)`  
5.x: `public Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest request)`  

**public method Nest.IElasticClient.ClusterStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector)`  
5.x: `public Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector)`  

**public method Nest.IElasticClient.CountAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ICountResponse> CountAsync<T>(ICountRequest request)`  
5.x: `public Task<ICountResponse> CountAsync<T>(ICountRequest request)`  

**public method Nest.IElasticClient.CountAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, ICountRequest> selector)`  
5.x: `public Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, ICountRequest> selector)`  

**public method Nest.IElasticClient.CreateIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICreateIndexResponse> CreateIndexAsync(ICreateIndexRequest request)`  
5.x: `public Task<ICreateIndexResponse> CreateIndexAsync(ICreateIndexRequest request)`  

**public method Nest.IElasticClient.CreateIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICreateIndexResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector)`  
5.x: `public Task<ICreateIndexResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector)`  

**public method Nest.IElasticClient.CreateRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICreateRepositoryResponse> CreateRepositoryAsync(ICreateRepositoryRequest request)`  
5.x: `public Task<ICreateRepositoryResponse> CreateRepositoryAsync(ICreateRepositoryRequest request)`  

**public method Nest.IElasticClient.CreateRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<ICreateRepositoryResponse> CreateRepositoryAsync(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector)`  
5.x: `public Task<ICreateRepositoryResponse> CreateRepositoryAsync(Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector)`  

**public method Nest.IElasticClient.DeactivateWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request)`  
5.x: `public Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request)`  

**public method Nest.IElasticClient.DeactivateWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector)`  
5.x: `public Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector)`  

**public method Nest.IElasticClient.DeleteAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteAliasResponse> DeleteAliasAsync(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector)`  
5.x: `public Task<IDeleteAliasResponse> DeleteAliasAsync(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector)`  

**public method Nest.IElasticClient.DeleteAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request)`  
5.x: `public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request)`  

**public method Nest.IElasticClient.DeleteAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteResponse> DeleteAsync(IDeleteRequest request)`  
5.x: `public Task<IDeleteResponse> DeleteAsync(IDeleteRequest request)`  

**public method Nest.IElasticClient.DeleteAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteResponse> DeleteAsync<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> selector)`  
5.x: `public Task<IDeleteResponse> DeleteAsync<T>(DocumentPath<T> document, Func<DeleteDescriptor<T>, IDeleteRequest> selector)`  

**public method Nest.IElasticClient.DeleteByQueryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request)`  
5.x: `public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest request)`  

**public method Nest.IElasticClient.DeleteByQueryAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  
5.x: `public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Indices indices, Types types, Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)`  

**public method Nest.IElasticClient.DeleteIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteIndexResponse> DeleteIndexAsync(IDeleteIndexRequest request)`  
5.x: `public Task<IDeleteIndexResponse> DeleteIndexAsync(IDeleteIndexRequest request)`  

**public method Nest.IElasticClient.DeleteIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteIndexResponse> DeleteIndexAsync(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector)`  
5.x: `public Task<IDeleteIndexResponse> DeleteIndexAsync(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector)`  

**public method Nest.IElasticClient.DeleteIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request)`  
5.x: `public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(IDeleteIndexTemplateRequest request)`  

**public method Nest.IElasticClient.DeleteIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector)`  
5.x: `public Task<IDeleteIndexTemplateResponse> DeleteIndexTemplateAsync(Name name, Func<DeleteIndexTemplateDescriptor, IDeleteIndexTemplateRequest> selector)`  

**public method Nest.IElasticClient.DeleteLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request)`  
5.x: `public Task<IDeleteLicenseResponse> DeleteLicenseAsync(IDeleteLicenseRequest request)`  

**public method Nest.IElasticClient.DeleteLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector)`  
5.x: `public Task<IDeleteLicenseResponse> DeleteLicenseAsync(Func<DeleteLicenseDescriptor, IDeleteLicenseRequest> selector)`  

**public method Nest.IElasticClient.DeleteRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request)`  
5.x: `public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest request)`  

**public method Nest.IElasticClient.DeleteRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector)`  
5.x: `public Task<IDeleteRepositoryResponse> DeleteRepositoryAsync(Names repositories, Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector)`  

**public method Nest.IElasticClient.DeleteRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request)`  
5.x: `public Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request)`  

**public method Nest.IElasticClient.DeleteRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector)`  
5.x: `public Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector)`  

**public method Nest.IElasticClient.DeleteScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request)`  
5.x: `public Task<IDeleteScriptResponse> DeleteScriptAsync(IDeleteScriptRequest request)`  

**public method Nest.IElasticClient.DeleteScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteScriptResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector)`  
5.x: `public Task<IDeleteScriptResponse> DeleteScriptAsync(Name language, Id id, Func<DeleteScriptDescriptor, IDeleteScriptRequest> selector)`  

**public method Nest.IElasticClient.DeleteSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request)`  
5.x: `public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request)`  

**public method Nest.IElasticClient.DeleteSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector)`  
5.x: `public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector)`  

**public method Nest.IElasticClient.DeleteSnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request)`  
5.x: `public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest request)`  

**public method Nest.IElasticClient.DeleteSnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector)`  
5.x: `public Task<IDeleteSnapshotResponse> DeleteSnapshotAsync(Name repository, Name snapshotName, Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> selector)`  

**public method Nest.IElasticClient.DeleteUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request)`  
5.x: `public Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request)`  

**public method Nest.IElasticClient.DeleteUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector)`  
5.x: `public Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector)`  

**public method Nest.IElasticClient.DeleteWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request)`  
5.x: `public Task<IDeleteWatchResponse> DeleteWatchAsync(IDeleteWatchRequest request)`  

**public method Nest.IElasticClient.DeleteWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IDeleteWatchResponse> DeleteWatchAsync(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector)`  
5.x: `public Task<IDeleteWatchResponse> DeleteWatchAsync(Id watchId, Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector)`  

**public method Nest.IElasticClient.DocumentExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request)`  
5.x: `public Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest request)`  

**public method Nest.IElasticClient.DocumentExistsAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> DocumentExistsAsync<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> DocumentExistsAsync<T>(DocumentPath<T> document, Func<DocumentExistsDescriptor<T>, IDocumentExistsRequest> selector)`  

**public method Nest.IElasticClient.ExecuteWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request)`  
5.x: `public Task<IExecuteWatchResponse> ExecuteWatchAsync(IExecuteWatchRequest request)`  

**public method Nest.IElasticClient.ExecuteWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector)`  
5.x: `public Task<IExecuteWatchResponse> ExecuteWatchAsync(Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector)`  

**public method Nest.IElasticClient.ExplainAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest<T> request)`  
5.x: `public Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest<T> request)`  

**public method Nest.IElasticClient.ExplainAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IExplainResponse<T>> ExplainAsync<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> selector)`  
5.x: `public Task<IExplainResponse<T>> ExplainAsync<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> selector)`  

**public method Nest.IElasticClient.FieldStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request)`  
5.x: `public Task<IFieldStatsResponse> FieldStatsAsync(IFieldStatsRequest request)`  

**public method Nest.IElasticClient.FieldStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector)`  
5.x: `public Task<IFieldStatsResponse> FieldStatsAsync(Indices indices, Func<FieldStatsDescriptor, IFieldStatsRequest> selector)`  

**public method Nest.IElasticClient.FlushAsync** *Declaration changed (Breaking)*

2.x: `public Task<IFlushResponse> FlushAsync(IFlushRequest request)`  
5.x: `public Task<IFlushResponse> FlushAsync(IFlushRequest request)`  

**public method Nest.IElasticClient.FlushAsync** *Declaration changed (Breaking)*

2.x: `public Task<IFlushResponse> FlushAsync(Indices indices, Func<FlushDescriptor, IFlushRequest> selector)`  
5.x: `public Task<IFlushResponse> FlushAsync(Indices indices, Func<FlushDescriptor, IFlushRequest> selector)`  

**public method Nest.IElasticClient.ForceMergeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request)`  
5.x: `public Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request)`  

**public method Nest.IElasticClient.ForceMergeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector)`  
5.x: `public Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector)`  

**public method Nest.IElasticClient.GetAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest request)`  
5.x: `public Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest request)`  

**public method Nest.IElasticClient.GetAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  
5.x: `public Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, IGetAliasRequest> selector)`  

**public method Nest.IElasticClient.GetAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGetResponse<T>> GetAsync<T>(IGetRequest request)`  
5.x: `public Task<IGetResponse<T>> GetAsync<T>(IGetRequest request)`  

**public method Nest.IElasticClient.GetAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGetResponse<T>> GetAsync<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> selector)`  
5.x: `public Task<IGetResponse<T>> GetAsync<T>(DocumentPath<T> document, Func<GetDescriptor<T>, IGetRequest> selector)`  

**public method Nest.IElasticClient.GetFieldMappingAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request)`  
5.x: `public Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest request)`  

**public method Nest.IElasticClient.GetFieldMappingAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector)`  
5.x: `public Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Fields fields, Func<GetFieldMappingDescriptor<T>, IGetFieldMappingRequest> selector)`  

**public method Nest.IElasticClient.GetIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request)`  
5.x: `public Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest request)`  

**public method Nest.IElasticClient.GetIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector)`  
5.x: `public Task<IGetIndexResponse> GetIndexAsync(Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector)`  

**public method Nest.IElasticClient.GetIndexSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request)`  
5.x: `public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest request)`  

**public method Nest.IElasticClient.GetIndexSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector)`  
5.x: `public Task<IGetIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector)`  

**public method Nest.IElasticClient.GetIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request)`  
5.x: `public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(IGetIndexTemplateRequest request)`  

**public method Nest.IElasticClient.GetIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector)`  
5.x: `public Task<IGetIndexTemplateResponse> GetIndexTemplateAsync(Func<GetIndexTemplateDescriptor, IGetIndexTemplateRequest> selector)`  

**public method Nest.IElasticClient.GetLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request)`  
5.x: `public Task<IGetLicenseResponse> GetLicenseAsync(IGetLicenseRequest request)`  

**public method Nest.IElasticClient.GetLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector)`  
5.x: `public Task<IGetLicenseResponse> GetLicenseAsync(Func<GetLicenseDescriptor, IGetLicenseRequest> selector)`  

**public method Nest.IElasticClient.GetMappingAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request)`  
5.x: `public Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest request)`  

**public method Nest.IElasticClient.GetMappingAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector)`  
5.x: `public Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector)`  

**public method Nest.IElasticClient.GetRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request)`  
5.x: `public Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request)`  

**public method Nest.IElasticClient.GetRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector)`  
5.x: `public Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, IGetRepositoryRequest> selector)`  

**public method Nest.IElasticClient.GetRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request)`  
5.x: `public Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request)`  

**public method Nest.IElasticClient.GetRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector)`  
5.x: `public Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector)`  

**public method Nest.IElasticClient.GetScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest request)`  
5.x: `public Task<IGetScriptResponse> GetScriptAsync(IGetScriptRequest request)`  

**public method Nest.IElasticClient.GetScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetScriptResponse> GetScriptAsync(Name language, Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector)`  
5.x: `public Task<IGetScriptResponse> GetScriptAsync(Name language, Id id, Func<GetScriptDescriptor, IGetScriptRequest> selector)`  

**public method Nest.IElasticClient.GetSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request)`  
5.x: `public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request)`  

**public method Nest.IElasticClient.GetSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector)`  
5.x: `public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector)`  

**public method Nest.IElasticClient.GetSnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest request)`  
5.x: `public Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest request)`  

**public method Nest.IElasticClient.GetSnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetSnapshotResponse> GetSnapshotAsync(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector)`  
5.x: `public Task<IGetSnapshotResponse> GetSnapshotAsync(Name repository, Names snapshots, Func<GetSnapshotDescriptor, IGetSnapshotRequest> selector)`  

**public method Nest.IElasticClient.GetUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetUserResponse> GetUserAsync(IGetUserRequest request)`  
5.x: `public Task<IGetUserResponse> GetUserAsync(IGetUserRequest request)`  

**public method Nest.IElasticClient.GetUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector)`  
5.x: `public Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector)`  

**public method Nest.IElasticClient.GetWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request)`  
5.x: `public Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request)`  

**public method Nest.IElasticClient.GetWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector)`  
5.x: `public Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector)`  

**public method Nest.IElasticClient.GraphExploreAsync** *Declaration changed (Breaking)*

2.x: `public Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request)`  
5.x: `public Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request)`  

**public method Nest.IElasticClient.GraphExploreAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IGraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)`  
5.x: `public Task<IGraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)`  

**public method Nest.IElasticClient.IndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndexResponse> IndexAsync(IIndexRequest request)`  
5.x: `public Task<IIndexResponse> IndexAsync(IIndexRequest request)`  

**public method Nest.IElasticClient.IndexAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IIndexResponse> IndexAsync<T>(T object, Func<IndexDescriptor<T>, IIndexRequest> selector)`  
5.x: `public Task<IIndexResponse> IndexAsync<T>(T object, Func<IndexDescriptor<T>, IIndexRequest> selector)`  

**public method Nest.IElasticClient.IndexExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest request)`  
5.x: `public Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest request)`  

**public method Nest.IElasticClient.IndexExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> IndexExistsAsync(Indices indices, Func<IndexExistsDescriptor, IIndexExistsRequest> selector)`  

**public method Nest.IElasticClient.IndexTemplateExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request)`  
5.x: `public Task<IExistsResponse> IndexTemplateExistsAsync(IIndexTemplateExistsRequest request)`  

**public method Nest.IElasticClient.IndexTemplateExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> IndexTemplateExistsAsync(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> IndexTemplateExistsAsync(Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector)`  

**public method Nest.IElasticClient.IndicesShardStoresAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndicesShardStoresResponse> IndicesShardStoresAsync(IIndicesShardStoresRequest request)`  
5.x: `public Task<IIndicesShardStoresResponse> IndicesShardStoresAsync(IIndicesShardStoresRequest request)`  

**public method Nest.IElasticClient.IndicesShardStoresAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndicesShardStoresResponse> IndicesShardStoresAsync(Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector)`  
5.x: `public Task<IIndicesShardStoresResponse> IndicesShardStoresAsync(Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector)`  

**public method Nest.IElasticClient.IndicesStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndicesStatsResponse> IndicesStatsAsync(IIndicesStatsRequest request)`  
5.x: `public Task<IIndicesStatsResponse> IndicesStatsAsync(IIndicesStatsRequest request)`  

**public method Nest.IElasticClient.IndicesStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IIndicesStatsResponse> IndicesStatsAsync(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector)`  
5.x: `public Task<IIndicesStatsResponse> IndicesStatsAsync(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector)`  

**public method Nest.IElasticClient.MapAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutMappingResponse> MapAsync(IPutMappingRequest request)`  
5.x: `public Task<IPutMappingResponse> MapAsync(IPutMappingRequest request)`  

**public method Nest.IElasticClient.MapAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IPutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)`  
5.x: `public Task<IPutMappingResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, IPutMappingRequest> selector)`  

**public method Nest.IElasticClient.MultiGetAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest request)`  
5.x: `public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest request)`  

**public method Nest.IElasticClient.MultiGetAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, IMultiGetRequest> selector)`  
5.x: `public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, IMultiGetRequest> selector)`  

**public method Nest.IElasticClient.MultiPercolateAsync** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
public Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest request, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.MultiPercolateAsync** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
public Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.MultiSearchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request)`  
5.x: `public Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request)`  

**public method Nest.IElasticClient.MultiSearchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> selector)`  
5.x: `public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> selector)`  

**public method Nest.IElasticClient.MultiTermVectorsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request)`  
5.x: `public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest request)`  

**public method Nest.IElasticClient.MultiTermVectorsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector)`  
5.x: `public Task<IMultiTermVectorsResponse> MultiTermVectorsAsync(Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector)`  

**public method Nest.IElasticClient.NodesHotThreadsAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest request)`  
5.x: `public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest request)`  

**public method Nest.IElasticClient.NodesHotThreadsAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector)`  
5.x: `public Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector)`  

**public method Nest.IElasticClient.NodesInfoAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request)`  
5.x: `public Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request)`  

**public method Nest.IElasticClient.NodesInfoAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector)`  
5.x: `public Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector)`  

**public method Nest.IElasticClient.NodesStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest request)`  
5.x: `public Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest request)`  

**public method Nest.IElasticClient.NodesStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<INodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector)`  
5.x: `public Task<INodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector)`  

**public method Nest.IElasticClient.OpenIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IOpenIndexResponse> OpenIndexAsync(IOpenIndexRequest request)`  
5.x: `public Task<IOpenIndexResponse> OpenIndexAsync(IOpenIndexRequest request)`  

**public method Nest.IElasticClient.OpenIndexAsync** *Declaration changed (Breaking)*

2.x: `public Task<IOpenIndexResponse> OpenIndexAsync(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector)`  
5.x: `public Task<IOpenIndexResponse> OpenIndexAsync(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector)`  

**public method Nest.IElasticClient.PercolateAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
public Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> request, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.PercolateAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
public Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.PercolateCountAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> request, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.PercolateCountAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.PingAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPingResponse> PingAsync(IPingRequest request)`  
5.x: `public Task<IPingResponse> PingAsync(IPingRequest request)`  

**public method Nest.IElasticClient.PingAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector)`  
5.x: `public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector)`  

**public method Nest.IElasticClient.PostLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request)`  
5.x: `public Task<IPostLicenseResponse> PostLicenseAsync(IPostLicenseRequest request)`  

**public method Nest.IElasticClient.PostLicenseAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector)`  
5.x: `public Task<IPostLicenseResponse> PostLicenseAsync(Func<PostLicenseDescriptor, IPostLicenseRequest> selector)`  

**public method Nest.IElasticClient.PutAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest request)`  
5.x: `public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest request)`  

**public method Nest.IElasticClient.PutAliasAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutAliasResponse> PutAliasAsync(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector)`  
5.x: `public Task<IPutAliasResponse> PutAliasAsync(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector)`  

**public method Nest.IElasticClient.PutIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request)`  
5.x: `public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(IPutIndexTemplateRequest request)`  

**public method Nest.IElasticClient.PutIndexTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector)`  
5.x: `public Task<IPutIndexTemplateResponse> PutIndexTemplateAsync(Name name, Func<PutIndexTemplateDescriptor, IPutIndexTemplateRequest> selector)`  

**public method Nest.IElasticClient.PutRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request)`  
5.x: `public Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request)`  

**public method Nest.IElasticClient.PutRoleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector)`  
5.x: `public Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector)`  

**public method Nest.IElasticClient.PutScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request)`  
5.x: `public Task<IPutScriptResponse> PutScriptAsync(IPutScriptRequest request)`  

**public method Nest.IElasticClient.PutScriptAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutScriptResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector)`  
5.x: `public Task<IPutScriptResponse> PutScriptAsync(Name language, Id id, Func<PutScriptDescriptor, IPutScriptRequest> selector)`  

**public method Nest.IElasticClient.PutSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request)`  
5.x: `public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request)`  

**public method Nest.IElasticClient.PutSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector)`  
5.x: `public Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(Id id, Func<PutSearchTemplateDescriptor, IPutSearchTemplateRequest> selector)`  

**public method Nest.IElasticClient.PutUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request)`  
5.x: `public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request)`  

**public method Nest.IElasticClient.PutUserAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector)`  
5.x: `public Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector)`  

**public method Nest.IElasticClient.PutWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request)`  
5.x: `public Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request)`  

**public method Nest.IElasticClient.PutWatchAsync** *Declaration changed (Breaking)*

2.x: `public Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector)`  
5.x: `public Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector)`  

**public method Nest.IElasticClient.RecoveryStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request)`  
5.x: `public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request)`  

**public method Nest.IElasticClient.RecoveryStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector)`  
5.x: `public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector)`  

**public method Nest.IElasticClient.RefreshAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRefreshResponse> RefreshAsync(IRefreshRequest request)`  
5.x: `public Task<IRefreshResponse> RefreshAsync(IRefreshRequest request)`  

**public method Nest.IElasticClient.RefreshAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRefreshResponse> RefreshAsync(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector)`  
5.x: `public Task<IRefreshResponse> RefreshAsync(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector)`  

**public method Nest.IElasticClient.RegisterPercolatorAsync** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest request, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.RegisterPercolatorAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
public Task<IRegisterPercolatorResponse> RegisterPercolatorAsync<T>(Name name, Func<RegisterPercolatorDescriptor<T>, IRegisterPercolatorRequest> selector, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.Reindex&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IObservable<IReindexResponse<T>> Reindex<T>(IReindexRequest request)`  
5.x: `public IObservable<IReindexResponse<T>> Reindex<T>(IReindexRequest request)`  

**public method Nest.IElasticClient.Reindex&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public IObservable<IReindexResponse<T>> Reindex<T>(IndexName from, IndexName to, Func<ReindexDescriptor<T>, IReindexRequest> selector)`  
5.x: `public IObservable<IReindexResponse<T>> Reindex<T>(IndexName from, IndexName to, Func<ReindexDescriptor<T>, IReindexRequest> selector)`  

**public method Nest.IElasticClient.ReindexOnServerAsync** *Declaration changed (Breaking)*

2.x: `public Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request)`  
5.x: `public Task<IReindexOnServerResponse> ReindexOnServerAsync(IReindexOnServerRequest request)`  

**public method Nest.IElasticClient.ReindexOnServerAsync** *Declaration changed (Breaking)*

2.x: `public Task<IReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector)`  
5.x: `public Task<IReindexOnServerResponse> ReindexOnServerAsync(Func<ReindexOnServerDescriptor, IReindexOnServerRequest> selector)`  

**public method Nest.IElasticClient.RenderSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request)`  
5.x: `public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(IRenderSearchTemplateRequest request)`  

**public method Nest.IElasticClient.RenderSearchTemplateAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector)`  
5.x: `public Task<IRenderSearchTemplateResponse> RenderSearchTemplateAsync(Func<RenderSearchTemplateDescriptor, IRenderSearchTemplateRequest> selector)`  

**public method Nest.IElasticClient.RestartWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRestartWatcherResponse> RestartWatcherAsync(IRestartWatcherRequest request)`  
5.x: `public Task<IRestartWatcherResponse> RestartWatcherAsync(IRestartWatcherRequest request)`  

**public method Nest.IElasticClient.RestartWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRestartWatcherResponse> RestartWatcherAsync(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector)`  
5.x: `public Task<IRestartWatcherResponse> RestartWatcherAsync(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector)`  

**public method Nest.IElasticClient.RestoreAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRestoreResponse> RestoreAsync(IRestoreRequest request)`  
5.x: `public Task<IRestoreResponse> RestoreAsync(IRestoreRequest request)`  

**public method Nest.IElasticClient.RestoreAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRestoreResponse> RestoreAsync(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector)`  
5.x: `public Task<IRestoreResponse> RestoreAsync(Name repository, Name snapshotName, Func<RestoreDescriptor, IRestoreRequest> selector)`  

**public method Nest.IElasticClient.RethrottleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request)`  
5.x: `public Task<IReindexRethrottleResponse> RethrottleAsync(IReindexRethrottleRequest request)`  

**public method Nest.IElasticClient.RethrottleAsync** *Declaration changed (Breaking)*

2.x: `public Task<IReindexRethrottleResponse> RethrottleAsync(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector)`  
5.x: `public Task<IReindexRethrottleResponse> RethrottleAsync(Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> selector)`  

**public method Nest.IElasticClient.RootNodeInfoAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request)`  
5.x: `public Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest request)`  

**public method Nest.IElasticClient.RootNodeInfoAsync** *Declaration changed (Breaking)*

2.x: `public Task<IRootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector)`  
5.x: `public Task<IRootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector)`  

**public method Nest.IElasticClient.ScrollAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> ScrollAsync<T>(Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector)`  
5.x: `public Task<ISearchResponse<T>> ScrollAsync<T>(Time scrollTime, string scrollId, Func<ScrollDescriptor<T>, IScrollRequest> selector)`  

**public method Nest.IElasticClient.ScrollAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest request)`  
5.x: `public Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest request)`  

**public method Nest.IElasticClient.SearchAsync&lt;T, TResult&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request)`  
5.x: `public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request)`  

**public method Nest.IElasticClient.SearchAsync&lt;T, TResult&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector)`  
5.x: `public Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, ISearchRequest> selector)`  

**public method Nest.IElasticClient.SearchAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request)`  
5.x: `public Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request)`  

**public method Nest.IElasticClient.SearchAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector)`  
5.x: `public Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector)`  

**public method Nest.IElasticClient.SearchShardsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request)`  
5.x: `public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request)`  

**public method Nest.IElasticClient.SearchShardsAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector)`  
5.x: `public Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector)`  

**public method Nest.IElasticClient.SearchTemplateAsync&lt;T, TResult&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)`  
5.x: `public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)`  

**public method Nest.IElasticClient.SearchTemplateAsync&lt;T, TResult&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)`  
5.x: `public Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)`  

**public method Nest.IElasticClient.SearchTemplateAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request)`  
5.x: `public Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request)`  

**public method Nest.IElasticClient.SearchTemplateAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)`  
5.x: `public Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, ISearchTemplateRequest> selector)`  

**public method Nest.IElasticClient.SegmentsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest request)`  
5.x: `public Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest request)`  

**public method Nest.IElasticClient.SegmentsAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISegmentsResponse> SegmentsAsync(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector)`  
5.x: `public Task<ISegmentsResponse> SegmentsAsync(Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector)`  

**public method Nest.IElasticClient.SnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request)`  
5.x: `public Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest request)`  

**public method Nest.IElasticClient.SnapshotAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector)`  
5.x: `public Task<ISnapshotResponse> SnapshotAsync(Name repository, Name snapshotName, Func<SnapshotDescriptor, ISnapshotRequest> selector)`  

**public method Nest.IElasticClient.SnapshotStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request)`  
5.x: `public Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest request)`  

**public method Nest.IElasticClient.SnapshotStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector)`  
5.x: `public Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector)`  

**public method Nest.IElasticClient.SourceAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<T> SourceAsync<T>(ISourceRequest request)`  
5.x: `public Task<T> SourceAsync<T>(ISourceRequest request)`  

**public method Nest.IElasticClient.SourceAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<T> SourceAsync<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector)`  
5.x: `public Task<T> SourceAsync<T>(DocumentPath<T> document, Func<SourceDescriptor<T>, ISourceRequest> selector)`  

**public method Nest.IElasticClient.StartWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request)`  
5.x: `public Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request)`  

**public method Nest.IElasticClient.StartWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector)`  
5.x: `public Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector)`  

**public method Nest.IElasticClient.StopWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request)`  
5.x: `public Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request)`  

**public method Nest.IElasticClient.StopWatcherAsync** *Declaration changed (Breaking)*

2.x: `public Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector)`  
5.x: `public Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector)`  

**public method Nest.IElasticClient.SuggestAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  
5.x: `public Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, ISuggestRequest> selector)`  

**public method Nest.IElasticClient.SyncedFlushAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request)`  
5.x: `public Task<ISyncedFlushResponse> SyncedFlushAsync(ISyncedFlushRequest request)`  

**public method Nest.IElasticClient.SyncedFlushAsync** *Declaration changed (Breaking)*

2.x: `public Task<ISyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector)`  
5.x: `public Task<ISyncedFlushResponse> SyncedFlushAsync(Indices indices, Func<SyncedFlushDescriptor, ISyncedFlushRequest> selector)`  

**public method Nest.IElasticClient.TermVectorsAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ITermVectorsResponse> TermVectorsAsync<T>(ITermVectorsRequest<T> request)`  
5.x: `public Task<ITermVectorsResponse> TermVectorsAsync<T>(ITermVectorsRequest<T> request)`  

**public method Nest.IElasticClient.TermVectorsAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<ITermVectorsResponse> TermVectorsAsync<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector)`  
5.x: `public Task<ITermVectorsResponse> TermVectorsAsync<T>(Func<TermVectorsDescriptor<T>, ITermVectorsRequest<T>> selector)`  

**public method Nest.IElasticClient.TypeExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request)`  
5.x: `public Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest request)`  

**public method Nest.IElasticClient.TypeExistsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IExistsResponse> TypeExistsAsync(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector)`  
5.x: `public Task<IExistsResponse> TypeExistsAsync(Indices indices, Types types, Func<TypeExistsDescriptor, ITypeExistsRequest> selector)`  

**public method Nest.IElasticClient.UnregisterPercolatorAsync** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest request)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest request, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.UnregisterPercolatorAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync<T>(Name name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector)
```

5.x
```csharp
[ObsoleteAttribute("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync<T>(Name name, Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector, CancellationToken cancellationToken)
```

**public method Nest.IElasticClient.UpdateAsync&lt;TDocument, TPartialDocument&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IUpdateRequest<TDocument, TPartialDocument> request)`  
5.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IUpdateRequest<TDocument, TPartialDocument> request)`  

**public method Nest.IElasticClient.UpdateAsync&lt;TDocument, TPartialDocument&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TPartialDocument>, IUpdateRequest<TDocument, TPartialDocument>> selector)`  
5.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TPartialDocument>, IUpdateRequest<TDocument, TPartialDocument>> selector)`  

**public method Nest.IElasticClient.UpdateAsync&lt;TDocument&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument>(IUpdateRequest<TDocument, TDocument> request)`  
5.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument>(IUpdateRequest<TDocument, TDocument> request)`  

**public method Nest.IElasticClient.UpdateAsync&lt;TDocument&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest<TDocument, TDocument>> selector)`  
5.x: `public Task<IUpdateResponse<TDocument>> UpdateAsync<TDocument>(DocumentPath<TDocument> documentPath, Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest<TDocument, TDocument>> selector)`  

**public method Nest.IElasticClient.UpdateByQueryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request)`  
5.x: `public Task<IUpdateByQueryResponse> UpdateByQueryAsync(IUpdateByQueryRequest request)`  

**public method Nest.IElasticClient.UpdateByQueryAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)`  
5.x: `public Task<IUpdateByQueryResponse> UpdateByQueryAsync<T>(Indices indices, Types types, Func<UpdateByQueryDescriptor<T>, IUpdateByQueryRequest> selector)`  

**public method Nest.IElasticClient.UpdateIndexSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request)`  
5.x: `public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(IUpdateIndexSettingsRequest request)`  

**public method Nest.IElasticClient.UpdateIndexSettingsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector)`  
5.x: `public Task<IUpdateIndexSettingsResponse> UpdateIndexSettingsAsync(Indices indices, Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> selector)`  

**public method Nest.IElasticClient.UpgradeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request)`  
5.x: `public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request)`  

**public method Nest.IElasticClient.UpgradeAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector)`  
5.x: `public Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector)`  

**public method Nest.IElasticClient.UpgradeStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector)`  
5.x: `public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector)`  

**public method Nest.IElasticClient.UpgradeStatusAsync** *Declaration changed (Breaking)*

2.x: `public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request)`  
5.x: `public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request)`  

**public method Nest.IElasticClient.ValidateQueryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IValidateQueryResponse> ValidateQueryAsync(IValidateQueryRequest request)`  
5.x: `public Task<IValidateQueryResponse> ValidateQueryAsync(IValidateQueryRequest request)`  

**public method Nest.IElasticClient.ValidateQueryAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public Task<IValidateQueryResponse> ValidateQueryAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)`  
5.x: `public Task<IValidateQueryResponse> ValidateQueryAsync<T>(Func<ValidateQueryDescriptor<T>, IValidateQueryRequest> selector)`  

**public method Nest.IElasticClient.VerifyRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request)`  
5.x: `public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest request)`  

**public method Nest.IElasticClient.VerifyRepositoryAsync** *Declaration changed (Breaking)*

2.x: `public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector)`  
5.x: `public Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(Name repository, Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector)`  

**public method Nest.IElasticClient.WatcherStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IWatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request)`  
5.x: `public Task<IWatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request)`  

**public method Nest.IElasticClient.WatcherStatsAsync** *Declaration changed (Breaking)*

2.x: `public Task<IWatcherStatsResponse> WatcherStatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector)`  
5.x: `public Task<IWatcherStatsResponse> WatcherStatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector)`  

**public method Nest.IHighLevelToLowLevelDispatcher.DispatchAsync&lt;TRequest, TQueryString, TResponse, TResponseInterface&gt;** *Declaration changed (Breaking)*

2.x: `public Task<TResponseInterface> DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(TRequest descriptor, Func<TRequest, PostData<object>, Task<ElasticsearchResponse<TResponse>>> dispatch) where TRequest : IRequest<TQueryString> where TQueryString : new(), FluentRequestParameters<TQueryString> where TResponse : ResponseBase, TResponseInterface where TResponseInterface : IResponse`  
5.x: `public Task<TResponseInterface> DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(TRequest descriptor, Func<TRequest, PostData<object>, Task<ElasticsearchResponse<TResponse>>> dispatch) where TRequest : IRequest<TQueryString> where TQueryString : new(), FluentRequestParameters<TQueryString> where TResponse : ResponseBase, TResponseInterface where TResponseInterface : IResponse`  

**public method Nest.IHighLevelToLowLevelDispatcher.DispatchAsync&lt;TRequest, TQueryString, TResponse, TResponseInterface&gt;** *Declaration changed (Breaking)*

2.x: `public Task<TResponseInterface> DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(TRequest descriptor, Func<IApiCallDetails, Stream, TResponse> responseGenerator, Func<TRequest, PostData<object>, Task<ElasticsearchResponse<TResponse>>> dispatch) where TRequest : IRequest<TQueryString> where TQueryString : new(), FluentRequestParameters<TQueryString> where TResponse : ResponseBase, TResponseInterface where TResponseInterface : IResponse`  
5.x: `public Task<TResponseInterface> DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(TRequest descriptor, Func<IApiCallDetails, Stream, TResponse> responseGenerator, Func<TRequest, PostData<object>, Task<ElasticsearchResponse<TResponse>>> dispatch) where TRequest : IRequest<TQueryString> where TQueryString : new(), FluentRequestParameters<TQueryString> where TResponse : ResponseBase, TResponseInterface where TResponseInterface : IResponse`  

**public method Nest.IndexManyExtensions.IndexManyAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static Task<IBulkResponse> IndexManyAsync<T>(IElasticClient client, IEnumerable<T> objects, string index, string type)
```

5.x
```csharp
[ExtensionAttribute]
public static Task<IBulkResponse> IndexManyAsync<T>(IElasticClient client, IEnumerable<T> objects, IndexName index, TypeName type, CancellationToken cancellationToken)
```

**public method Nest.ReindexObservable&lt;T&gt;..ctor** *Declaration changed (Breaking)*

2.x: `public  .ctor(IElasticClient client, IConnectionSettingsValues connectionSettings, IReindexRequest reindexRequest)`  
5.x: `public  .ctor(IElasticClient client, IConnectionSettingsValues connectionSettings, IReindexRequest reindexRequest)`  

**public method Nest.SourceManyExtensions.SourceManyAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[ExtensionAttribute]
public static Task<IEnumerable<T>> SourceManyAsync<T>(IElasticClient client, IEnumerable<long> ids, string index, string type)
```

5.x
```csharp
[ExtensionAttribute]
public static Task<IEnumerable<T>> SourceManyAsync<T>(IElasticClient client, IEnumerable<long> ids, string index, string type, CancellationToken cancellationToken)
```

**public method Nest.SourceManyExtensions.SourceManyAsync&lt;T&gt;** *Declaration changed (Breaking)*

2.x
```csharp
[AsyncStateMachineAttribute(Nest.SourceManyExtensions+<SourceManyAsync>d__2`1[T])]
[ExtensionAttribute]
public static Task<IEnumerable<T>> SourceManyAsync<T>(IElasticClient client, IEnumerable<string> ids, string index, string type)
```

5.x
```csharp
[AsyncStateMachineAttribute(Nest.SourceManyExtensions+<SourceManyAsync>d__2`1[T])]
[ExtensionAttribute]
public static Task<IEnumerable<T>> SourceManyAsync<T>(IElasticClient client, IEnumerable<string> ids, string index, string type, CancellationToken cancellationToken)
```

