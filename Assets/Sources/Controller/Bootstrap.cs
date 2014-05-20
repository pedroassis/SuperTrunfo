using UnityEngine;
using System.Collections;
using SuperTrunfo;

public class Bootstrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Configuration.configure();

        Application.LoadLevel("MainMenu");
	}
}
