using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    Movement movement;
    [SerializeField] GameObject forSmartPhonePlayer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        movement = player.GetComponent<Movement>();


        if (!movement.isMobile)
        {
            forSmartPhonePlayer.SetActive(false);
        }
    }

    public void OnButtonUpArrow_Down()
    {
        Debug.Log("Up_On");
        movement.doThrust = true;
    }
    public void OnButtonUpArrow_Up()
    {
        Debug.Log("Up_Off");
        movement.doThrust = false;
    }
    public void OnClickRightArrow_Down()
    {
        Debug.Log("Right");
        movement.rotationValue = 1;
    }
    public void OnClickRightArrow_Up()
    {
        Debug.Log("Right");
        movement.rotationValue = 0;
    }
    public void OnClickLeftArrow_Down()
    {
        Debug.Log("Left");
        movement.rotationValue = -1;
    }
    public void OnClickLeftArrow_Up()
    {
        Debug.Log("Left");
        movement.rotationValue = 0;
    }

}
