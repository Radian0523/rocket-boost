using UnityEngine;

public class HatenaBox : MonoBehaviour
{
    enum HatenaBoxState
    {
        unpressed,
        creatingPlanet,
        movingUp,
        movingDown,
        pressed,
    }
    [SerializeField] GameObject[] planetPrefab;
    [SerializeField] GameObject mainBox;
    [SerializeField] Color pressedBoxColor;
    [SerializeField] GameObject hatena;
    [SerializeField] float speed = 2f;
    float movementTimer;
    HatenaBoxState currentState;
    Renderer mainBoxRenderer;
    Vector3 startPos;
    Vector3 endPos;
    [SerializeField] AudioClip hatenaBoxPush;
    AudioSource audioSource;
    [SerializeField] float movementY = 1f;
    void Start()
    {
        currentState = HatenaBoxState.unpressed;

        startPos = this.transform.position;
        endPos = startPos + Vector3.up * movementY;

        mainBoxRenderer = mainBox.GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        switch (currentState)
        {
            case HatenaBoxState.unpressed:
                break;
            case HatenaBoxState.movingUp:
                movementTimer += speed * Time.deltaTime;
                //startPosからendPosまでの道のりの、movementTimerの割合分の位置を返す
                transform.position = Vector3.Lerp(startPos, endPos, movementTimer);
                if (movementTimer >= 1f)
                {
                    currentState = HatenaBoxState.movingDown;
                }
                break;
            case HatenaBoxState.movingDown:
                movementTimer -= speed * Time.deltaTime;
                //startPosからendPosまでの道のりの、movementTimerの割合分の位置を返す
                transform.position = Vector3.Lerp(startPos, endPos, movementTimer);
                if (movementTimer <= 0f)
                {
                    currentState = HatenaBoxState.creatingPlanet;
                    movementTimer = 0;
                    mainBoxRenderer.material.color = pressedBoxColor;
                    hatena.SetActive(false);
                }
                break;
            case HatenaBoxState.creatingPlanet:
                int rndIdx = Random.Range(0, planetPrefab.Length);
                Instantiate(planetPrefab[rndIdx], this.transform.position + 2 * Vector3.up, Quaternion.identity);
                currentState = HatenaBoxState.pressed;
                break;
            case HatenaBoxState.pressed:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && currentState == HatenaBoxState.unpressed)
        {
            currentState = HatenaBoxState.movingUp;
            audioSource.PlayOneShot(hatenaBoxPush);
        }
    }
}
