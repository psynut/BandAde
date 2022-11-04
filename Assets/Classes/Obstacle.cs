using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool passable = false;

    public void ClearObstacle() {
        Destroy(this);
    }
}
