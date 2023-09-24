namespace ResoniteModLoader
{
	internal class LoadedResoniteMod
	{
		internal bool AllowSavingConfiguration = true;

		internal bool FinishedLoading { get => ResoniteMod.FinishedLoading; set => ResoniteMod.FinishedLoading = value; }

		internal AssemblyFile ModAssembly { get; private set; }

		internal ModConfiguration? ModConfiguration { get; set; }

		internal string Name { get => ResoniteMod.Name; }

		internal ResoniteMod ResoniteMod { get; private set; }

		internal LoadedResoniteMod(ResoniteMod ResoniteMod, AssemblyFile modAssembly)
		{
			ResoniteMod = ResoniteMod;
			ModAssembly = modAssembly;
		}
	}
}
