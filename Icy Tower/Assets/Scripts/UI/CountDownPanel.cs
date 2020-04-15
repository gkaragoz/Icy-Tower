using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownPanel : MonoBehaviour{
    [Header("Initializations")]
    [SerializeField]
    private Text _txtCountdown = null;
    [SerializeField]
    private GameObject _gameplayMenu = null;

    private int _countdown = 0;

    private void OnEnable() {
        _countdown = LevelManager.instance.CountDownTime;
    }

    private IEnumerator ICountdown() {
        while (true) {
            _txtCountdown.text = _countdown.ToString();
            _countdown--;

            yield return new WaitForSeconds(1f);

            if (_countdown <= 0) {
                break;
            }
        }

        OpenGamePlayMenu();
        HideThisMenu();
    }

    private void OpenGamePlayMenu() {
        _gameplayMenu.SetActive(true);
    }

    private void HideThisMenu() {
        this.gameObject.SetActive(false);
    }

    public void StartCountdown() {
        StartCoroutine(ICountdown());
    }


}
