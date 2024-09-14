using UnityEngine;

public class TankCanon : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Range(0,10), SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private Rigidbody2D bulletPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        RotateCannonToMouse();
    }

    private void RotateCannonToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 

        Vector3 direction = mousePosition - transform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float currentAngle = transform.eulerAngles.z;
        float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
    }

    private void Shoot()
    {
        var shoot = Instantiate(bulletPrefab, transform.position, transform.rotation);
        shoot.AddForce(transform.right * 10, ForceMode2D.Impulse);
        Destroy(shoot, 1f);
    }
}
