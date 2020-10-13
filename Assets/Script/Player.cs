using UnityEngine;

public class Player {
    private static GameObject player;

    public static GameObject GetInstance() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        return player;
    }
}