using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class AimStateManager : MonoBehaviour
{
    [HideInInspector] public AimBaseState currentState;
    [HideInInspector] public HipfireStete hip = new HipfireStete();
    [HideInInspector] public AimState aim = new AimState();

    [HideInInspector] public InputAxis xAxis, yAxis;
    [HideInInspector] public PlayerInput playerInput;
    [SerializeField] Transform camFollowPos;
    [SerializeField] Transform firePos;
    [SerializeField] Transform gunAimPos;
    [SerializeField] float Sensitivity = 1;
    AimUI aimUI;


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

    float xFollowPos;
    float yFollowPos, ogYPos;
    [SerializeField] float crouchCamHeight = 0.6f;
    [SerializeField] float shoulderSwapSpeed = 10;
    MovementStateManager movementStateManager;

    public Image crosshair;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementStateManager = GetComponent<MovementStateManager>();
        xFollowPos = camFollowPos.localPosition.x;
        ogYPos = camFollowPos.localPosition.y;
        yFollowPos = ogYPos;
        vCam = GetComponentInChildren<CinemachineCamera>();
        hipFOV = vCam.Lens.FieldOfView;
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        aimUI = GetComponentInChildren<AimUI>();
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

        currentState.UpdateState(this);

        if(movementStateManager.currentState == movementStateManager.Run) 
        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, currentFOV + 10, fovSmoothSpeed * Time.deltaTime);
        else
        vCam.Lens.FieldOfView = Mathf.Lerp(vCam.Lens.FieldOfView, currentFOV, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = cam.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }

        Ray ray1 = new Ray(firePos.position, (aimPos.position - firePos.position).normalized);
        if(Physics.Raycast(ray1, out RaycastHit hit1, Mathf.Infinity, aimMask))
        {
            gunAimPos.position = hit1.point;
        }

        if (Vector3.Distance(gunAimPos.position, aimPos.position) > 0.01f)
        {
            aimUI.IsDifferent = true;
        }
        else
        {
            aimUI.IsDifferent = false;
        }
        MoveCamera();

        if(Physics.Raycast(ray, out RaycastHit hit2, Mathf.Infinity, aimMask))
        {
            if(hit2.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                crosshair.color = Color.red;
            else crosshair.color = Color.white;
        }
        else
        {
            crosshair.color = Color.white;
        }
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    void MoveCamera()
    {
        if (playerInput.actions["Switch"].WasPressedThisFrame())
        {
            xFollowPos = -xFollowPos;
        }
        if (movementStateManager.currentState == movementStateManager.Crouch) yFollowPos = crouchCamHeight;
        else yFollowPos = ogYPos;

        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, camFollowPos.localPosition.z);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);
    }
}
