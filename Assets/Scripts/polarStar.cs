using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class polarStar : MonoBehaviour
{
    public GameObject level1beam;
    public GameObject level2beam;
    public GameObject level3beam;
    public Text ui_xp;
    public Text ui_lvl;

    public string debug_xp;
    public string debug_level;

    public int xp;
    public int xpmax;
    public enum level
    {
        one,
        two,
        max
    }

    public level currentLevel = level.one;
    public int levelUpOne;
    public int levelUpTwo;

    void Start()
    {

    }

    void Update()
    {
        xpDebug();
        xpCheck();
        shootCheck();
        debugStrings();
    }

    void xpDebug()  //give us xp when we press w; remove some when we press s
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(xp < xpmax)
            {
                xp += 2;    //2 at a time since it's easier
            }
        } else if(Input.GetKeyDown(KeyCode.S))
        {
            if(xp > 0)
            {
                xp -= 2;
            }
        }
    }

    void xpCheck()  //all xp logic
    {
        if(xp >= levelUpTwo)
        {
            currentLevel = level.max;
        } else if(xp >= levelUpOne)
        {
            currentLevel = level.two;
        } else if(xp < levelUpOne)
        {
            currentLevel = level.one;
        }
        if(xp > xpmax)
        {
            xp = xpmax; //failsafe
        }
    }

    void shootCheck()   //all the shoot logic; fire w space
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(currentLevel == level.one)
            {
                if(gameObject.transform.childCount < 1)
                {
                    GameObject newBeam = Instantiate(level1beam, new Vector3(transform.position.x + (0.5f * transform.localScale.x), transform.position.y), transform.rotation);
                    newBeam.transform.SetParent(gameObject.transform);
                }
                
            } else if(currentLevel == level.two)
            {
                if(gameObject.transform.childCount < 1)
                {
                    GameObject newBeam = Instantiate(level2beam, new Vector3(transform.position.x + (0.5f * transform.localScale.x), transform.position.y), transform.rotation);
                    newBeam.transform.SetParent(gameObject.transform);
                }
            } else if(currentLevel == level.max)
            {
                if(gameObject.transform.childCount < 2)
                {
                    GameObject newBeam = Instantiate(level3beam, new Vector3(transform.position.x + (0.5f * transform.localScale.x), transform.position.y), transform.rotation);
                    newBeam.transform.SetParent(gameObject.transform);
                }
            }
        }
    }

    void debugStrings()   //making strings for debug numbers etc
    {
        debug_xp = "XP: " + xp;
        debug_level = "Level " + currentLevel;
        ui_xp.text = debug_xp;
        ui_lvl.text = debug_level;
    }
}
