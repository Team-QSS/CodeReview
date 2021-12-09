using UnityEngine;

public partial class CharacterControl
{
    public enum State
    {
        Idle,
        Skill,
        Walk
    }

    public class IdleState : State<CharacterControl>
    {
        public override State<CharacterControl> InputHandle(CharacterControl t)
        {
            throw new System.NotImplementedException();
            // ToDo [Status조건 설정]: Idle -> Skill => 스킬 준비 조작을 했을 때
            return t.states[(int)State.Skill];
            // ToDo [Status조건 설정]: Idle -> Walk => 이동 조작을 했을 때
            return t.states[(int)State.Walk];
        }

        // ToDo : 상태별 동작 설정
        public override void Enter(CharacterControl t)
        {
            base.Enter(t);
            // ToDo : 애니메이션 재생
        }

        public override void Update(CharacterControl t)
        {
            base.Update(t);
        }

        public override void Exit(CharacterControl t)
        {
            base.Exit(t);
        }
    }
    
    public class SkillState : State<CharacterControl>
    {
        public override State<CharacterControl> InputHandle(CharacterControl t)
        {
            throw new System.NotImplementedException();
            // ToDo [Status조건 설정]: Skill -> Idle => 스킬을 취소했을 때 or 스킬 시전이 완료 되었을 때 
            return t.states[(int)State.Idle];
        }

        // ToDo : 상태별 동작 설정
        public override void Enter(CharacterControl t)
        {
            base.Enter(t);
            // ToDo : 애니메이션 재생
        }

        public override void Update(CharacterControl t)
        {
            base.Update(t);
        }

        public override void Exit(CharacterControl t)
        {
            base.Exit(t);
        }
    }
    
    public class WalkState : State<CharacterControl>
    {
        public override State<CharacterControl> InputHandle(CharacterControl t)
        {
            throw new System.NotImplementedException();
            // ToDo [Status조건 설정]: Walk -> Idle => 이동 조이스틱에서 손을 땠을 때 or 이동 조이스틱 중앙에 손가락이 위치할 때 
            return t.states[(int)State.Idle];
        }

        // ToDo : 상태별 동작 설정
        public override void Enter(CharacterControl t)
        {
            base.Enter(t);
            // ToDo : 애니메이션 재생
        }

        public override void Update(CharacterControl t)
        {
            base.Update(t);
        }

        public override void Exit(CharacterControl t)
        {
            base.Exit(t);
        }
    }

}