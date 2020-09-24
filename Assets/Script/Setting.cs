using System;
using UnityEngine;

namespace Script {
    public class Setting : MonoBehaviour {
        
        [SerializeField] private float slow;
        private void Start() {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }

        private void Update() {
            if (Input.GetKeyDown("f")) {
                Time.timeScale = slow;
            }

            if (Input.GetKeyDown("j")) {
                Time.timeScale = 1f;
            }
        }
    }
}