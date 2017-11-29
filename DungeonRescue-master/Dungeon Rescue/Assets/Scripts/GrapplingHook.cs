using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float distance = 5.0f;
    public LayerMask mask;
    public LineRenderer line;
    public float step = 0.02f;
    float dist;
    public static bool swinging = false;
    public GameObject hook;
    GameObject hook1;

    // Use this for initialization
    void Start () {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;

        dist = Vector3.Distance(targetPos, transform.position);

        /*
        if (dist < 6)
        {

            Debug.Log("hi");
        }
        else
        {
            Debug.Log("cmonbruh");
        }*/


        if (Input.GetMouseButtonDown(0) && OnHover.isHovered && dist < 7) //&& Vector2.Distance(transform.position, targetPos) < 4)
        {
            swinging = true;
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
            Debug.Log("hook");
            
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                //joint.distance = Vector2.Distance(transform.position, hit.point);

                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                //joint.distance = Vector2.Distance(transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);

                float x = hit.point.x;
                float y = hit.point.y;

                hook1 = Instantiate(hook);
                hook1.transform.position = hit.point;


            }

        }

        //line.SetPosition(1, joint.connectedBody.transform.TransformPoint(joint.connectedAnchor));

        if (Input.GetMouseButton(0) && OnHover.isHovered)
        {
            //Debug.Log("Getkey");
            line.SetPosition(0, transform.position);

        }

        if (Input.GetMouseButtonUp(0))
        {
            swinging = false;
            OnHover.isHovered = false;
            Debug.Log("Getkeyup");
            line.startWidth = 0;
            line.endWidth = 0;
            //line.SetPosition(1, transform.position);
            joint.enabled = false;
            Destroy(hook1);
        }
        //line.SetPosition(1, transform.position);
    }

}
