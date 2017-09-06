using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
        
	[SerializeField] float walkMoveStopRadius = 0.2f;
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        ProcessIndirectMovement ();

    }

	void ProcessIndirectMovement ()
	{
		if (Input.GetMouseButton (0)) {
			//print("Cursor raycast hit " + cameraRaycaster.hit.collider.gameObject.name.ToString());
			switch (cameraRaycaster.layerHit) {
			case Layer.Walkable:
				currentClickTarget = cameraRaycaster.hit.point;
				break;
			case Layer.Enemy:
				print ("Not moving to enemy");
				break;
			default:
				print ("Shouldn't be here");
				return;
			}
			// So not set in default case
		}
		var playerToClickPoint = currentClickTarget - transform.position;
		//print (currentClickTarget);
		if (playerToClickPoint.magnitude >= walkMoveStopRadius) {
			m_Character.Move (playerToClickPoint, false, false);
		}
		else {
			m_Character.Move (Vector3.zero, false, false);
		}
	}
}

