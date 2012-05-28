---
layout: default
title: Script Update
menu_section: core
menu_item: update
---


# Update by script API 

The update API allows to update a document based on a script provided. The operation gets the document (collocated with the shard) from the index, runs the script (with optional script language and parameters), and index back the result (also allows to delete, or ignore the operation). It uses versioning to make sure no updates have happened during the “get” and “reindex”. (available from `0.19` onwards).

	this.ConnectedClient.Update<ElasticSearchProject>(u => u
		.Object(project)
		.Script("ctx._source.loc += 10")
		.RetriesOnConflict(5)
		.Refresh()
	);

This is just a simple example all the options that are available (such as passing params) are available. 