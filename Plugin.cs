using BepInEx;
using DevHoldableEngine;
using GorillaNetworking;
using Steamworks;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using TMPro;
using Utilla;

namespace GorillaMirror
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        public GameObject gorillaMirror;
        public Camera mirrorCamera;
        public static Plugin Instance;
        public float cameraFOV = 120;
        public float nearClip = 0.3f;
        public float cameraQuality = 1;
        public TextMeshPro fovText;
        public TextMeshPro nearClipText;
        public TextMeshPro qualityText;

        void Start()
        {
            Instance = this;
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            var bundle = LoadAssetBundle("GorillaMirror.Resources.mirrormod");
            var asset = bundle.LoadAsset<GameObject>("MirrorMod");
            gorillaMirror = Instantiate(asset);
            fovText = gorillaMirror.transform.Find("Handle/FOVTEXT").gameObject.GetComponent<TextMeshPro>();
            nearClipText = gorillaMirror.transform.Find("Handle/NEARCLIPTEXT").gameObject.GetComponent<TextMeshPro>();
            gorillaMirror.transform.Find("Handle/VersionText").gameObject.GetComponent<TextMeshPro>().text = $"GorillaMirror v{PluginInfo.Version}";
            mirrorCamera = gorillaMirror.transform.Find("Handle/Camera").gameObject.GetComponent<Camera>();
            gorillaMirror.transform.Find("Handle/UPFOV").gameObject.AddComponent<ButtonManager>(); gorillaMirror.transform.Find("Handle/DOWNFOV").gameObject.AddComponent<ButtonManager>();
            gorillaMirror.transform.Find("Handle/NEARUP").gameObject.AddComponent<ButtonManager>(); gorillaMirror.transform.Find("Handle/NEARDOWN").gameObject.AddComponent<ButtonManager>();
            gorillaMirror.transform.Find("Handle").AddComponent<DevHoldable>();
            gorillaMirror.transform.Find("Handle").gameObject.layer = 18;
            gorillaMirror.transform.localScale = new Vector3(0.15f, 0.15f, -0.15f);
            gorillaMirror.transform.position = new Vector3(-65, 12, -82);
        }

        public void Update()
        {
            System.DateTime currentTime = System.DateTime.Now;
            string timeFormat = currentTime.ToString("hh:mm:ss tt").Replace(" ", "/");
            string dateFormat = currentTime.ToString("MM-dd-yy");
            gorillaMirror.transform.Find("Handle/ClockText").gameObject.GetComponent<TextMeshPro>().text = $"{timeFormat} - {dateFormat}";
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
    }
}
