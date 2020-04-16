using UnityEngine;

public class ClothChanger : MonoBehaviour {

    #region Cloth Up

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRendererUp = null;
    public SkinnedMeshRenderer SkinnedMeshRendererUp { get => _skinnedMeshRendererUp; set => _skinnedMeshRendererUp = value; }
    [SerializeField]
    private ClothUpData[] _clothUpDatas = null;

    #endregion

    #region Cloth Down

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRendererDown = null;
    public SkinnedMeshRenderer SkinnedMeshRendererDown { get => _skinnedMeshRendererDown; set => _skinnedMeshRendererDown = value; }
    [SerializeField]
    private ClothDownData[] _clothDownDatas = null;

    #endregion

    #region Cloth Shoe

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRendererShoe = null;
    public SkinnedMeshRenderer SkinnedMeshRendererShoe { get => _skinnedMeshRendererShoe; set => _skinnedMeshRendererShoe = value; }
    [SerializeField]
    private ClothShoeData[] _clothShoeDatas = null;

    #endregion

    #region Cloth Accesory

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRendererAccesory = null;
    public SkinnedMeshRenderer SkinnedMeshRendererAccesory { get => _skinnedMeshRendererAccesory; set => _skinnedMeshRendererAccesory = value; }
    [SerializeField]
    private ClothAccesoryData[] _clothAccesoryDatas = null;

    #endregion

    #region Cloth Hair

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRendererHair = null;
    public SkinnedMeshRenderer SkinnedMeshRendererHair { get => _skinnedMeshRendererHair; set => _skinnedMeshRendererHair = value; }
    [SerializeField]
    private ClothHairData[] _clothHairDatas = null;

    #endregion
}
