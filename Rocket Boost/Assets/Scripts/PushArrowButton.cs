using UnityEngine;

public class PushArrowButton : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip buttonOn;
    [SerializeField] AudioClip buttonOff;
    [SerializeField] Vector3 buttonMoveVector;
    [SerializeField] Transform arrowPlatform;
    Vector3 buttonStartPos;
    Vector3 buttonEndPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
