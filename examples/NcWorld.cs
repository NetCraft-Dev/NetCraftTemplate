// NcWorld: block and world state.
//
// Members without a level argument act on the overworld. Overloads that take
// a level work for the nether and the end as well, see NcServer.GetLevel.
//
// Replace the MyMod namespace with the one your mod uses.
using NetCraft.Logging;
using NetCraft.ModApi.Wrapper;
using NetCraft.Primitives;
using NetCraft.Registry.State;

namespace MyMod;

public static class NcWorldExample
{
    // Read one block. A null result means the chunk is not loaded right now.
    public static void Inspect(int x, int y, int z)
    {
        BlockState? state = NcWorld.GetBlock(x, y, z);
        if (state is null)
        {
            Log.Info("chunk is not loaded");
            return;
        }

        Log.Info($"block at {x} {y} {z} is {state.Value.Owner.Id}");
    }

    // Write one block. The call runs the full update chain and syncs clients,
    // the return value tells whether anything actually changed.
    public static void Replace(int x, int y, int z, string blockId)
    {
        var state = NcWorld.FindState(blockId);
        if (state is null)
            return;

        var changed = NcWorld.SetBlock(new BlockPos(x, y, z), state);
        Log.Info($"set {blockId}: {changed}");
    }

    // Break a block the way a player would, drops and callbacks included.
    // Pass a player to get the player specific callbacks, null for environment.
    public static void Break(int x, int y, int z, NcPlayer? player = null)
    {
        NcWorld.BreakBlock(new BlockPos(x, y, z), player);
    }

    // Time and weather are read and written through the world.
    public static void SetNoon()
    {
        NcWorld.DayTime = 6000;
    }

    public static void DryOut()
    {
        NcWorld.RainLevel = 0f;
        NcWorld.ThunderLevel = 0f;
    }
}
