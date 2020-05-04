using Library.Social.Leaderboard;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LeaderboardUI : MonoBehaviour {

    [SerializeField]
    private GameObject _facebookListObj = null;
    [SerializeField]
    private GameObject _globalListObj = null;

    [SerializeField]
    private LeaderboardPlayerUI[] _globalPlayersUI = null;
    [SerializeField]
    private LeaderboardPlayerUI[] _facebookPlayersUI = null;

    [Header("Debug")]
    [SerializeField]
    private List<ResultPlayer> _globalResultPlayers = null;
    [SerializeField]
    private List<ResultPlayer> _facebookResultPlayers = null;
    
    [SerializeField]
    [Utils.ReadOnly]
    private int[] _isFacebookReady = new int[2];
    [SerializeField]
    [Utils.ReadOnly]
    private int[] _isGlobalReady = new int[2];
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isGlobalTabActive = false;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isFacebookTabActive = false;

    // Onclick in MainMenu
    public void OpenLeaderboard() {
        _isFacebookReady = new int[] { -1, -1 };
        _isGlobalReady = new int[] { -1, -1 };
        _isGlobalTabActive = false;
        _isFacebookTabActive = false;

        OpenGlobalTab();
    }

    // Onclick in LeaderboardTab
    public void OpenGlobalTab() {
        if (_isGlobalTabActive == false) {
            FetchGlobalData();
        }

        _isGlobalTabActive = true;
        _isFacebookTabActive = false;
    }

    // Onclick in LeaderboardTab
    public void OpenFacebookTab() {
        if (Library.FaceBook.FacebookService.IsLinkedWithFacebook() == false) {
            ConnectionServices.instance.ConnectFacebook(
                (isLinked) => {
                    if (isLinked) {
                        if (_isFacebookTabActive == false) {
                            FetchFacebookData();
                        }

                        _isGlobalTabActive = false;
                        _isFacebookTabActive = true;
                    } else {
                        OpenGlobalTab();
                    }
                });
        }
    }

    private void OrderFacebookData() {
        if (_facebookResultPlayers == null) {
            return;
        }

        _facebookResultPlayers = _facebookResultPlayers.OrderBy(p => p.Position).ToList();
    }

    private void OrderGlobalData() {
        if (_globalResultPlayers == null) {
            return;
        }

        _globalResultPlayers = _globalResultPlayers.OrderBy(p => p.Position).ToList();
    }

    private void CleanNamesOfFacebookPlayers() {
        try {
            _facebookResultPlayers.ForEach(x => x.DisplayName = x.DisplayName.Remove(x.DisplayName.Length - 5));
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    private void CleanNamesOfGlobalPlayers() {
        try {
            _globalResultPlayers.ForEach(x => x.DisplayName = x.DisplayName.Remove(x.DisplayName.Length - 5));
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    private void OnFetchFacebookDataCompleted(int index, int isSuccess) {
        _isFacebookReady[index] = isSuccess;

        // Still fetching
        if (_isFacebookReady[0] == -1 || _isFacebookReady[1] == -1) {
            return;
        }

        if (_isFacebookReady[0] == 1 && _isFacebookReady[1] == 1) {
            OrderFacebookData();
            CleanNamesOfFacebookPlayers();
            SetOrderVisibilityForFacebook();

            _globalListObj.SetActive(false);
            _facebookListObj.SetActive(true);
        } else {
            string firstLog = System.Convert.ToInt32(_isFacebookReady[0]).ToString();
            string secondLog = System.Convert.ToInt32(_isFacebookReady[1]).ToString();
            string message = "ErrorCode[" + firstLog + "," + secondLog + "]";

            UIManager.instance.OpenPopup("Oh no!", message);
        }
    }

    private void OnFetchGlobalDataCompleted(int index, int isSuccess) {
        _isGlobalReady[index] = isSuccess;

        // Still fetching
        if (_isGlobalReady[0] == -1 || _isGlobalReady[1] == -1) {
            return;
        }

        if (_isGlobalReady[0] == 1 && _isGlobalReady[1] == 1) {
            OrderGlobalData();
            CleanNamesOfGlobalPlayers();
            SetOrderVisibilityForGlobal();

            _facebookListObj.SetActive(false);
            _globalListObj.SetActive(true);
        } else {
            string firstLog = System.Convert.ToInt32(_isGlobalReady[0]).ToString();
            string secondLog = System.Convert.ToInt32(_isGlobalReady[1]).ToString();
            string message = "ErrorCode[" + firstLog + "," + secondLog + "]";

            UIManager.instance.OpenPopup("Oh no!", message);
        }
    }

    private void FetchFacebookData() {
        _isFacebookReady = new int[] { -1, -1 };
        _facebookResultPlayers = new List<ResultPlayer>();

        Leaderboard.GetFacebookLeaderboardTrimByPosition(
            0,
            3,
            (resultPlayers) => {
                _facebookResultPlayers.AddRange(resultPlayers);
                _facebookResultPlayers = _facebookResultPlayers.DistinctBy(x => x.UserID).ToList();

                OnFetchFacebookDataCompleted(0, 1);
            },
            (errorMessage) => {
                OnFetchFacebookDataCompleted(0, -1);
            });

        Leaderboard.GetFacebookLeaderboardAroundMe(
            9,
            (resultPlayers) => {
                _facebookResultPlayers.AddRange(resultPlayers);
                _facebookResultPlayers = _facebookResultPlayers.DistinctBy(x => x.UserID).ToList();

                OnFetchFacebookDataCompleted(1, 1);
            },
            (errorMessage) => {
                OnFetchFacebookDataCompleted(1, -1);
            });
    }

    private void FetchGlobalData() {
        _isGlobalReady = new int[] { -1, -1 };
        _globalResultPlayers = new List<ResultPlayer>();

        Leaderboard.GetGlobalLeaderboardTrimByPosition(
            0, 
            3,
            (resultPlayers) => {
                _globalResultPlayers.AddRange(resultPlayers);
                _globalResultPlayers = _globalResultPlayers.DistinctBy(x => x.UserID).ToList();

                OnFetchGlobalDataCompleted(0, 1);
            },
            (errorMessage) => {
                UIManager.instance.OpenPopup("Oh no!", errorMessage);
                OnFetchGlobalDataCompleted(0, -1);
            });

        Leaderboard.GetGlobalLeaderboardAroundMe(
            9,
            (resultPlayers) => {
                _globalResultPlayers.AddRange(resultPlayers);
                _globalResultPlayers = _globalResultPlayers.DistinctBy(x => x.UserID).ToList();

                OnFetchGlobalDataCompleted(1, 1);
            },
            (errorMessage) => {
                UIManager.instance.OpenPopup("Oh no!", errorMessage);
                OnFetchGlobalDataCompleted(1, -1);
            });
    }

    private void SetOrderVisibilityForFacebook() {
        for (int ii = 0; ii < _facebookPlayersUI.Length; ii++) {
            if (ii >= _facebookResultPlayers.Count) {
                _facebookPlayersUI[ii].SetActive(false);
            } else {
                _facebookPlayersUI[ii].SetData(_facebookResultPlayers[ii]);
                _facebookPlayersUI[ii].SetActive(true);
            }
        }
    }

    private void SetOrderVisibilityForGlobal() {
        for (int ii = 0; ii < _globalPlayersUI.Length; ii++) {
            if (IsThereMoreThanEightPlayer()) {
                if (AmILast()) {

                    for (int i = 0; i < 3; i++) {
                        _globalPlayersUI[i].SetData(_globalResultPlayers[i]);
                        _globalPlayersUI[i].SetActive(true);
                    }

                    for (int j = 2; j <= 5; j++) {
                        _globalPlayersUI[_globalPlayersUI.Length - j].SetData((_globalResultPlayers[_globalResultPlayers.Count - j]));
                        _globalPlayersUI[_globalPlayersUI.Length - j].SetActive(true);
                    }
                    
                    _globalPlayersUI[_globalPlayersUI.Length - 1].SetData((_globalResultPlayers[_globalResultPlayers.Count - 1]));
                    _globalPlayersUI[_globalPlayersUI.Length - 1].SetActive(true);

                }else if(GetPlayerCountLowerThanMe() == 1) {

                    for (int i = 0; i < 3; i++) {
                        _globalPlayersUI[i].SetData(_globalResultPlayers[i]);
                        _globalPlayersUI[i].SetActive(true);
                    }

                    for (int j = 3; j <= 5; j++) {
                        _globalPlayersUI[_globalPlayersUI.Length - j].SetData((_globalResultPlayers[_globalResultPlayers.Count - j]));
                        _globalPlayersUI[_globalPlayersUI.Length - j].SetActive(true);
                    }

                    _globalPlayersUI[_globalPlayersUI.Length - 2].SetData((_globalResultPlayers[_globalResultPlayers.Count - 2]));
                    _globalPlayersUI[_globalPlayersUI.Length - 2].SetActive(true);

                    _globalPlayersUI[_globalPlayersUI.Length - 1].SetData((_globalResultPlayers[_globalResultPlayers.Count - 1]));
                    _globalPlayersUI[_globalPlayersUI.Length - 1].SetActive(true);

                } else {

                    for (int i = 0; i < 3; i++) {
                        _globalPlayersUI[i].SetData(_globalResultPlayers[i]);
                        _globalPlayersUI[i].SetActive(true);
                    }

                    for (int j = 4; j <= 5; j++) {
                        _globalPlayersUI[_globalPlayersUI.Length - j].SetData((_globalResultPlayers[_globalResultPlayers.Count - j]));
                        _globalPlayersUI[_globalPlayersUI.Length - j].SetActive(true);
                    }

                    _globalPlayersUI[_globalPlayersUI.Length - 3].SetData((_globalResultPlayers[_globalResultPlayers.Count - 3]));
                    _globalPlayersUI[_globalPlayersUI.Length - 3].SetActive(true);

                    _globalPlayersUI[_globalPlayersUI.Length - 2].SetData((_globalResultPlayers[_globalResultPlayers.Count - 2]));
                    _globalPlayersUI[_globalPlayersUI.Length - 2].SetActive(true);

                    _globalPlayersUI[_globalPlayersUI.Length - 1].SetData((_globalResultPlayers[_globalResultPlayers.Count - 1]));
                    _globalPlayersUI[_globalPlayersUI.Length - 1].SetActive(true);

                }
            } else {
                _globalPlayersUI[ii].SetData(_globalResultPlayers[ii]);
                _globalPlayersUI[ii].SetActive(true);
            }
        }
    }

    public bool IsThereMoreThanEightPlayer() {
        if (_globalResultPlayers.Count > 7) {
            return true;
        } else {
            return false;
        }

    }

    private bool AmILast() {
        if (_globalResultPlayers[_globalResultPlayers.Count - 1].UserID == Library.Authentication.PlayfabCustomAuth.PlayFabID) {
            return true;
        } else {
            return false;
        }
    }

    private int GetPlayerCountLowerThanMe() {
        int meIndex = -1;
        for (int ii = 0; ii < _globalResultPlayers.Count; ii++) {
            if (_globalResultPlayers[ii].UserID == Library.Authentication.PlayfabCustomAuth.PlayFabID) {
                meIndex = ii;
            }
        }

        return (_globalResultPlayers.Count - 1) - meIndex; 
    }

}
