using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Robot : MonoBehaviour
{
    public int ID;
    private List<GameObject> limbList;

    public float value;

    public string type;

    public Robot(int ID, float minVal)
    {
        this.ID = ID;
        CreateRobot(minVal);
    }

    private void CreateRobot(float minVal)
    {
        limbList = new List<GameObject>();
        int i = 0;
        Debug.Log("MinVal: " + minVal);
        while(minVal > value && i < 7)
        {
            GameObject limbObject = Resources.Load("Limb") as GameObject;
            Limb newLimb = limbObject.GetComponent<Limb>();
            newLimb.Initialize(i);
            value += newLimb.value;
            limbList.Add(limbObject);
            i++;
        }

    }

}
