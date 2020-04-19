using System;
using UnityEngine;

public class InGameObjectClicks : MonoBehaviour {

    private void Update() {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.GetGameState() == GameState.MainMenu) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.transform != null) {
                    IngameObjectUI ui = hit.transform.GetComponent<IngameObjectUI>();

                    if (ui != null) {
                        ui.OnClick();
                    }
                }
            }
        }
    }

}
