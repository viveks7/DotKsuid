using System;
using System.Threading;

namespace DotKsuid
{
    static class ThreadSafeRandom
    {
        private static readonly Random GlobalRandom = new Random();
        private static readonly ThreadLocal<Random> LocalRandom = new ThreadLocal<Random>(() =>
        {
            lock (GlobalRandom)
            {
                return new Random(GlobalRandom.Next());
            }
        });

        public static void NextBytes(byte[] bytes)
        {
            LocalRandom.Value.NextBytes(bytes);
        }
    }
}
