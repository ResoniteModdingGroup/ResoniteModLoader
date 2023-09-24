using System;
using System.Reflection;
using System.Runtime.Versioning;

namespace ResoniteModLoader
{
    internal class DebugInfo
    {
        internal static void Log()
        {
            Logger.MsgInternal($"ResoniteModLoader v{ModLoader.VERSION} starting up!{(ModLoaderConfiguration.Get().Debug ? " Debug logs will be shown." : "")}");
            Logger.MsgInternal($"CLR v{Environment.Version}");
            Logger.DebugFuncInternal(() => $"Using .NET Framework: \"{AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName}\"");
            Logger.DebugFuncInternal(() => $"Using .NET Core: \"{Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName}\"");
            Logger.MsgInternal($"Using Harmony v{GetAssemblyVersion(typeof(HarmonyLib.Harmony))}");
            Logger.MsgInternal($"Using BaseX v{GetAssemblyVersion(typeof(Elements.Core.colorX))}");
            Logger.MsgInternal($"Using FrooxEngine v{GetAssemblyVersion(typeof(FrooxEngine.IComponent))}");
            Logger.MsgInternal($"Using Json.NET v{GetAssemblyVersion(typeof(Newtonsoft.Json.JsonSerializer))}");
        }

        private static string? GetAssemblyVersion(Type typeFromAssembly)
        {
            return typeFromAssembly.Assembly.GetName()?.Version?.ToString();
        }
    }
}