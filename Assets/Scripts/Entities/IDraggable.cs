using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable
{
    //MAKE IT SO THAT THINGS CAN BE PUT BACK IN THE GREEN AREA!!!
    public Vector3 DragOffset { get;}
    public Camera Cam { get; }

    void Awake();
    void OnMouseDown();
    void OnMouseDrag();
    void OnMouseUp();
    Vector3 GetMousePos(Transform transform);


}
