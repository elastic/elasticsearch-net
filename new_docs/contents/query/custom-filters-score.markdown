---
template: layout.jade
title: Custom Filters Score Query
menusection: query
menuitem: custom-filters-score
---

# Custom Filters Score Query

A custom_filters_score query allows to execute a query, and if the hit matches a provided filter (ordered), use either a boost or a script associated with it to compute the score. 

This is currently only mapped in the Factory Query DSL see `CustomFiltersScoreQueryBuilder`

