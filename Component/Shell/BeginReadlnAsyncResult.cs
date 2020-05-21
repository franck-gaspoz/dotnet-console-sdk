﻿using System;
using System.Threading;

namespace DotNetConsoleSdk.Component.Shell
{
    public class BeginReadlnAsyncResult : IAsyncResult
    {
        public object AsyncState { get; protected set; }

        public WaitHandle AsyncWaitHandle { get; protected set; }

        public bool CompletedSynchronously { get; protected set; } = false;

        public bool IsCompleted { get; protected set; } = true;

        public BeginReadlnAsyncResult(string line)
        {
            AsyncState = line;
        }
    }
}
