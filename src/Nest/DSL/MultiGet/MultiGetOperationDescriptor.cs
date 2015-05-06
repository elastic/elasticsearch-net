using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{

	public class MultiGetOperation<T> : IMultiGetOperation
	{
		public MultiGetOperation(string id)
		{
			this.Id = id;
			this.Index = typeof(T);
			this.Type = typeof(T);
		}

		public MultiGetOperation(long id) : this(id.ToString(CultureInfo.InvariantCulture)) {}

		Type IMultiGetOperation.ClrType { get { return typeof(T); } }
		
		public IndexNameMarker Index { get; set; }
		
		public TypeNameMarker Type { get; set; }
		
		public string Id { get; set; }
		
		public IList<PropertyPathMarker> Fields { get; set; }
		
		public ISourceFilter Source { get; set; }

		public string Routing { get; set; }

		public object Document { get; set; }

		public IDictionary<PropertyPathMarker, string> PerFieldAnalyzer { get; set; }
	}

	public class MultiGetOperationDescriptor<T> : IMultiGetOperation
		where T : class
	{
		private IMultiGetOperation Self { get { return this; } }

		IndexNameMarker IMultiGetOperation.Index { get; set; }
		TypeNameMarker IMultiGetOperation.Type { get; set; }
		string IMultiGetOperation.Id { get; set; }
		string IMultiGetOperation.Routing { get; set; }
		ISourceFilter IMultiGetOperation.Source { get; set; }
		IList<PropertyPathMarker> IMultiGetOperation.Fields { get; set; }
		object IMultiGetOperation.Document { get; set; }
		IDictionary<PropertyPathMarker, string> IMultiGetOperation.PerFieldAnalyzer { get; set; }
		Type IMultiGetOperation.ClrType { get { return typeof(T); } }

		public MultiGetOperationDescriptor()
		{
			Self.Index = Self.ClrType;
			Self.Type = Self.ClrType;
		}

		/// <summary>
		/// when rest.action.multi.allow_explicit_index is set to false you can use this constructor to generate a multiget operation
		/// with no index and type set
		/// <pre>
		/// See also: https://github.com/elasticsearch/elasticsearch/issues/3636
		/// </pre>
		/// </summary>
		/// <param name="initializeEmpty"></param>
		public MultiGetOperationDescriptor(bool allowExplicitIndex)
			: this()
		{
			if (allowExplicitIndex) return;
			Self.Index = null;
		}

		/// <summary>
		/// Manually set the index, default to the default index or the index set for the type on the connectionsettings.
		/// </summary>
		public MultiGetOperationDescriptor<T> Index(string index)
		{
			if (index.IsNullOrEmpty()) return this;
			Self.Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed.
		/// </summary>
		public MultiGetOperationDescriptor<T> Type(string type)
		{
			if (type.IsNullOrEmpty()) return this;
			Self.Type = type;
			return this;
		}


		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public MultiGetOperationDescriptor<T> Type(Type type)
		{
			if (type == null) return this;
			Self.Type = type;
			return this;
		}

		public MultiGetOperationDescriptor<T> Id(long id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		public MultiGetOperationDescriptor<T> Id(string id)
		{
			Self.Id = id;
			return this;
		}
		/// <summary>
		/// Control how the document's source is loaded
		/// </summary>
		public MultiGetOperationDescriptor<T> Source(ISourceFilter source)
		{
			Self.Source = source;
			return this;
		}

		/// <summary>
		/// Control how the document's source is loaded
		/// </summary>
		public MultiGetOperationDescriptor<T> Source(Func<SearchSourceDescriptor<T>, SearchSourceDescriptor<T>> source)
		{
			Self.Source = source(new SearchSourceDescriptor<T>());
			return this;
		}

		/// <summary>
		/// Set the routing for the get operation
		/// </summary>
		public MultiGetOperationDescriptor<T> Routing(string routing)
		{
			routing.ThrowIfNullOrEmpty("routing");
			Self.Routing = routing;
			return this;
		}

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public MultiGetOperationDescriptor<T> Fields(params Expression<Func<T, object>>[] expressions)
		{
			Self.Fields = expressions.Select(e => (PropertyPathMarker)e).ToList();
			return this;
		}

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public MultiGetOperationDescriptor<T> Fields(params string[] fields)
		{
			Self.Fields = fields.Select(f => (PropertyPathMarker)f).ToList();
			return this;
		}

		// Only used for the MLT query for specifying an artificial document.
		// TODO: For 2.0, we should consider decoupling IMultiGetOperation from 
		// MoreLikeThisQuery and have a dedicatd MoreLikeThisDocument object.
		public MultiGetOperationDescriptor<T> Document(T document)
		{
			Self.Document = document;
			return this;
		}

		// Only used for the MLT query for providing a different analyzer per
		// artificial document field.
		// TODO: For 2.0, we should consider decoupling IMultiGetOperation from 
		// MoreLikeThisQuery and have a dedicatd MoreLikeThisDocument object.
		public MultiGetOperationDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<Expression<Func<T, object>>, string>, FluentDictionary<Expression<Func<T, object>>, string>> analyzerSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, string>();
			analyzerSelector(d);
			Self.PerFieldAnalyzer = d.ToDictionary(x => PropertyPathMarker.Create(x.Key), x => x.Value);
			return this;
		}

		// Only used for the MLT query for providing a different analyzer per
		// artificial document field.
		// TODO: For 2.0, we should consider decoupling IMultiGetOperation from 
		// MoreLikeThisQuery and have a dedicatd MoreLikeThisDocument object.
		public MultiGetOperationDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<PropertyPathMarker, string>, FluentDictionary<PropertyPathMarker, string>> analyzerSelector)
		{
			Self.PerFieldAnalyzer = analyzerSelector(new FluentDictionary<PropertyPathMarker, string>());
			return this;
		}
	}
}