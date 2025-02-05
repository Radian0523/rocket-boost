using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    AudioSource audioSource;
    [SerializeField] AudioClip buttonOn;
    [SerializeField] AudioClip buttonOff;
    [SerializeField] Vector3 buttonMoveVector;
    [SerializeField] Vector3[] doorMoveVector;
    Vector3 buttonStartPos;
    Vector3[] doorStartPos;
    Vector3 buttonEndPos;
    Vector3[] doorEndPos;
    [SerializeField] float speed = 10f;
    [SerializeField] float openDuration = 2f;
    [SerializeField] Transform[] door;
    int pressingState;
    float timer;
    float ctrlTimer;
    float distance;
    float buttonMoveDuration;
    float[] doorMoveDuration;
    float moveDuration;
    bool flagButton = false;
    bool[] flagDoor;
    int doorNum;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        buttonStartPos = transform.position;
        buttonEndPos = transform.position + buttonMoveVector;

        doorNum = door.Length;
        // 配列の初期化を追加
        doorStartPos = new Vector3[doorNum];
        doorEndPos = new Vector3[doorNum];
        doorMoveDuration = new float[doorNum];
        flagDoor = new bool[doorNum];

        for (int i = 0; i < doorNum; i++)
        {
            doorStartPos[i] = door[i].position;
            doorEndPos[i] = doorStartPos[i] + doorMoveVector[i];
        }

        distance = Vector3.Distance(buttonStartPos, buttonEndPos);


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moveDuration);
        switch (pressingState)
        {
            case 0:
                break;
            case 1:
                OnButtonMovement();
                break;
            case 2:
                audioSource.PlayOneShot(buttonOff);
                pressingState = 3;
                break;
            case 3:
                BackPos();
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && pressingState == 0)
        {
            pressingState = 1;

            audioSource.PlayOneShot(buttonOn);
        }
    }

    private void OnButtonMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, buttonEndPos, speed * Time.deltaTime);
        for (int i = 0; i < doorNum; i++)
        {
            door[i].position = Vector3.MoveTowards(door[i].position, doorEndPos[i], speed * Time.deltaTime);
        }
        timer += Time.deltaTime;
        if (transform.position == buttonEndPos && flagButton == false)
        {
            buttonMoveDuration = timer;
            flagButton = true;
        }
        for (int i = 0; i < doorNum; i++)
        {
            if (door[i].transform.position == doorEndPos[i] && flagDoor[i] == false)
            {
                doorMoveDuration[i] = timer;
                flagDoor[i] = true;

            }
            if (doorMoveDuration[i] >= moveDuration)
            {
                moveDuration = doorMoveDuration[i];
            }
        }
        moveDuration = Mathf.Max(moveDuration, buttonMoveDuration);
        if (timer > openDuration + moveDuration)
        {
            pressingState = 2;
            timer = 0;
        }
    }

    private void BackPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, buttonStartPos, speed * Time.deltaTime);
        for (int i = 0; i < doorNum; i++)
        {
            door[i].position = Vector3.MoveTowards(door[i].position, doorStartPos[i], speed * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if (timer > openDuration + moveDuration)
        {
            //TODOコードをキレイにする。一連の流れで必要な初期化は、StateのSwitch文の中で行う方がキレイ。
            moveDuration = 0;
            flagButton = false;
            for (int i = 0; i < doorNum; i++)
            {
                flagDoor[i] = false;
            }
            pressingState = 0;
            timer = 0;
        }
    }


}
