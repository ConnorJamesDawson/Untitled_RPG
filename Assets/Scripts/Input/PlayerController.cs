using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement controls;

    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;

    private Vector2 MoveRight = new Vector3(0.16f,0);
    private Vector2 MoveLeft = new Vector3(-0.16f,0);
    private Vector2 MoveUp = new Vector3(0,0.16f);
    private Vector2 MoveDown = new Vector3(0,-0.16f);

    private void Awake()
    {
        controls = new PlayerMovement();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 Direction)
    {
        if (Direction.x == 1)
            Direction = MoveRight;
        else if (Direction.x == -1)
            Direction = (Vector3)MoveLeft;
        else if (Direction.y == 1)
            Direction = (Vector3)MoveUp;
        else if (Direction.y == -1)
            Direction = (Vector3)MoveDown;

        if (CanMove(Direction))
        {
            Debug.Log("Value: " + Direction);
            //Debug.Log("Moving to position: " + Direction);

            transform.position += (Vector3)Direction;   
        }
    }

    private bool CanMove(Vector2 Direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)Direction);

        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        else
            return true;
    }
}
