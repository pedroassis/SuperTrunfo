using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class Bootstrap : MonoBehaviour {

    private TimeoutService timeout;

	void Start () {
        DontDestroyOnLoad(this);

        Application.LoadLevel("MainMenu");
	}

    void Update() {
        TimeoutService.check();
    }


}
