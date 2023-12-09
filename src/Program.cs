var serviceProvider = new ServiceCollection()
    .AddSingleton<ExcelReader>()
    .AddSingleton<JsonWriter>()
    .BuildServiceProvider();

var excelReader = serviceProvider.GetRequiredService<ExcelReader>();
if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "ArkShopItems.xlsx")))
{
    Console.WriteLine("ArkShopItems.xlsx could not be found");
}

var dinoEntries = excelReader.GetWorksheetData<DinoEntry>(Path.Combine(Environment.CurrentDirectory, "ArkShopItems.xlsx"), "Dinos");
var itemEntries = excelReader.GetWorksheetData<ItemEntry>(Path.Combine(Environment.CurrentDirectory, "ArkShopItems.xlsx"), "Items");

var jsonWriter = serviceProvider.GetRequiredService<JsonWriter>();
jsonWriter.WriteDinoJson(dinoEntries);
jsonWriter.WriteItemJson(itemEntries);

jsonWriter.WriteCombineFile();