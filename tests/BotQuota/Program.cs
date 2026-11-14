using System.Text.Json;
using HanZombiePlagueS2;
static void Check(bool value, string label) { if (!value) throw new Exception(label); Console.WriteLine("PASS " + label); }
Check(new HZPMainCFG().BotQuota == 10, "default 10");
foreach (var (input, expected) in new[] { (0,0), (5,5), (10,10), (20,20), (64,64), (-1,0), (65,64), (int.MinValue,0), (int.MaxValue,64) }) {
 var cfg = JsonSerializer.Deserialize<HZPMainCFG>("{\"BotQuota\":" + input + "}");
 Check(cfg!.BotQuota == expected, $"deserialize {input} -> {expected}");
}
Check(JsonSerializer.Deserialize<HZPMainCFG>("{}")!.BotQuota == 10, "old config defaults 10");
var source = File.ReadAllText("src/HanZombiePlagueS2/HZP.Commands.cs");
Check(source.Contains("ExecuteCommand(\"bot_quota_mode fill\")"), "fill retained");
Check(source.Contains("ExecuteCommand($\"bot_quota {_mainCFG.CurrentValue.BotQuota}\")"), "map-load uses configured quota");
Check(!source.Contains("bot_quota 20"), "hardcoded 20 removed");
