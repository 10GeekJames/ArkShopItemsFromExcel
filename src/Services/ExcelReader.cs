namespace ArkShopItemsFromExcel.Services;

public class ExcelReader
{
    public IEnumerable<T> GetWorksheetData<T>(string fileName, string workSheet) where T : new()
    {
        var file = new FileInfo(fileName);
        OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        using (var excelReader = new OfficeOpenXml.ExcelPackage(file))
        {
            var activeWorkSheet = excelReader.Workbook.Worksheets[workSheet];
            Console.WriteLine($"activeWorkSheetInfo: {activeWorkSheet.Name}");
            return activeWorkSheet.ConvertSheetToObjects<T>();
        }
    }
}
