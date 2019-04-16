using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(SearchTemplateRequest))]
	public partial interface ISearchTemplateRequest : ICovariantSearchRequest
	{
		[DataMember(Name ="id")]
		string Id { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		[IgnoreDataMember]
		string Inline { get; set; }

		[DataMember(Name ="params")]
		IDictionary<string, object> Params { get; set; }

		[DataMember(Name ="source")]
		string Source { get; set; }
	}

	public partial class SearchTemplateRequest
	{
		public string Id { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public string Inline
		{
			get => Source;
			set => Source = value;
		}

		public IDictionary<string, object> Params { get; set; }

		public string Source { get; set; }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		protected Type ClrType { get; set; }
		Type ICovariantSearchRequest.ClrType => ClrType;

		protected sealed override void Initialize() => TypedKeys = true;
	}

	public class SearchTemplateRequest<T> : SearchTemplateRequest
		where T : class
	{
		public SearchTemplateRequest() : base(typeof(T)) => ClrType = typeof(T);

		public SearchTemplateRequest(Indices indices) : base(indices) => ClrType = typeof(T);
	}

	public partial class SearchTemplateDescriptor<T> where T : class
	{
		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }

		Type ICovariantSearchRequest.ClrType => typeof(T);

		string ISearchTemplateRequest.Id { get; set; }

		string ISearchTemplateRequest.Inline
		{
			get => Self.Source;
			set => Self.Source = value;
		}

		IDictionary<string, object> ISearchTemplateRequest.Params { get; set; }

		string ISearchTemplateRequest.Source { get; set; }

		protected sealed override void Initialize() => TypedKeys();

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public SearchTemplateDescriptor<T> Inline(string template) => Assign(a => a.Inline = template);

		public SearchTemplateDescriptor<T> Source(string template) => Assign(a => a.Source = template);

		public SearchTemplateDescriptor<T> Id(string id) => Assign(a => a.Id = id);

		public SearchTemplateDescriptor<T> Params(Dictionary<string, object> paramDictionary) => Assign(a => a.Params = paramDictionary);

		public SearchTemplateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(a => a.Params = paramDictionary?.Invoke(new FluentDictionary<string, object>()));
	}
}
