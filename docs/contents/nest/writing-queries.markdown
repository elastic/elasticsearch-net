---
template: layout.jade
title: Writing Queries
menusection: concepts
menuitem: writing-queries
---

# Writing Queries
One of the most important things to grasp when using NEST is how to write queries. NEST offers you several possibilities.

All the examples in this section are assumed to be wrapped in:

    var result = client.Search<ElasticSearchProject>(s=>s
        .From(0)
        .Size(10)
        // Query here
    );

Or if using the object initializer syntax:

    var result = new SearchRequest
    {
        From = 0,
        Size = 10,
        // Query here
    };

## Raw Strings
Although not preferred, many folks like to build their own JSON strings and just pass that along:

    .QueryRaw(@"{""match_all"": {} }")
    .FilterRaw(@"{""match_all"": {} }")

NEST does not modify this in anyway and just writes this straight into the JSON output. 

## Query DSL
The preferred way to write queries, since it gives you alot of cool features.

### Lambda Expressions
    .Query(q=>q
        .Term(p=>p.Name, "NEST")
    )

Here you'll see we can use expressions to address properties in a type safe matter. This also works for `IEnumerable` types e.g.

    .Query(q=>q
        .Term(p=>p.Followers.First().FirstName, "NEST")
    )

Because these property lookups are expressions you don't have to do any null checks. The previous would expand to the `followers.firstName` property name. 

Of course if you need to pass the property name as string NEST will allow you to do so:

    .Query(q=>q
        .Term("followers.firstName", "martijn")
    )

This can be alternatively written using the object initializer syntax:

    QueryContainer query = new TermQuery
    {
        Field = "followers.firstName",
        Value = "NEST"
    };

    Query = query

### Static Query/Filter Generator
Sometimes you'll need to reuse a filter or query often. To aid with this you can also write queries like this:

    var termQuery = Query<ElasticSearchProject>
        .Term(p=>p
            .Followers.First().FirstName, "martijn");
    
    .Query(q=>q
        .Bool(bq=>bq
            .Must(
                mq=>mq.MatchAll()
                , termQuery
            )
        )
    )

Similarly `Filter<T>.[Filter]()` methods exist for filters.

### Boolean Queries 
As can be seen in the previous example writing out boolean queries can turn into a really tedious and verbose effort. Luckily, NEST supports bitwise operators, so we can rewrite the previous as such:

    .Query(q=>q.MatchAll() && termQuery)

Note how we are mixing and matching the lambda and static queries here.

We can also do the same thing using the OIS:

    QueryContainer query = new MatchAllQuery() && new TermQuery
    {
        Field = "firstName",
        Value = "martijn"
    };

Similary an `OR` looks like this:

Fluent...

    .Query(q=>q
        q.Term("name", "Nest")
        || q.Term("name", "Elastica")
    )

OIS...

    QueryContainer query1 = new TermQuery
    {
        Field = "name",
        Value = "Nest"
    };
    QueryContainer query2 = new TermQuery
    {
        Field = "name",
        Value = "Elastica"
    };

    Query = query1 || query2

`NOT`'s are also supported:

Fluent...

    .Query(q=>q
        q.Term("language", "php")
        && !q.Term("name", "Elastica")
    )

OIS...

    Query = query1 && !query2

This will query for all the php clients except `Elastica`.

You can mix and match this to any level of complexity until it satisfies your query requirements.

Fluent...

    .Query(q=>q
        (q.Term("language", "php")
            && !q.Term("name", "Elastica")
        )
        ||
        q.Term("name", "NEST")
    )

OIS...

    Query = (query1 && !query2) || query3

Will query all php clients except `Elastica` or where the name equals `NEST`.

#### Clean Output Support
Normally writing three boolean must clauses looks like this (psuedo code)

    must
        clause1
        clause2
        clause3

A naive implemenation of the bitwise operators would make all the queries sent to Elasticsearch look like

    must
        must
            clause1
            clause2
        clause3

This degrades rather rapidly and makes inspecting generated queries quite a chore. NEST does its best to detect these cases and will always write them in the first, cleaner form.

## Conditionless Queries

Writing complex boolean queries is one thing, but more often then not you'll want to make decisions on how to query based on user input. 

    public class UserInput
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public int? LOC { get; set; }
    }

and then

    .Query(q=> {
            BaseQuery query = null;
            if (!string.IsNullOrEmpty(userInput.Name))
                query &= q.Term(p=>p.Name, userInput.Name);
            if (!string.IsNullOrEmpty(userInput.FirstName))
                query &= q
                    .Term("followers.firstName", userInput.FirstName);
            if (userInput.LOC.HasValue)
                query &= q.Range(r=>r.OnField(p=>p.Loc).From(userInput.Loc.Value))
            return query;
        })

This again becomes tedious and verbose rather quickly as well. Therefore, NEST allows you to write the previous query as:

    .Query(q=>
        q.Term(p=>p.Name, userInput.Name);
        && q.Term("followers.firstName", userInput.FirstName)
        && q.Range(r=>r.OnField(p=>p.Loc).From(userInput.Loc))
    )

If any of the queries would result in an empty query they won't be sent to Elasticsearch. 

So if all the terms are null (or empty string) on `userInput` except `userInput.Loc` it wouldn't even wrap the range query in a boolean query but just issue a plain range query. 

If all of them are empty it will result in a `match_all` query. 

This conditionless behavior is turned on by default but can be turned of like so:

     var result = client.Search<ElasticSearchProject>(s=>s
        .From(0)
        .Size(10)
        .Strict() //disable conditionless queries by default
        ...
    );

However queries themselves can opt back in or out.

    .Query(q=>
        q.Strict().Term(p=>p.Name, userInput.Name);
        && q.Term("followers.firstName", userInput.FirstName)
        && q.Strict(false).Range(r=>r.OnField(p=>p.Loc).From(userInput.Loc))
    )

In this example if `userInput.Name` is null or empty it will result in a `DslException`. The range query will use conditionless logic no matter if the SearchDescriptor uses `.Strict()` or not.

Please note that conditionless query logic propagates:

    q.Strict().Term(p=>p.Name, userInput.Name);
    && q.Term("followers.firstName", userInput.FirstName)
    && q.Filtered(fq => fq
        .Query(qff => 
            qff.Terms(p => p.Country, userInput.Countries)
            && qff.Terms(p => p.Loc, userInput.Loc)
        )
    )

If both `userInput.Countries` and `userInput.Loc` are null or empty the entire `filtered` query will not be issued. 

















