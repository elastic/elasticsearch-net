---
template: layout.jade
title: x
menusection: concepts
menuitem: breaking-changes
---
```
serialized = null;
serialized = this.Serialize(o);
var actualJson = JObject.Parse(serialized);
var matches = JToken.DeepEquals(this._expectedJsonJObject, actualJson);
this.ShouldBeEquivalentTo(serialized);
var bytes = TestClient.GetClient().Serializer.Serialize(o);
string serialized;
var oAgain = this.Deserialize<T>(serialized);
this.SerializesAndMatches(oAgain, out serialized);
```
