using System;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    private static Player instance;
    public static Player Instance => instance ?? (instance = new Player());

    public GameObject Object { get; private set; }
    
    public List<Action> Attack { get; private set; }
    
    private Player() {
        Object = GameObject.FindGameObjectWithTag("Player");
        Attack = new List<Action>();
    }
    
    
}