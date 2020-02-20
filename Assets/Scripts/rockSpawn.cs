﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockSpawn : MonoBehaviour
{
    public List<GameObject> rock;
    public List<Transform> spawn;
    public float reload = 3f;
    private float timeTill;
    void Update()
    {
        timeTill += Time.deltaTime;
        if (timeTill >= reload)
        {
            Instantiate(rock[Random.Range(0,rock.Count)], spawn[Random.Range(0, spawn.Count)].position, Quaternion.identity);
            timeTill = 0;
        }
        if (spaceMove.scoreSpa % 3 == 0 && reload >1)
        {
            reload = 3 - spaceMove.scoreSpa / 3;
        }
    }
}
