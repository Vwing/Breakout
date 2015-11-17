using UnityEngine;
using System.Collections;

public class MoveWall : MonoBehaviour
{
    private float radius;
    private Vector3 down;
    private Vector3 up;
    public bool moving;

    void Awake()
    {
        radius = GetComponent<Renderer>().bounds.extents.y;
        down = transform.position;
        up = transform.position + Vector3.up * radius * 2;
    }

	public void Up(float time)
    {
        StartCoroutine(MoveUp(time));
    }

    public void Down(float time)
    {
        StartCoroutine(MoveDown(time));
    }

    IEnumerator MoveUp(float time)
    {
        moving = true;
        for (float i = 0f; i <= time; i += 0.05f )
        {
            transform.position = Vector3.Lerp(down, up, i / time);
            yield return new WaitForSeconds(0.05f);
        }
        moving = false;
    }

    IEnumerator MoveDown(float time)
    {
        moving = true;
        for (float i = time; i >= 0; i -= 0.05f)
        {
            transform.position = Vector3.Lerp(down, up, i / time);
            yield return new WaitForSeconds(0.05f);
        }
        moving = false;
    }
}
