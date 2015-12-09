using UnityEngine;
using System.Collections;

public class MenuSelectItem : MonoBehaviour
{
    private MenuSelect menu;
    private GameObject myChild;
	// Use this for initialization
	void Start ()
	{
	    menu = GameObject.Find("Player").GetComponent<MenuSelect>();
	    myChild = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	    if(menu.hitThing == transform)
            myChild.SetActive(true);
	    else
	    {
            myChild.SetActive(false);
	    }
	}
}
