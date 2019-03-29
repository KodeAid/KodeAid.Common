﻿// Copyright © Kris Penner. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.



namespace KodeAid.FaultTolerance
{
    public enum RetryStatus
    {
        Succeeded,
        RetryDisabled,
        NonRetryable,
        Retry,
        RetryExhausted
    }
}