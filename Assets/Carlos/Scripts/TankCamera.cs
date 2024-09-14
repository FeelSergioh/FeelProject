using UnityEngine;

public class TankCamera : MonoBehaviour
{

#region Fields

    public Transform target;  // El tanque u objeto que la cámara va a seguir
    [Range(0.001f, 0.01f)] public float smoothSpeed;  // Velocidad de suavizado

#endregion

#region Properties

    public Vector2 TargetPosition => new (target.position.x, target.position.y);
    public Vector2 CameraPosition
    {
        get => new (transform.position.x, transform.position.y);
        set 
        {
            transform.position = new Vector3(value.x, value.y, transform.position.z);
        }
    }

#endregion

#region Unity Methods

    private void LateUpdate()
    {
        if (CameraPosition.Equals(TargetPosition)) return;

        CameraPosition = Vector2.Lerp(CameraPosition, TargetPosition, smoothSpeed);
    }

#endregion

}
