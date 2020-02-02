using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    MeshRenderer mesh;
    public Rigidbody jointParent;
    public bool jointBroken = false;
    public float attachThreshold = 1;
    public CharacterJoint joint;
    public Vector3 savedAnchor;
    public Vector3 savedConnectedAnchor;
    public Quaternion savedOrientation;
    public float breakForce = 600;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        joint = GetComponent<CharacterJoint>();
        savedConnectedAnchor = joint.connectedAnchor;
        savedAnchor = joint.anchor;
        savedOrientation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponents<CharacterJoint>().Length > 1)
        {
            Debug.LogError("Multiple joints on limb!");
            Debug.DebugBreak();
        }
    }

    public void SetColor(Color color)
    {
        mesh.material.SetColor("_Color", color);
    }
}
