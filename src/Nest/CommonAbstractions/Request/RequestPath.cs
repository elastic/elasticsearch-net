using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;

namespace Nest
{
	public interface IRequestPath
	{
		HttpMethod HttpMethod { get; set; }
		Indices Index { get; set; }
		Types Type { get; set; }
		string Id { get; set; }
		string Name { get; set; }
		string Field { get; set; }
		string ScrollId { get; set; }
		string NodeId { get; set; }
		string Fields { get; set; }
		string SearchGroups { get; set; }
		string IndexingTypes { get; set; }
		string Repository { get; set; }
		string Snapshot { get; set; }
		string Metric { get; set; }
		string IndexMetric { get; set; }
	}

	public interface IRequestPath<TParameters> : IRequestPath
		where TParameters : IRequestParameters, new()
	{
		TParameters RequestParameters { get; set; }
	}

	public class RequestPath<TParameters> : IRequestPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public HttpMethod HttpMethod { get; set; }
		public Indices Index { get; set; }
		public Types Type { get; set; }
		public string Id { get; set; }
		public TParameters RequestParameters { get; set; }
		public string Name { get; set; }
		public string Field { get; set; }
		public string ScrollId { get; set; }
		public string NodeId { get; set; }
		public string Fields { get; set; }
		public string SearchGroups { get; set; }
		public string IndexingTypes { get; set; }
		public string Repository { get; set; }
		public string Snapshot { get; set; }
		public string Feature { get; set; }
		public string Metric { get; set; }
		public string IndexMetric { get; set; }
		public string Lang { get; set; }

		public RequestPath()
		{
			this.RequestParameters = new TParameters();
		}

        public RequestPath<TParameters> Required(Indices indices)
        {
            this.Index = indices;
            return this;
        }

        public RequestPath<TParameters> Optional(Indices indices)
        {
            this.Index = indices;
            return this;
        }

        public RequestPath<TParameters> Required(Types types)
        {
            this.Type = types;
            return this;
        }

        public RequestPath<TParameters> Optional(Types types)
        {
            this.Type = types;
            return this;
        }

        [Obsolete("TODO: Rename to Required once NodeId type is implemented")]
        public RequestPath<TParameters> RequiredNodeId(string nodeId)
        {
            this.NodeId = nodeId;
            return this;
        }

        [Obsolete("TODO: Rename to Optional once NodeId type is implemented")]
        public RequestPath<TParameters> OptionalNodeId(string nodeId)
        {
            this.NodeId = nodeId;
            return this;
        }

        [Obsolete]
        public RequestPath<TParameters> RequiredId(string id)
        {
            this.Id = id;
            return this;
        }

        [Obsolete]
        public RequestPath<TParameters> OptionalId(string id)
        {
            this.Id = id;
            return this;
        }

		public RequestPath<TParameters> DeserializationOverride(Func<IApiCallDetails, Stream, object> customObjectCreation)
		{
			this.RequestParameters.DeserializationOverride = customObjectCreation;
			return this;
		}

		internal ElasticsearchResponse<T> CallWhen<T>(HttpMethod method, bool allSet, Func<IRequestPath<TParameters>, ElasticsearchResponse<T>> action) =>
				allSet ? action(this) : null;
	}
}