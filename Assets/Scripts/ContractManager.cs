using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    Queue<Contract> contractQueue;
    int nextID;
    // Start is called before the first frame update
    void Start()
    {
        contractQueue = new Queue<Contract>();
        nextID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            float min, max;
            min = Random.Range(300, 1000);
            max = Random.Range(min, min * 5);
            Contract newContract = new Contract(nextID, min, max);
            nextID++;
            contractQueue.Enqueue(newContract);
        }

        if(Input.GetKeyDown("t"))
        {
            foreach (Contract contract in contractQueue)
            {
                Debug.Log("ID: " + contract.ID);
                Debug.Log("Min val: " + contract.minVal);
                Debug.Log("Max val: " + contract.maxVal);
                Debug.Log("===========================");
            }
        }
    }


    internal class Contract
    {
        public int ID { get; }
        public float minVal, maxVal;
        public Robot cRobot;

        internal Contract(int ID, float minVal, float maxVal)
        {
            this.ID = ID;
            this.minVal = minVal;
            this.maxVal = maxVal;
            cRobot = new Robot(this.ID, this.minVal);
        }


    }
}
