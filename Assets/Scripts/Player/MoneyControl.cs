using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyControl : MonoBehaviour {

    public Text display;
    public int money;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        display.text = string.Format("Money; ${0}", money);
	}

}
