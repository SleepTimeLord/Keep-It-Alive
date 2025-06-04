using UnityEngine;


[CreateAssetMenu(menuName = "Projectile/Movement/Wave")]
public class WaveBulletMovementSO : ProjectileMovementSO
{
    public float waveFrequency;
    public float waveAmplitude;

    public override IProjectileMovement CreateMovement()
    {
        return new WaveBulletMovement(waveFrequency, waveAmplitude);
    }
}

public class WaveBulletMovement : IProjectileMovement
{
    private float waveFrequency;
    private float waveAmplitude;
    public WaveBulletMovement(float waveFrequency, float waveAmplitude)
    {
        this.waveFrequency = waveFrequency;
        this.waveAmplitude = waveAmplitude;
    }

    public void Move(ProjectileBase projectile)
    {
        Vector3 pos = projectile.transform.position;
        pos += -projectile.transform.right * Time.deltaTime * projectile.projectileSpeed;
        // put the 0.001 so it doesn't go crazy
        pos.y += Mathf.Sin(Time.time * waveFrequency) * (waveAmplitude * 0.1f);
        projectile.transform.position = pos;
    }
}