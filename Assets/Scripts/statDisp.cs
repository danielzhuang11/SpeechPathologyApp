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
    public RectTransform rT;

    private Vector3 lastEndPosition;
    public Transform statweer;
    private int worLis;
    private void Start()
    {
        //try to put top of pareent back at 0 
        worLis = 60 * (WordBase.termData.groupScore.Count-13);
        //rT.sizeDelta = new Vector2(rT.sizeDelta.x, rT.sizeDelta.y + worLis);
        //statweer = stater.GetComponent<Transform>();
        
        //rT.offsetMax = new Vector2(rT.offsetMax.x, 0);
        rT.offsetMin = new Vector2(rT.offsetMin.x, rT.offsetMin.y-worLis);
        
        statweer.localPosition = new Vector2(statweer.localPosition.x, statweer.localPosition.y + worLis / 2);
        lastEndPosition = stater.GetComponent<Transform>().Find("spawnPoint").position;
        for (int x = 0; x < WordBase.termData.groupScore.Count; x++)
        {
            SpawnLevelPart();
            
        }
       

    }
    void FixedUpdate()
    {
        //Debug.Log(worLis);
       
        for (int x =0; x<WordBase.termData.groupScore.Count; x++)
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
