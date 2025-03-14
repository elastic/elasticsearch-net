// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch.Features;

public partial class FeaturesNamespacedClient : NamespacedClientProxy
{
	/// <summary>
	/// <para>
	/// Initializes a new instance of the <see cref="FeaturesNamespacedClient"/> class for mocking.
	/// </para>
	/// </summary>
	protected FeaturesNamespacedClient() : base()
	{
	}

	internal FeaturesNamespacedClient(ElasticsearchClient client) : base(client)
	{
	}

	/// <summary>
	/// <para>
	/// Get the features.
	/// Get a list of features that can be included in snapshots using the <c>feature_states</c> field when creating a snapshot.
	/// You can use this API to determine which feature states to include when taking a snapshot.
	/// By default, all feature states are included in a snapshot if that snapshot includes the global state, or none if it does not.
	/// </para>
	/// <para>
	/// A feature state includes one or more system indices necessary for a given feature to function.
	/// In order to ensure data integrity, all system indices that comprise a feature state are snapshotted and restored together.
	/// </para>
	/// <para>
	/// The features listed by this API are a combination of built-in features and features defined by plugins.
	/// In order for a feature state to be listed in this API and recognized as a valid feature state by the create snapshot API, the plugin that defines that feature must be installed on the master node.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/get-features-api.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual GetFeaturesResponse GetFeatures(GetFeaturesRequest request)
	{
		request.BeforeRequest();
		return DoRequest<GetFeaturesRequest, GetFeaturesResponse, GetFeaturesRequestParameters>(request);
	}

	/// <summary>
	/// <para>
	/// Get the features.
	/// Get a list of features that can be included in snapshots using the <c>feature_states</c> field when creating a snapshot.
	/// You can use this API to determine which feature states to include when taking a snapshot.
	/// By default, all feature states are included in a snapshot if that snapshot includes the global state, or none if it does not.
	/// </para>
	/// <para>
	/// A feature state includes one or more system indices necessary for a given feature to function.
	/// In order to ensure data integrity, all system indices that comprise a feature state are snapshotted and restored together.
	/// </para>
	/// <para>
	/// The features listed by this API are a combination of built-in features and features defined by plugins.
	/// In order for a feature state to be listed in this API and recognized as a valid feature state by the create snapshot API, the plugin that defines that feature must be installed on the master node.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/get-features-api.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetFeaturesResponse> GetFeaturesAsync(GetFeaturesRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<GetFeaturesRequest, GetFeaturesResponse, GetFeaturesRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the features.
	/// Get a list of features that can be included in snapshots using the <c>feature_states</c> field when creating a snapshot.
	/// You can use this API to determine which feature states to include when taking a snapshot.
	/// By default, all feature states are included in a snapshot if that snapshot includes the global state, or none if it does not.
	/// </para>
	/// <para>
	/// A feature state includes one or more system indices necessary for a given feature to function.
	/// In order to ensure data integrity, all system indices that comprise a feature state are snapshotted and restored together.
	/// </para>
	/// <para>
	/// The features listed by this API are a combination of built-in features and features defined by plugins.
	/// In order for a feature state to be listed in this API and recognized as a valid feature state by the create snapshot API, the plugin that defines that feature must be installed on the master node.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/get-features-api.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual GetFeaturesResponse GetFeatures(GetFeaturesRequestDescriptor descriptor)
	{
		descriptor.BeforeRequest();
		return DoRequest<GetFeaturesRequestDescriptor, GetFeaturesResponse, GetFeaturesRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>
	/// Get the features.
	/// Get a list of features that can be included in snapshots using the <c>feature_states</c> field when creating a snapshot.
	/// You can use this API to determine which feature states to include when taking a snapshot.
	/// By default, all feature states are included in a snapshot if that snapshot includes the global state, or none if it does not.
	/// </para>
	/// <para>
	/// A feature state includes one or more system indices necessary for a given feature to function.
	/// In order to ensure data integrity, all system indices that comprise a feature state are snapshotted and restored together.
	/// </para>
	/// <para>
	/// The features listed by this API are a combination of built-in features and features defined by plugins.
	/// In order for a feature state to be listed in this API and recognized as a valid feature state by the create snapshot API, the plugin that defines that feature must be installed on the master node.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/get-features-api.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual GetFeaturesResponse GetFeatures()
	{
		var descriptor = new GetFeaturesRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequest<GetFeaturesRequestDescriptor, GetFeaturesResponse, GetFeaturesRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>
	/// Get the features.
	/// Get a list of features that can be included in snapshots using the <c>feature_states</c> field when creating a snapshot.
	/// You can use this API to determine which feature states to include when taking a snapshot.
	/// By default, all feature states are included in a snapshot if that snapshot includes the global state, or none if it does not.
	/// </para>
	/// <para>
	/// A feature state includes one or more system indices necessary for a given feature to function.
	/// In order to ensure data integrity, all system indices that comprise a feature state are snapshotted and restored together.
	/// </para>
	/// <para>
	/// The features listed by this API are a combination of built-in features and features defined by plugins.
	/// In order for a feature state to be listed in this API and recognized as a valid feature state by the create snapshot API, the plugin that defines that feature must be installed on the master node.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/get-features-api.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual GetFeaturesResponse GetFeatures(Action<GetFeaturesRequestDescriptor> configureRequest)
	{
		var descriptor = new GetFeaturesRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<GetFeaturesRequestDescriptor, GetFeaturesResponse, GetFeaturesRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>
	/// Get the features.
	/// Get a list of features that can be included in snapshots using the <c>feature_states</c> field when creating a snapshot.
	/// You can use this API to determine which feature states to include when taking a snapshot.
	/// By default, all feature states are included in a snapshot if that snapshot includes the global state, or none if it does not.
	/// </para>
	/// <para>
	/// A feature state includes one or more system indices necessary for a given feature to function.
	/// In order to ensure data integrity, all system indices that comprise a feature state are snapshotted and restored together.
	/// </para>
	/// <para>
	/// The features listed by this API are a combination of built-in features and features defined by plugins.
	/// In order for a feature state to be listed in this API and recognized as a valid feature state by the create snapshot API, the plugin that defines that feature must be installed on the master node.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/get-features-api.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetFeaturesResponse> GetFeaturesAsync(GetFeaturesRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<GetFeaturesRequestDescriptor, GetFeaturesResponse, GetFeaturesRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the features.
	/// Get a list of features that can be included in snapshots using the <c>feature_states</c> field when creating a snapshot.
	/// You can use this API to determine which feature states to include when taking a snapshot.
	/// By default, all feature states are included in a snapshot if that snapshot includes the global state, or none if it does not.
	/// </para>
	/// <para>
	/// A feature state includes one or more system indices necessary for a given feature to function.
	/// In order to ensure data integrity, all system indices that comprise a feature state are snapshotted and restored together.
	/// </para>
	/// <para>
	/// The features listed by this API are a combination of built-in features and features defined by plugins.
	/// In order for a feature state to be listed in this API and recognized as a valid feature state by the create snapshot API, the plugin that defines that feature must be installed on the master node.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/get-features-api.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetFeaturesResponse> GetFeaturesAsync(CancellationToken cancellationToken = default)
	{
		var descriptor = new GetFeaturesRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequestAsync<GetFeaturesRequestDescriptor, GetFeaturesResponse, GetFeaturesRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the features.
	/// Get a list of features that can be included in snapshots using the <c>feature_states</c> field when creating a snapshot.
	/// You can use this API to determine which feature states to include when taking a snapshot.
	/// By default, all feature states are included in a snapshot if that snapshot includes the global state, or none if it does not.
	/// </para>
	/// <para>
	/// A feature state includes one or more system indices necessary for a given feature to function.
	/// In order to ensure data integrity, all system indices that comprise a feature state are snapshotted and restored together.
	/// </para>
	/// <para>
	/// The features listed by this API are a combination of built-in features and features defined by plugins.
	/// In order for a feature state to be listed in this API and recognized as a valid feature state by the create snapshot API, the plugin that defines that feature must be installed on the master node.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/get-features-api.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetFeaturesResponse> GetFeaturesAsync(Action<GetFeaturesRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetFeaturesRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetFeaturesRequestDescriptor, GetFeaturesResponse, GetFeaturesRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Reset the features.
	/// Clear all of the state information stored in system indices by Elasticsearch features, including the security and machine learning indices.
	/// </para>
	/// <para>
	/// WARNING: Intended for development and testing use only. Do not reset features on a production cluster.
	/// </para>
	/// <para>
	/// Return a cluster to the same state as a new installation by resetting the feature state for all Elasticsearch features.
	/// This deletes all state information stored in system indices.
	/// </para>
	/// <para>
	/// The response code is HTTP 200 if the state is successfully reset for all features.
	/// It is HTTP 500 if the reset operation failed for any feature.
	/// </para>
	/// <para>
	/// Note that select features might provide a way to reset particular system indices.
	/// Using this API resets all features, both those that are built-in and implemented as plugins.
	/// </para>
	/// <para>
	/// To list the features that will be affected, use the get features API.
	/// </para>
	/// <para>
	/// IMPORTANT: The features installed on the node you submit this request to are the features that will be reset. Run on the master node if you have any doubts about which plugins are installed on individual nodes.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual ResetFeaturesResponse ResetFeatures(ResetFeaturesRequest request)
	{
		request.BeforeRequest();
		return DoRequest<ResetFeaturesRequest, ResetFeaturesResponse, ResetFeaturesRequestParameters>(request);
	}

	/// <summary>
	/// <para>
	/// Reset the features.
	/// Clear all of the state information stored in system indices by Elasticsearch features, including the security and machine learning indices.
	/// </para>
	/// <para>
	/// WARNING: Intended for development and testing use only. Do not reset features on a production cluster.
	/// </para>
	/// <para>
	/// Return a cluster to the same state as a new installation by resetting the feature state for all Elasticsearch features.
	/// This deletes all state information stored in system indices.
	/// </para>
	/// <para>
	/// The response code is HTTP 200 if the state is successfully reset for all features.
	/// It is HTTP 500 if the reset operation failed for any feature.
	/// </para>
	/// <para>
	/// Note that select features might provide a way to reset particular system indices.
	/// Using this API resets all features, both those that are built-in and implemented as plugins.
	/// </para>
	/// <para>
	/// To list the features that will be affected, use the get features API.
	/// </para>
	/// <para>
	/// IMPORTANT: The features installed on the node you submit this request to are the features that will be reset. Run on the master node if you have any doubts about which plugins are installed on individual nodes.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ResetFeaturesResponse> ResetFeaturesAsync(ResetFeaturesRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<ResetFeaturesRequest, ResetFeaturesResponse, ResetFeaturesRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Reset the features.
	/// Clear all of the state information stored in system indices by Elasticsearch features, including the security and machine learning indices.
	/// </para>
	/// <para>
	/// WARNING: Intended for development and testing use only. Do not reset features on a production cluster.
	/// </para>
	/// <para>
	/// Return a cluster to the same state as a new installation by resetting the feature state for all Elasticsearch features.
	/// This deletes all state information stored in system indices.
	/// </para>
	/// <para>
	/// The response code is HTTP 200 if the state is successfully reset for all features.
	/// It is HTTP 500 if the reset operation failed for any feature.
	/// </para>
	/// <para>
	/// Note that select features might provide a way to reset particular system indices.
	/// Using this API resets all features, both those that are built-in and implemented as plugins.
	/// </para>
	/// <para>
	/// To list the features that will be affected, use the get features API.
	/// </para>
	/// <para>
	/// IMPORTANT: The features installed on the node you submit this request to are the features that will be reset. Run on the master node if you have any doubts about which plugins are installed on individual nodes.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual ResetFeaturesResponse ResetFeatures(ResetFeaturesRequestDescriptor descriptor)
	{
		descriptor.BeforeRequest();
		return DoRequest<ResetFeaturesRequestDescriptor, ResetFeaturesResponse, ResetFeaturesRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>
	/// Reset the features.
	/// Clear all of the state information stored in system indices by Elasticsearch features, including the security and machine learning indices.
	/// </para>
	/// <para>
	/// WARNING: Intended for development and testing use only. Do not reset features on a production cluster.
	/// </para>
	/// <para>
	/// Return a cluster to the same state as a new installation by resetting the feature state for all Elasticsearch features.
	/// This deletes all state information stored in system indices.
	/// </para>
	/// <para>
	/// The response code is HTTP 200 if the state is successfully reset for all features.
	/// It is HTTP 500 if the reset operation failed for any feature.
	/// </para>
	/// <para>
	/// Note that select features might provide a way to reset particular system indices.
	/// Using this API resets all features, both those that are built-in and implemented as plugins.
	/// </para>
	/// <para>
	/// To list the features that will be affected, use the get features API.
	/// </para>
	/// <para>
	/// IMPORTANT: The features installed on the node you submit this request to are the features that will be reset. Run on the master node if you have any doubts about which plugins are installed on individual nodes.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual ResetFeaturesResponse ResetFeatures()
	{
		var descriptor = new ResetFeaturesRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequest<ResetFeaturesRequestDescriptor, ResetFeaturesResponse, ResetFeaturesRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>
	/// Reset the features.
	/// Clear all of the state information stored in system indices by Elasticsearch features, including the security and machine learning indices.
	/// </para>
	/// <para>
	/// WARNING: Intended for development and testing use only. Do not reset features on a production cluster.
	/// </para>
	/// <para>
	/// Return a cluster to the same state as a new installation by resetting the feature state for all Elasticsearch features.
	/// This deletes all state information stored in system indices.
	/// </para>
	/// <para>
	/// The response code is HTTP 200 if the state is successfully reset for all features.
	/// It is HTTP 500 if the reset operation failed for any feature.
	/// </para>
	/// <para>
	/// Note that select features might provide a way to reset particular system indices.
	/// Using this API resets all features, both those that are built-in and implemented as plugins.
	/// </para>
	/// <para>
	/// To list the features that will be affected, use the get features API.
	/// </para>
	/// <para>
	/// IMPORTANT: The features installed on the node you submit this request to are the features that will be reset. Run on the master node if you have any doubts about which plugins are installed on individual nodes.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	[Obsolete("Synchronous methods are deprecated and could be removed in the future.")]
	public virtual ResetFeaturesResponse ResetFeatures(Action<ResetFeaturesRequestDescriptor> configureRequest)
	{
		var descriptor = new ResetFeaturesRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<ResetFeaturesRequestDescriptor, ResetFeaturesResponse, ResetFeaturesRequestParameters>(descriptor);
	}

	/// <summary>
	/// <para>
	/// Reset the features.
	/// Clear all of the state information stored in system indices by Elasticsearch features, including the security and machine learning indices.
	/// </para>
	/// <para>
	/// WARNING: Intended for development and testing use only. Do not reset features on a production cluster.
	/// </para>
	/// <para>
	/// Return a cluster to the same state as a new installation by resetting the feature state for all Elasticsearch features.
	/// This deletes all state information stored in system indices.
	/// </para>
	/// <para>
	/// The response code is HTTP 200 if the state is successfully reset for all features.
	/// It is HTTP 500 if the reset operation failed for any feature.
	/// </para>
	/// <para>
	/// Note that select features might provide a way to reset particular system indices.
	/// Using this API resets all features, both those that are built-in and implemented as plugins.
	/// </para>
	/// <para>
	/// To list the features that will be affected, use the get features API.
	/// </para>
	/// <para>
	/// IMPORTANT: The features installed on the node you submit this request to are the features that will be reset. Run on the master node if you have any doubts about which plugins are installed on individual nodes.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ResetFeaturesResponse> ResetFeaturesAsync(ResetFeaturesRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<ResetFeaturesRequestDescriptor, ResetFeaturesResponse, ResetFeaturesRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Reset the features.
	/// Clear all of the state information stored in system indices by Elasticsearch features, including the security and machine learning indices.
	/// </para>
	/// <para>
	/// WARNING: Intended for development and testing use only. Do not reset features on a production cluster.
	/// </para>
	/// <para>
	/// Return a cluster to the same state as a new installation by resetting the feature state for all Elasticsearch features.
	/// This deletes all state information stored in system indices.
	/// </para>
	/// <para>
	/// The response code is HTTP 200 if the state is successfully reset for all features.
	/// It is HTTP 500 if the reset operation failed for any feature.
	/// </para>
	/// <para>
	/// Note that select features might provide a way to reset particular system indices.
	/// Using this API resets all features, both those that are built-in and implemented as plugins.
	/// </para>
	/// <para>
	/// To list the features that will be affected, use the get features API.
	/// </para>
	/// <para>
	/// IMPORTANT: The features installed on the node you submit this request to are the features that will be reset. Run on the master node if you have any doubts about which plugins are installed on individual nodes.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ResetFeaturesResponse> ResetFeaturesAsync(CancellationToken cancellationToken = default)
	{
		var descriptor = new ResetFeaturesRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequestAsync<ResetFeaturesRequestDescriptor, ResetFeaturesResponse, ResetFeaturesRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Reset the features.
	/// Clear all of the state information stored in system indices by Elasticsearch features, including the security and machine learning indices.
	/// </para>
	/// <para>
	/// WARNING: Intended for development and testing use only. Do not reset features on a production cluster.
	/// </para>
	/// <para>
	/// Return a cluster to the same state as a new installation by resetting the feature state for all Elasticsearch features.
	/// This deletes all state information stored in system indices.
	/// </para>
	/// <para>
	/// The response code is HTTP 200 if the state is successfully reset for all features.
	/// It is HTTP 500 if the reset operation failed for any feature.
	/// </para>
	/// <para>
	/// Note that select features might provide a way to reset particular system indices.
	/// Using this API resets all features, both those that are built-in and implemented as plugins.
	/// </para>
	/// <para>
	/// To list the features that will be affected, use the get features API.
	/// </para>
	/// <para>
	/// IMPORTANT: The features installed on the node you submit this request to are the features that will be reset. Run on the master node if you have any doubts about which plugins are installed on individual nodes.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/master/modules-snapshots.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<ResetFeaturesResponse> ResetFeaturesAsync(Action<ResetFeaturesRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new ResetFeaturesRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<ResetFeaturesRequestDescriptor, ResetFeaturesResponse, ResetFeaturesRequestParameters>(descriptor, cancellationToken);
	}
}