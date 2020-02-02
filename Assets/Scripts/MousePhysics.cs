using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Raycasting based on https://sharpcoderblog.com/blog/drag-rigidbody-with-mouse-cursor-unity-3d-tutorial
public class MousePhysics : MonoBehaviour
{
    public float forceAmount = 500;

    Limb selectedLimb;
    Rigidbody selectedRigidbody;
    CharacterJoint selectedJoint;

    Camera cameraComp;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;

    

    void Start()
    {
        cameraComp = GetComponent<Camera>();
    }

    void Update()
    {
        if (!cameraComp)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            selectedRigidbody = GetRigidbody();
            if (selectedRigidbody)
            {
                selectedLimb = selectedRigidbody.GetComponent<Limb>();
                selectedJoint = selectedRigidbody.GetComponent<CharacterJoint>();
                if (selectedLimb && selectedJoint)
                {
                    Debug.Log("Setting break force.");
                    selectedLimb.SetColor(Color.red);
                    selectedJoint.breakForce = selectedLimb.breakForce;
                }
            }
        }
        if (selectedRigidbody && selectedLimb && !selectedJoint)
        {
            selectedRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            selectedLimb.transform.rotation = selectedLimb.savedOrientation;
            if (Vector3.Distance(selectedLimb.transform.position, selectedLimb.jointParent.transform.position) < selectedLimb.attachThreshold)
            {
                selectedLimb.SetColor(Color.green);
            } else
            {
                selectedLimb.SetColor(Color.yellow);
            }

        }
        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
        {
            if (selectedLimb)
            {
                selectedLimb.SetColor(Color.white);
                if (!selectedJoint && (Vector3.Distance(selectedLimb.transform.position, selectedLimb.jointParent.transform.position) < selectedLimb.attachThreshold))
                {
                    selectedRigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ;
                    selectedLimb.gameObject.AddComponent(typeof(CharacterJoint));
                    selectedLimb.joint = selectedLimb.GetComponent<CharacterJoint>();
                    selectedLimb.joint.autoConfigureConnectedAnchor = false;
                    selectedLimb.joint.connectedBody = selectedLimb.jointParent;
                    selectedLimb.joint.connectedAnchor = selectedLimb.savedConnectedAnchor;
                    selectedLimb.joint.anchor = selectedLimb.savedAnchor;
                    selectedJoint = selectedLimb.GetComponent<CharacterJoint>();
                } 
            }
            if (selectedJoint)
            {
                selectedJoint.breakForce = Mathf.Infinity;
                selectedJoint = null;
            } 
            selectedRigidbody = null;
            selectedLimb = null;
        }
    }

    private void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = cameraComp.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
        }
    }

    Rigidbody GetRigidbody()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = cameraComp.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.GetComponent<Rigidbody>())
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = cameraComp.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }
        return null;
    }
}
