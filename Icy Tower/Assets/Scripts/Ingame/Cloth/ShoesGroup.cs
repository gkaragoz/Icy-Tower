using UnityEngine;

public class ShoesGroup : MonoBehaviour {
    public ShoesGroups[] shoes;

    public void ChooseShoes(int shoesIndex) {
        foreach (var shoe in shoes) {
            shoe.shoesObject.SetActive(false);
        }
        Debug.Log(shoes[shoesIndex].shoesName + " is weared!");
        shoes[shoesIndex].shoesObject.SetActive(true);
    }
}

[System.Serializable]
public struct ShoesGroups {
    public string shoesName;
    public GameObject shoesObject;

}