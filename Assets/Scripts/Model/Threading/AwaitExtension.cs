using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public static class AwaitExtensions
    {
        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        {
            return Task.Delay(timeSpan).GetAwaiter();
        }
    }

