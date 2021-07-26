using System.Collections;
using UnityEngine;

public class Viewport : Singleton<Viewport>
{
    float minX;
    float maxX;
    float minY;
    float maxY;
    
    void Start(){
        Camera mainCamera = Camera.main;
        
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f,0f));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f,1f));

        minX = bottomLeft.x;
        minY = bottomLeft.y;
        maxX = topRight.x;
        maxY = topRight.y;
        
    }

    public Vector3 PlayerMoveablePosition(Vector3 playerPosition){

        Vector3 position = Vector3.zero;

        position.x = Mathf.Clamp(playerPosition.x, minX , maxX);
        position.y = Mathf.Clamp(playerPosition.y, minY , maxY);

        return position;
    }
    
}
