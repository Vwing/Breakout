using UnityEngine;
using System.Collections;

public class MenuSelectItem : MonoBehaviour
{
    private MenuSelect menu;
    private GameObject myChild;
    private bool t = false;
    private bool usingCardboard = false;
	// Use this for initialization
	void Start ()
	{
	    menu = GameObject.Find("Player").GetComponent<MenuSelect>();
	    myChild = transform.GetChild(0).gameObject;
	    usingCardboard = Cardboard.SDK;
	}
	
	// Update is called once per frame
	void Update () {
	    if (menu.hitThing == transform)
	    {
            if (usingCardboard)
                t = Cardboard.SDK.Triggered;
	        myChild.SetActive(true);
            if (Input.GetMouseButtonDown(0) || t)
            {
                GameManager.currentLevel = int.Parse("" + name[name.Length - 1]) - 1;
                Application.LoadLevel("Main");
            }
	    }
	    else
	    {
	        myChild.SetActive(false);
	    }
	}
}
