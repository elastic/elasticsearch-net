---
layout: default
title: Connecting
menu_section: concepts
menu_item: handling-responces
---


# Handling responces

All responses have the following properties

`IsValid` and `ConnectionStatus`

These properties pertain to NEST and whether NEST thinks the response is valid or not. In some cases though elasticsearch responds back with with an `ok` or `acknowledged` properties. NEST will **always** map these fields but they will not influence the `IsValid` property. If NEST was successful in connecting and getting back a 200 status `IsValid` will always be **true** by design. If you need to check for elasticsearch validity check the `OK` or `Acknowledged` properties where these apply.

`ConnectionStatus` holds the HttpStatusCode and various other interesting information in case a transport error occurred.

**NOTE** in most cases elasticsearch will throw a 500 and in that case `IsValid` will be false too. `ConnectionStatus.Result` will hold the error message as recieved from elasticsearch.

