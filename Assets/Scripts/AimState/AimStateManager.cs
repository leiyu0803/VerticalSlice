using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipfireStete hip = new HipfireStete();
    public AimState aim = new AimState();

    public InputAxis xAxis, yAxis;
    [HideInInspector] public PlayerInput playerInput;
    [SerializeField] Transform camFollowPos;
    [SerializeField] float Sensitivity = 1;
    [SerializeField] AnimationCurve switchCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private bool isSwitching = false;
    [SerializeField] float switchDuration = 0.5f;

    [HideInInspector] public Animator animator;
    [HideInInspector] public CinemachineCamera vCam;
    public Camera cam;
    public float adsFOV = 40;
    [HideInInspector] public float hipFOV;
    [HideInInspector] public float currentFOV;
    public float fovSmoothSpeed = 10;

    public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineCamera>();
        hipFOV = vCam.Lens.FieldOfView;
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SwitchState(hip);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = playerInput.actions["Look"].ReadValue<Vector2>();
        xAxis.Value += input.x * Sensitivity;
        yAxis.Value -= input.y * Sensitivity;
        yAxis.Value = Mathf.Clamp(yAxis.Value, -60, 60);
        Switch();

        currentState.UpdateState(this);

        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, currentFOV, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = cam.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
    void Switch()
    {
        if (playerInput.actions["Switch"].WasPressedThisFrame() && !isSwitching)
        {
            StartCoroutine(SmoothSwitch());
        }
    }

    System.Collections.IEnumerator SmoothSwitch()
    {
        isSwitching = true;
        float startX = camFollowPos.localPosition.x;
        float targetX = -startX;
        float elapsedTime = 0f;

        while (elapsedTime < switchDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / switchDuration;
            float curvedT = switchCurve.Evaluate(t);
            float newX = Mathf.Lerp(startX, targetX, curvedT);
            camFollowPos.localPosition = new Vector3(newX, camFollowPos.localPosition.y, camFollowPos.localPosition.z);
            yield return null;
        }

        camFollowPos.localPosition = new Vector3(targetX, camFollowPos.localPosition.y, camFollowPos.localPosition.z);
        isSwitching = false;
    }

    public void SwitchState(AimBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
