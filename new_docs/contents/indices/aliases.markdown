---
template: layout.jade
title: Connecting
menusection: indices
menuitem: aliases
---


#Aliasing 

## Adding


	var response = this.ConnectedClient.Alias("nest_test_data", "nest_test_data2");


## Renaming

    var response = this.ConnectedClient.Rename("nest_test_data", "nest_test_data2", "nest_test_data3");


## Removing

	var response = this.ConnectedClient.RemoveAlias("nest_test_data", "nest_test_data3");
