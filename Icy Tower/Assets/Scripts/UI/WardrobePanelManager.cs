using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WardrobePanelManager : MonoBehaviour
{
    [SerializeField]
    private BodyGroupNew _bodyGroup;
    [SerializeField]
    private HeadGroup _headGroup;
    [SerializeField]
    private ShoesGroup _shoesGroup;


    [SerializeField]
    private Sprite _wardrobeSelected;
    [SerializeField]
    private Sprite _wardrobeNormal;
    [SerializeField]
    private Image[] _tabButtons;
    [SerializeField]
    private GameObject[] _wardrobeObjectPanel;
    [SerializeField]
    private Image[] _buttonInsideImage;


    public void ChooseTab(int index)
    {
        foreach (var item in _tabButtons)
        {
            item.sprite = _wardrobeNormal;
        }
        _tabButtons[index].sprite = _wardrobeSelected;

        foreach (var panel in _wardrobeObjectPanel)
        {
            panel.SetActive(false);

        }
        _wardrobeObjectPanel[index].SetActive(true);

        foreach (var buttonImage in _buttonInsideImage)
        {
            buttonImage.color = new Color32(126,126,126,255);
        }

        _buttonInsideImage[index].color = new Color32(56,107,20,255);
    }

    public void ChangeHair(string values)
    {
        /*First parameter is Hair Id
            Default Hair    -> 0
            Blonde Hair     -> 1
            Red Hair        -> 2   Hair 1 and Hair 2 has no accesories default 0
         */
        /*Second Parameter is Accesories Id
          No accesorie  -> 0
          Crown         -> 1
          Crown 2       -> 2
          Hat           -> 3
          Wizard Hat    -> 4
         */
        string[] myStringSplit = values.Split(',');
        int idx = int.Parse(myStringSplit[0]);
        int idy = int.Parse(myStringSplit[1]);
         _headGroup.ChooseHead(idx,idy);

    }

    public void ChangeBodyUp(string input)
    {
        /*First parameter is Hair Id
          BodyUpShort     -> 0
          BodyUpLong      -> 1
       */
        string[] myStringSplit = input.Split(',');
        int bodyPartGroup = 0; // Up is always 0
        int upStyle =int.Parse( myStringSplit[0]);
        int color =int.Parse( myStringSplit[1]);
        _bodyGroup.ChooseBodyObject(bodyPartGroup, upStyle,color);

    }

    public void ChangeBodyDown(string input)
    {
        /*First parameter is Hair Id
  Default Hair    -> 0
  Blonde Hair     -> 1
  Red Hair        -> 2  
*/
        string[] myStringSplit = input.Split(',');
        int bodyPartGroup = 1; // Down is always 1
        int upStyle = int.Parse(myStringSplit[0]);
        int color = int.Parse(myStringSplit[1]);
        _bodyGroup.ChooseBodyObject(bodyPartGroup, upStyle, color);
    }

    public void ChangeShoes(int id)
    {
        _shoesGroup.ChangeShoes(id);
    }

}
