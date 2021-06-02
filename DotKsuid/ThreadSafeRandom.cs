using System;
using System.Threading;

namespace DotKsuid
{
    public static class ThreadSafeRandom
    {
        private static readonly System.Random GlobalRandom = new Random();
        private static readonly ThreadLocal<Random> LocalRandom = new ThreadLocal<Random>(() =>
        {
            lock (GlobalRandom)
            {
                return new Random(GlobalRandom.Next());
            }
        });

        public static void NextBytes(byte[] array)
        {
            LocalRandom.Value.NextBytes(array);
        }
    }
}
