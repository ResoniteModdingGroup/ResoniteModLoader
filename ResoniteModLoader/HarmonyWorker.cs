using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace ResoniteModLoader
{
	// this class does all the harmony-related NML work.
	// this is needed to avoid importing harmony in ExecutionHook, where it may not be loaded yet.
	internal class HarmonyWorker
	{
		internal static void LoadModsAndHideModAssemblies(HashSet<Assembly> initialAssemblies)
		{
			Harmony harmony = new("com.Resonitemodloader");
			ModLoader.LoadMods(harmony);
			AssemblyHider.PatchResonite(harmony, initialAssemblies);
		}
	}
}
