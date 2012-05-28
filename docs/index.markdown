---
layout: default
title: Introduction
menu_section: concepts
menu_item: introduction
---

# Introduction

NEST aims to be a .net client with a very concise API. Its main goal is to provide a solid strongly typed Elasticsearch client. It also has string/dynamic overloads for more dynamic usecases. 
 

Indexing is as simple as:

	var post = new Post() { Id = 12, ... }
	client.Index(post);

Indexing asynchronously is as easy as:

	//IndexAsync returns a Task<ConnectionStatus>
	var task = client.IndexAsync(post);

Searching is fluid:

	var results = this.ConnectedClient.Search<ElasticSearchProject>(s => s
			.From(0)
			.Size(10)
			.Fields(f => f.Id, f => f.Name)
			.SortAscending(f => f.LOC)
			.SortDescending(f => f.Name)
			.Query(q=>q.Term(f=>f.Name, "NEST", Boost: 2.0))
	);

## Copyright

Copyright (c) 2010 Martijn Laarman and everyone wonderful enough to contribute to [NEST](https://github.com/Mpdreamz/NEST)

## License

NEST is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [license.txt](https://github.com/Mpdreamz/NEST/blob/master/src/license.txt) for more information.

## Contributors

A special shoutout to [@stephenpope](http://github.com/stephenpope) for allowing his port 
of the java factory based dsl [Rubber](http://github.com/stephenpope/Rubber) to be merged into NEST. 
NEST now has **two types of query dsl's** (lambda and factory based)!

Some of the other wonderful features in NEST were pushed by these wonderful folks:

* [@nordbergm](https://github.com/nordbergm/NEST)
* [@kevingessner](https://github.com/kevingessner/NEST)
* [@EFJoseph](https://github.com/EFJoseph/NEST)
* [@pkrakowiak](https://github.com/pkrakowiak/NEST) 
* [@q42jaap] (https://github.com/q42jaap/NEST)



<script id="contributorTemplate" type="text/x-jQuery-tmpl">
{{each contributors}}
  {{if login != "invalid-email-address"}}  
   <div style="clear:both; margin: 8px; height: 80px; width: 600px;" id="contributor">
	    <img src="http://gravatar.com/avatar/${gravatar_id}" alt="${name || login}" style="float:left;" />
	    <div style="float:left; margin-left: 12px;">
	      <h3>#${$index + 1} (${contributions} commits)</h3>
	      <h2>${name ? name.replace(" ", " '" + login + "' ") + "!" : login}</h2>
	      <a href="https://github.com/${login}">https://github.com/${login}</a>
	    </div>
	</div>
  {{/if}}
{{/each}}
</script>

<div id="contributors">&nbsp;</div>
