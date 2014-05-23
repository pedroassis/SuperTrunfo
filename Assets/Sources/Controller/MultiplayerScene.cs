using UnityEngine;
using System.Collections;
using SuperTrunfo;

[ExecuteInEditMode] 
public class MultiplayerScene : MonoBehaviour {

    private string writeThis = "Nome da Sala";
     
    public string userInput = "";
    private string userInput2 = "";

    private WebSocketService webSocketService = Container.get<WebSocketService>();

    public MultiplayerScene() {


        webSocketService.open();
    }

    void OnGUI(){

        float width = Screen.height;

        GUI.Label(new Rect(width * 0.85F, width * 0.3F, 200, 30), writeThis);

        userInput = GUI.TextField(new Rect(width * 0.85F, width * 0.3F + 20, 200, 30), userInput);

    }
}
