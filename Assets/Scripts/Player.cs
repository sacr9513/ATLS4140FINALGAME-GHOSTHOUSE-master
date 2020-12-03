using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputHandler _input;

    private Transform pfHealthBar;
    private HealthSystem healthSystem;

    // jumping
    private Vector3 jump;
    [SerializeField]
    private float jumpForce;
    private bool isGrounded = true;
    [SerializeField]
    private Rigidbody rb;


    // movement
    [SerializeField]
    private bool RotateTowardMouse;

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private Camera Camera;

    public static Player Instance { get; private set; }

    private const float SPEED = 50f;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private FlashLight flashLight;
    //[SerializeField] private UI_Inventory uiInventory;
//
    //private Player_Base playerBase;

    private enum State {
        Normal,
    }

    

    private void Awake() {
        Instance = this;
        //playerBase = gameObject.GetComponent<Player_Base>();
        //SetStateNormal();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        healthSystem = new HealthSystem(100);
        Transform healthBarTransform = GameObject.FindGameObjectWithTag("PlayerHealth").transform;
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        inventory = GetComponent<Inventory>();
        flashLight = GetComponent<FlashLight>();
        _input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 10.0f,0.0f); // change this to change jump height
    
    }

    //private void OnTriggerEnter2D(Collider2D collider) {
    //    ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
    //    if (itemWorld != null) {
    //        // Touching Item
    //        inventory.AddItem(itemWorld.GetItem());
    //        itemWorld.DestroySelf();
    //    }
    //}
//
    //private void UseItem(Item item) {
    //    switch (item.type) {
    //    case ItemTypes.Battery:
    //        inventory.RemoveItem(new Item { type = Item.type.Battery, amount = 1 });
    //        // fill battery bar
    //        break;
    //    case ItemTypes.Key:
    //        inventory.RemoveItem(new Item { type = Item.type.Key, amount = 1 });
    //        break;
    //    case ItemTypes.Note:
    //        // read note
    //        break;
    //    }
    //}

    private void Update() {
        //switch (state) {
        //case State.Normal:
            HandleMovement();
            HandleJump();
            HandleFlashLight();
        //    break;
        //}
    }
    
    //private void SetStateNormal() {
    //    state = State.Normal;
    //}

 
        //USE FLASHLIGHT
    private void HandleFlashLight(){
        if(Input.GetKeyDown(KeyCode.F)){ // F to toggle flashlight on and off
            flashLight.UseFlashLight();
        }
    }
        //JUMP FUNCTIONS
    private void OnCollisionStay(){
        isGrounded = true;
    }
    private void HandleJump(){
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            //healthSystem.Damage(20);
            //inventory.UseItem(new Item("Battery", ItemTypes.Battery)); // for debugging purposes
        }
    }

    //MOVEMENT FUNCTIONS
    private void HandleMovement(){
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movementVector = MoveTowardTarget(targetVector);

        if (!RotateTowardMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        if (RotateTowardMouse)
        {
            RotateFromMouseVector();
        }
    }
    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if(movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }
    
    public Vector3 GetPosition() {
        return transform.position;
    }

    public void Heal(int amount){
        healthSystem.Heal(amount);
    }
    public void TakeDamage(int amount){
        healthSystem.Damage(amount);
        checkIsDead();
    }

    private void checkIsDead(){
        if(healthSystem.GetHealthPercentage() == 0){
            // GAMEOVER screen
        }
    }
}
