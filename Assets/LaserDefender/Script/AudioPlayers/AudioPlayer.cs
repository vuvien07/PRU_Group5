using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

	[Header("Damage")]
	[SerializeField] AudioClip damageClip;
	[SerializeField][Range(0f, 1f)] float damageVolume = 1f;

	[Header("Lose")]
	[SerializeField] AudioClip loseClip;
	[SerializeField][Range(0f, 1f)] float loseVolume = 1f;

	[Header("Defend")]
	[SerializeField] AudioClip defendClip;
	[SerializeField][Range(0f, 1f)] float defendVolume = 1f;



	public void PlayShootingClip()
    {
		PlayClip(shootingClip, shootingVolume);
    }

	public void PlayDamageClip()
	{
		PlayClip(damageClip, damageVolume);
	}

	public void PlayLoseClip()
	{
		PlayClip(loseClip, loseVolume);
	}

	public void PlayDefendClip()
	{
		PlayClip(defendClip, defendVolume);
	}

	void PlayClip(AudioClip audioClip, float value)
	{
		if (audioClip != null)
		{
			AudioSource.PlayClipAtPoint(audioClip,
				transform.position,
				value);
		}	
	}
}
