using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState currentState;

    public DefaultState Default = new DefaultState();
    public ReloadState Reload = new ReloadState();

    public GameObject currentWeapon;
    [HideInInspector]public WeaponAmmo ammo;

    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Animator weaponAnimator;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public UIManager uIManager;

    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint lHandIK;

    float DrawTimer;
    void Start()
    {
        SwitchState(Default);
        playerInput = GetComponentInParent<PlayerInput>();
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
        animator = GetComponentInChildren<Animator>();
        weaponAnimator = currentWeapon.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        uIManager = GetComponent<UIManager>();
        animator.SetBool("Draw", true);
        weaponAnimator.SetBool("Draw", true);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        DrawTimer += Time.deltaTime;
        if (DrawTimer > 0.1f)
        {
            animator.SetBool("Draw", false);
            weaponAnimator.SetBool("Draw", false);
        }
    }

    public void SwitchState(ActionBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
    }
    public void ReloadWeapon()
    {
        ammo.Reload();
        SwitchState(Default);
    }
}
