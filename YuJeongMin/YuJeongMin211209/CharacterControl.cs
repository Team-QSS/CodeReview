using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public partial class CharacterControl : MonoBehaviour
{
    private readonly State<CharacterControl>[] states = {new IdleState(), new SkillState(), new WalkState()};
    private State<CharacterControl> _curState;

    // Todo : 플레이어 캐릭터 기본 세팅(상태0, 히트박스0, 이동)

    private void Start()
    {
        _curState = states[(int)State.Idle];
        
        this.UpdateAsObservable().Subscribe(_ =>
        {
            _curState.Action(this);
            var nextState = _curState.InputHandle(this);
            
            if (!_curState.Equals(nextState))
            {
                _curState.Exit(this);
                _curState = nextState;
                _curState.Enter(this);
            }
        }).AddTo(this);
    }
    
    // DamageCollider
    public void OnCollisionEnterInChildren(Collision2D other)
    {
        throw new NotImplementedException();
        // ToDo : 피해
    }

    // ObjectCollider
    public void OnTriggerEnterInChildren(Collider2D other)
    {
        throw new NotImplementedException();
        // ToDo : 오브젝트 교환
    }
}
