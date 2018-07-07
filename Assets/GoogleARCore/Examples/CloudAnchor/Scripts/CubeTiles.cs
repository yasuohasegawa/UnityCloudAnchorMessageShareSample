using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GoogleARCore.Examples.CloudAnchor;

public class CubeTiles : MonoBehaviour {

    [SerializeField]
    private GameObject tile;

    private Camera arkitCam;
    private Camera arcoreCam;
    private float rayDistance = 100f;

	void Awake()
	{
        arkitCam = CloudAnchorController.GetInstance().arkitCam;
        arcoreCam = CloudAnchorController.GetInstance().arcoreCam;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(tile != null){
            if (CloudAnchorController.GetInstance().data.touch == 0)
            {
                tile.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            else
            {
                tile.GetComponent<MeshRenderer>().material.color = Color.green;
            }   
        }

        if(OnTouchDown()){
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, rayDistance)){
                UpdateTile(hit.collider.gameObject);
            }
        }

	}

    private void UpdateTile(GameObject target) {

        int touch = CloudAnchorController.GetInstance().data.touch;
        if(touch == 0){
            target.GetComponent<MeshRenderer>().material.color = Color.green;
            touch = 1;
        } else {
            target.GetComponent<MeshRenderer>().material.color = Color.white;
            touch = 0;
        }

        if (CloudAnchorController.GetInstance().m_CurrentMode == CloudAnchorController.ApplicationMode.Hosting) {
            CloudAnchorController.GetInstance().RoomSharingServer.SentTouchToClient(touch);
        } else {
            CloudAnchorController.GetInstance().roomSharingClient.SendTouchToHost(touch);
        }

        CloudAnchorController.GetInstance().data.touch = touch;
    }

    private bool OnTouchDown() {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray;

                Vector3 touchPos = touch.position;
                if(arkitCam.gameObject.activeSelf){
                    ray = arkitCam.ScreenPointToRay(touchPos);
                } else {
                    ray = arcoreCam.ScreenPointToRay(touchPos);
                }

                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, rayDistance))
                {
                    UpdateTile(hit.collider.gameObject);
                    return true;
                }
            }
        }

        return false;
    }

}
