using UnityEngine;
public class Wander : MonoBehaviour
{
    private Vector3 targetPosition;

    private float movementSpeed = 2.0f;
    private float rotationSpeed = 1.0f;
    private float targetPositionTolerance = 3.0f;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;
    public Material mad;

    void Start()
    {
        minX = -2.5f;
        maxX = 2.5f;

        minZ = -6.5f;
        maxZ = 4.5f;

        //Get Wander Position
        GetNextPosition();
    }

    void Update()
    {
        if (Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerance)
        {
            GetNextPosition();
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
       

    }

    void GetNextPosition()
    {
        targetPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

    // Als er een andere agent in radius van deze agent komt wordt deze functie aangeroepen
    private void OnTriggerEnter(Collider collision)
    {
        if (name == "Police" && collision.gameObject.tag == "Activist")
        {
            Renderer rend = collision.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = mad;
            }
        }
    }
}