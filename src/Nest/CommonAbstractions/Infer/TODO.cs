using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	public class ScrollIds : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class ScrollId : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
		public static implicit operator ScrollId(string id) => new ScrollId();
	}

	public class NodeIds : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class Metrics : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class Features : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class Feature : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class Names : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}

		public static implicit operator Names(Name name)
		{
			return new Names();
		}
	}

	public class Name : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class PropertyNames : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}
	
	public interface IDocumentPath
	{
		Id Id { get; set; }
		IndexName Index { get; set; }
		TypeName Type { get; set; }
	}


	public class Document<T> : IDocumentPath
		where T : class
	{
		internal IDocumentPath Self => this;
		Id IDocumentPath.Id { get; set; }
		IndexName IDocumentPath.Index { get; set; }
		TypeName IDocumentPath.Type { get; set; }

		internal Document(Id id) { Self.Id = id; }

		public static Document<T> Id(Id id) => new Document<T>(id);
		public static Document<T> From(T @object) => new Document<T>(Nest.Id.From(@object));

		public Document<T> Index(IndexName index)
		{
			Self.Index = index;
			return this;
		}
		public Document<T> Type(TypeName type)
		{
			Self.Type = type;
			return this;
		}
	}

}
