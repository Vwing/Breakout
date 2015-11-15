using UnityEngine;
using System.Collections;

public class MoveWall : MonoBehaviour
{
    private float radius;

    void Awake()
    {
        radius = GetComponent<Renderer>().bounds.extents.y;
    }

	public void Up()
    {
        StartCoroutine("MoveUp");
    }

    public void Down()
    {
        StartCoroutine("MoveDown");
    }

    IEnumerator MoveUp()
    {
        return null;
    }

    IEnumerator MoveDown()
    {
        return null;
    }
}
