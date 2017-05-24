using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	private Camera camera; 
	private GameObject particle;
	private RaycastHit hit;
	private Vector3 tempPos;
	private Vector3 pushZ;
	private int playerTurn;
	public GameObject player1;
	public GameObject player2;
	private float timeStamp;
	public GameObject detector;
	private Vector3 xTravel;
	private Vector3 yTravel;
	private GameObject temp;
	


	void Start(){
		camera = GetComponent<Camera> ();
		pushZ = new Vector3 (0,0,-0.4f);
		playerTurn = 1;
		xTravel = new Vector3 (1,0,0);
		yTravel = new Vector3 (0,1,0);
		Debug.Log ("Script is RUnning now");
	}

	void Update(){
		

		if (Input.GetMouseButtonDown(0) && timeStamp <= Time.time) {

			Ray ray = camera.ScreenPointToRay (Input.mousePosition);


			if (Physics.Raycast (ray, out hit,1000)) {
				
				if (hit.collider.gameObject.tag == "Brick") {
					
					tempPos = hit.transform.gameObject.transform.position;
					//Vector3 (0,0,0);

					timeStamp = Time.time + 2.0f; // 2 second CoolDown

					if (playerTurn % 2 == 1) {
						Instantiate (player1, tempPos+pushZ, Quaternion.Euler(90,0,0));
						playerTurn++;
					} else {
						Instantiate (player2, tempPos+pushZ, Quaternion.Euler(90,0,0));
						playerTurn++;
					}
				}	
			}
		}			
	}

	public void CheckForWin(){

		Ray ray = new Ray (detector.transform.position,xTravel); 
	//	Debug.DrawRay (detector.transform.position,new Vector3(1000,0,0),Color.black,2,true);

		RaycastHit[] playerhits; 
		playerhits = Physics.RaycastAll (ray,100000);
		int temp1 = 0;
		int temp2 = 0;
		temp = null;
		for (int i = 0; i < playerhits.Length; i++) {

			temp = playerhits [i].transform.gameObject;

			if (temp.tag == "Player1" || temp.tag == "Player2") {
				Debug.Log ("We have a player at iteration number: " + i);
			}
			if (temp.tag == "Player1") {
				temp1++;
				temp2 = 0;
			} 
			if (temp.tag == "Player1") {
				temp2++;
				temp1 = 0;
			}
			if (temp1 == 4) {
				Debug.Log ("Player 1 wins");
			}
			else if(temp2 == 4){
				Debug.Log ("Player 2 wins");
			}

	}
}
}
