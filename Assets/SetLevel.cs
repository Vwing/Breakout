using UnityEngine;
using System.Collections;

public class SetLevel : MonoBehaviour
{
    public int level = 1;
    void Awake()
    {
        GameManager.currentLevel = level;
    }
}
