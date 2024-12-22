using UnityEngine;

public class PortalPlacement : MonoBehaviour
{
    /*
    [SerializeField]
    PortalPair portals;
    [SerializeField]
    LayerMask layerMask;
    Camera_Movement camMovement;

    private void Awake()
    {
        camMovement = GetComponent<Camera_Movement>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FirePortal(0, transform.position, transform.forward, 100f);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            FirePortal(1, transform.position, transform.forward, 100f);
        }
    }

    void FirePortal(int portalID, Vector3 pos, Vector3 dir, float distance)
    {
        RaycastHit hit;
        Physics.Raycast(pos, dir, out hit, distance, layerMask);

        if (hit.collider.CompareTag("Portal"))
        {
            var inPortal = hit.collider.GetComponent<PortalAdvanced>();

            if (inPortal == null)
            {
                return;
            }

            var outPortal = inPortal.OtherPortal;

            Vector3 relativePos = inPortal.transform.InverseTransformPoint(hit.point + dir);
            relativePos = Quaternion.Euler(0, 180, 0) * relativePos;
            pos = outPortal.transform.TransformPoint(relativePos);

            Vector3 relativeDir = inPortal.transform.InverseTransformDirection(dir);
            relativeDir = Quaternion.Euler(0, 180, 0) * relativeDir;
            dir = outPortal.transform.TransformDirection(relativeDir);

            distance -= Vector3.Distance(pos, hit.point);

            FirePortal(portalID, pos, dir, distance);
            return;
        }

        var cameraRot = camMovement.transform.rotation;
        var portalRight = cameraRot * Vector3.right;

        if (Mathf.Abs(portalRight.x) >= Mathf.Abs(portalRight.z))
        {
            portalRight = (portalRight.x >= 0) ? Vector3.right : -Vector3.right;
        }
        else
        {
            portalRight = (portalRight.z >= 0) ? Vector3.forward : -Vector3.forward;
        }

        var portalForward = -hit.normal;
        var portalUp = -Vector3.Cross(portalRight, portalForward);

        var portalRot = Quaternion.LookRotation(portalForward, portalUp);

        bool wasPlaced = portals.Portals[portalID].PlacePortal(hit.collider, hit.point, portalRot);

        if (wasPlaced)
        {
            //crosshair.SetPortalPlaced(portalID, true);
        }
    }
    */
}
