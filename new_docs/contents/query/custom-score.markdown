---
template: layout.jade
title: Custom Score
menusection: query
menuitem: custom-score
---


# Custom Score
custom_score query allows to wrap another query and customize the scoring of it optionally with a computation derived from other field values in the doc (numeric ones) using script expression. Here is a simple sample:

	.Query(qd=>qd
		.CustomScore(cs => cs
			.Script("doc['num1'].value > myvar")
			.Params(p=>p.Add("myvar", 1.0))
			.Query(qq => qq.MatchAll())
		)
	)

