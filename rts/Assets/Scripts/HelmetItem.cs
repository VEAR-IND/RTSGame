public class HelmetItem : Item
{
    public HelmetItem()
        : base()
    { }
    public HelmetItem(bool isEmpty)
        : base(isEmpty)
    { }
    public HelmetItem(string itemName, string itemDiscription = "This is...", int itemCost = 10, double itemWeight = 1, bool isConsumable = false, bool isStackable = false, ItemRarity itemRariry = ItemRarity.Junk)
        : base(itemName, itemDiscription, itemCost, itemWeight, isConsumable, isStackable, itemRariry)
    { }
    public HelmetItem(string itemName, ItemStats itemStats, string itemDiscription = "This is...", int itemCost = 10, double itemWeight = 1, bool isConsumable = false, bool isStackable = false, ItemRarity itemRariry = ItemRarity.Junk)
        : base(itemName, itemStats, itemDiscription, itemCost, itemWeight, isConsumable, isStackable, itemRariry)
    { }
    public HelmetItem(string itemName, ItemStats itemStats, string itemSpritePath, string itemSpriteName, string itemDiscription = "This is...", int itemCost = 10, double itemWeight = 1, bool isConsumable = false, bool isStackable = false, ItemRarity itemRariry = ItemRarity.Junk)
        : base(itemName, itemStats, itemSpritePath, itemSpriteName, itemDiscription, itemCost, itemWeight, isConsumable, isStackable, itemRariry)
    { }
    public HelmetItem(string itemName, ItemStats itemStats, string itemSpritePath, string itemSpriteName, string itemObjectPath, string itemDiscription = "This is...", int itemCost = 10, double itemWeight = 1, bool isConsumable = false, bool isStackable = false, ItemRarity itemRariry = ItemRarity.Junk)
        : base(itemName, itemStats, itemSpritePath, itemSpriteName, itemObjectPath, itemDiscription, itemCost, itemWeight, isConsumable, isStackable, itemRariry)
    { }
    public override Item Erase()
    {
        return new HelmetItem();
    }
}
