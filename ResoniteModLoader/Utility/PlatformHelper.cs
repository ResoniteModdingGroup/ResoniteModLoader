using System.IO;

namespace ResoniteModLoader.Utility
{
    // Provides helper functions for platform-specific operations.
    // Used for cases such as file handling which can vary between platforms.
    internal class PlatformHelper
    {
        public static readonly string AndroidResonitePath = "/sdcard/ModData/com.Solirax.Resonite";

        public static string MainDirectory => UseFallbackPath() ? AndroidResonitePath : Directory.GetCurrentDirectory();

        public static bool IsPathEmbedded(string path) => path.StartsWith("/data/app/com.Solirax.Resonite");

        // Android does not support Directory.GetCurrentDirectory(), so will fall back to the root '/' directory.
        public static bool UseFallbackPath() => Directory.GetCurrentDirectory().Replace('\\', '/') == "/" && !Directory.Exists("/Resonite_Data");
    }
}