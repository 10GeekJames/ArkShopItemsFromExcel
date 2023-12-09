namespace ArkShopItemsFromExcel.Services;

public class JsonWriter
{
    public void WriteDinoJson(IEnumerable<DinoEntry> items)
    {
        var OutputFile = "DinoResults.json";
        if (File.Exists(OutputFile))
        {
            File.Delete(OutputFile);
        }
        using (var file = File.CreateText(OutputFile))
        {
            foreach (var item in items.Where(rs => !rs.Suppress))
            {
                var smashed = @$" 
        ""{item.ShopName}"": {{
            ""Title"": ""{item.ShopName}"",
            ""Description"": ""{item.Description}"",
            ""Categories"": [ ""{item.Category}"" ],
            ""Price"": {item.Price},
            ""Exclude_From_Discount"": false,
            ""Limits"": {{
                ""Max_Purchase_Same_Time_Amount"": 1,
                ""Max_Purchases/Uses"": 0,
                ""Prevent_Usage_After_Wipe_For_Minutes"": 0,
                ""Cooldown_Days"": 0,
                ""Cooldown_Hours"": 0,
                ""Cooldown_Minutes"": 0,
                ""Cooldown_Seconds"": 10
            }},
            ""Items"": [],
            ""Dinos"": [
                {{
                    ""Level"": {item.Level},
                    ""Max_Level"": {item.MaxLevel},
                    ""Neutered"": false,
                    ""Put_In_CryoPod"": true,
                    ""Blueprint"": {item.Blueprint},
                    ""Options"": {{}}
                }}
            ],
            ""ConsoleCommands"": [],
            ""PermissionGroupRequired"": [],
            ""Only_On_These_Maps"": [],
            ""NOT_On_These_Maps"": [] }}";

                if (item.ShopName != items.Where(rs => !rs.Suppress).LastOrDefault().ShopName)
                {
                    smashed += ", ";
                }
                file.Write(smashed);
            }
        }
    }


    public void WriteItemJson(IEnumerable<ItemEntry> items)
    {
        var OutputFile = "ItemResults.json";
        if (File.Exists(OutputFile))
        {
            File.Delete(OutputFile);
        }
        using (var file = File.CreateText(OutputFile))
        {
            foreach (var item in items.Where(rs => !rs.Suppress))
            {
                var smashed = @$" 
        ""{item.ShopName}"": {{
            ""Title"": ""{item.Title}"",
            ""Description"": ""{item.Description}"",
            ""Categories"": [ ""{item.Category}"" ],
            ""Price"": {item.Price},
            ""Exclude_From_Discount"": false,
            ""Limits"": {{
                ""Max_Purchase_Same_Time_Amount"": 1,
                ""Max_Purchases/Uses"": 0,
                ""Prevent_Usage_After_Wipe_For_Minutes"": 0,
                ""Cooldown_Days"": 0,
                ""Cooldown_Hours"": 0,
                ""Cooldown_Minutes"": 0,
                ""Cooldown_Seconds"": 10
            }},
            ""Items"": [
                {{
                    ""Quality"": {item.Quality},
                    ""ForceBlueprint"": false,
                    ""Amount"": {item.Amount},
                    ""Blueprint"": {item.Blueprint}
                }}
            ],
            ""Dinos"": [],
            ""ConsoleCommands"": [],
            ""PermissionGroupRequired"": [],
            ""Only_On_These_Maps"": [],
            ""NOT_On_These_Maps"": [] }}";
                if (item.ShopName != items.Where(rs => !rs.Suppress).LastOrDefault().ShopName)
                {
                    smashed += ", ";
                }
                file.Write(smashed);
            }
        }
    }

    public void WriteCombineFile()
    {
        if (!File.Exists(Path.Combine(Environment.CurrentDirectory,"RootConfig.json")))
        {
            Console.WriteLine("Could not find the RootConfig.json");
            throw new Exception("Could not find the RootConfig.json");
        }

        if (!File.Exists(Path.Combine(Environment.CurrentDirectory,"DinoResults.json")))
        {
            Console.WriteLine("Could not find the DinoResults.json");
            throw new Exception("Could not find the DinoResults.json");
        }

        if (!File.Exists(Path.Combine(Environment.CurrentDirectory,"ItemResults.json")))
        {
            Console.WriteLine("Could not find the ItemResults.json");
            throw new Exception("Could not find the ItemResults.json");
        }

        var rootContents = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"RootConfig.json"));
        var dinoContents = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"DinoResults.json"));
        var itemContents = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"ItemResults.json"));

        using (var finalFile = File.CreateText(Path.Combine(Environment.CurrentDirectory,"config.json")))
        {
            finalFile.Write(rootContents.Replace("|DinoReplace|", dinoContents).Replace("|ItemReplace|", itemContents));
        }
    }
}
