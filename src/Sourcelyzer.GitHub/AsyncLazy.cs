using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Sourcelyzer.GitHub
{
    /// <summary>
    ///     Provides support for asynchronous lazy initialization. This type is fully thread safe.
    ///     <remarks>See https://blogs.msdn.microsoft.com/pfxteam/2011/01/15/asynclazyt/</remarks>
    /// </summary>
    /// <typeparam name="T">The type of object that is being asynchronously initialized.</typeparam>
    public sealed class AsyncLazy<T> : Lazy<Task<T>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncLazy{T}" /> class.
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked on a background thread to produce the value when it is needed.</param>
        public AsyncLazy(Func<T> valueFactory) :
            base(() => Task.Factory.StartNew(valueFactory))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncLazy{T}" /> class.
        /// </summary>
        /// <param name="taskFactory">
        ///     The asynchronous delegate that is invoked on a background thread to produce the value when it is
        ///     needed.
        /// </param>
        public AsyncLazy(Func<Task<T>> taskFactory) :
            base(() => Task.Factory.StartNew(() => taskFactory()).Unwrap())
        {
        }

        /// <summary>
        ///     Asynchronous infrastructure support. This method permits instances of <see cref="AsyncLazy{T}" /> to be
        ///     awaited.
        /// </summary>
        public TaskAwaiter<T> GetAwaiter()
        {
            return Value.GetAwaiter();
        }
    }
}
