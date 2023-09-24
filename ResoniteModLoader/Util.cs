using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ResoniteModLoader
{
    internal static class Util
    {
        // check if a type is allowed to have null assigned
        internal static bool CanBeNull(Type t)
        {
            return !CannotBeNull(t);
        }

        // check if a type cannot possibly have null assigned
        internal static bool CannotBeNull(Type t)
        {
            return t.IsValueType && Nullable.GetUnderlyingType(t) == null;
        }

        /// <summary>
        /// Used to debounce calls to a given method. The given method will be called after there have been no additional calls
        /// for the given number of milliseconds.
        /// <para/>
        /// The <see cref="Action{T}"/> returned by this method has internal state used for debouncing,
        /// so you will need to store and reuse the Action for each call.
        /// </summary>
        /// <typeparam name="T">The type of the debounced method's input.</typeparam>
        /// <param name="func">The method to be debounced.</param>
        /// <param name="milliseconds">How long to wait before a call to the debounced method gets passed through.</param>
        /// <returns>A debouncing wrapper for the given method.</returns>
        // credit: https://stackoverflow.com/questions/28472205/c-sharp-event-debounce
        internal static Action<T> Debounce<T>(this Action<T> func, int milliseconds)
        {
            // this variable gets embedded in the returned Action via the magic of closures
            CancellationTokenSource? cancelTokenSource = null;

            return arg =>
            {
                // if there's already a scheduled call, then cancel it
                cancelTokenSource?.Cancel();
                cancelTokenSource = new CancellationTokenSource();

                // schedule a new call
                Task.Delay(milliseconds, cancelTokenSource.Token)
              .ContinueWith(t =>
              {
                  if (t.IsCompletedSuccessfully())
                  {
                      Task.Run(() => func(arg));
                  }
              }, TaskScheduler.Default);
            };
        }

        /// <summary>
        /// Get the executing mod by stack trace analysis.
        /// You may skip extra frames if you know your callers are guaranteed to be NML code.
        /// </summary>
        /// <param name="stackTrace">A stack trace captured by the callee</param>
        /// <returns>The executing mod, or null if none found</returns>
        internal static ResoniteMod? ExecutingMod(StackTrace stackTrace)
        {
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                Assembly? assembly = stackTrace.GetFrame(i)?.GetMethod()?.DeclaringType?.Assembly;
                if (assembly != null && ModLoader.AssemblyLookupMap.TryGetValue(assembly, out ResoniteMod mod))
                {
                    return mod;
                }
            }
            return null;
        }

        //credit to delta for this method https://github.com/XDelta/
        internal static string GenerateSHA256(string filepath)
        {
            using var hasher = SHA256.Create();
            using var stream = File.OpenRead(filepath);
            var hash = hasher.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        internal static IEnumerable<Type> GetLoadableTypes(this Assembly assembly, Predicate<Type> predicate)
        {
            try
            {
                return assembly.GetTypes().Where(type => CheckType(type, predicate));
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(type => CheckType(type, predicate));
            }
        }

        internal static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }

        // check a potentially unloadable type to see if it is (A) loadable and (B) satsifies a predicate without throwing an exception
        // this does a series of increasingly aggressive checks to see if the type is unsafe to touch
        private static bool CheckType(Type type, Predicate<Type> predicate)
        {
            if (type == null)
            {
                return false;
            }

            try
            {
                string _name = type.Name;
            }
            catch (Exception e)
            {
                Logger.DebugFuncInternal(() => $"Could not read the name for a type: {e}");
                return false;
            }

            try
            {
                return predicate(type);
            }
            catch (Exception e)
            {
                Logger.DebugFuncInternal(() => $"Could not load type \"{type}\": {e}");
                return false;
            }
        }

        // shim because this doesn't exist in .NET 4.6
        private static bool IsCompletedSuccessfully(this Task t)
        {
            return t.IsCompleted && !t.IsFaulted && !t.IsCanceled;
        }
    }
}