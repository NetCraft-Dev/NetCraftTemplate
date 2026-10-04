// InjectAttribute: replace a kernel call site with your own method.
//
// The annotated class and method are the replacement, so the rule cannot
// misspell them. This route names kernel members directly, which makes it the
// most powerful one and the most fragile one: when the kernel changes shape
// the rule stops matching. Prefer an event whenever ModApi offers one.
//
// Two rules to keep in mind
//   1. The method signature may only use BCL types. Reference parameters and
//      the return value have to be declared as object, value types keep their
//      real type because the stack layout has to match the call site.
//   2. The kernel no longer runs its own code at that call site, so put the
//      original logic back yourself.
//
// Replace the MyMod namespace with the one your mod uses.
using NetCraft.Game.Server;
using NetCraft.ModApi.Extension;
using NetCraft.Network;
using NetCraft.Network.Protocol.Login;

namespace MyMod;

public static class InjectExample
{
    // Runs instead of every PlayerList.PlaceNewPlayer call site.
    // nameof keeps the rule in sync when the kernel method gets renamed.
    [Inject(typeof(PlayerList), nameof(PlayerList.PlaceNewPlayer))]
    public static object OnPlaceNewPlayer(object self, object connection, object profile)
    {
        // Put the original call back first, it returns null when the server
        // is full and the player was rejected.
        var player = ((PlayerList)self).PlaceNewPlayer((Connection)connection, (GameProfile)profile);

        // Then do the extra work.
        return player;
    }

    // Environment narrows the rule to one side. server, client or both,
    // the default is both.
    [Inject(typeof(PlayerList), nameof(PlayerList.PlayerCount), Environment = "server")]
    public static int OnPlayerCount(object self) => ((PlayerList)self).PlayerCount;
}
