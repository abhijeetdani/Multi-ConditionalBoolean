using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerProtected {
	Shield = 0,
	Revive = 1,
}


public class UIScreen : MonoBehaviour {

	[SerializeField] private float shieldLifetime = 10f;
	[SerializeField] private float reviveLifetime = 5f;
	[SerializeField] private Slider shieldSlider;
	[SerializeField] private Slider reviveSlider;
	[SerializeField] private Text protectedValue;
	[SerializeField] private Button shieldButton;
	[SerializeField] private Button reviveButton;

	private MultiConditionalBoolean<PlayerProtected> _playerProtected;
	
	public bool IsPlayerProtected {
		get {
			return _playerProtected.IsTrue();
		}
	}

	public void OnShieldButtonPressed() {
		shieldButton.interactable = false;
		// Set Player Protected
		_playerProtected.Set(PlayerProtected.Shield);
		protectedValue.text = IsPlayerProtected.ToString();
		StartCoroutine(ShieldTimerCountdown());
	}
	
	public void OnReviveButtonPressed() {
		reviveButton.interactable = false;
		// Set Player Protected
		_playerProtected.Set(PlayerProtected.Revive);
		protectedValue.text = IsPlayerProtected.ToString();
		StartCoroutine(ReviveTimerCountdown());
	}

	private void Awake() {
		_playerProtected = new MultiConditionalBoolean<PlayerProtected>();
		protectedValue.text = IsPlayerProtected.ToString();
	}

	private IEnumerator ShieldTimerCountdown() {
		float timer = shieldLifetime;
		while (timer > 0f) {
			shieldSlider.value = timer / shieldLifetime;
			yield return null;
			timer -= Time.deltaTime;
		}

		shieldSlider.value = 0f;
		// Reset Player Protected
		_playerProtected.Reset(PlayerProtected.Shield);
		protectedValue.text = IsPlayerProtected.ToString();
		shieldButton.interactable = true;
	}
	
	private IEnumerator ReviveTimerCountdown() {
		float timer = reviveLifetime;
		while (timer > 0f) {
			reviveSlider.value = timer / reviveLifetime;
			yield return null;
			timer -= Time.deltaTime;
		}

		reviveSlider.value = 0f;
		// Reset Player Protected
		_playerProtected.Reset(PlayerProtected.Revive);
		protectedValue.text = IsPlayerProtected.ToString();
		reviveButton.interactable = true;
	}
}
