namespace CustomBackgrounds;

public class SkyboxBehavior : MonoBehaviour
{
    internal GameObject skyboxObject;

    private void Awake()
    {
        this.skyboxObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        this.skyboxObject.transform.position = Vector3.zero;
        this.skyboxObject.layer = 13;
        this.skyboxObject.name = "_SkyBGObject";
        this.skyboxObject.transform.localScale = Vector3.one * -800;
    }
}