---
template: layout.jade
title: Named Filters
menusection: search
menuitem: named-filters
---


# Named Filters

Each filter allows you specify a name i.e 

	.Filter(f=>f.Name("myfilter").Term(...))

This will allow you to see on each with which filter applied to it.

Right now I'm waiting on this active Elasticsearch issue to complete mapping this functionality

https://github.com/elasticsearch/elasticsearch/issues/3097

