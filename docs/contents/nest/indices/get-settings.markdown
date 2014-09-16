---
template: layout.jade
title: Get Settings
menusection: indices
menuitem: get-settings
---


# Get settings

The get settings API allows to retrieve settings of index/indices

	var r = this.ConnectedClient.GetIndexSettings(index);
	Assert.True(r.Success);
	Assert.NotNull(r.Settings);
	Assert.AreEqual(r.Settings.NumberOfReplicas, 4);
	Assert.AreEqual(r.Settings.NumberOfShards, 8);
	Assert.Greater(r.Settings.Count(), 0);
	Assert.True(r.Settings.ContainsKey("merge.policy.merge_factor"));


