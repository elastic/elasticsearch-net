---
template: layout.jade
title: Quick Start
menusection: 
menuitem: esnet-quick-start
---

# Quick Start

`Elasticsearch.Net` is a low level client to talk to Elasticsearch.

## Installing

From the package manager console inside visual studio 

    PM> Install-Package Elasticsearch.Net

Or search in the Package Manager UI for `Elasticsearch.Net` and go from there

## Connecting

Assumming Elasticsearch is already installed and running on your machine, go to http://localhost:9200 in your browser. You should see a similar response to this:

    {
      "status" : 200,
      "name" : "Sin-Eater",
      "version" : {
        "number" : "1.0.0",
        "build_hash" : "a46900e9c72c0a623d71b54016357d5f94c8ea32",
        "build_timestamp" : "2014-02-12T16:18:34Z",
        "build_snapshot" : false,
        "lucene_version" : "4.6"
      },
      "tagline" : "You Know, for Search"
    }

To connect to your local node from C# simply:

    var client = new ElasticsearchClient();
    //index a document under /myindex/mytype/1
    var indexResponse = client.Index("myindex","mytype","1", new { Hello = "World" });
    

Please be sure to read the full documentation on [connecting with elasticsearch.net](/elasticsearch-net/connecting.html) to see all the available options.


