using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LimbTypes
{
    head,upperTorso,lowerTorso,leftArm,rightArm,leftLeg,rightLeg
}
public class Limb : MonoBehaviour
{
    //Game Data
    public int ID;
    public int quality;
    public float value;

    //Physics Data
    MeshRenderer mesh;
    public Rigidbody jointParent;
    public bool jointBroken = false;
    public float attachThreshold = 1;
    public CharacterJoint joint;
    public Vector3 savedAnchor;
    public Vector3 savedConnectedAnchor;
    public Quaternion savedOrientation;
    public float breakForce = 600;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        joint = GetComponent<CharacterJoint>();
        savedConnectedAnchor = joint.connectedAnchor;
        savedAnchor = joint.anchor;
        savedOrientation = transform.rotation;
    }

    void Update()
    {
        //if (GetComponents<CharacterJoint>().Length > 1)
        //{
        //    Debug.LogError("Multiple joints on limb!");
        //    Debug.DebugBreak();
        //}
    }

    public void SetColor(Color color)
    {
        mesh.material.SetColor("_Color", color);
    }

    public void Initialize(int type)
    {

        switch (type)
        {
            case 0:
                CreateHead();
                break;
            case 1:
                CreateTorso();
                break;
            default:
                CreateLimb();
                break;
        }
    }

    private void CreateLimb()
    {
        quality = Random.Range(1, 100);
        value = Random.Range(100, 1000);

    }

    private void CreateTorso()
    {
        quality = 100;
        value = 100;
    }

    private void CreateHead()
    {
        quality = 100;
        value = 100;
        
    }
}
