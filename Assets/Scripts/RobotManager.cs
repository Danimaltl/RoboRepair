using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Robot
{
    public int ID;
    private List<Limb> limbList;

    public float value;

    public string type;

    public Robot(int ID, float minVal)
    {
        this.ID = ID;
        CreateRobot(minVal);
    }

    private void CreateRobot(float minVal)
    {
        limbList = new List<Limb>();
        int i = 0;
        Debug.Log("MinVal: " + minVal);
        while(minVal > value)
        {
            Limb newLimb = new Limb(i);
            value += newLimb.value;
            limbList.Add(newLimb);
            i++;
        }

    }



    public class Limb
    {
        public int ID;
        public int quality;
        public float value;

        GameObject limbObject;

            

        public Limb(int type)
        {

            switch(type)
            {
                case 0: CreateTorso();
                    break;
                case 1: CreateHead();
                    break;
                default: CreateLimb();
                    break;
            }
        }

        private void CreateLimb()
        {
            var dLimb = (GameObject)Resources.Load("dLimb", typeof(GameObject));
            quality = Random.Range(0, 100);
            if (quality != 0)
                value = Random.Range(100, 1000);
            else
                value = 0;

            limbObject = Object.Instantiate(dLimb);
        }

        private void CreateTorso()
        {
            quality = 100;
            value = 100;
            limbObject = Object.Instantiate(Resources.Load("dTorso", typeof(GameObject))) as GameObject;
        }

        private void CreateHead()
        {
            var dHead = (GameObject)Resources.Load("dHead", typeof(GameObject));
            quality = 100;
            value = 100;
            limbObject = Object.Instantiate(dHead);
        }
    }
}
