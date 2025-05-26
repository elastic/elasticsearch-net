---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/serialization.html
---

# Serialization [serialization]

By default, the .NET client for {{es}} uses the Microsoft System.Text.Json library for serialization. The client understands how to serialize and deserialize the request and response types correctly. It also handles (de)serialization of user POCO types representing documents read or written to {{es}}.

The client has two distinct serialization responsibilities - serialization of the types owned by the `Elastic.Clients.Elasticsearch` library and serialization of source documents, modeled in application code. The first responsibility is entirely internal; the second is configurable.
