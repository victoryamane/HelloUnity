using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Player.Instance.Attack.Add(() => Attack()); 
        Debug.Log($"Register Slime attack - {gameObject.name}");
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void Attack() {
        Debug.Log($"Slime attack - {gameObject.name}");
    }
}