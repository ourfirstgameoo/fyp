using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class Item : ScriptableObject
{
    public enum itemType {origin, defense, attack };

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public GameObject objPrefeb = null;
    public itemType itemtype;
    public int trackableTime = 3;

}
