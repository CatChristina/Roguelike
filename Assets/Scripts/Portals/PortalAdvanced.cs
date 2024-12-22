using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PortalAdvanced : MonoBehaviour
{
    /*
    [field: SerializeField]
    public PortalAdvanced OtherPortal { get; private set; }

    [SerializeField]
    Renderer outlineRenderer;

    [field: SerializeField]
    public Color PortalColour { get; private set; }

    [SerializeField]
    LayerMask placementMask;

    [SerializeField]
    Transform testTransform;

    List<PortalableObjects> portalObjects = new List<PortalableObjects>();
    public bool isPlaced { get; private set; } = false;
    Collider wallCollider;

    public Renderer Renderer { get; private set; }
    new BoxCollider collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        Renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        outlineRenderer.material.SetColor("_OutlineColour", PortalColour);

        gameObject.SetActive(false);
    }

    private void Update()
    {
        Renderer.enabled = OtherPortal.isPlaced;

        for (int i = 0; i < portalObjects.Count; i++)
        {
            Vector3 objPos = transform.InverseTransformDirection(portalObjects[i]).transform.position;

            if (objPos.z > 0)
            {
                portalObjects[i].Warp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<PortalableObject>();

        if (obj != null)
        {
            portalObjects.Add(obj);
            obj.SetIsInPortal(this, OtherPortal, wallCollider);
        }
    }

    public bool PlacePortal(Collider wallCollider, Vector3 pos, Quaternion rot)
    {
        testTransform.position = pos;
        testTransform.rotation = rot;
        testTransform.position -= testTransform.forward * 0.001f;

        FixOverhangs();
        FixIntersects();

        if (CheckOverlap())
        {
            this.wallCollider = wallCollider;
            transform.position = testTransform.position;
            transform.rotation = testTransform.rotation;

            gameObject.SetActive(true);
            isPlaced = true;
            return true;
        }

        return false;
    }

    void FixOverhangs()
    {
        var testPoints = new List<Vector3>
        {
            new Vector3 (-1.1f, 0, 0.1f),
            new Vector3 (1.1f, 0, 0.1f),
            new Vector3 (0, -2.1f, 0.1f),
            new Vector3 (0, 2.1f, 0.1f)
        };

        var testDirections = new List<Vector3>
        {
            Vector3.right,
            -Vector3.right,
            Vector3.up,
            -Vector3.up
        };

        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;
            Vector3 raycastPos = testTransform.TransformPoint(testPoints[i]);
            Vector3 raycastDirection = testTransform.TransformDirection(testDirections[i]);

            if (Physics.CheckSphere(raycastPos, 0.05f, placementMask))
            {
                break;
            }
            else if (Physics.Raycast(raycastPos, raycastDirection, out hit, placementMask))
            {
                var offset = hit.point - raycastPos;
                testTransform.Translate(offset, Space.World);
            }
        }
    }

    void FixIntersects()
    {
        var testDirections = new List<Vector3>
        {
            Vector3.right,
            -Vector3.right,
            Vector3.up,
            -Vector3.up
        };

        var testDistances = new List<float> { 1.1f, 1.1f, 2.1f, 2.1f };

        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;
            Vector3 raycastPos = testTransform.TransformPoint(0, 0, 0);
            Vector3 raycastDirection = testTransform.TransformDirection(testDirections[i]);

            if (Physics.Raycast(raycastPos, raycastDirection, testDistances, out hit, placementMask))
            {
                var offset = (hit.point - raycastPos);
                var newOffset = -raycastDirection * (testDistances[i] - offset.magnitude);
                testTransform.Translate(newOffset, Space.World);
            }
        }
    }

    private bool CheckOverlap()
    {
        return false;
    }
    */
}