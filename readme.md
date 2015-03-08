Repository for both **Elasticsearch.Net** and **NEST**, the two official [elasticsearch](https://github.com/elasticsearch/elasticsearch) .NET clients.

[![install from nuget](http://img.shields.io/nuget/v/NEST.svg?style=flat-square)](https://www.nuget.org/packages/NEST)[![downloads](http://img.shields.io/nuget/dt/NEST.svg?style=flat-square)](https://www.nuget.org/packages/NEST)    
Bleeding edge package:    
[![download](http://img.shields.io/myget/elasticsearch-net/v/NEST.svg?style=flat-square)](https://www.myget.org/gallery/elasticsearch-net)[![downloads](http://img.shields.io/myget/elasticsearch-net/dt/NEST.svg?style=flat-square)](https://www.myget.org/gallery/elasticsearch-net)    
Builds:    
[![teamcity](http://img.shields.io/teamcity/http/teamcity.codebetter.com/e/bt993.svg?style=flat-square)](http://teamcity.codebetter.com/viewType.html?buildTypeId=bt993)[![elasticsearch-net MyGet Build Status](https://www.myget.org/BuildSource/Badge/elasticsearch-net?identifier=624cebb3-a461-466f-9bac-7026c8ba615a)](https://www.myget.org/gallery/elasticsearch-net)

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
* Comes with a very powerful query DSL that maps one-to-one with Elasticsearch
* Takes advantage of .NET features where they make sense (i.e., covariant `IEnumerable<T>` result types, type and index inference)
* All calls have async variants

[Read more here](https://github.com/elasticsearch/elasticsearch-net/tree/master/src/Nest#nest-)


