using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour, IInventoryItem
{
    public Vector3 PickupPosition;
    public Vector3 PickupRotation;

    public string Name => throw new System.NotImplementedException();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnUse()
    {
    
    }

    public void OnPickup()
    {
        transform.localPosition = PickupPosition;
        transform.localEulerAngles = PickupPosition;
        gameObject.SetActive(true);
        Debug.Log("the coords are ");
        Debug.Log(PickupPosition);
        Debug.Log(PickupRotation);
    }
}
