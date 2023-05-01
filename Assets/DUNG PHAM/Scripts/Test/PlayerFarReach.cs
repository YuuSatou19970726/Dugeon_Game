using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarReach : MonoBehaviour
{
    PlayerController playerController;
    TargetJoint2D joint;
    Collider2D target;
    string GROUND = "Ground";
    string GRAPPLE = "Grapplable";
    Vector2 grapVelocity;
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        joint = GetComponent<TargetJoint2D>();
    }
    void Start()
    {
        joint.enabled = false;
    }

    void Update()
    {
        GetConnectPoint();

        if (playerController.playerProperties.isRightMousePress && target) ShootAnchor(target.transform.position);

        ReleaseAnchor();
    }

    void GetConnectPoint()
    {
        target = Physics2D.OverlapCircle(transform.position, playerController.playerProperties.maxRange, playerController.playerProperties.farReachLayer);

        if (!target) return;

        HighlightObject();
    }
    void HighlightObject()
    {
        // if (!target) return;

        target.transform.RotateAround(target.transform.position, Vector3.up, 120 * Time.deltaTime);
    }
    void ShootAnchor(Vector3 target)
    {
        joint.enabled = true;
        joint.target = target;
    }
    void ReleaseAnchor()
    {
        if (!target) return;

        float distance = Vector2.Distance(target.transform.position, transform.position);

        if (distance < 0.5f)
        {
            joint.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(GRAPPLE))
        {
            Vector3 direction = transform.position - other.transform.position;
            playerController.playerRigid.AddForce(-direction * playerController.playerProperties.spotBoostForce, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerController.playerProperties.maxRange);
    }
}
