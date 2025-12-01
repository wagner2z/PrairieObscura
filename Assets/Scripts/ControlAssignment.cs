using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControlAssignment
{
    public KeyCode characterMoveLeft()
    {
        return KeyCode.A;
    }

    public KeyCode characterMoveRight()
    {
        return KeyCode.D;
    }

    public KeyCode characterMoveBack()
    {
        return KeyCode.S;
    }

    public KeyCode characterMoveForward()
    {
        return KeyCode.W;
    }

    public KeyCode characterDash()
    {
        return KeyCode.LeftShift;
    }

    public KeyCode playerPush()
    {
        return KeyCode.Space;
    }

    public KeyCode playerShoot()
    {
        return KeyCode.Mouse0;
    }

    public KeyCode playerFirePosition()
    {
        return KeyCode.Mouse1;
    }

    public KeyCode reloadGun()
    {
        return KeyCode.R;
    }

    public KeyCode pickUpOrDrop()
    {
        return KeyCode.Mouse0;
    }

    public KeyCode start()
    {
        return KeyCode.Return;
    }

    public KeyCode exit()
    {
        return KeyCode.Escape;
    }

    public KeyCode switchOptionUp()
    {
        return KeyCode.W;
    }

    public KeyCode switchOptionDown()
    {
        return KeyCode.S;
    }

    public KeyCode switchOptionRight()
    {
        return KeyCode.D;
    }

    public KeyCode switchOptionLeft()
    {
        return KeyCode.A;
    }
}
