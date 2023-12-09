using System.Reflection;
namespace ArkShopItemsFromExcel.Entities;
public static class EPPlusExtensions
{
    public static IEnumerable<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet) where T : new()
    {
        Func<CustomAttributeData, bool> columnOnly = y => y.AttributeType == typeof(Column);
        var columns = typeof(T)
                .GetProperties()
                .Where(x => x.CustomAttributes.Any(columnOnly))
        .Select(p => new
        {
            Property = p,
            Column = p.GetCustomAttributes<Column>().First().ColumnIndex //safe because if where above
        }).ToList();
        var rows = worksheet.Cells
            .Select(cell => cell.Start.Row)
            .Distinct()
            .OrderBy(x => x).Skip(1);
        //Create the collection container
        var collection = new List<T>();
        foreach (var row in rows)
        {
            var item = new T();
            foreach (var col in columns)
            {
                //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                var val = worksheet.Cells[row, col.Column];
                //If it is numeric it is a double since that is how excel stores all numbers
                switch (col.Property.PropertyType.ToString())
                {
                    case "System.Int64":
                        col.Property.SetValue(item, val.GetValue<long>());
                        break;
                    case "System.Int32":
                        col.Property.SetValue(item, val.GetValue<int>());
                        break;
                    case "System.Boolean":
                        col.Property.SetValue(item, val.GetValue<Boolean>());
                        break;
                    case "System.String":
                        col.Property.SetValue(item, val.GetValue<string>());
                        break;
                    default:
                        throw new Exception($"{col.Property.PropertyType.ToString()} Something went wrong parsing sheet into memory objects");
                        break;
                }
            }
            collection.Add(item);
        }
        return collection;
    }
}