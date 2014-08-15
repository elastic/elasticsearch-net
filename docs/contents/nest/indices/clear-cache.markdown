---
template: layout.jade
title: Clear Cache
menusection: indices
menuitem: clear-cache
---


# Clear cache

The clear cache API allows to clear either all caches or specific cached associated with one ore more indices.

## ClearAll

	var r = this.ConnectedClient.ClearCache();


## Typed with advanced options

	var r = this.ConnectedClient.ClearCache<ElasticSearchProject>(ClearCacheOptions.Filter | ClearCacheOptions.Bloom); 

