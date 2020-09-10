using System;
using UnityEngine;

namespace Script {
    public class Setting : MonoBehaviour {
        private void Start() {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }
    }
}