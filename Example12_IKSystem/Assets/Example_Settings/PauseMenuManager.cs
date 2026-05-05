using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
	[Header("UI Reference Panels")]
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject page_Settings;

	[Header("Audio Settings")]
	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private Slider bgmSlider;

	// The name must match the Exposed Parameter in the Audio Mixer
	private const string BGM_VOLUME = "bgmVolume";

	/// <summary>
	/// Struct to store the volume state for undo/redo functionality
	/// </summary>
	struct SettingsSnapshot
	{
		public float bgmVolume;
	}

	private SettingsSnapshot newSettingsSnapshot;
	private SettingsSnapshot currentSettingsSnapshot;

	private PlayerInput playerInput;
	private InputAction exitAction;
	private bool isPaused = false;

	void Start()
	{
		// Initialize Input System references
		playerInput = GetComponent<PlayerInput>();
		exitAction = playerInput.actions["Cancel"];

		// Get the initial volume from the mixer to sync the UI
		if (audioMixer.GetFloat(BGM_VOLUME, out float initialdB))
		{
			float initialVol = Mathf.Pow(10f, initialdB / 20f);

			// Clamp the value to ensure it stays within slider bounds
			initialVol = Mathf.Clamp(initialVol, bgmSlider.minValue, bgmSlider.maxValue);
			currentSettingsSnapshot.bgmVolume = initialVol;
			bgmSlider.value = initialVol;
		}

		// Add listener to handle real-time slider changes
		//bgmSlider.onValueChanged.AddListener(SetBGMVolume);
	}

	void Update()
	{
		// Toggle pause state when the Exit action (e.g., Esc key) is triggered
		if (exitAction.triggered)
		{
			if (isPaused) Resume();
			else Pause();
		}
	}

	#region Pause Logic

	public void Resume()
	{
		OnBackSettings();
		pauseMenu.SetActive(false);
		page_Settings.SetActive(false);
		Time.timeScale = 1f; // Resume game time
		isPaused = false;
	}

	void Pause()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f; // Freeze game time
		isPaused = true;

		// Take a snapshot of the current mixer volume before the user starts editing
		newSettingsSnapshot = currentSettingsSnapshot;
	}

	#endregion

	#region Settings

	/// <summary>
	/// Updates the AudioMixer in real-time as the slider moves
	/// </summary>
	public void SetBGMVolume(float value)
	{
		newSettingsSnapshot.bgmVolume = value;
		// Formula: dB = Log10(0.0001 to 1.0) * 20
		// Result: Log10(0.0001)*20 = -80dB | Log10(1)*20 = 0dB
		float dB = Mathf.Log10(value) * 20;
		audioMixer.SetFloat(BGM_VOLUME, dB);
	}

	/// <summary>
	/// Triggered by the 'Apply' Button. Confirms the new volume as the saved state.
	/// </summary>
	public void OnApplySettings()
	{
		currentSettingsSnapshot = newSettingsSnapshot;
		Debug.Log("Settings saved successfully.");
	}

	/// <summary>
	/// Triggered by the 'Back' Button. Reverts the volume to the state before editing.
	/// </summary>
	public void OnBackSettings()
	{
		// Roll back the mixer and the slider UI to the previous value
		newSettingsSnapshot = currentSettingsSnapshot;
		SetBGMVolume(newSettingsSnapshot.bgmVolume);
		bgmSlider.value = newSettingsSnapshot.bgmVolume;

		// Navigate back to the main pause menu
		page_Settings.SetActive(false);
	}

	#endregion
}