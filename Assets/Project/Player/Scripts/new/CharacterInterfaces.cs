using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AActor
{

}

public interface IDamagable
{
    int Health { get; }
    void TakeDamage(int damageAmount);
}

public interface IHealable
{
    int Health { get; }
    void Heal(int healAmount);
}

public interface IMovable
{
    Vector3 Velocity { get; }
    void SetMovementDirection(Vector3 moveDir);
}

public interface IJumpable
{
    event Action OnJustJumped;
    event Action OnJustLanded;
    void SetJump();
}

public interface IGroundable
{
    bool IsOnTheGround { get; }
}

public interface IShootable
{
    bool CanShoot { get; }
    void SetShoot();
}

public interface ICharacter: IDamagable, IHealable, IMovable, IJumpable, IGroundable, IShootable { }
