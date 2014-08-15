---
template: layout.jade
title: Aliases
menusection: indices
menuitem: aliases
---


#Aliasing 

Adding/removing and updating aliases are also easy to do in NEST. For more information look at the [Alias Doc](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html)

## Add

### Fluent Syntax

	client.Alias(a => a
		.Add(add => add
			.Index("myindex")
			.Alias("myalias")
		)
	);

### Object Initializer Syntax

	var request = new AliasRequest
	{
		Actions = new IAliasAction[]
		{
			new AliasAddAction 
			{ 
				Add = new AliasAddOperation { Index = "myindex", Alias = "myalias" } 
			}
		}
	};

	client.Alias(request);

## Remove

### Fluent Syntax

	client.Alias(a => a
		.Remove(remove => remove
			.Index("myindex")
			.Alias("myalias")
		)
	);

### Object Initializer Syntax

	var request = new AliasRequest
	{
		Actions = new IAliasAction[]
		{
			new AliasRemoveAction 
			{ 
				Remove = new AliasRemoveOperation { Index = "myindex", Alias = "myalias" } 
			}
		}
	};

	client.Alias(request);

## Rename

To rename an alias, just do an Add and a Remove in the same operation. Elasticsearch will then atomically rename your alias:

### Fluent Syntax

	client.Alias(a => a
		.Add(add => add
			.Index("myindex")
			.Alias("newalias")
		)
		.Remove(remove => remove
			.Index("myindex")
			.Alias("oldalias")
		)
	);

### Object Initializer Syntax

	var request = new AliasRequest
	{
		Actions = new IAliasAction[]
		{
			new AliasAddAction 
			{ 
				Add = new AliasAddOperation { Index = "myindex", Alias = "myalias" } 
			},
			new AliasRemoveAction 
			{ 
				Remove = new AliasRemoveOperation { Index = "myindex", Alias = "myalias" } 
			}
		}
	};

	client.Alias(request);