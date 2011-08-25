var x =
{ 
    "query":
    {
        "match_all": {}
    },
    "facets": {
        "followers.lastName": { "terms": { "field": "followers.lastName"} },
        "datehisto": {
            "date_histogram": {
                "field": "followers.dateOfBirth",
                "interval": "year"
            }
        },
        "histo2": {
            "histogram": {
                "field": "followers.id",
                "interval": 10
            }
        },
        "filterfacet": {
            "filter": {
                "term": { "followers.id": 20 }
            }
        },
        "queryfacet": {
            "query": {
                "term": { "followers.id": 20 }
            }
        },
        "statistical": {
            "statistical": {
                "field": "id"
            }
        },
        "statisticalmultiple": {
            "statistical": {
                "fields": ["id", "followers.id"]
            }
        },
        "statscript": {
            "statistical": {
                "script": "doc.score"
            }
        },
        "tag_price_stats": {
            "terms_stats": {
                "key_field": "followers.lastName",
                "value_field": "id"
            }
        },
        "geo1": {
            "geo_distance": {
                "followers.placeOfBirth": {
                    "lat": 21.0,
                    "lon": 21.0
                },
                "ranges": [
                    { "to": 10 },
                    { "from": 10, "to": 20 },
                    { "from": 20, "to": 100 },
                    { "from": 100 }
                ]
            }
        }
    }
};