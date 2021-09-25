using UnityEngine;

public class SwitchObject : MonoBehaviour
{
    [SerializeField] private GameObject inactiveObject;
    [SerializeField] private GameObject activeObject;

    public void Switch()
    {
        EnableObject(inactiveObject.activeInHierarchy ? activeObject : inactiveObject);
        DisableObject(inactiveObject.activeInHierarchy ? activeObject : inactiveObject);
    }
        
    private void EnableObject(GameObject objectToActivate)
    {
        objectToActivate.SetActive(true);
        
    }

    private void DisableObject(GameObject objectToDeactivate)
    {
        objectToDeactivate.SetActive(false);
        
    }
    
    
}