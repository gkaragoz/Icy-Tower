using Library.Social.Leaderboard;
using TMPro;
using UnityEngine;

public class LeaderboardPlayerUI : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private TextMeshProUGUI _txtPosition = null;
    [SerializeField]
    private TextMeshProUGUI _txtName = null;
    [SerializeField]
    private TextMeshProUGUI _txtScore = null;
    [SerializeField]
    private GameObject _isMeIndicatorObj = null;

    [SerializeField]
    [Utils.ReadOnly]
    private ResultPlayer _resultPlayer = null;

    private const string NA_STRING = "N/A";

    public void SetData(ResultPlayer resultPlayer) {
        this._resultPlayer = resultPlayer;

        SetName();
        SetPosition();
        SetScore();
        SetIsMeIndicator();
    }

    public void SetActive(bool isActive) {
        gameObject.SetActive(isActive);
    }

    private void SetName() {
        if (_resultPlayer == null) {
            _txtName.text = NA_STRING;
            return;
        }

        _txtName.text = _resultPlayer.DisplayName;
    }

    private void SetPosition() {
        if (_resultPlayer == null) {
            _txtPosition.text = NA_STRING;
            return;
        }

        _txtPosition.text = (_resultPlayer.Position + 1).ToString();
    }

    private void SetScore() {
        if (_resultPlayer == null) {
            _txtScore.text = NA_STRING;
            return;
        }

        _txtScore.text = _resultPlayer.StatValue.ToString();
    }

    private void SetIsMeIndicator() {
        if (_resultPlayer == null) {
            _isMeIndicatorObj.SetActive(false);
            return;
        }

        if (Library.Authentication.PlayfabCustomAuth.UserDisplayName == _resultPlayer.DisplayName) {
            _isMeIndicatorObj.SetActive(true);
        } else {
            _isMeIndicatorObj.SetActive(false);
        }
    }
    
}
