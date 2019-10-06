using UnityEngine;
using System.Linq;

public class PlayerMoveScript : MonoBehaviour
{
    public float playerMass = 0.5f;
    // float lastObjectVelocityK;
    float playerSpeedK;
    public AudioClip deathCry;
    public ParticleSystem DeathEffect;
    public ParticleSystem WinEffect;
    public AudioSource TakeSound;
    public AudioSource ThrowSound;

    Vector3 dropDirection;
    Vector3 impulseDirection;
    Vector3 currentPosition;
    Vector3 impulse;
    Vector3 PlayerStartPosition;

    Vector3 mousePosition;
    Rigidbody2D rb;
    Transform _objectHolder;
    Animator _animator;

    Vector3 heading;
    float maxRange = 0.03f;

    bool isStarted = false;

    DirectionPointer _pointer;

    DirectionPointer Pointer {
        get {
            if ( !_pointer ) {
                _pointer = GetComponentInChildren<DirectionPointer>();
            }
            return _pointer;
        }
    }

    ObjectScript _grabbedObject;
    ObjectScript grabbedObject
    {

        get => _grabbedObject;
        set
        {
            _grabbedObject = value;
            var anim = _grabbedObject != null ? HoldAnimation : IdleAnimation;
            _animator.Play(anim);
        }
    }

    static readonly int HoldAnimation = Animator.StringToHash("PlayerHoldAnimation");
    static readonly int IdleAnimation = Animator.StringToHash("PlayerIdleAnimation");




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _objectHolder = transform.Find("ObjectHolder");
        _animator = GetComponent<Animator>();
        grabbedObject = null;
        playerSpeedK = 6f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPosition = Camera.main.WorldToScreenPoint(transform.position);
            mousePosition = Input.mousePosition;
            impulseDirection = (currentPosition - mousePosition).normalized;

            if (grabbedObject != null)
            {
                throwObject(impulseDirection);
            }
        }
    }

    private void throwObject(Vector3 impulseDirection)
    {
        //  rb.mass -= grabbedObject.objectMass;


        //  playerMass -= grabbedObject.objectMass;
        rb.velocity = impulseDirection * playerSpeedK;

        grabbedObject.transform.SetParent(null);
     //   rb.AddForce(impulseDirection / playerMass, ForceMode2D.Impulse);
        grabbedObject.GetComponent<Rigidbody2D>().velocity = -rb.velocity;
        grabbedObject.tilt = -1f;
        grabbedObject = null;


        Pointer.Hide();
        ThrowSound.Play();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "object")
        {
            processObjectCollision(coll.gameObject);
        }
    }

    void processObjectCollision(GameObject obj)
    {
        if (grabbedObject != null)
        {
            return;
        }
        else
        {
            grabbedObject = obj.GetComponent<ObjectScript>();
            grabbedObject.transform.SetParent(_objectHolder);
            grabbedObject.transform.localPosition = Vector3.zero;
            grabbedObject.GetComponent<Collider2D>().enabled = false;
            grabbedObject.tilt = 0f;

            //    rb.AddForce(-impulseDirection / playerMass, ForceMode2D.Impulse); //player stops
            // rb.mass += grabbedObject.objectMass;
            //    playerMass += grabbedObject.objectMass;
            //    rb.AddForce(impulseDirection / playerMass, ForceMode2D.Impulse);
            rb.velocity = impulseDirection * grabbedObject.objectVelocityK;

            Pointer.Show();
            TakeSound.Play();
        }
    }

    public void PlayerHide()
    {
        GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(r => r.enabled = false);
        GetComponent<Collider2D>().enabled = false;
    }

    public void PlayDeathEffect()
    {
        GetComponent<AudioSource>().PlayOneShot(deathCry);
        DeathEffect.Play();
    }

    public void PlayWinEffect()
    {
        WinEffect.Play();
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
}
