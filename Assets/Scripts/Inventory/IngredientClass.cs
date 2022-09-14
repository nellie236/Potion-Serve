using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Ingredient Class", menuName = "Item/Ingredient")]
public class IngredientClass : ItemClass
{
    [Header("Ingredient")]
    public bool inPot;
    //data specific to ingredients

    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return null; }
    public override MiscClass GetMisc() { return null; }
    public override ConsumableClass GetConsumable() { return null; }
    public override IngredientClass GetIngredient() { return this; }
}
