---
template: layout.jade
title: Type/Index Inference
menusection: concepts
menuitem: index-type-inference
---

#Inference

Imagine we have a Person [POCO](http://en.wikipedia.org/wiki/Plain_Old_CLR_Object)
   
    public class Person
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

That we would like to index in Elasticsearch 

    var person = new Person
    {
        Id = "1",
        Firstname = "Martijn",
        Lastname = "Laarman"
    };

    var index = client.Index(person);

This will index the object to `/my-default-index/person/1`. 

`NEST` is smart enough to infer the index and type name for the `Person` CLR type. It was also able to get the id of `1` by the convention of looking for `Id` property on the specified object. Where it will look for the Id can be specified using the `ElasticType` attribute.

As noted in the [quick start](/nest/quick-start.html) you can always pass **explicit values** for inferred ones.

    var index = client.Index(person, i=>i
        .Index("another-index")
        .Type("another-type")
        .Id("1-should-not-be-the-id")
        .Refresh()
        .Ttl("1m")
    );

This will index the document using `/another-index/another-type/1-should-not-be-the-id?refresh=true&ttl=1m` as the URL. 

There are a couple of places within NEST where inference comes in to play...

## Index Name Inference

Whenever an explicit index name is not provided, NEST will look to see if the type has its own default index name on the connection settings.

     settings.MapDefaultTypeIndices(d=>d
        .Add(typeof(MyType), "my-type-index")
     );

     client = new ElasticClient(settings, defaultIndex: "my-default-index");

     // searches in /my-type-index/mytype/_search
     client.Search<MyType>()

     // searches in /my-default-index/person/_search
     client.Search<Person>()

`MyType` defaults to `my-type-index` because it is explicitly configured, but `Person` will default to the global fallback `my-default-index`.

## Type Name Inference

Whenever NEST needs a type name but is not given one explicitly, it will use the given CLR type to infer it's Elasticsearch type name.

    settings.MapDefaultTypeNames(d=>d
        .Add(typeof(MyType), "MY_TYPO")
    );
    
    // searches in /inferred-index/MY_TYPO/_search
    client.Search<MyType>();
    
    // searches in /inferred-index/person/_search
    client.Search<Person>();

Another way of setting an explicit inferred value for a type is through setting an attribute:

    [ElasticType(Name="automobile")]
    public class Car {} 

As you can also see in the search example, NEST by default lowercases type names that do not have a configured inferred value.

    settings.SetDefaultTypeNameInferrer(t=>t.Name.ToUpperInvariant());

Now all type names that have not been explictly specified or have not been explicitly configured will be uppercased.

Prior to NEST 1.0 type names were by default lowercased AND pluralized, if you want this behavior back use:

    settings.PluralizeTypeNames();

## Property Name Inference
In many places `NEST` allows you to pass property names and JSON paths as C# expressions, i.e:

    .Query(q=>q
        .Term(p=>p.Followers.First().FirstName, "martijn"))

`NEST` by default will camelCase properties. So the `FirstName` property above will be translated to "followers.firstName".

This can be configured by setting 

    settings.SetDefaultPropertyNameInferrer(p=>p);

This will leave property names untouched.

Properties marked with `[ElasticAttibute(Name="")]` or `[JsonProperty(Name="")]` will pass the configured name verbatim.

## Id Inference

Whenever an object is passed that needs to specify an id (i.e index, bulk operations) the object is inspected to see if it has an `Id` property and if so, that value will be used.

This inspection happens once per type. The result of the function call that returns the id for an object of type T is cached; therfore, it is only called once per object of type T throughout the applications lifetime.

An example of this is at the top of this documentation where the `Index()` call could figure out the object's id was `1`.

You can control which propery holds the Id:


    [ElasticType(IdProperty="CrazyGuid")]
    public class Car 
    {
        public Guid CrazyGuidId { get; set; }
    }

This will cause the the id inferring to happen on `CrazyGuid` instead of `Id`.


