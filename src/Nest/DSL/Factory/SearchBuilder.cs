using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Facet;
using Nest.FactoryDsl.Filter;
using Nest.FactoryDsl.Query;
using Nest.FactoryDsl.Sort;

namespace Nest.FactoryDsl
{
    /// <summary>
    /// A search source builder allowing to easily build search source. Simple construction
    /// using {@link org.elasticsearch.search.builder.SearchSourceBuilder#searchSource()}.
    /// </summary>
    public class SearchBuilder : IJsonSerializable
    {
        private bool _explain;
        private List<AbstractFacetBuilder> _facets;
        private byte[] _facetsBinary;
        private int _facetBinaryOffset;
        private int _facetBinaryLength;
        private List<string> _fieldNames;
        private IFilterBuilder _filterBuilder;
        private byte[] _filterBinary;
        private int _filterBinaryOffset;
        private int _filterBinaryLength;
        private int? _from;
        private HighlightBuilder _highlightBuilder;
        private Dictionary<string, float> _indexBoost;
        private float? _minScore;
        private IQueryBuilder _queryBuilder;
        private byte[] _queryBinary;
        private int _queryBinaryOffset;
        private int _queryBinaryLength;
        private List<ScriptFieldInternal> _scriptFields;
        private int? _size;
        private List<ISortBuilder> _sorts;
        private string[] _stats;
        private bool _trackScores;
        private bool _version;

        /// <summary>
        ///  A static factory method to construct a new search source.
        /// </summary>
        /// <returns></returns>
        public static SearchBuilder SearchSource()
        {
            return new SearchBuilder();
        }

        /// <summary>
        ///  A static factory method to construct a new search builder.
        /// </summary>
        /// <returns></returns>
        public static SearchBuilder Builder()
        {
            return new SearchBuilder();
        }

        /// <summary>
        /// A static factory method to construct new search highlights.
        /// </summary>
        /// <returns></returns>
        public static HighlightBuilder Highlight()
        {
            return new HighlightBuilder();
        }

        /// <summary>
        /// Constructs a new search source builder with a search query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SearchBuilder Query(IQueryBuilder query)
        {
            _queryBuilder = query;
            return this;
        }

        /// <summary>
        /// Constructs a new search source builder with a raw search query.
        /// </summary>
        /// <param name="queryBinary"></param>
        /// <returns></returns>
        public SearchBuilder Query(byte[] queryBinary)
        {
            return Query(queryBinary, 0, queryBinary.Length);
        }

        /// <summary>
        /// Constructs a new search source builder with a raw search query.
        /// </summary>
        /// <param name="queryBinary"></param>
        /// <param name="queryBinaryOffset"></param>
        /// <param name="queryBinaryLength"></param>
        /// <returns></returns>
        public SearchBuilder Query(byte[] queryBinary, int queryBinaryOffset, int queryBinaryLength)
        {
            _queryBinary = queryBinary;
            _queryBinaryOffset = queryBinaryOffset;
            _queryBinaryLength = queryBinaryLength;
            return this;
        }

        /// <summary>
        /// Constructs a new search source builder with a raw search query.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public SearchBuilder Query(string queryString)
        {
            return Query(System.Text.Encoding.Unicode.GetBytes(queryString));
        }

        //public SearchBuilder Query(object query)
        //{
        //    (Covert to JObject)
        //    try
        //    {
        //        return query(query.underlyingBytes(), 0, query.underlyingBytesLength());
        //    }
        //    catch (IOException e)
        //    {
        //        throw new ElasticSearchGenerationException("failed to generate query from builder", e);
        //    }
        //}

        public SearchBuilder Filter(IFilterBuilder filter)
        {
            _filterBuilder = filter;
            return this;
        }

        /// <summary>
        /// Sets a filter on the query executed that only applies to the search query
        /// (and not facets for example). 
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public SearchBuilder Filter(string filterString)
        {
            return Filter(System.Text.Encoding.Unicode.GetBytes(filterString));
        }

        /// <summary>
        /// Sets a filter on the query executed that only applies to the search query
        /// (and not facets for example). 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public SearchBuilder Filter(byte[] filter)
        {
            return Filter(filter, 0, filter.Length);
        }

        /// <summary>
        /// Sets a filter on the query executed that only applies to the search query
        /// (and not facets for example).
        /// </summary>
        /// <param name="filterBinary"></param>
        /// <param name="filterBinaryOffset"></param>
        /// <param name="filterBinaryLength"></param>
        /// <returns></returns>
        public SearchBuilder Filter(byte[] filterBinary, int filterBinaryOffset, int filterBinaryLength)
        {
            _filterBinary = filterBinary;
            _filterBinaryOffset = filterBinaryOffset;
            _filterBinaryLength = filterBinaryLength;
            return this;
        }

        //public SearchBuilder Filter(object query)
        //{
        //    (Covert to JObject)
        //    try
        //    {
        //        return query(query.underlyingBytes(), 0, query.underlyingBytesLength());
        //    }
        //    catch (IOException e)
        //    {
        //        throw new ElasticSearchGenerationException("failed to generate query from builder", e);
        //    }
        //}

        /// <summary>
        /// From index to start the search from. Defaults to <tt>0</tt>.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public SearchBuilder From(int from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// The number of search hits to return. Defaults to <tt>10</tt>.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public SearchBuilder Size(int size)
        {
            _size = size;
            return this;
        }

        /// <summary>
        /// Sets the minimum score below which docs will be filtered out.
        /// </summary>
        /// <param name="minScore"></param>
        /// <returns></returns>
        public SearchBuilder MinScore(float minScore)
        {
            _minScore = minScore;
            return this;
        }

        /// <summary>
        /// Should each {@link org.elasticsearch.search.SearchHit} be returned with an
        /// explanation of the hit (ranking).
        /// </summary>
        /// <param name="explain"></param>
        /// <returns></returns>
        public SearchBuilder Explain(bool explain)
        {
            _explain = explain;
            return this;
        }

        /// <summary>
        /// Should each {@link org.elasticsearch.search.SearchHit} be returned with a version
        /// associated with it.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public SearchBuilder Version(bool version)
        {
            _version = version;
            return this;
        }

        /// <summary>
        /// Adds a sort against the given field name and the sort ordering.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="order">The sort ordering</param>
        /// <returns></returns>
        public SearchBuilder Sort(string name, SortOrder order)
        {
            return Sort(SortFactory.FieldSort(name).Order(order));
        }

        /// <summary>
        /// Add a sort against the given field name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SearchBuilder Sort(string name)
        {
            return Sort(SortFactory.FieldSort(name));
        }

        /// <summary>
        /// Adds a sort builder.
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public SearchBuilder Sort(ISortBuilder sort)
        {
            if (_sorts == null)
            {
                _sorts = new List<ISortBuilder>();
            }

            _sorts.Add(sort);
            return this;
        }

        /// <summary>
        /// Applies when sorting, and controls if scores will be tracked as well. Defaults to
        /// <tt>false</tt>.
        /// </summary>
        /// <param name="trackScores"></param>
        /// <returns></returns>
        public SearchBuilder TrackScores(bool trackScores)
        {
            _trackScores = trackScores;
            return this;
        }

        /// <summary>
        /// Add a facet to perform as part of the search.
        /// </summary>
        /// <param name="facet"></param>
        /// <returns></returns>
        public SearchBuilder Facet(AbstractFacetBuilder facet)
        {
            if (_facets == null)
            {
                _facets = new List<AbstractFacetBuilder>();
            }

            _facets.Add(facet);
            return this;
        }

        /// <summary>
        /// Sets raw (json) facets.
        /// </summary>
        /// <param name="facetsBinary"></param>
        /// <returns></returns>
        public SearchBuilder Facets(byte[] facetsBinary)
        {
            return Facets(facetsBinary, 0, facetsBinary.Length);
        }

        /// <summary>
        /// Sets raw (json) facets.
        /// </summary>
        /// <param name="facetsBinary"></param>
        /// <param name="facetBinaryOffset"></param>
        /// <param name="facetBinaryLength"></param>
        /// <returns></returns>
        public SearchBuilder Facets(byte[] facetsBinary, int facetBinaryOffset, int facetBinaryLength)
        {
            _facetsBinary = facetsBinary;
            _facetBinaryOffset = facetBinaryOffset;
            _facetBinaryLength = facetBinaryLength;
            return this;
        }

        //public SearchBuilder Facets(object query)
        //{
        //    (Covert to JObject)
        //    try {
        //        return facets(facets.underlyingBytes(), 0, facets.underlyingBytesLength());
        //    } catch (IOException e) {
        //        throw new ElasticSearchGenerationException("failed to generate filter from builder", e);
        //    } 
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HighlightBuilder Highlighter()
        {
            return _highlightBuilder ?? (_highlightBuilder = new HighlightBuilder());
        }

        /// <summary>
        /// Adds highlight to perform as part of the search.
        /// </summary>
        /// <param name="highlight"></param>
        /// <returns></returns>
        public SearchBuilder Highlight(HighlightBuilder highlight)
        {
            _highlightBuilder = highlight;
            return this;
        }

        /// <summary>
        /// Sets no fields to be loaded, resulting in only id and type to be returned per field.
        /// </summary>
        /// <returns></returns>
        public SearchBuilder NoFields()
        {
            _fieldNames.Clear();
            return this;
        }

        /// <summary>
        /// Sets the fields to load and return as part of the search request. If none are specified,
        /// the source of the document will be returned.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public SearchBuilder Fields(List<string> fields)
        {
            _fieldNames = fields;
            return this;
        }

        /// <summary>
        /// Sets the fields to load and return as part of the search request. If none are specified,
        /// the source of the document will be returned.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public SearchBuilder Fields(params string[] fields)
        {
            if (_fieldNames == null)
            {
                _fieldNames = new List<string>();
            }

            _fieldNames.AddRange(fields);
            return this;
        }

        /// <summary>
        /// Adds a field to load and return (note, it must be stored) as part of the search request.
        /// If none are specified, the source of the document will be return.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public SearchBuilder Field(string field)
        {
            if (_fieldNames == null)
            {
                _fieldNames = new List<string>();
            }

            _fieldNames.Add(field);
            return this;
        }

        /// <summary>
        /// Adds a script field under the given name with the provided script.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="script">The script</param>
        /// <returns></returns>
        public SearchBuilder ScriptField(string name, string script)
        {
            return ScriptField(name, null, script, null);
        }

        /// <summary>
        /// Adds a script field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="script">The script to execute</param>
        /// <param name="params">The script parameters</param>
        /// <returns></returns>
        public SearchBuilder ScriptField(string name, string script, Dictionary<string, object> @params) 
        {
            return ScriptField(name, null, script, @params);
        }

        /// <summary>
        /// Adds a script field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="lang">The language of the script</param>
        /// <param name="script">The script to execute</param>
        /// <param name="params">The script parameters (can be <tt>null</tt>)</param>
        /// <returns></returns>
        public SearchBuilder ScriptField(string name, string lang, string script, Dictionary<string, object> @params)
        {
            if (_scriptFields == null)
            {
                _scriptFields = new List<ScriptFieldInternal>();
            }

            _scriptFields.Add(new ScriptFieldInternal(name, lang, script, @params));

            return this;
        }

        /// <summary>
        /// Sets the boost a specific index will receive when the query is executeed against it.
        /// </summary>
        /// <param name="index">The index to apply the boost against</param>
        /// <param name="indexBoost">The boost to apply to the index</param>
        /// <returns></returns>
        public SearchBuilder IndexBoost(string index, float indexBoost)
        {
            if (_indexBoost == null)
            {
                _indexBoost = new Dictionary<string, float>();
            }

            _indexBoost.Add(index, indexBoost);
            return this;
        }

        /// <summary>
        /// The stats groups this request will be aggregated under.
        /// </summary>
        /// <param name="statsGroups"></param>
        /// <returns></returns>
        public SearchBuilder Stats(params string[] statsGroups)
        {
            _stats = statsGroups;
            return this;
        }

        #region IJsonSerializable Members

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        public object ToJsonObject()
        {
            var content = new JObject();

            if (_from != null)
            {
                content["from"] = _from;
            }

            if (_size != null)
            {
                content["size"] = _size;
            }

            if (_queryBuilder != null)
            {
                content["query"] = _queryBuilder.ToJsonObject() as JObject;
            }

            if (_filterBuilder != null)
            {
                content["filter"] = _filterBuilder.ToJsonObject() as JObject;
            }

            if (_minScore != null)
            {
                content["min_score"] = _minScore;
            }

            if (_version)
            {
                content["version"] = _version;
            }

            if (_explain)
            {
                content["explain"] = _explain;
            }

            if (_fieldNames != null)
            {
                if (_fieldNames.Count == 1)
                {
                    content["fields"] = _fieldNames[0];
                }
                else
                {
                    content["fields"] = new JArray(_fieldNames);
                }
            }

            if(_scriptFields != null)
            {
                content["script_fields"] = new JObject();
                
                foreach (var scriptField in _scriptFields)
                {
                    content["script_fields"][scriptField.FieldName()] = new JObject(new JProperty("script", scriptField.Script()));
                    
                    if(scriptField.Lang() != null)
                    {
                        content["script_fields"][scriptField.FieldName()]["lang"] = scriptField.Lang();
                    }

                    if(scriptField.Params() != null)
                    {
                        content["script_fields"][scriptField.FieldName()]["params"] = new JObject();

                        foreach (var param in scriptField.Params())
                        {
                            content["script_fields"][scriptField.FieldName()]["params"][param.Key] = new JValue(param.Value);
                        }
                    }

                }
            }

            if (_sorts != null)
            {
                if (_sorts.Count > 0)
                {
                    content["sort"] = new JArray(_sorts.Select(t => t.ToJsonObject()));

                    if (_trackScores)
                    {
                        content["track_scores"] = _trackScores;
                    }
                }
            }

            if (_indexBoost != null)
            {
                if (_indexBoost.Count > 0)
                {
                    content["indices_boost"] = new JObject();

                    foreach (var index in _indexBoost)
                    {
                        content["indices_boost"][index.Key] = index.Value;
                    }
                }
            }

            if (_facets != null)
            {
                content["facets"] = new JObject();

                foreach (var facet in _facets)
                {
                    content["facets"][facet.Name] = facet.ToJsonObject() as JObject;
                }
            }

            if (_highlightBuilder != null)
            {
                content.Add(_highlightBuilder.ToJsonObject());
            }

            if (_stats != null)
            {
                if (_stats.Any())
                {
                    content["stats"] = new JArray(_stats);
                }
            }

            return content;
        }

        #endregion

        private class ScriptFieldInternal
        {
            private readonly string _fieldName;
            private readonly string _script;
            private readonly string _lang;
            private readonly Dictionary<string, object> _params;

            public ScriptFieldInternal(string fieldName, string lang, string script, Dictionary<string, object> @params)
            {
                _fieldName = fieldName;
                _lang = lang;
                _script = script;
                _params = @params;
            }

            public string FieldName()
            {
                return _fieldName;
            }

            public string Script()
            {
                return _script;
            }

            public string Lang()
            {
                return _lang;
            }

            public Dictionary<string, object> Params()
            {
                return _params;
            }
        }
    }


}