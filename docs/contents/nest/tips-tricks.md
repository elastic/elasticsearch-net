---
template: layout.jade
title: Tips & Tricks
menusection: 
menuitem: tips-tricks
---

# Tips & Tricks

This page lists some general tips and tricks provided by the community

## Posting Elasticsearch queries from javascript

Consider a scenario where you are using client side libraries like [elasticjs](https://github.com/fullscale/elastic.js)  
but want security to be provided by server side business logic. Consider this example using `WebAPI`

**NOTE** make sure dynamic scripting is turned off if you decide to open the full query DSL to the client!

```cs
    [RoutePrefix("api/Search")]
    public class SearchController : ApiController
    {
        [ActionName("_search")]
        public IHttpActionResult Post([FromBody]SearchDescriptor<dynamic> query)
        {;
            var client = new ElasticClient();
    
            //Your server side security goes here

            var result = client.Search(q => query);
            return Ok(result);
        }
    }
```
The fragments `[RoutePrefix("api/Search")]` and `[ActionName("_search")]` will let you change your elastic search Url 
from `http://localhost:9200/_search` to `http://yourwebsite/api/Search/_search` and let things work as normal. 
The fragment `[FromBody]SearchDescriptor<dynamic> query` will convert the JSON query into NEST SearchDescriptor. 
The fragment `client.Search(q => query)` will execute the query. 
