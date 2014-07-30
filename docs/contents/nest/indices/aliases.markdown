---
template: layout.jade
title: Connecting
menusection: indices
menuitem: aliases
---


#Aliasing 

Adding/removing and updating aliases are also easy to do in NEST. For more information look at the [Alias Doc](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html)

## Adding

	_client.Alias(a => a
				  .Add(add => add
				    .Index("myindex")
				    .Alias("myalias")));

## Removing

	_client.Alias(a => a
					.Remove(remove => remove
						.Index("myindex")
						.Alias("myalias")));


## Renaming

To rename a alias, just do an Add and a Remove in the same operation. Elasticsearch will then atomically rename your alias

	_client.Alias(a => a
					.Add(add => add
						.Index("myindex")
						.Alias("newalias"))
					.Remove(remove => remove
						.Index("myindex")
						.Alias("oldalias")));

## Asynchronous

Doing alias operations Async is simple:

	_client.AliasAsync(...);
