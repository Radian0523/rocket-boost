using UnityEngine;

public interface IHatenaBoxState
{
    void Enter(HatenaBoxStateMachine hatenaBox);
    void Update(HatenaBoxStateMachine hatenaBox);
    void Exit(HatenaBoxStateMachine hatenaBox);
}
public class HatenaBoxUnpressedState : IHatenaBoxState
{
    private HatenaBoxStateMachine hatenaBox;
    [SerializeField] GameObject planet;
    public void Enter(HatenaBoxStateMachine hatenaBox)
    {

    }
    public void Update(HatenaBoxStateMachine hatenaBox)
    {

    }
    public void Exit(HatenaBoxStateMachine hatenaBox)
    {

    }

}
public class HatenaBoxMovingState : IHatenaBoxState
{
    public void Enter(HatenaBoxStateMachine hatenaBox)
    {

    }
    public void Update(HatenaBoxStateMachine hatenaBox)
    {

    }
    public void Exit(HatenaBoxStateMachine hatenaBox)
    {

    }
}
public class HatenaBoxPressedState : IHatenaBoxState
{
    public void Enter(HatenaBoxStateMachine hatenaBox)
    {

    }
    public void Update(HatenaBoxStateMachine hatenaBox)
    {

    }
    public void Exit(HatenaBoxStateMachine hatenaBox)
    {

    }
}
public class HatenaBoxStateMachine : MonoBehaviour
{
    IHatenaBoxState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.Update(this);
        }
    }

    public void ChangeState(IHatenaBoxState newState)
    {
        //この this は、現在の HatenaBoxStateMachine のインスタンスを IHatenaBoxState の Exit() や Enter() に渡しています。
        if (currentState != null)
        {
            currentState.Exit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // isは、型があっているかどうかを確認するときに使う。==ではコンパイルエラーとなる。
        if (other.gameObject.tag == "Player" && currentState is HatenaBoxUnpressedState)
        {
            // newは、クラスではなく、インスタンスを呼び出すため。()は、コンストラクタがなんとか
            ChangeState(new HatenaBoxMovingState());
        }
    }
}

