---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/recommendations.html
---

# Usage recommendations [recommendations]

To achieve the most efficient use of the Elasticsearch .NET Client, we recommend following the guidance defined in this article.


## Reuse the same client instance [_reuse_the_same_client_instance]

When working with the Elasticsearch .NET Client we recommend that consumers reuse a single instance of `ElasticsearchClient` for the entire lifetime of the application. When reusing the same instance:

* initialization overhead is limited to the first usage.
* resources such as TCP connections can be pooled and reused to improve efficiency.
* serialization overhead is reduced, improving performance.

The `ElasticsearchClient` type is thread-safe and can be shared and reused across multiple threads in consuming applications. Client reuse can be achieved by creating a singleton static instance or by registering the type with a singleton lifetime when using dependency injection containers.


## Prefer asynchronous methods [_prefer_asynchronous_methods]

The Elasticsearch .NET Client exposes synchronous and asynchronous methods on the `ElasticsearchClient`. We recommend always preferring the asynchronous methods, which have the `Async` suffix. Using the Elasticsearch .NET Client requires sending HTTP requests to {{es}} servers. Access to {{es}} is sometimes slow or delayed, and some complex queries may take several seconds to return. If such operations are blocked by calling the synchronous methods, the thread must wait until the HTTP request is complete. In high-load scenarios, this can cause significant thread usage, potentially affecting the throughput and performance of consuming applications. By preferring the asynchronous methods, application threads can continue with other work that doesn’t depend on the web resource until the potentially blocking task completes.

