---
template: layout.jade
title: Update Settings
menusection: indices
menuitem: update-settings
---


#Update Settings
This call allows you to update the index settings. 
NEST whitelists which settings can be updated based on the allowed values mentioned [here in the Elasticsearch documentation]( http://www.elasticsearch.org/guide/reference/api/admin-indices-update-settings.html) this allows you to reuse an `IndexSettings` object.

##Example
this example first creates an index and then uses the same IndexSettings to update the index.

	var index = Guid.NewGuid().ToString();
	var client = this.ConnectedClient;
	var settings = new IndexSettings();
	settings.NumberOfReplicas = 1;
	settings.NumberOfShards = 5;
	settings.Add("refresh_interval", "1s");
	settings.Add("search.slowlog.threshold.fetch.warn", "1s");
	client.CreateIndex(index, settings);

	settings["refresh_interval"] = "-1";
	settings["search.slowlog.threshold.fetch.warn"] = "5s";

	var r = this.ConnectedClient.UpdateSettings(index, settings);
				
	Assert.True(r.Success);
	Assert.True(r.OK);
	var getResponse = this.ConnectedClient.GetIndexSettings(index);
	Assert.AreEqual(getResponse.Settings["refresh_interval"], "-1");
	Assert.AreEqual(getResponse.Settings["search.slowlog.threshold.fetch.warn"], "1s");


