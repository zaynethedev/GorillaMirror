using Steamworks;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static OVRPlugin;

namespace GorillaMirror
{
    internal class ButtonManager : GorillaPressableButton
    {
        public override void Start()
        {
            gameObject.layer = 18;
        }

        public override void ButtonActivation()
        {
            switch (gameObject.name)
            {
                case "UPFOV":
                    Plugin.Instance.cameraFOV = ChangeFOV(Plugin.Instance.cameraFOV, 5, true);
                    break;
                case "DOWNFOV":
                    Plugin.Instance.cameraFOV = ChangeFOV(Plugin.Instance.cameraFOV, 5, false);
                    break;
                case "NEARUP":
                    Plugin.Instance.nearClip = ChangeNearClip(Plugin.Instance.nearClip, 0.1f, true);
                    break;
                case "NEARDOWN":
                    Plugin.Instance.nearClip = ChangeNearClip(Plugin.Instance.nearClip, 0.1f, false);
                    break;
                case "QUALITYUP":
                    ChangeQuality(Plugin.Instance.cameraQuality, 0.1f, true);
                    break;
                case "QUALITYDOWN":
                    ChangeQuality(Plugin.Instance.cameraQuality, 0.1f, false);
                    break;
            }
        }

        public float ChangeFOV(float FOV, float changeBy, bool isAdd)
        {
            if (isAdd)
            {
                FOV += changeBy;
            }
            else
            {
                FOV -= changeBy;
            }
            FOV = Mathf.Clamp(FOV, 30f, 180f);

            Plugin.Instance.mirrorCamera.fieldOfView = FOV;
            Plugin.Instance.fovText.text = $"FOV: {FOV}";
            return FOV;
        }

        public float ChangeNearClip(float nearClip, float changeBy, bool isAdd)
        {
            if (isAdd)
            {
                nearClip += changeBy;
            }
            else
            {
                nearClip -= changeBy;
            }

            nearClip = Mathf.Clamp(nearClip, 0.1f, 1f);

            Plugin.Instance.mirrorCamera.nearClipPlane = nearClip;
            Plugin.Instance.nearClipText.text = $"NEAR CLIP: {nearClip}";
            return nearClip;
        }

        public float ChangeQuality(float quality, float changeBy, bool isAdd)
        {
            if (isAdd)
            {
                quality += changeBy;
            }
            else
            {
                quality -= changeBy;
            }
            quality = Mathf.Clamp(quality, 0.1f, 1f);

            Plugin.Instance.qualityText.text = $"QUALITY: {quality}";
            return quality;
        }
    }
}
