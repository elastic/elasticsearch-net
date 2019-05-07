using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IApiKeyRole
	{
		/// <summary>
		/// A list of clusters
		/// </summary>
		[JsonProperty("cluster")]
		IEnumerable<string> Cluster { get; set; }

		/// <summary>
		/// A list of API key privileges for indices.
		/// </summary>
		[JsonProperty("index")]
		IEnumerable<IApiKeyPrivileges> Index { get; set; }
	}

	public class ApiKeyRole : IApiKeyRole
	{
		/// <inheritdoc />
		[JsonProperty("cluster")]
		public IEnumerable<string> Cluster { get; set; }

		/// <inheritdoc />
		[JsonProperty("index")]
		public IEnumerable<IApiKeyPrivileges> Index { get; set; }
	}

	public class ApiKeyRoleDescriptor
		: DescriptorBase<ApiKeyRoleDescriptor, IApiKeyRole>, IApiKeyRole
	{
		/// <inheritdoc cref="IApiKeyRole.Cluster" />
		IEnumerable<string> IApiKeyRole.Cluster { get; set; }

		/// <inheritdoc cref="IApiKeyRole.Index" />
		IEnumerable<IApiKeyPrivileges> IApiKeyRole.Index { get; set; }

		/// <inheritdoc cref="IApiKeyRole.Cluster" />
		public ApiKeyRoleDescriptor Cluster(params string[] cluster) => Assign(cluster, (a, v) => a.Cluster = v);

		/// <inheritdoc cref="IApiKeyRole.Cluster" />
		public ApiKeyRoleDescriptor Cluster(IEnumerable<string> cluster) => Assign(cluster, (a, v) => a.Cluster = v);

		/// <inheritdoc cref="IApiKeyRole.Index" />
		public ApiKeyRoleDescriptor Indices(Func<ApiKeyPrivilegesDescriptor, IPromise<List<IApiKeyPrivileges>>> selector
		) => Assign(selector, (a, v) => a.Index = v?.Invoke(new ApiKeyPrivilegesDescriptor())?.Value);
	}
}
