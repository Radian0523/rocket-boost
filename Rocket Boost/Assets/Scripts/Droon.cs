using UnityEngine;

public class Droon : MonoBehaviour
{
    enum DroonState
    {
        pausing,
        falling,
        stuning,
        rising,
    };

    AudioSource audioSource;
    [SerializeField] GameObject droon;
    [SerializeField] AudioClip droonCollision;
    [SerializeField] Vector3 droonMoveVector;
    Vector3 droonStartPos;
    Vector3 droonEndPos;
    [SerializeField] float droonRiseSpeed = 5f;
    [SerializeField] float droonFallSpeed = 10f;
    [SerializeField] float stunTime = 1f;
    DroonState droonState;
    float timer;



    void Start()
    {
        droonStartPos = droon.transform.position;
        droonEndPos = droonStartPos + droonMoveVector;

        droonState = DroonState.pausing;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (droonState)
        {
            case DroonState.pausing:
                break;
            case DroonState.falling:
                droon.transform.position = Vector3.MoveTowards(droon.transform.position, droonEndPos, droonFallSpeed * Time.deltaTime);
                if (droon.transform.position == droonEndPos)
                {
                    audioSource.PlayOneShot(droonCollision);
                    droonState = DroonState.stuning;
                }
                break;
            case DroonState.stuning:
                timer += Time.deltaTime;
                if (timer >= stunTime)
                {
                    droonState = DroonState.rising;
                    timer = 0;
                }
                break;
            case DroonState.rising:
                droon.transform.position = Vector3.MoveTowards(droon.transform.position, droonStartPos, droonRiseSpeed * Time.deltaTime);
                if (droon.transform.position == droonStartPos)
                {
                    droonState = DroonState.pausing;
                }
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && droonState == DroonState.pausing)
        {
            droonState = DroonState.falling;
        }
    }
}
