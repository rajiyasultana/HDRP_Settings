using Studio23.SS2.SettingsManager.Core;
using Studio23.SS2.SettingsManager.Data;
using Studio23.SS2.SettingsManager.Utilities;
using System;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;


namespace Studio23.SS2.SettingsManager.Video
{
	//[RequireComponent(typeof(TMP_Dropdown))]
	public class GameQualitySettings : Settings
	{
		//[SerializeField] private TMP_Dropdown _uiItem;
		[SerializeField] private QualityName _defaultVal = QualityName.Medium; //default 1; medium, 0 high, 2 low 

		public QualityName CurrentQuality => (QualityName)CurrentValue.ToInt();
		public QualityName DefaultQuality => _defaultVal;

		public event Action<QualityName> OnQualityChanged;

		
		public override void Setup()
		{
			base.Initialized((int)_defaultVal, GetType().Name);
			Apply();
		}

		public void SetQuality(QualityName quality)
		{
			CurrentValue = (int)quality;

			if (IsLive)
			{
				Apply();
				//NotifyQualityChanged();
			}

			OnQualityChanged?.Invoke(CurrentQuality);
		}

		// public void SetQuality(int qualityIndex)
		// {
		// 	SetQuality((QualityName)qualityIndex);
		// }

		public override void RestoreAction()
		{
			//	_uiItem.value = (int)_defaultVal; // on change CurrentValue will be changed
			CurrentValue = (int)_defaultVal;
			base.Save();
			if (!IsLive) Apply(); // if Live then already applied this

			OnQualityChanged?.Invoke(CurrentQuality);
		}

		public override void ApplyAction()
		{
			base.Save();
			if (!IsLive) Apply(); // if Live then already applied this

			OnQualityChanged?.Invoke(CurrentQuality);
		}

		public void Apply()
		{
			QualitySettings.SetQualityLevel(CurrentValue.ToInt(), true);
		}

		// private void NotifyQualityChanged()
		// {
		// 	VideoSettingsController?.QualityChangedAction?.Invoke(CurrentQuality);
		// }
	}
}