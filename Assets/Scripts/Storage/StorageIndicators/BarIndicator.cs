using UnityEngine;

public class BarIndicator : StorageIndicator
{
    public override void UpdateIndicator(Storage storage)
    {
        transform.localScale = new Vector3(1f * storage.ItemCount / storage.StorageCapacity, transform.localScale.y, transform.localScale.z);
    }
}
