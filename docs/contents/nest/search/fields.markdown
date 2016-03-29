---
template: layout.jade
title: Fields
menusection: search
menuitem: fields
---


# Fields

Quite often you don't need to waste bandwidth by returning the complete object graph foreach hit. 

Using 

	.Fields(p=>p.Id, p=>p.Name, p=>p.Followers.First().Name, ...)

You can limit the returned fields for each hit.

An overload taking plain strings also exists.

