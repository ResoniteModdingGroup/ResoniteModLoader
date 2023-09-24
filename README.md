# ResoniteModLoader

A mod loader for [Resonite](https://resonite.com/). Consider joining our community on [Discord][Resonite Modding Discord] for support, updates, and more.

## Installation

If you are using the Steam version of Resonite you are in the right place. If you are using the standalone version, read the [Resonite Standalone Setup](doc/Resonite_standalone_setup.md) instructions. If you are on Linux, read the [Linux Notes](doc/linux.md).

1. Download [ResoniteModLoader.dll](https://github.com/Resonite-modding-group/ResoniteModLoader/releases/latest/download/ResoniteModLoader.dll) to Resonite's `Libraries` folder (`C:\Program Files (x86)\Steam\steamapps\common\Resonite\Libraries`).
2. Place [0Harmony.dll](https://github.com/Resonite-modding-group/ResoniteModLoader/releases/latest/download/0Harmony.dll) into a `nml_libs` folder under your Resonite install directory (`C:\Program Files (x86)\Steam\steamapps\common\Resonite\nml_libs`). You will need to create this folder.
3. Add mod DLL files to a `nml_mods` folder under your Resonite install directory (`C:\Program Files (x86)\Steam\steamapps\common\Resonite\nml_mods`). You can create the folder if it's missing, or simply launch Resonite once with ResoniteModLoader installed and it will be created automatically.
4. Add the following to Resonite's [launch options](https://wiki.Resonite.com/Command_Line_Arguments): `-LoadAssembly Libraries\ResoniteModLoader.dll`, substituting the path for wherever you put `ResoniteModLoader.dll`.
5. Start the game. If you want to verify that ResoniteModLoader is working you can check the Resonite logs. (`C:\Program Files (x86)\Steam\steamapps\common\Resonite\Logs`). The modloader adds some very obvious logs on startup, and if they're missing something has gone wrong. Here is an [example log file](doc/example_log.log) where everything worked correctly.

If ResoniteModLoader isn't working after following those steps, take a look at our [troubleshooting page](doc/troubleshooting.md).

### Example Directory Structure

Your Resonite directory should now look similar to the following. Files not related to modding are not shown.

```
<Resonite Install Directory>
│   Resonite.exe
│   ResoniteLauncher.exe
│
├───Logs
│       <Log files will generate here>
│
├───nml_mods
│       InspectorScroll.dll
│       MotionBlurDisable.dll
│       ResoniteContactsSort.dll
|       <More mods go here>
├───nml_libs
│       0Harmony.dll
|       <More libs go here>
│
└───Libraries
        ResoniteModLoader.dll
```

Note that the libraries can also be in the root of the Resonite install directory if you prefer, but the loading of those happens outside of NML itself.

## Finding Mods

A list of known mods is available in the [Resonite Mod List](https://www.Resonitemodloader.com/mods). New mods and updates are also announced in [our Discord][Resonite Modding Discord].

## Frequently Asked Questions

Many questions about what NML is and how it works are answered on our [frequently asked questions page](doc/faq.md).

## Making a Mod

Check out the [Mod Creation Guide](doc/making_mods.md).

## Configuration

ResoniteModLoader aims to have a reasonable default configuration, but certain things can be adjusted via an [optional config file](doc/modloader_config.md).

## Contributing

Issues and PRs are welcome. Please read our [Contributing Guidelines](.github/CONTRIBUTING.md)!

## Licensing and Credits

ResoniteModLoader is licensed under the GNU Lesser General Public License (LGPL). See [LICENSE.txt](LICENSE.txt) for the full license.

Third-party libraries distributed alongside ResoniteModLoader:

- [LibHarmony] ([MIT License](https://github.com/pardeike/Harmony/blob/v2.2.1.0/LICENSE))

Third-party libraries used in source:

- [.NET](https://github.com/dotnet) (Various licenses)
- [Resonite](https://Resonite.com/) ([EULA](https://store.steampowered.com/eula/740250_eula_0))
- [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) ([MIT License](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md))

<!--- Link References -->
[LibHarmony]: https://github.com/pardeike/Harmony
[Resonite Modding Discord]: https://discord.gg/vCDJK9xyvm
