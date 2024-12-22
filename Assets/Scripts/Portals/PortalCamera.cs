using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class PortalCamera : MonoBehaviour
{
    /*
    PortalAdvanced[] portals = new PortalAdvanced[2];

    Camera portalCamera;
    Camera mainCamera;

    int iterations = 5;

    RenderTexture portalTexture1;
    RenderTexture portalTexture2;

    

    void Awake()
    {
        mainCamera = Camera.main;
        portalTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        portalTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }

    private void Start()
    {
        portals[0].GetComponent<Material>().mainTexture = portalTexture1;
        //portals[0].GetComponent<Material>().SetTexture("_EmissionMap", portalTexture1);
        portals[1].GetComponent<Material>().mainTexture = portalTexture2;
        //portals[0].GetComponent<Material>().SetTexture("_EmissionMap", portalTexture2);
    }

    

    private void OnEnable()
    {
        RenderPipeline.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable()
    {
        RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(ScriptableRenderContext src, Camera camera)
    {
        if (!portals[0].isPlaced || !portals[1].isPlaced)
        {
            return;
        }

        if (portals[0].GetComponent<Renderer>().isVisible)
        {
            portalCamera.targetTexture = portalTexture1;
            for (int i = iterations - 1; i >= 0; i--)
            {
                RenderCamera(portals[0], portals[1], i, src);
            }
        }

        if (portals[1].GetComponent<Renderer>().isVisible)
        {
            portalCamera.targetTexture = portalTexture2;
            for (int i = iterations - 1; i >= 0; i--)
            {
                RenderCamera(portals[1], portals[0], i, src);
            }
        }
    }

    void RenderCamera(PortalAdvanced inPortal, PortalAdvanced outPortal, int iterationNum, ScriptableRenderContext src)
    {
        Transform inTransform = inPortal.transform;
        Transform outTransform = outPortal.transform;
        Transform cameraTransform = portalCamera.transform;

        cameraTransform.position = transform.position;
        cameraTransform.rotation = transform.rotation;

        for (int i = 0; i <= iterationNum; i++)
        {
            //Moves camera behind the portal
            Vector3 relativePos = inTransform.InverseTransformPoint(cameraTransform.position);
            relativePos = Quaternion.Euler(0, 180f, 0) * relativePos;
            cameraTransform.position = outTransform.TransformPoint(relativePos);

            //Rotate camera to look through the portal
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * cameraTransform.rotation;
            relativeRot = Quaternion.Euler(0, 180f, 0) * relativeRot;
            cameraTransform.rotation = outTransform.rotation * relativeRot;
        }

        Plane plane = new Plane(-outTransform.forward, outTransform.position);
        Vector4 clipPlaneWorldSpace = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
        Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;

        var newMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        portalCamera.projectionMatrix = newMatrix;

        HDRenderPipeline.SubmitRenderRequest(portalCamera, src);
    }

    void Update()
    {
        
    }
    */
}
