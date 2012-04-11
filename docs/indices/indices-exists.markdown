---
layout: default
title: Connecting
menu_section: indices
menu_item: indices-exists
---


# Indices exists

Used to check if the index (indices) exists or not.

## Unknown index 

	var r = this.ConnectedClient.IndexExists("yadadadadadaadada");
	Assert.False(r.Exists);
	//404 is a valid response in this case
	Assert.True(r.IsValid);

## Known index 

	var r = this.ConnectedClient.IndexExists("nest_test_data");
	Assert.True(r.Exists);


