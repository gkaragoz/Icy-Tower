using UnityEngine;

public class WardrobePanelManager : MonoBehaviour {

    #region Singleton

    public static WardrobePanelManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public static int gurkan = 0;

    [SerializeField]
    private BodyGroupNew _bodyGroup = null;
    [SerializeField]
    private HeadGroup _headGroup = null;
    [SerializeField]
    private ShoesGroup _shoesGroup = null;

    public void Buy(object data, ClothType clothType) {
        int myMoney = Account.instance.GetCurrencyAmount(VirtualCurrency.Gold);

        switch (clothType) {
            case ClothType.Head:
                ClothHeadMapping headData = (ClothHeadMapping)data;

                if (ExtensionMethods.AmIAbleToBuyIt(myMoney, headData.price)) {
                    Account.instance.DecreaseVirtualCurrency(headData.price, VirtualCurrency.Gold);
                    Account.instance.AddCloth(ClothType.Head, headData.id, true);
                    Account.instance.Save();
                }

                break;
            case ClothType.Body:
                ClothBodyMapping bodyData = (ClothBodyMapping)data;

                if (ExtensionMethods.AmIAbleToBuyIt(myMoney, bodyData.price)) {
                    Account.instance.DecreaseVirtualCurrency(bodyData.price, VirtualCurrency.Gold);
                    Account.instance.AddCloth(ClothType.Body, bodyData.id, true);
                    Account.instance.Save();
                }

                break;
            case ClothType.Shoe:
                ClothShoeMapping shoeData = (ClothShoeMapping)data;

                if (ExtensionMethods.AmIAbleToBuyIt(myMoney, shoeData.price)) {
                    Account.instance.DecreaseVirtualCurrency(shoeData.price, VirtualCurrency.Gold);
                    Account.instance.AddCloth(ClothType.Shoe, shoeData.id, true);
                    Account.instance.Save();
                }
                break;
            default:
                break;
        }
    }

    public void Use(object data, ClothType clothType) {
        string values = string.Empty;

        switch (clothType) {
            case ClothType.Head:
                ClothHeadMapping headData = (ClothHeadMapping)data;

                values = headData.headId + "," + headData.accesoryId;
                ChangeHair(values);

                break;
            case ClothType.Body:
                ClothBodyMapping bodyData = (ClothBodyMapping)data;

                values = bodyData.bodyGroup + "," + bodyData.style + "," + bodyData.color;
                ChangeBody(values);

                break;
            case ClothType.Shoe:
                ClothShoeMapping shoeData = (ClothShoeMapping)data;

                values = shoeData.id;

                ChangeShoes(values);
                break;
            default:
                break;
        }
    }

    private void ChangeHair(string values)
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

    private void ChangeBody(string input) {
        string[] myStringSplit = input.Split(',');
        int bodyPartGroup = int.Parse(myStringSplit[0]);
        int upStyle = int.Parse(myStringSplit[1]);
        int color = int.Parse(myStringSplit[2]);
        _bodyGroup.ChooseBodyObject(bodyPartGroup, upStyle, color);

    }

    private void ChangeShoes(string id)
    {
        _shoesGroup.ChangeShoes(id);
    }

}
