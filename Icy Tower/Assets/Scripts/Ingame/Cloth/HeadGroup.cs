using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGroup : MonoBehaviour
{
    public HeadAccesories[] heads;

    public Vector2Int changeHead;
   

    public void ChooseHead(int headIndex, int accesoriesIndex)
    {
        foreach (var head in heads)
        {
            head.head.SetActive(false);

            foreach (var accesorie in head.accesories)
            {
                accesorie.SetActive(false);
            }
        }
        heads[headIndex].head.SetActive(true);
        heads[headIndex].accesories[accesoriesIndex].SetActive(true);
    }
 

    public void CallChoose()
    {
        ChooseHead(changeHead.x,changeHead.y);
    }

}






[System.Serializable]
public struct HeadAccesories
{
    public GameObject head;
    public GameObject[] accesories;
}