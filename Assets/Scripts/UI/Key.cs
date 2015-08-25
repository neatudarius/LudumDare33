using UnityEngine;
using System.Collections;

public enum KeyState {
    free = 0,
    pressed = 1,
    down = 2,
    up = 3
}

public class Key {
    public bool
        pressed,
        down,
        up;


    KeyCode system;

    public Key ( ) {
        system = KeyCode.None;
    }

    public Key ( KeyCode keycode) {
        system = keycode;
    }
    
    void Start ( ) {
        pressed = down = up = false;
    }



	
}
