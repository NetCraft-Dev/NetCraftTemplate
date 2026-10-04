// NcRegistries: look up built in registry entries by id.
//
// The registries are filled in while the game bootstraps, and mods are loaded
// before that finishes. Do not cache what you read during Init, read it again
// from a server event once the world is up.
//
// Replace the MyMod namespace with the one your mod uses.
using NetCraft.Logging;
using NetCraft.ModApi.Wrapper;

namespace MyMod;

public static class NcRegistriesExample
{
    // The Find helpers take a namespaced id and return null when nothing matches.
    public static void Lookup()
    {
        var stone = NcRegistries.FindState("minecraft:stone");
        var diamond = NcRegistries.FindItem("minecraft:diamond");
        var plains = NcRegistries.FindBiome("minecraft:plains");

        if (stone is null || diamond is null || plains is null)
            return;

        // A block state carries the block it belongs to, the block carries the id.
        Log.Info($"stone comes from {stone.Value.Owner.Id}");
    }

    // The tables themselves are for iteration and tag lookups.
    public static void Count()
    {
        Log.Info($"blocks={NcRegistries.Blocks.KeySet.Count} items={NcRegistries.Items.KeySet.Count}");
    }
}
