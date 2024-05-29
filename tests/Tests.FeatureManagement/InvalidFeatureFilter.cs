﻿// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT license.

/* Unmerged change from project 'Tests.FeatureManagement(net6.0)'
Before:
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
After:
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT license.
*/

/* Unmerged change from project 'Tests.FeatureManagement(net7.0)'
Before:
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
After:
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT license.
*/

/* Unmerged change from project 'Tests.FeatureManagement(net8.0)'
Before:
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
After:
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT license.
*/
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT license.

using System.Threading.Tasks;
using Microsoft.FeatureManagement;

namespace Tests.FeatureManagement
{
    //
    // Cannot implement more than one IFeatureFilter interface
    class InvalidFeatureFilter : IContextualFeatureFilter<IAccountContext>, IContextualFeatureFilter<object>
    {
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context, IAccountContext accountContext)
        {
            return Task.FromResult(false);
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext featureFilterContext, object appContext)
        {
            return Task.FromResult(false);
        }
    }

    //
    // Cannot implement more than one IFeatureFilter interface
    class InvalidFeatureFilter2 : IFeatureFilter, IContextualFeatureFilter<object>
    {
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext featureFilterContext, object appContext)
        {
            return Task.FromResult(false);
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            return Task.FromResult(false);
        }
    }
}
