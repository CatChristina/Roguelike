using UnityEngine;
public class ItemData : MonoBehaviour
{
    private string itemName;
    public int cost;
    [TextArea(3, 10)] // 3 to 10 lines
    public string description;
}
