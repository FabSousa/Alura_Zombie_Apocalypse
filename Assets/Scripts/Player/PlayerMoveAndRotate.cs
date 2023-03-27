using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAndRotate : MoveAndRotate
{
    private LayerMask floorMask;
    public Vector3 Dir {get; private set;}

    private void Start(){
        floorMask = LayerMask.GetMask(Strings.FloorMask);
    }

    public void Move(float speed)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Dir = new Vector3(x, 0, z);
        base.Move(Dir, speed);
    }

    public void Rotate(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorCollision;

        if (Physics.Raycast(ray, out floorCollision, 100, floorMask))
        {
            Vector3 playerAim = floorCollision.point - transform.position;
            playerAim.y = transform.position.y;

            base.Rotate(playerAim);
        }
    }
}
