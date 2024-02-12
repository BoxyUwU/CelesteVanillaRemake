# VanillaRemake

This repo contains two celeste mods, `Celestish` and `VanillaRemake`. `VanillaRemake` is a non-code mod containing a mostly
accurate recreation of vanilla celeste maps. `Celestish` is a code mod containing a variety of entities and triggers to
emulate behaviour in vanilla maps that does not work correctly when used outside of vanilla.

The `Celestish` mod can be found on [GameBanana](https://gamebanana.com/mods/495367)

## How do I use this

The intended usage is to start with the `VanillaRemake` mod and edit it to suit your use case. The steps required before
you can start editing are below:
- Copy the `VanillaRemake` folder to wherever you want to develop your mod and rename it to your mod name
- Rename graphics and map locations
    - `Graphics/Atlases/Checkpoints/Boxy/VanillaRemake` to `Graphics/Atlases/Checkpoints/YourName/YourModName`
    - `Maps/Boxy/VanillaRemake` to `Maps/YourName/YourModName`
- Open `everest.yaml` in your mod and change the name from `VanillaRemake` to your mod name
- Open `Dialog/English.txt`
    - find replace all instances of `Boxy_VanillaRemake` to `YourName_YourModName`
    - change the first line which reads `YourName_YourModName= Vanilla Remake` to specify your mod name instead of "Vanilla Remake"

## Building `Celestish`

This section is not relevent to you unless you would like to edit the `Celestish` mod.

The c# project for `Celestish` expects there to be a symlink named `CelesteLink` placed in the root of this repo, it should link to
your `Celeste` game directory. Once this is done you should be able to run `dotnet build` in the repo root. If this does not work
first check to make sure that you have installed dotnet as specified in
[Everests documentation for code mods](https://github.com/EverestAPI/Resources/wiki/Code-Mod-Setup#prerequisites) if it still does not
work, open an issue on this repo.