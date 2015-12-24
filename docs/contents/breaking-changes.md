---
template: layout.jade
title: Breaking Changes
menusection: concepts
menuitem: breaking-changes
---

#Breaking Changes

## Elasticsearch 1.0

Elasticsearch 1.0 comes with it's own set of breaking changes which [are all documented in the elasticsearch documentation](http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/breaking-changes.html). This page describes breaking changes NEST introduces in its 1.0 release and to an extend how you should handle Elasticsearch 1.0 changes in your exisiting code base using NEST prior to NEST 1.0.

## NEST 1.0

### Strong Named Packages

Prior to 1.0 NEST came with a `NEST` and `NEST.Signed` nuget package. In 1.0 there is one package called `NEST` which is a signed strong named assembly. We follow the example of JSON.NET and only change our `AssemblyVersion` on major releases only update the `AssemblyFileVersion` for every release. This way you get most of the benefits of unsigned assemblies while still providing support for developers whose business guidelines mandates the usage of signed assemblies.


### IElasticClient

The outer layer of NEST has been completely rewritten from scratch. Many calls will now have a different signature. Although the most common ones have been reimplemented as [extensions methods](http://github.com/elasticsearch/elasticsearch-net/tree/master/src/Nest/ConvenienceExtensions). Two notable changes should be outlined though. 

### Renamed Get() to Source(), Removed GetFull()
When Martijn first wrote NEST back in 2010, he thought it would be handy if the Get() operation returned only the document, and if you wanted the full enveloped Elasticsearch response, you'd use `GetFull()`. This was rather confusing and on top of that Elasticsearch 1.0 now has it's own endpoint for getting JUST the document `_source`.
Similarily `GetMany()` is now called `SourceMany()`.

### Renamed QueryResponse to SearchResponse

The fact that `client.Search<T>()` returns a `QueryResponse<T>` and not a `SearchResponse<T>` never felt right, NEST 1.0 therefore renamed `QueryResponse<T>` to `SearchResponse<T>`

### Renamed PutTemplateRaw()

to `.PutTemplate` and can be used as follows:

    client.PutTemplate("template_name", s => s.Template("template_body"));

### Other Renames

#### `RootObjectMappingDescriptor` => `PutMappingDescriptor<T>`
#### `BaseFilter` => `FilterContainer`
#### `BaseQuery` => `QueryContainer`
#### `SortDescriptor` => `SortFieldDescriptor`

### Removed IResponse.Error

IResponse.Error.Exception no longer exists, it is inlined to IResponse.OriginalException. The Error property did not hold any information that was not available on IResponse.ConnectionStatus.

### Removed IndexMany()

`IndexMany` no longer has an overload that takes `SimpleBulkParameters`.  You are now required to use `Bulk()` directly if you need more fine grained control.

### Removed MapFromAttributes()

Attributes are too limited in what they can specify, so `[ElasticType()]` can now only specify the type name and the id property.
All the other annotations have been removed from `[ElasticType()]`. The properties on `[ElasticProperty()]` still exists an can be applied like this:

    var x = this._client.CreateIndex(index, s => s
        .AddMapping<ElasticsearchProject>(m => m
             .MapFromAttributes()
             .DateDetection()
             .IndexAnalyzer())
    );

Or in a separate put mapping call:

    var response = client.Map<ElasticsearchProject>(m => m.MapFromAttributes()......);

### Response Shortcuts

Prior to 1.0, some calls directly returned a bool or value instead of the full enveloped Elasticsearch response.

i.e `client.IndexExists("myIndexName")` used to return a bool but should now be called like this:

     client.IndexExists(i => i.Index("myIndexName")).Exists

### Alias Helpers

NEST 0.12.0 had some alias helpers, `SwapAlias()`, `GetIndicesPointingToAlias()`, etc.  These have been removed in favor of just `Alias()` and `GetAliases()`.

### Fields() vs SourceInclude()

Prior to Elasticsearch 1.0, you could specify to return only certain fields and they would return like this:

    ...
    "fields" {
         "name": "NEST"
         "followers.firstName": ["Martijn", "John", ...]
    }
    ...


In many case this could be mapped to the type of DTO you give search (i.e in `.Search<DTO>()`). Elasticsearch 1.0 now always returns the fields as arrays.

    ...
    "fields" {
         "name": ["NEST"]
         "followers.firstName": ["Martijn", "John", ...]
    }
    ...

Previously, Documents was a collection of T with just the specified fields filled in, other fields would be null of have their default value. Now, Documents is an empty collection.

Previously, DocumentsWithMetaData -> Fields would be an instance of T with just the specified fields filled in, other fields would be null or have their default value. Now, Hits -> Fields is (backed by) a dictionary containing only the specified fields.

NEST 1.0 still supports fields, but is now a bit more verbose in how it supports mapping the fields back:


    var fields = _client.Get<DTO>(g => g
        .Id(4)
        .Fields(f => f.Name, f => f.Followers.First().FirstName)
    ).Fields;

    var name = fields.FieldValue<DTO, string>(f => f.Name);
    var list = fields.FieldValue<DTO, string>(f => f.Followers[0].FirstName);

`name` and `list` are of type `string[]` 

### DocumentsWithMetaData

When you do a search with NEST 0.12, you'd get back a `QueryResponse<T>` with two ways to loop over your results. `.Documents` is an `IEnumerable<T>` and `.DocumentsWithMetaData` is and `IEnumerable<IHit<T>>` depending on your needs one of them might be easier to use.

Starting from NEST 1.0 `.DocumentsWithMetaData` is now called simply `.Hits`.

The old `.Hits` has been renamed to `HitsMetaData`.

### int Properties

In quite a few places values that should have been `long` were mapped as `int` in NEST 0.12.0 which could be troublesome if you for instance have more than `2,147,483,647` matching documents. In my preperations for this release I helped port one of my former employees applications to Elasticsearch 1.1 and NEST 1.0 and found that this change had the most impact on the application and all of its models. 

### 

# Found another breaking change?

If you found another breaking chage please let us know on [the github issues](http://www.github.com/elasticsearch/elasticsearch-net/issues)
 
