using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// This API will decrypt and re-read the entire keystore, on every cluster node, but only the reloadable secure settings will
		/// be applied.
		/// <para>Changes to other settings will not go into effect until the next restart. Once the call returns, the reload has been
		/// completed, meaning that all internal datastructures dependent on these settings have been changed. Everything should look as if
		/// the settings had the new value from the start.</para>
		/// <para>
		/// When changing multiple reloadable secure settings, modify all of them, on each cluster node, and then issue a reload_secure_settings call,
		/// instead of reloading after each modification.</para>
		/// </summary>
		public static ReloadSecureSettingsResponse ReloadSecureSettings(this IElasticClient client,Func<ReloadSecureSettingsDescriptor, IReloadSecureSettingsRequest> selector = null);

		/// <inheritdoc cref="ReloadSecureSettings(System.Func{Nest.ReloadSecureSettingsDescriptor,Nest.IReloadSecureSettingsRequest})"/>
		public static ReloadSecureSettingsResponse ReloadSecureSettings(this IElasticClient client,IReloadSecureSettingsRequest request);

		/// <inheritdoc cref="ReloadSecureSettings(System.Func{Nest.ReloadSecureSettingsDescriptor,Nest.IReloadSecureSettingsRequest})"/>
		public static Task<ReloadSecureSettingsResponse> ReloadSecureSettingsAsync(this IElasticClient client,
			Func<ReloadSecureSettingsDescriptor, IReloadSecureSettingsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ReloadSecureSettings(System.Func{Nest.ReloadSecureSettingsDescriptor,Nest.IReloadSecureSettingsRequest})"/>
		public static Task<ReloadSecureSettingsResponse> ReloadSecureSettingsAsync(this IElasticClient client,IReloadSecureSettingsRequest request,
			CancellationToken ct = default
		);
	}

}
