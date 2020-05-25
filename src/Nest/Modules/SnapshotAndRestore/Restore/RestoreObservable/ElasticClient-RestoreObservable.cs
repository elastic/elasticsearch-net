// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IObservable<RecoveryStatusResponse> RestoreObservable(Name repository, Name snapshot, TimeSpan interval,
			Func<RestoreDescriptor, IRestoreRequest> selector = null
		);

		/// <inheritdoc />
		IObservable<RecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IObservable<RecoveryStatusResponse> RestoreObservable(Name repository, Name snapshot, TimeSpan interval,
			Func<RestoreDescriptor, IRestoreRequest> selector = null
		)
		{
			var restoreDescriptor = selector.InvokeOrDefault(new RestoreDescriptor(repository, snapshot));
			var observable = new RestoreObservable(this, restoreDescriptor, interval);
			return observable;
		}

		/// <inheritdoc />
		public IObservable<RecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest request) =>
			new RestoreObservable(this, request, interval);
	}
}
