# Configurable bot fill target

This fork retains the upstream GPL-3.0 license. `HZPMainCFG.BotQuota` defaults to 10 and is bounded to 0..64 (0 disables filling). Existing configs without the key also default to 10. The plugin applies this on map load alongside `bot_quota_mode fill`; restart/map change is required for this setting to be reapplied. The target is humans + bots, not 10 additional bots. Engine slot limits still apply. Upstream `bot_join_after_player 1` is unchanged, so an empty server may wait for a human.

Build: `dotnet build src/HanZombiePlagueS2/HanZombiePlagueS2.csproj -c Release`
Tests from repo root: `dotnet run --project tests/BotQuota -c Release`

No live gameplay or deployment is implied by compilation. Verify human join/leave, spectator handling, full-server admission, map changes and mode-specific bot behavior on an authorized isolated instance.

Security prerequisite: upstream `AdminMenuPermission` defaults to an empty string and `HasAdminMenuPermission` treats empty/whitespace as unrestricted for valid human Steam IDs. This fork leaves that behavior unchanged. Configure an explicit administrator permission before public use and test ordinary-player denial and administrator access. This is a source finding, not evidence that any live server is exposed.

The upstream prebuilt `net10.0` binaries are unchanged and do NOT contain this fork change. Build from source to use it.

Local validation: .NET SDK 10.0.115; resolved SwiftlyS2.CS2 1.5.2 for the plugin, API project retains upstream beta.27 reference. Release build succeeded with 0 errors and 53 upstream/dependency warnings (including deprecated classic menus and NU1902 for transitive MySqlConnector 2.4.0). The 14 configuration/source assertions passed. These warnings must be reviewed before deployment; this quota-only patch does not claim to resolve them.

## Transitive MySqlConnector remediation

The API project alone restored SwiftlyS2.CS2 1.1.5-beta.27 -> MySqlConnector 2.4.0. The plugin graph already resolved SwiftlyS2.CS2 1.5.2 -> MySqlConnector 2.6.2. An explicit API-project MySqlConnector 2.6.2 reference removes the vulnerable standalone API restore without changing either Swiftly reference or suppressing NuGet audit. NuGet's direct dependency override meets the upstream >=2.4.0 constraint. There are no direct MySqlConnector calls in these plugin/API sources.

Official advisory: https://github.com/mysql-net/MySqlConnector/security/advisories/GHSA-473q-m89c-ghf8
Patched version starts at 2.6.1: https://github.com/mysql-net/MySqlConnector/releases/tag/2.6.1

This fixes the source build dependency graph only. It does not update the actual server host's shared assemblies or certify its TLS configuration. Do not copy replacement database DLLs over a running Swiftly host. Deployment still requires a compatible host/provider and runtime verification in isolation. Existing tracked upstream binaries remain unchanged.
`nRemediation validation: Release Rebuild exit 0, 0 errors / 51 remaining warnings; NU1902 eliminated. Both API and plugin `dotnet list package --vulnerable --include-transitive` reported no vulnerable packages in current configured feeds; all 14 bot configuration/source assertions passed. No database connection or live host test was performed.
