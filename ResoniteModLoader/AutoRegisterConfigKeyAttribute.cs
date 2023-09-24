using System;

namespace ResoniteModLoader
{
	/// <summary>
	/// Marks a field of type <see cref="ModConfigurationKey{T}"/> on a class
	/// deriving from <see cref="ResoniteMod"/> to be automatically included in that mod's configuration.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class AutoRegisterConfigKeyAttribute : Attribute
	{ }
}
