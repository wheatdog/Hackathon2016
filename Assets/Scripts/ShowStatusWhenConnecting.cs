using UnityEngine;
using System.Collections;

public class ShowStatusWhenConnecting : MonoBehaviour
{
    public GUISkin Skin;
	public int x;
	public int y;
	public int width, height;
	public int maxClientNum = 4;
	public int clientOffsetX, clientIntervalX, clientOffsetY, clientWidthOffset;
	public bool isClient = false;

	Rect[] clientRect;
	string[] clientNameList = {"Client0", "Client1", "Client2", "Client3"};
	int currentClientNum = 0;


	public void setClientName(int currentClientNum, string str){
		Debug.Log ("index" + (currentClientNum-1) + " " + str);
		clientNameList [currentClientNum-1] = str;
		this.currentClientNum = currentClientNum;
	}

    void OnGUI()
    {
        if( Skin != null )
        {
            GUI.skin = Skin;
        }

		if (isClient) {//-120, 150, 100, -160
			Rect centeredRect = new Rect (x, y, width, height);
			GUILayout.BeginArea (centeredRect, GUI.skin.box);
			{
				GUILayout.Label ("Connecting" + GetConnectingDots (), GUI.skin.customStyles [0]);
				GUILayout.Label ("Status: " + PhotonNetwork.connectionStateDetailed);
			}
			GUILayout.EndArea ();

			if (PhotonNetwork.inRoom) {

				GUILayout.BeginArea (centeredRect, GUI.skin.box);
				{
					GUILayout.Label ("Complete", GUI.skin.customStyles [0]);
					GUILayout.Label ("Wating For Other Clients" + GetConnectingDots ());
				}
				GUILayout.EndArea ();
				clientRect = new Rect[maxClientNum-1];
				for (int i = 0; i < maxClientNum-1; i++) {
					clientRect [i] = new Rect (x + clientOffsetX + clientIntervalX * i, y + clientOffsetY, width + clientWidthOffset, height);
					//200+120*i, y+100, width-130,
					GUILayout.BeginArea (clientRect [i], GUI.skin.box);
					{
						GUILayout.Label ("Client" + clientNameList[i], GUI.skin.customStyles [0]);
						if(i+1 <= currentClientNum)
							GUILayout.Label ("Connected");
						else
							GUILayout.Label ("Connecting" + GetConnectingDots ());
						//GUILayout.Label();
					}
					GUILayout.EndArea ();
				}
				//enabled = false;
			}

		} else {
			Rect centeredRect = new Rect (x, y, width, height);
			GUILayout.BeginArea (centeredRect, GUI.skin.box);
			{
				GUILayout.Label ("Connecting" + GetConnectingDots (), GUI.skin.customStyles [0]);
				GUILayout.Label ("Status: " + PhotonNetwork.connectionStateDetailed);
			}
			GUILayout.EndArea ();

			if (PhotonNetwork.inRoom) {
			
				GUILayout.BeginArea (centeredRect, GUI.skin.box);
				{
					GUILayout.Label ("Complete", GUI.skin.customStyles [0]);
					GUILayout.Label ("Wating For Clients" + GetConnectingDots ());
				}
				GUILayout.EndArea ();
				clientRect = new Rect[maxClientNum];
				for (int i = 0; i < maxClientNum; i++) {
					clientRect [i] = new Rect (x + clientOffsetX + clientIntervalX * i, y + clientOffsetY, width + clientWidthOffset, height);
					//200+120*i, y+100, width-130,
					GUILayout.BeginArea (clientRect [i], GUI.skin.box);
					{
						GUILayout.Label (clientNameList[i], GUI.skin.customStyles [0]);
						if(i+1 <= currentClientNum)
							GUILayout.Label ("Connected");
						else
							GUILayout.Label ("Connecting" + GetConnectingDots ());
						//GUILayout.Label();
					}
					GUILayout.EndArea ();
				}
				//enabled = false;
			}
		}
    }

    string GetConnectingDots()
    {
        string str = "";
        int numberOfDots = Mathf.FloorToInt( Time.timeSinceLevelLoad * 3f % 4 );

        for( int i = 0; i < numberOfDots; ++i )
        {
            str += " .";
        }

        return str;
    }
}
