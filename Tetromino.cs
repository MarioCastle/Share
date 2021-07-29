using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    #region FIELDS
    //==========================================================================================
    static Transform[,] grid = new Transform[WIDTH, HEIGHT];// Tetromino Landing

    const int WIDTH = 10; //GENERIC 通用的
    const int HEIGHT = 22;//GENERIC 通用的
    const float ROTATE_ANGLE = 90f;
    float dropTime;
    float dropTimer;

    #endregion


    #region UNITY EVENT FUNCTIONS
    //==========================================================================================
    void OnEnable()
    {
        PlayerInput.onMoveLeft += MoveLeft;
        PlayerInput.onMoveRight += MoveRight;
        PlayerInput.onDrop += Drop;
        PlayerInput.onCancelDrop += CancelDrop;
        PlayerInput.onRotate += Rotate;

        dropTime = GameManager.Instance.AutoDropTime;
    }


    void OnDisable()
    {
        PlayerInput.onMoveLeft -= MoveLeft;
        PlayerInput.onMoveRight -= MoveRight;
        PlayerInput.onDrop -= Drop;
        PlayerInput.onCancelDrop -= CancelDrop;
        PlayerInput.onRotate -= Rotate;
    }


    void Start()
    {
        dropTime = GameManager.Instance.AutoDropTime;
    }


    void FixedUpdate()
    {
        dropTimer += Time.fixedDeltaTime;

        if (dropTimer >= dropTime)
        {
            dropTimer = 0f;
            MoveDown();
        }
    }
    #endregion

    #region GENERIC 通用的
    bool Movable
    {
        get
        {
            foreach (Transform child in transform)
            {
                int x = Mathf.RoundToInt(child.position.x);
                int y = Mathf.RoundToInt(child.position.y);

                if (x < 0 || x >= WIDTH || y < 0 || y >= HEIGHT || grid[x, y] != null)
                {
                    return false;
                }
            }
            return true;
        }
    }


    void Land()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            grid[x, y] = child;

            if (y == HEIGHT - 1)
            {
                //Game Over !
                GameManager.Instance.GameOver();
            }
        }
    }


    #endregion


    #region HORIZONTAL MOVE
    //==========================================================================================

    void MoveLeft()
    {
        transform.position += Vector3.left;

        if (!Movable)
        {
            transform.position += Vector3.right;
        }
    }

    void MoveRight()
    {
        transform.position += Vector3.right;

        if (!Movable)
        {
            transform.position += Vector3.left;
        }
    }

    #endregion

    #region VERTICAL MOVE 
    //==========================================================================================

    void MoveDown()
    {
        transform.position += Vector3.down;

        if (!Movable)
        {
            transform.position += Vector3.up;
            Land();
            ClearFullRows();
            enabled = false;
            GameManager.Instance.SpawnTetromino();

        }
    }

    void Drop()
    {
        dropTime = Time.fixedDeltaTime;
    }


    void CancelDrop()
    {
        dropTime = GameManager.Instance.AutoDropTime;
    }
    #endregion

    #region ROTATE
    //==========================================================================================

    void Rotate()
    {
        transform.Rotate(Vector3.forward, ROTATE_ANGLE);

        if (!Movable)
        {
            transform.Rotate(Vector3.forward, -ROTATE_ANGLE);
        }
    }

    #endregion

    #region CHECK ROWS
    void ClearFullRows()
    {
        //check rows from top to bottom
        for (int y = HEIGHT - 1; y >= 0; y--)
        {
            // if row y is full
            if (IsRowFull(y))
            {
                //   TODO : you could spawn SFX here
                //destroy the full row
                DestroyRow(y);

                //Decrease the rows above
                DecreaseRow(y);
            }
        }
    }

    bool IsRowFull(int y)
    {
        // check grids in row if full
        for (int x = 0; x < WIDTH; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DestroyRow(int y)
    {
        for (int x = 0; x < WIDTH; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    void DecreaseRow(int row)
    {
        //after clear a full row, decrease row above
        for (int y = row; y < HEIGHT - 1; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                // if the row above has a block
                if (grid[x, y + 1] != null)
                {
                    //switch the transforms of this row and the row above
                    grid[x, y] = grid[x, y + 1];
                    //it should be null because the transform was swapped
                    grid[x, y + 1] = null;
                    //move the swapped blocks down
                    grid[x, y].gameObject.transform.position += Vector3.down;
                }
            }
        }
    }
    #endregion



}
