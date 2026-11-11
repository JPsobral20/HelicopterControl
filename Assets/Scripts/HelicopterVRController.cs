using UnityEngine;
using Valve.VR;

public class HelicopterVRController : MonoBehaviour
{
    [Header("Movement Speeds")]
    public float moveSpeed = 15.0f;
    public float liftSpeed = 10.0f;
    [Header("Rotation")]
    public float rotationSpeed = 60.0f;

    [Header("SteamVR Input")]
    public SteamVR_Input_Sources controlHand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Pose poseAction = null;

    // Choose one of these in the Inspector:
    [Header("Lift Input Options (pick one)")]
    [Tooltip("Use a Vector2 (thumbstick) and read .y")]
    public SteamVR_Action_Vector2 thumbstickAction = null;

    [Tooltip("Or use an analog single (trigger/grip) for lift")]
    public SteamVR_Action_Single analogLiftAction = null;

    [Tooltip("Fallback boolean buttons (if you want discrete up/down)")]
    public SteamVR_Action_Boolean liftUpButton = null;
    public SteamVR_Action_Boolean liftDownButton = null;

    [Header("Motion Control Settings")]
    public float maxMoveDistance = 0.5f;
    public float moveDeadZone = 0.05f;
    public float maxTwistAngle = 45.0f;
    public float rotateDeadZone = 5.0f;

    private Vector3 neutralPosition;
    private Quaternion neutralRotation;

    void Start()
    {
        // Wait a frame or two in case SteamVR needs to initialize, then recenter
        Invoke(nameof(RecenterController), 0.5f);
    }

    public void RecenterController()
    {
        if (poseAction != null)
        {
            neutralPosition = poseAction.GetLocalPosition(controlHand);
            neutralRotation = poseAction.GetLocalRotation(controlHand);
        }
    }

    void Update()
    {
        if (poseAction == null) return;

        // --- POSE ---
        Vector3 currentPosition = poseAction.GetLocalPosition(controlHand);
        Quaternion currentRotation = poseAction.GetLocalRotation(controlHand);

        // --- FORWARD/BACKWARD by Z movement ---
        float moveInput = currentPosition.z - neutralPosition.z;
        float moveAmount = 0f;
        if (Mathf.Abs(moveInput) > moveDeadZone)
        {
            float activeMove = moveInput - Mathf.Sign(moveInput) * moveDeadZone;
            moveAmount = Mathf.Clamp(activeMove / (maxMoveDistance - moveDeadZone), -10f, 10f);
        }
        transform.Translate(Vector3.forward * moveAmount * moveSpeed * Time.deltaTime);

        // --- ROTATION (yaw) by wrist twist ---
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(neutralRotation);
        float twistInput = deltaRotation.eulerAngles.y;
        if (twistInput > 180f) twistInput -= 360f;
        float rotateAmount = 0f;
        if (Mathf.Abs(twistInput) > rotateDeadZone)
        {
            float activeTwist = twistInput - Mathf.Sign(twistInput) * rotateDeadZone;
            rotateAmount = Mathf.Clamp(activeTwist / (maxTwistAngle - rotateDeadZone), -10f, 10f);
        }
        transform.Rotate(Vector3.up, rotateAmount * rotationSpeed * Time.deltaTime);

        // --- ALTITUDE (lift) --- 
        // Priority: 1) thumbstick.y (analog), 2) analogLiftAction, 3) boolean buttons
        float liftValue = 0f;

        if (thumbstickAction != null)
        {
            Vector2 stick = thumbstickAction.GetAxis(controlHand);
            // positive Y = up on most controllers; adjust sign if reversed
            liftValue = stick.y;
        }
        else if (analogLiftAction != null)
        {
            liftValue = analogLiftAction.GetAxis(controlHand);
            // To allow negative (down) with a single trigger, you could interpret e.g. trigger - otherTrigger
        }
        else
        {
            if (liftUpButton != null && liftUpButton.GetState(controlHand)) liftValue += 1f;
            if (liftDownButton != null && liftDownButton.GetState(controlHand)) liftValue -= 1f;
        }

        // Apply deadzone for analog lift
        if (Mathf.Abs(liftValue) > 0.05f)
        {
            transform.Translate(Vector3.up * liftValue * liftSpeed * Time.deltaTime, Space.World);
        }
    }
}
