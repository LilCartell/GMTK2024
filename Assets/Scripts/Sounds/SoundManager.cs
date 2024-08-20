using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource MusicsSource;
	public AudioSource SoundEffectsSource;

	public AudioClip TitleSceneMusic;
	public AudioClip BuildingSceneMusic;
	public AudioClip ShmupSceneMusic;
	public AudioClip GameOverSceneMusic;
	public AudioClip BriefingSceneMusic;

	public AudioClip ClicSound;
	public AudioClip PlaceCellSound;

	public static SoundManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
			return;
		}

		Instance = this;

		DontDestroyOnLoad(this.gameObject);
	}

	public void PlayMusic(AudioClip musicClip)
	{
		if(musicClip != null && musicClip != MusicsSource.clip)
        {
			MusicsSource.clip = musicClip;
			MusicsSource.Play();
		}
	}

	public void PlaySound(AudioClip effectSound)
	{
		if(effectSound)
        {
			SoundEffectsSource.clip = effectSound;
			SoundEffectsSource.Play();
		}
	}
}