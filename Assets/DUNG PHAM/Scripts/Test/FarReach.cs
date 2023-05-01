using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarReach : MonoBehaviour
{
    TargetJoint2D joint;
    public LayerMask grappableLayer;
    public float maxRange;
    Vector3 mousePos, direction;
    // Rigidbody2D rigid;
    Vector3 target;
    void Awake()
    {
        joint = GetComponent<TargetJoint2D>();
        // rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        AimAtMouse();

        if (Input.GetMouseButtonDown(0)) GetConnectPoint();
        ReleaseConnection(target);
        // if (Input.GetMouseButtonDown(1)) ReleaseConnection();
    }
    void AimAtMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void GetConnectPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRange, grappableLayer);
        if (hit.point != null)
        {
            target = hit.point;
        }
        ShootAnchor(target);
    }
    void ShootAnchor(Vector3 target)
    {
        joint.enabled = true;
        joint.target = target;

        // ReleaseConnection(target);
        // StartCoroutine(Wait());
    }
    void ReleaseConnection(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) > 0.5f) return;
        joint.enabled = false;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        // ReleaseConnection();
    }
}
