using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ResoniteModLoader
{
	/// <summary>
	/// Represents a fluent configuration interface to define mod configurations.
	/// </summary>
	public class ModConfigurationDefinitionBuilder
	{
		private readonly HashSet<ModConfigurationKey> Keys = new();
		private readonly ResoniteModBase Owner;
		private bool AutoSaveConfig = true;
		private Version ConfigVersion = new(1, 0, 0);

		internal ModConfigurationDefinitionBuilder(ResoniteModBase owner)
		{
			Owner = owner;
		}

		/// <summary>
		/// Sets the AutoSave property of this configuration definition. Default is <c>true</c>.
		/// </summary>
		/// <param name="autoSave">If <c>false</c>, the config will not be autosaved on Resonite close.</param>
		/// <returns>This builder.</returns>
		public ModConfigurationDefinitionBuilder AutoSave(bool autoSave)
		{
			AutoSaveConfig = autoSave;
			return this;
		}

		/// <summary>
		/// Adds a new key to this configuration definition.
		/// </summary>
		/// <param name="key">A configuration key.</param>
		/// <returns>This builder.</returns>
		public ModConfigurationDefinitionBuilder Key(ModConfigurationKey key)
		{
			Keys.Add(key);
			return this;
		}

		/// <summary>
		/// Sets the semantic version of this configuration definition. Default is 1.0.0.
		/// </summary>
		/// <param name="version">The config's semantic version.</param>
		/// <returns>This builder.</returns>
		public ModConfigurationDefinitionBuilder Version(Version version)
		{
			ConfigVersion = version;
			return this;
		}

		/// <summary>
		/// Sets the semantic version of this configuration definition. Default is 1.0.0.
		/// </summary>
		/// <param name="version">The config's semantic version, as a string.</param>
		/// <returns>This builder.</returns>
		public ModConfigurationDefinitionBuilder Version(string version)
		{
			ConfigVersion = new Version(version);
			return this;
		}

		internal ModConfigurationDefinition? Build()
		{
			if (Keys.Count > 0)
			{
				return new ModConfigurationDefinition(Owner, ConfigVersion, Keys, AutoSaveConfig);
			}
			return null;
		}

		internal void ProcessAttributes()
		{
			var fields = AccessTools.GetDeclaredFields(Owner.GetType());
			fields
				.Where(field => Attribute.GetCustomAttribute(field, typeof(AutoRegisterConfigKeyAttribute)) != null)
				.Do(ProcessField);
		}

		private void ProcessField(FieldInfo field)
		{
			if (!typeof(ModConfigurationKey).IsAssignableFrom(field.FieldType))
			{
				// wrong type
				Logger.WarnInternal($"{Owner.Name} had an [AutoRegisterConfigKey] field of the wrong type: {field}");
				return;
			}

			ModConfigurationKey fieldValue = (ModConfigurationKey)field.GetValue(field.IsStatic ? null : Owner);
			Keys.Add(fieldValue);
		}
	}
}
