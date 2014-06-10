Repository for both the offical low level client `Elasticsearch.Net` as the high level opiniated `NEST` client.


#[Elasticsearch.Net](src/Elasticsearch.Net)

* One-to-one mapping with REST API and the other official clients
* Load balancing / Cluster failover support.
* Almost completely generated from the official rest API spec which makes it easy to keep up to date.
* Comes with an integration test suite that can be generated from the yaml test definitions that the elasticsearch core team uses to test their REST API.
* All calls have async variants
* Has no opinions on how you create or consume the request and response although comes with a special dynamic type you can deserialize too.
* 
[Read more here](src/Elasticsearch.Net)

#[NEST](https://github.com/elasticsearch/elasticsearch-net/tree/master/src/Nest#nest-)
* High level client that internally uses the low level client.
* Maps requests and response to strongly typed objects with a fluent interface to build them.
* Comes with a very powerful query dsl thats maps one-to-one with Elasticsearch.
* Takes advantage of .NET features where they make sense (i.e: covariant `IEnumerable<T>` result types, inferring typenames and indexnames automatically)
* All calls have async variants

[Read more here](https://github.com/elasticsearch/elasticsearch-net/tree/master/src/Nest#nest-)

#Build statuses

[![Build Status](http://teamcity.codebetter.com/app/rest/builds/buildType:%28id:bt993%29/statusIcon)](http://teamcity.codebetter.com/viewType.html?buildTypeId=bt993&guest=1)

[![elasticsearch-net MyGet Build Status](https://www.myget.org/BuildSource/Badge/elasticsearch-net?identifier=624cebb3-a461-466f-9bac-7026c8ba615a)](https://www.myget.org/)
