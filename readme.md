Repository for both **Elasticsearch.Net** and **NEST**, the two official [elasticsearch](https://github.com/elasticsearch/elasticsearch) .NET clients.


#[Elasticsearch.Net](src/Elasticsearch.Net)

* Low-level client that provides a one-to-one mapping with the Elasticsearch REST API
* No dependencies
* Almost completely generated from the official REST API spec which makes it easy to keep up to date
* Comes with an integration test suite that can be generated from the YAML test definitions that the Elasticsearch core team uses to test their REST API
* Has no opinions on how you create or consume requests and responses
* Load balancing and cluster failover support
* All calls have async variants

[Read more here](src/Elasticsearch.Net)

#[NEST](https://github.com/elasticsearch/elasticsearch-net/tree/master/src/Nest#nest-)
* High-level client that internally uses the low-level **Elasticsearch.Net** client
* Maps requests and responses to strongly typed objects with a fluent interface and object initializer syntax to build them
* Comes with a very powerful query DSL thats maps one-to-one with Elasticsearch
* Takes advantage of .NET features where they make sense (i.e., covariant `IEnumerable<T>` result types, type and index inference)
* All calls have async variants

[Read more here](https://github.com/elasticsearch/elasticsearch-net/tree/master/src/Nest#nest-)

###Build Statuses

[![Build Status](http://teamcity.codebetter.com/app/rest/builds/buildType:%28id:bt993%29/statusIcon)](http://teamcity.codebetter.com/viewType.html?buildTypeId=bt993&guest=1)

[![elasticsearch-net MyGet Build Status](https://www.myget.org/BuildSource/Badge/elasticsearch-net?identifier=624cebb3-a461-466f-9bac-7026c8ba615a)](https://www.myget.org/)
