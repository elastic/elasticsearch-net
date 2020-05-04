// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;

namespace Nest
{
	public class ScrollAllObserver<T> : CoordinatedRequestObserverBase<IScrollAllResponse<T>> where T : class
	{
		public ScrollAllObserver(
			Action<IScrollAllResponse<T>> onNext = null,
			Action<Exception> onError = null,
			Action onCompleted = null
		)
			: base(onNext, onError, onCompleted) { }
	}
}
