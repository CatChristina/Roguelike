using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class CameraMovement : MonoBehaviour
{
    public float sensX = 75;
    public float sensY = 75;

    public Transform cameraPosition;

    private float mouseX;
    private float mouseY;

    private float xRotation;
    private float yRotation;

    private NativeArray<float> rotationData;
    private JobHandle jobOut;

    private void Awake()
    {
        // Uses a native array to store the rotation data so that it can be accessed by the job thread (ALWAYS destroy this somewhere in the script!!!)
        rotationData = new NativeArray<float>(2, Allocator.Persistent);
    }

    // Clears the memory when the script is destroyed to avoid memory leaks
    private void OnDestroy()
    {
        if (rotationData.IsCreated)
        {
            rotationData.Dispose();
        }
    }

    // Gets mouse input and runs the other functions
    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        ScheduleJob();
    }

    // LateUpdate is called after Update each frame
    private void LateUpdate()
    {
        jobOut.Complete();

        xRotation = rotationData[0];
        yRotation = rotationData[1];

        MousePos();
    }

    // Schedule the job to a new thread
    private void ScheduleJob()
    {
        rotationData[0] = xRotation;
        rotationData[1] = yRotation;

        CameraJob job = new()
        {
            mouseX = mouseX,
            mouseY = mouseY,
            deltaTime = Time.deltaTime,
            sensX = sensX,
            sensY = sensY,
            rotationData = rotationData
        };

        jobOut = job.Schedule();
    }

    // Job that handles the cameras rotation, storing the data within the NativeArray
    [BurstCompile]
    private struct CameraJob : IJob
    {
        public float mouseX;
        public float mouseY;
        public float deltaTime;
        public float sensX;
        public float sensY;

        public NativeArray<float> rotationData;

        public void Execute()
        {
            float xRotation = rotationData[0];
            float yRotation = rotationData[1];

            yRotation += mouseX * deltaTime * sensX * 30f;
            xRotation -= mouseY * deltaTime * sensY * 30f;

            xRotation = Mathf.Clamp(xRotation, -89f, 89f);

            rotationData[0] = xRotation;
            rotationData[1] = yRotation;
        }
    }

    // Update camera position and rotation
    private void MousePos()
    {
        cameraPosition.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        transform.position = cameraPosition.position + new Vector3(0, 0.5f, 0);
    }
}
