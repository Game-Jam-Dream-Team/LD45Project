using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{

    public float playerSpeed = 0.5f;
    public float playerStartSpeed = 0.3f;
  //  public int playerMass;
    Vector3 dropDirection;
    Vector3 impulseDirection;
    Vector3 currentPosition;
    Vector3 impulse;

    Vector3 mousePosition;
    ObjectScript grabbedObject;
    bool isStarted = false;
    Rigidbody2D rb;

    DirectionPointer _pointer;

    DirectionPointer Pointer {
        get {
            if ( !_pointer ) {
                _pointer = GetComponentInChildren<DirectionPointer>();
            }
            return _pointer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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

        rb.mass -= grabbedObject.objectMass;
        grabbedObject.transform.SetParent(null);
        rb.AddForce(impulseDirection * playerSpeed, ForceMode2D.Impulse);
        //  grabbedObject.GetComponent<Collider2D>().enabled = false;
        grabbedObject.GetComponent<Rigidbody2D>().velocity = -rb.velocity;
        grabbedObject.tilt = -1f;
        grabbedObject = null;

        Pointer.Hide();

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
            obj.transform.position = transform.position + new Vector3(0.5f, 0, 0);
            obj.transform.SetParent(transform);
            grabbedObject.GetComponent<Collider2D>().enabled = false;
            grabbedObject.tilt = 0f;


            rb.AddForce(-impulseDirection * playerSpeed, ForceMode2D.Impulse);
            rb.mass += grabbedObject.objectMass;
            rb.AddForce(impulseDirection * playerSpeed, ForceMode2D.Impulse);

            Pointer.Show();
        }

    }

}
