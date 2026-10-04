// NcLists: white list, operators, bans and server.properties.
//
// These are the kernel's live list objects, not copies. The SetXxx helpers
// only touch memory; nothing reaches the file until SaveSettings.
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
using NetCraft.Logging;
using NetCraft.ModApi.Wrapper;

namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{
    // Change the writable server.properties values, then persist them.
    public static void Harden()
    {
        NcLists.SetDifficulty("hard");
        NcLists.SetWhiteList(true);
        NcLists.SetPlayerIdleTimeout(30);
        NcLists.SaveSettings();          //only now does it reach the file
        Log.Info("server.properties updated");
    }

    // The list objects themselves are reachable when you need to read them.
    public static void ShowSettings()
    {
        Log.Info($"game mode {NcLists.Settings.GameMode}, difficulty {NcLists.Settings.Difficulty}");
        Log.Info($"white list has {NcLists.WhiteList.Count} entries");
    }
}
