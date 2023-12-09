namespace ArkShopItemsFromExcel.Entities;
public class DinoEntry
{
    [Column(1)]
    public bool Suppress { get; set; }
    [Column(2)]
    public string ShopName { get; set; }
    [Column(3)]
    public string Title { get; set; }
    [Column(4)]
    public string Description { get; set; }
    [Column(5)]
    public string Category { get; set; }
    [Column(6)]
    public long Price { get; set; }
    [Column(7)]
    public long Level { get; set; }
    [Column(8)]
    public long MaxLevel { get; set; }
    [Column(9)]
    public string Blueprint { get; set; }
}
