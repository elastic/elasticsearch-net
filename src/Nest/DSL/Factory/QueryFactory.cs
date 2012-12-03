using Nest.FactoryDsl.Filter;
using Nest.FactoryDsl.Query;

namespace Nest.FactoryDsl
{
    public static class QueryFactory
    {
        /// <summary>
        /// A query that match on all documents.
        /// </summary>
        /// <returns></returns>
        public static MatchAllQueryBuilder MatchAllQuery()
        {
            return new MatchAllQueryBuilder();
        }

        /// <summary>
        ///  Creates a text query with type "BOOLEAN" for the provided field name and text.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="text">The query text (to be analyzed).</param>
        /// <returns></returns>
        public static TextQueryBuilder TextQuery(string name, object text)
        {
            return new TextQueryBuilder(name, text).Type(TextQueryType.BOOLEAN);
        }

        /// <summary>
        /// Creates a text query with type "PHRASE" for the provided field name and text.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="text">The query text (to be analyzed).</param>
        /// <returns></returns>
        public static TextQueryBuilder TextPhraseQuery(string name, object text)
        {
            return new TextQueryBuilder(name, text).Type(TextQueryType.PHRASE);
        }

        /// <summary>
        /// Creates a text query with type "PHRASE_PREFIX" for the provided field name and text.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="text">The query text (to be analyzed).</param>
        /// <returns></returns>
        public static TextQueryBuilder TextPhrasePrefixQuery(string name, object text)
        {
            return new TextQueryBuilder(name, text).Type(TextQueryType.PHRASE_PREFIX);
        }

        /// <summary>
        /// A query that generates the union of documents produced by its sub-queries, and that scores each document
        /// with the maximum score for that document as produced by any sub-query, plus a tie breaking increment for any
        /// additional matching sub-queries.
        /// </summary>
        /// <returns></returns>
        public static DisMaxQueryBuilder DisMaxQuery()
        {
            return new DisMaxQueryBuilder();
        }

        /// <summary>
        /// Constructs a query that will match only specific ids within types.
        /// </summary>
        /// <param name="types">The mapping/doc type</param>
        /// <returns></returns>
        public static IdsQueryBuilder IdsQuery(params string[] types)
        {
            return new IdsQueryBuilder(types);
        }

        /// <summary>
        /// A Query that matches documents containing a term.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        /// <returns></returns>
        public static TermQueryBuilder TermQuery(string name, string value)
        {
            return new TermQueryBuilder(name, value);
        }

        /// <summary>
        /// A Query that matches documents containing a term.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        /// <returns></returns>
        public static TermQueryBuilder TermQuery(string name, int value)
        {
            return new TermQueryBuilder(name, value);
        }

        /// <summary>
        /// A Query that matches documents containing a term.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        /// <returns></returns>
        public static TermQueryBuilder TermQuery(string name, long value)
        {
            return new TermQueryBuilder(name, value);
        }

        /// <summary>
        /// A Query that matches documents containing a term.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        /// <returns></returns>
        public static TermQueryBuilder TermQuery(string name, float value)
        {
            return new TermQueryBuilder(name, value);
        }

        /// <summary>
        /// A Query that matches documents containing a term.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        /// <returns></returns>
        public static TermQueryBuilder TermQuery(string name, double value)
        {
            return new TermQueryBuilder(name, value);
        }

        /// <summary>
        /// A Query that matches documents containing a term.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        /// <returns></returns>
        public static TermQueryBuilder TermQuery(string name, bool value)
        {
            return new TermQueryBuilder(name, value);
        }

        /// <summary>
        /// A Query that matches documents containing a term.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        /// <returns></returns>
        public static TermQueryBuilder TermQuery(string name, object value)
        {
            return new TermQueryBuilder(name, value);
        }

        /// <summary>
        /// A Query that matches documents using fuzzy query.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="value">The value of the term</param>
        /// <returns></returns>
        public static FuzzyQueryBuilder FuzzyQuery(string name, string value)
        {
            return new FuzzyQueryBuilder(name, value);
        }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        /// a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        /// <returns></returns>
        public static FieldQueryBuilder FieldQuery(string name, string query)
        {
            return new FieldQueryBuilder(name, query);
        }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        /// a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        /// <returns></returns>
        public static FieldQueryBuilder FieldQuery(string name, int query)
        {
            return new FieldQueryBuilder(name, query);
        }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        /// a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        /// <returns></returns>
        public static FieldQueryBuilder FieldQuery(string name, long query)
        {
            return new FieldQueryBuilder(name, query);
        }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        /// a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        /// <returns></returns>
        public static FieldQueryBuilder FieldQuery(string name, float query)
        {
            return new FieldQueryBuilder(name, query);
        }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        /// a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        /// <returns></returns>
        public static FieldQueryBuilder FieldQuery(string name, double query)
        {
            return new FieldQueryBuilder(name, query);
        }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        /// a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        /// <returns></returns>
        public static FieldQueryBuilder FieldQuery(string name, bool query)
        {
            return new FieldQueryBuilder(name, query);
        }

        /// <summary>
        /// A query that executes the query string against a field. It is a simplified
        /// version of {@link QueryStringQueryBuilder} that simply runs against
        /// a single field.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="query">The query string</param>
        /// <returns></returns>
        public static FieldQueryBuilder FieldQuery(string name, object query)
        {
            return new FieldQueryBuilder(name, query);
        }

        /// <summary>
        /// A Query that matches documents containing terms with a specified prefix.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="prefix">The prefix query</param>
        /// <returns></returns>
        public static PrefixQueryBuilder PrefixQuery(string name, string prefix)
        {
            return new PrefixQueryBuilder(name, prefix);
        }

        /// <summary>
        /// A Query that matches documents within an range of terms.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <returns></returns>
        public static RangeQueryBuilder RangeQuery(string name)
        {
            return new RangeQueryBuilder(name);
        }

        /// <summary>
        /// Implements the wildcard search query. Supported wildcards are <tt>*</tt>, which
        /// matches any character sequence (including the empty one), and <tt>?</tt>,
        /// which matches any single character. Note this query can be slow, as it
        /// needs to iterate over many terms. In order to prevent extremely slow WildcardQueries,
        /// a Wildcard term should not start with one of the wildcards <tt>*</tt> or
        /// <tt>?</tt>.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="query">The wildcard query string</param>
        /// <returns></returns>
        public static WildcardQueryBuilder WildcardQuery(string name, string query)
        {
            return new WildcardQueryBuilder(name, query);
        }

        /// <summary>
        /// A query that parses a query string and runs it. There are two modes that this operates. The first,
        /// when no field is added (using {@link QueryStringQueryBuilder#field(String)}, will run the query once and non prefixed fields
        /// will use the {@link QueryStringQueryBuilder#defaultField(String)} set. The second, when one or more fields are added
        /// (using {@link QueryStringQueryBuilder#field(String)}), will run the parsed query against the provided fields, and combine
        /// them either using DisMax or a plain boolean query (see {@link QueryStringQueryBuilder#useDisMax(boolean)}).
        /// </summary>
        /// <param name="queryString">The query string to run</param>
        /// <returns></returns>
        public static QueryStringQueryBuilder QueryString(string queryString)
        {
            return new QueryStringQueryBuilder(queryString);
        }

        /// <summary>
        /// The BoostingQuery class can be used to effectively demote results that match a given query.
        /// Unlike the "NOT" clause, this still selects documents that contain undesirable terms,
        /// but reduces their overall score:
        /// </summary>
        /// <returns></returns>
        public static BoostingQueryBuilder BoostingQuery()
        {
            return new BoostingQueryBuilder();
        }

        /// <summary>
        /// A Query that matches documents matching boolean combinations of other queries.
        /// </summary>
        /// <returns></returns>
        public static BoolQueryBuilder BoolQuery()
        {
            return new BoolQueryBuilder();
        }

        public static SpanTermQueryBuilder SpanTermQuery(string name, string value)
        {
            return new SpanTermQueryBuilder(name, value);
        }

        public static SpanTermQueryBuilder SpanTermQuery(string name, int value)
        {
            return new SpanTermQueryBuilder(name, value);
        }

        public static SpanTermQueryBuilder SpanTermQuery(string name, long value)
        {
            return new SpanTermQueryBuilder(name, value);
        }

        public static SpanTermQueryBuilder SpanTermQuery(string name, float value)
        {
            return new SpanTermQueryBuilder(name, value);
        }

        public static SpanTermQueryBuilder SpanTermQuery(string name, double value)
        {
            return new SpanTermQueryBuilder(name, value);
        }

        public static SpanFirstQueryBuilder SpanFirstQuery(ISpanQueryBuilder match, int end)
        {
            return new SpanFirstQueryBuilder(match, end);
        }

        public static SpanNearQueryBuilder SpanNearQuery()
        {
            return new SpanNearQueryBuilder();
        }

        public static SpanNotQueryBuilder SpanNotQuery()
        {
            return new SpanNotQueryBuilder();
        }

        public static SpanOrQueryBuilder SpanOrQuery()
        {
            return new SpanOrQueryBuilder();
        }

        public static FieldMaskingSpanQueryBuilder FieldMaskingSpanQuery(ISpanQueryBuilder query, string field)
        {
            return new FieldMaskingSpanQueryBuilder(query, field);
        }

        /// <summary>
        /// A query that applies a filter to the results of another query.
        /// </summary>
        /// <param name="queryBuilder">The query to apply the filter to</param>
        /// <param name="filterBuilder">The filter to apply on the query</param>
        /// <returns></returns>
        public static FilteredQueryBuilder FilteredQuery(IQueryBuilder queryBuilder, IFilterBuilder filterBuilder)
        {
            return new FilteredQueryBuilder(queryBuilder, filterBuilder);
        }

        /// <summary>
        /// A query that wraps a filter and simply returns a constant score equal to the
        /// query boost for every document in the filter.
        /// </summary>
        /// <param name="filterBuilder">The filter to wrap in a constant score query</param>
        /// <returns></returns>
        public static ConstantScoreQueryBuilder ConstantScoreQuery(IFilterBuilder filterBuilder)
        {
            return new ConstantScoreQueryBuilder(filterBuilder);
        }

        /// <summary>
        /// A query that simply applies the boost fact to the wrapped query (multiplies it).
        /// </summary>
        /// <param name="queryBuilder">The query to apply the boost factor to.</param>
        /// <returns></returns>
        public static CustomBoostFactorQueryBuilder CustomBoostFactorQuery(IQueryBuilder queryBuilder)
        {
            return new CustomBoostFactorQueryBuilder(queryBuilder);
        }

        /// <summary>
        /// A query that allows to define a custom scoring script.
        /// </summary>
        /// <param name="queryBuilder">The query to custom score</param>
        /// <returns></returns>
        public static CustomScoreQueryBuilder CustomScoreQuery(IQueryBuilder queryBuilder)
        {
            return new CustomScoreQueryBuilder(queryBuilder);
        }

        public static CustomFiltersScoreQueryBuilder CustomFiltersScoreQuery(IQueryBuilder queryBuilder)
        {
            return new CustomFiltersScoreQueryBuilder(queryBuilder);
        }

        /// <summary>
        /// A more like this query that finds documents that are "like" the provided {@link MoreLikeThisQueryBuilder#likeText(String)}
        /// which is checked against the fields the query is constructed with.
        /// </summary>
        /// <param name="fields">The fields to run the query against</param>
        /// <returns></returns>
        public static MoreLikeThisQueryBuilder MoreLikeThisQuery(params string[] fields)
        {
            return new MoreLikeThisQueryBuilder(fields);
        }

        /// <summary>
        /// A more like this query that finds documents that are "like" the provided {@link MoreLikeThisQueryBuilder#likeText(String)}
        /// which is checked against the "_all" field.
        /// </summary>
        /// <returns></returns>
        public static MoreLikeThisQueryBuilder MoreLikeThisQuery()
        {
            return new MoreLikeThisQueryBuilder();
        }

        /// <summary>
        /// A fuzzy like this query that finds documents that are "like" the provided {@link FuzzyLikeThisQueryBuilder#likeText(String)}
        /// which is checked against the fields the query is constructed with.
        /// </summary>
        /// <param name="fields">The fields to run the query against</param>
        /// <returns></returns>
        public static FuzzyLikeThisQueryBuilder FuzzyLikeThisQuery(params string[] fields)
        {
            return new FuzzyLikeThisQueryBuilder(fields);
        }

        /// <summary>
        /// A fuzzy like this query that finds documents that are "like" the provided {@link FuzzyLikeThisQueryBuilder#likeText(String)}
        /// which is checked against the "_all" field.
        /// </summary>
        /// <returns></returns>
        public static FuzzyLikeThisQueryBuilder FuzzyLikeThisQuery()
        {
            return new FuzzyLikeThisQueryBuilder();
        }

        /// <summary>
        /// A fuzzy like this query that finds documents that are "like" the provided {@link FuzzyLikeThisFieldQueryBuilder#likeText(String)}.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FuzzyLikeThisFieldQueryBuilder FuzzyLikeThisFieldQuery(string name)
        {
            return new FuzzyLikeThisFieldQueryBuilder(name);
        }

        /// <summary>
        /// A query that will execute the wrapped query only for the specified indices, and "match_all" when
        /// it does not match those indices.
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="indices"> </param>
        /// <returns></returns>
        public static IndicesQueryBuilder IndicesQuery(IQueryBuilder queryBuilder, params string[] indices) 
        {
            return new IndicesQueryBuilder(queryBuilder, indices);
        }

        /// <summary>
        /// A more like this query that runs against a specific field.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <returns></returns>
        public static MoreLikeThisFieldQueryBuilder MoreLikeThisFieldQuery(string name)
        {
            return new MoreLikeThisFieldQueryBuilder(name);
        }

        /// <summary>
        /// Constructs a new scoring child query, with the child type and the query to run on the child documents. The
        /// results of this query are the parent docs that those child docs matched.
        /// </summary>
        /// <param name="type">The child type</param>
        /// <param name="query">The query</param>
        /// <returns></returns>
        public static TopChildrenQueryBuilder TopChildrenQuery(string type, IQueryBuilder query)
        {
            return new TopChildrenQueryBuilder(type, query);
        }

        /// <summary>
        /// Constructs a new NON scoring child query, with the child type and the query to run on the child documents. The
        /// results of this query are the parent docs that those child docs matched.
        /// </summary>
        /// <param name="type">The child type.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public static HasChildQueryBuilder HasChildQuery(string type, IQueryBuilder query)
        {
            return new HasChildQueryBuilder(type, query);
        }

        public static NestedQueryBuilder NestedQuery(string path, IQueryBuilder query)
        {
            return new NestedQueryBuilder(path, query);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder TermsQuery(string name, params string[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder TermsQuery(string name, params int[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder TermsQuery(string name, params long[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder TermsQuery(string name, params float[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder TermsQuery(string name, params double[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder TermsQuery(string name, params object[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder InQuery(string name, params string[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder InQuery(string name, params int[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder InQuery(string name, params long[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder InQuery(string name, params float[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder InQuery(string name, params double[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsQueryBuilder InQuery(string name, params object[] values)
        {
            return new TermsQueryBuilder(name, values);
        }

        /// <summary>
        /// A filter that restricts search results to values that have a matching prefix in a given
        /// field.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="prefix">The prefix</param>
        /// <returns></returns>
        public static PrefixFilterBuilder InQuery(string name, string prefix)
        {
            return new PrefixFilterBuilder(name, prefix);
        }

        /// <summary>
        /// A Query builder which allows building a query thanks to a JSON string or binary data.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static WrapperQueryBuilder WrapperQuery(string source)
        {
            return new WrapperQueryBuilder(source);
        }

        /// <summary>
        /// A Query builder which allows building a query thanks to a JSON string or binary data.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static WrapperQueryBuilder WrapperQuery(byte[] source, int offset, int length)
        {
            return new WrapperQueryBuilder(source, offset, length);
        }
    }
}