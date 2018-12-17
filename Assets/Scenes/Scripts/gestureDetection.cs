using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Leap;

public class gestureDetection : MonoBehaviour {
    public Leap.Controller controller;
    private Hand right,left; 
    public GameObject player; 

	// Use this for initialization
	void Start () {
        controller = new Controller();
        Debug.Log("Leap start");

	}

	// Update is called once per frame
    void assignHands(){
         Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;
        if (hands.Count>1 ){
            if (hands[0].IsLeft){
                left = hands[0];
                right = hands[1];
            }
            else {
                right = hands[0];
                left = hands[1];
            }
        }
        else if (hands.Count > 0){
            if(hands[0].IsLeft){
                left = hands[0];
                right = null; 
            } else {
                right = hands[0];
                left = null;
            }
        }
    }

    void RightControl()
    {
        if (right != null)
        {
            player.GetComponent<Transform>().transform.localScale = new Vector3(4*right.PinchStrength+0.9f, 4*right.PinchStrength+0.9f, 4*right.PinchStrength+0.9f);

        }
        else
        {
            //player.GetComponent<Transform>().transform.localScale = new Vector3(1,1,1);
        }
    }
    void LeftControl()
    {
        if (left != null)
        {
            player.GetComponent<Transform>().transform.rotation = new Quaternion(left.Rotation.x, left.Rotation.y, left.Rotation.z, left.Rotation.w);
            float deltaY = left.PalmPosition.y/100;
            if(deltaY>3){
                deltaY = 3;
            }
            float mappedY = MapValue(0f, 3f, -2f, 2f, deltaY);
            print(mappedY);
            player.GetComponent<Transform>().transform.position = new Vector3(0, mappedY, 0);
        }
    }
     float MapValue(double a0, double a1, double b0, double b1, double a)
    {
        return (float)(b0 + (b1 - b0) * ((a - a0) / (a1 - a0)));
    }
	void Update () {
       
            assignHands();
            RightControl();
            LeftControl();
            
    

        }
   
	
}
