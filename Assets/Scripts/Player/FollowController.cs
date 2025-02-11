using UnityEngine;
using UnityEngine.XR;

public class FollowController : MonoBehaviour
{
    public XRNode controllerNode = XRNode.LeftHand; // Default: Left Controller
    private InputDevice device;

    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    void Update()
    {
        if (device.isValid)
        {
            device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

            transform.position = position + (rotation * new Vector3(0, 0.2f, 0)); // UI di atas controller
            transform.rotation = rotation;
        }
    }
}
