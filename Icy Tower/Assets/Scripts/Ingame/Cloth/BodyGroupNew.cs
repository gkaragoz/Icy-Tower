using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyGroupNew : MonoBehaviour
{
    public BodyGroups[] bodyParts;

    private void Start()
    {


        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i]._clothManager = new ClothManager[bodyParts[i].bodyObject.Length];
            for (int j = 0; j < bodyParts[i].bodyObject.Length; j++)
            {
                bodyParts[i]._clothManager[j] = bodyParts[i].bodyObject[j].GetComponent<ClothManager>();
            }
        }
    }


    public void ChooseBodyObject(int bodyPartIndex,int bodyObjectIndex,int ColorIndex)
    {
        foreach (var item in bodyParts[bodyPartIndex].bodyObject)
        {
            item.SetActive(false);
        }
        bodyParts[bodyPartIndex].bodyObject[bodyObjectIndex].SetActive(true);
        bodyParts[bodyPartIndex]._clothManager[bodyObjectIndex].SetColor(ColorIndex);
    }




}






[System.Serializable]
public struct BodyGroups
{
    public string bodyPartName;
    public GameObject[] bodyObject;
    [HideInInspector]
    public ClothManager[] _clothManager;
}