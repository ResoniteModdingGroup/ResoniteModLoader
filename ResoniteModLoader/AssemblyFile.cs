using System;
using System.Reflection;

namespace ResoniteModLoader
{
	internal class AssemblyFile
	{
		private string? sha256;
		internal Assembly Assembly { get; set; }
		internal string File { get; }

		internal string Sha256
		{
			get
			{
				if (sha256 == null)
				{
					try
					{
						sha256 = Util.GenerateSHA256(File);
					}
					catch (Exception e)
					{
						Logger.ErrorInternal($"Exception calculating sha256 hash for {File}:\n{e}");
						sha256 = "failed to generate hash";
					}
				}
				return sha256;
			}
		}

		internal AssemblyFile(string file, Assembly assembly)
		{
			File = file;
			Assembly = assembly;
		}
	}
}
