using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class statDisp : MonoBehaviour
{
    public List<Text> stDisp = new List<Text>();
    public GameObject stater;
    public Transform pareent;

    private Vector3 lastEndPosition;
    private void Start()
    {
        lastEndPosition = stater.GetComponent<Transform>().Find("spawnPoint").position;
        for (int x = 0; x < WordBase.termData.groupScore.Count; x++)
        {
            SpawnLevelPart();
            Debug.Log("woek");
        }

    }
    void FixedUpdate()
    {   
        for(int x =0; x<WordBase.termData.groupScore.Count; x++)
        {
            stDisp[x].text = WordBase.termData.groupScore.ElementAt(x).Key + ": " + WordBase.termData.groupScore.ElementAt(x).Value;

        }
    }
        private void SpawnLevelPart()
        {
            Transform lastLevelPartTransform = SpawnLevelPart(stater.GetComponent<Transform>(), lastEndPosition);
            lastEndPosition = lastLevelPartTransform.Find("spawnPoint").position;
        }
        private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
        {
            Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        levelPartTransform.transform.parent = pareent.transform;
        stDisp.Add(levelPartTransform.Find("score0").GetComponent<Text>());
        return levelPartTransform;
        }
    }
