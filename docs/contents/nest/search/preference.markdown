---
template: layout.jade
title: Preference
menusection: search
menuitem: preference
---


# Preference

Signals on what node to execute the search

You can call the following methods on `SearchDescriptor`

	.Preference("my_custom_preference")


	.ExecuteOnPrimary()


	.ExecuteOnLocalShard


	.ExecuteOnNode("mynodename")

