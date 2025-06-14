using TsSDK;
using UnityEngine;

/// <summary>
/// Material object component where the haptic impact is depended from physical impulse value.
/// Modified to support particle collisions.
/// </summary>
public class Particles : MonoBehaviour
{
    public float maxImpulse = 100.0f;
    public int minCollisionDurationMs = 50;

    [SerializeField]
    private TsHapticMaterialAsset m_hapticMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        HandleHapticImpact(collision.gameObject, collision.impulse.magnitude);
    }

    private void OnCollisionStay(Collision collision)
    {
        HandleHapticImpact(collision.gameObject, collision.impulse.magnitude);
    }

    private void OnCollisionExit(Collision collision)
    {
        StopHapticImpact(collision.gameObject);
    }

    // *** NUEVA FUNCIÓN PARA DETECTAR PARTÍCULAS ***
    private void OnParticleCollision(GameObject other)
    {
        HandleHapticImpact(other, maxImpulse * 0.1f); // Usamos un valor fijo o ajustable para la "intensidad"
    }

    private void HandleHapticImpact(GameObject other, float impulseMagnitude)
    {
        var collisionHandler = other.GetComponent<TsHapticCollisionHandler>();

        if (collisionHandler != null && collisionHandler.HapticPlayer.Device != null)
        {
            var playable = collisionHandler.HapticPlayer.PlayerHandle.GetPlayable(m_hapticMaterial.Instance as IHapticAsset) as IHapticMaterialPlayable;
            if (playable != null)
            {
                collisionHandler.AddImpact(playable, impulseMagnitude / maxImpulse, minCollisionDurationMs);
                playable.Play();
            }
        }
    }

    private void StopHapticImpact(GameObject other)
    {
        var collisionHandler = other.GetComponent<TsHapticCollisionHandler>();

        if (collisionHandler != null && collisionHandler.HapticPlayer.Device != null)
        {
            var playable = collisionHandler.HapticPlayer.PlayerHandle.GetPlayable(m_hapticMaterial.Instance as IHapticAsset) as IHapticMaterialPlayable;
            if (playable != null)
            {
                collisionHandler.RemoveImpact(playable);
            }
        }
    }
}

