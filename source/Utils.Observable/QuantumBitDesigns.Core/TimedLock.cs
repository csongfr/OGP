using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Utils.Observable
{
    /// <summary>
    /// Taken From: http://www.interact-sw.co.uk/iangblog/2004/03/23/locking
    /// 
    /// Thanks to Eric Gunnerson for recommending this be a struct rather
    /// than a class - avoids a heap allocation.
    /// (In Debug mode, we make it a class so that we can add a finalizer
    /// in order to detect when the object is not freed.)
    /// Thanks to Chance Gillespie and Jocelyn Coulmance for pointing out
    /// the bugs that then crept in when I changed it to use struct...
    ///
    /// VRU : Récupéré sur http://blog.quantumbitdesigns.com/2008/07/22/wpf-cross-thread-collection-binding-part-1/
    /// </summary>
#if DEBUG
    public class TimedLock : IDisposable
#else
    public struct TimedLock : IDisposable
#endif
    {
        /// <summary>
        /// Lock.
        /// </summary>
        /// <param name="o">o</param>
        /// <returns>Lock</returns>
        public static TimedLock Lock(object o)
        {
            return Lock(o, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Lock.
        /// </summary>
        /// <param name="o">o</param>
        /// <param name="timeout">Timeout.</param>
        /// <returns>Lock</returns>
        public static TimedLock Lock(object o, TimeSpan timeout)
        {
            TimedLock tl = new TimedLock(o);
            if (!Monitor.TryEnter(o, timeout))
            {
#if DEBUG
                System.GC.SuppressFinalize(tl);
#endif
                throw new LockTimeoutException();
            }

            return tl;
        }

        /// <summary>
        /// Constructeur privé.
        /// </summary>
        /// <param name="o">o</param>
        private TimedLock(object o)
        {
            target = o;
        }

        /// <summary>
        /// Target.
        /// </summary>
        private object target;

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Monitor.Exit(target);

            // It's a bad error if someone forgets to call Dispose,
            // so in Debug builds, we put a finalizer in to detect
            // the error. If Dispose is called, we suppress the
            // finalizer.
#if DEBUG
            GC.SuppressFinalize(this);
#endif
        }

#if DEBUG
        /// <summary>
        /// Destructeur.
        /// </summary>
        ~TimedLock()
        {
            // If this finalizer runs, someone somewhere failed to
            // call Dispose, which means we've failed to leave
            // a monitor!
            System.Diagnostics.Debug.Fail("Undisposed lock");
        }
#endif
    }
}
