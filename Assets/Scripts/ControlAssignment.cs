using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class ControlAssignment
{
    static bool moveByWorldAxis = false;
    static int mouseSensitivity = 100;

    public static KeyCode characterMoveLeft()
    {
        return KeyCode.A;
    }

    public static KeyCode characterMoveRight()
    {
        return KeyCode.D;
    }

    public static KeyCode characterMoveBack()
    {
        return KeyCode.S;
    }

    public static KeyCode characterMoveForward()
    {
        return KeyCode.W;
    }

    public static KeyCode characterDash()
    {
        return KeyCode.LeftShift;
    }

    public static KeyCode playerPush()
    {
        return KeyCode.Space;
    }

    public static KeyCode playerShoot()
    {
        return KeyCode.Mouse0;
    }

    public static KeyCode playerFirePosition()
    {
        return KeyCode.Mouse1;
    }

    public static KeyCode reloadGun()
    {
        return KeyCode.R;
    }

    public static KeyCode pickUpOrDrop()
    {
        return KeyCode.Mouse0;
    }

    public static KeyCode start()
    {
        return KeyCode.Return;
    }

    public static KeyCode select1()
    {
        return KeyCode.Return;
    }

    public static KeyCode select2()
    {
        return KeyCode.Mouse0;
    }

    public static KeyCode exit()
    {
        return KeyCode.Escape;
    }

    public static KeyCode switchOptionUp()
    {
        return KeyCode.W;
    }

    public static KeyCode switchOptionDown()
    {
        return KeyCode.S;
    }

    public static KeyCode switchOptionRight()
    {
        return KeyCode.D;
    }

    public static KeyCode switchOptionLeft()
    {
        return KeyCode.A;
    }

    public static bool getMoveByWorldAxis()
    {
        return moveByWorldAxis;
    }

    public static void setMoveByWorldAxis(bool m)
    {
        moveByWorldAxis = m;
    }

    public static int getMouseSensitivity()
    {
        return mouseSensitivity;
    }

    public static void setMouseSensitivity(int i)
    {
        mouseSensitivity = i;
    }
}
