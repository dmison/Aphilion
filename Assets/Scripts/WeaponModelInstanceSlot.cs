using UnityEngine;

public class WeaponModelInstanceSlot: MonoBehaviour
{
    public GameObject currentWeaponModel;
    public WeaponModelSlot weaponModelSlot;
    
    public void UnLoadWeaponModel()
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }
    
    public void LoadWeaponModel(GameObject weaponModel)
    {
        currentWeaponModel = weaponModel;
        weaponModel.transform.parent = transform;
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localRotation = Quaternion.identity;
        weaponModel.transform.localScale = Vector3.one;
    }
}
