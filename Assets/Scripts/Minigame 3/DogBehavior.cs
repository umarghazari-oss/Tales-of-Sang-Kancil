using UnityEngine;

[RequireComponent(typeof(Dog))]
public abstract class DogBehavior : MonoBehaviour
{
    public Dog dog { get; private set; }
    public float duration;

    private void Awake()
    {
        dog = GetComponent<Dog>();
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;

        CancelInvoke();
    }
}
