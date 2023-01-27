using Assets.Script.Player;
using UnityEngine;

public class FlyingEyeController : EnemyController
{
    [SerializeField] float _hitPower;
    protected override void RegistrateAction()
    {
        if (AllFlyingEyesManager.Instance != null)
        {
            AllFlyingEyesManager.Registrate(this);
        }
    }

    protected override void RemoveRegistratationAction()
    {
        if (AllFlyingEyesManager.Instance != null)
        {
            AllFlyingEyesManager.Remove(this);
        }
    }

    protected override void SpecialAttack(PlayerController player)
    {
        base.SpecialAttack(player);
        var lostFirewood = player.LooseFirewood();
        if (lostFirewood == null)
        {
            return;
        }
        var hitDirection = lostFirewood.CurrentPosition - player.CurrentPosition;
        hitDirection.Normalize();
        lostFirewood.ProcessHit(_hitPower * hitDirection);
    }
}
