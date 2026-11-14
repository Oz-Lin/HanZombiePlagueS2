# Configurable bot fill target

This fork retains the upstream GPL-3.0 license. `HZPMainCFG.BotQuota` defaults to 10 and is bounded to 0..64 (0 disables filling). Existing configs without the key also default to 10. The plugin applies this on map load alongside `bot_quota_mode fill`; restart/map change is required for this setting to be reapplied. The target is humans + bots, not 10 additional bots. Engine slot limits still apply. Upstream `bot_join_after_player 1` is unchanged, so an empty server may wait for a human.

Build: `dotnet build src/HanZombiePlagueS2/HanZombiePlagueS2.csproj -c Release`
Tests from repo root: `dotnet run --project tests/BotQuota -c Release`

No live gameplay or deployment is implied by compilation. Verify human join/leave, spectator handling, full-server admission, map changes and mode-specific bot behavior on an authorized isolated instance.

Security prerequisite: upstream `AdminMenuPermission` defaults to an empty string and `HasAdminMenuPermission` treats empty/whitespace as unrestricted for valid human Steam IDs. This fork leaves that behavior unchanged. Configure an explicit administrator permission before public use and test ordinary-player denial and administrator access. This is a source finding, not evidence that any live server is exposed.

The upstream prebuilt `net10.0` binaries are unchanged and do NOT contain this fork change. Build from source to use it.

Local validation: .NET SDK 10.0.115; resolved SwiftlyS2.CS2 1.5.2 for the plugin, API project retains upstream beta.27 reference. Release build succeeded with 0 errors and 53 upstream/dependency warnings (including deprecated classic menus and NU1902 for transitive MySqlConnector 2.4.0). The 14 configuration/source assertions passed. These warnings must be reviewed before deployment; this quota-only patch does not claim to resolve them.
