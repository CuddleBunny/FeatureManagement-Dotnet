﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.FeatureManagement
{
    /// <summary>
    /// Provides a snapshot of feature state to ensure consistency across a given request.
    /// </summary>
    class FeatureManagerSnapshot : IFeatureManagerSnapshot
    {
        private readonly IFeatureManager _featureManager;
        private readonly ConcurrentDictionary<string, Task<bool>> _flagCache = new ConcurrentDictionary<string, Task<bool>>();
        private IEnumerable<string> _featureNames;

        public FeatureManagerSnapshot(IFeatureManager featureManager)
        {
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
        }

        public async IAsyncEnumerable<string> GetFeatureNamesAsync([EnumeratorCancellation]CancellationToken cancellationToken)
        {
            if (_featureNames == null)
            {
                var featureNames = new List<string>();

                await foreach (string featureName in _featureManager.GetFeatureNamesAsync(cancellationToken).ConfigureAwait(false))
                {
                    featureNames.Add(featureName);
                }

                _featureNames = featureNames;
            }

            foreach (string featureName in _featureNames)
            {
                yield return featureName;
            }
        }

        public async Task<bool> IsEnabledAsync(string feature, CancellationToken cancellationToken)
        {
            return _flagCache.GetOrAdd(
                feature,
                (key) => _featureManager.IsEnabledAsync(key));
        }

        public Task<bool> IsEnabledAsync<TContext>(string feature, TContext context)
        {
            return _flagCache.GetOrAdd(
                feature,
                (key) => _featureManager.IsEnabledAsync(key, context));
        }
    }
}
