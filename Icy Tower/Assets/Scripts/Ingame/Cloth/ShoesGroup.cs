using UnityEngine;

public class ShoesGroup : MonoBehaviour
{
    public ShoesGroups[] shoes;
   

    public void ChangeShoes(string shoesIndex)
    {
        foreach (var shoe in shoes)
        {
            shoe.shoesObject.SetActive(false);
        }
        Debug.Log(shoes[int.Parse(shoesIndex)].shoesName+" is weared!");
        shoes[int.Parse(shoesIndex)].shoesObject.SetActive(true);
    }

}






[System.Serializable]
public struct ShoesGroups
{
    public string shoesName;
    public GameObject shoesObject;

}