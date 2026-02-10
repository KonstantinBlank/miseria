using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class CutSceneManager : MonoBehaviour {
    private static bool _firstRun = true;

    public static int CurrentPhase = Constants.PHASE_I;
    public static bool Helped = false;
    public static bool GameOver = false;

    public GameObject attacker;
    public GameObject victim;
    public GameObject HelpPost;
    public GameObject HelpPostNpc;

    private FightTrigger _fightTrigger;

    public PlayableDirector Fall;
    public PlayableDirector FallAndSafe;
    public PlayableDirector Credits;

    public GameObject FallObjects;


    public GameObject Player;
    public Transform StreetRespawn;
    public Transform BorderRespawn;
    public float BlackHoleTime;
    public int InitialBorderWaitTime;
    public float SecondTryWaitTime;

    public string CreditsText;
    public Text CreditsTextContainer;
    public float LetterDelay = 0.04f;
    private IEnumerator _Writer = null;
    private bool _finishedWriting = false;
    private bool _creditsplaying = false;
    private bool _finalTextWriting = false;

    private MusicManager _musicManager;
    private SoundManager _soundManager;
    private FadeManager _fadeManager;

    private SecondChance secondChance;

    private static bool _restarting = false;
    private static bool _falling = false;

    private static FirstPersonController _fps;

    //public static List<FollowPlayer> Followers = new List<FollowPlayer>();


    private void Start() {
        _musicManager = (MusicManager)GameObject.Find("Musicmanager").GetComponent(typeof(MusicManager));
        _soundManager = (SoundManager)GameObject.Find("Sounds").GetComponent(typeof(SoundManager));
        secondChance = (SecondChance)GetComponent(typeof(SecondChance));
        _fadeManager = (FadeManager)GameObject.Find("FadeManager").GetComponent(typeof(FadeManager));

        _fps = Player.GetComponent<FirstPersonController>();

        //_fightTrigger = (FightTrigger)GameObject.Find("Fight").GetComponent(typeof(FightTrigger));
    }

    private void Update() {
        if (_finalTextWriting && !_finishedWriting && Input.GetMouseButtonDown(0)) {
            finishWriting();
        }
        else if (_finishedWriting && !_creditsplaying && Input.GetMouseButtonDown(0)) {
            writeCreditNames();
        }
    }

    public void LoadSecondRun() {
        _firstRun = false;
        if (attacker != null) {
            attacker.SetActive(true);
        }
        if (victim != null) {
            victim.SetActive(true);
        }
        //foreach(FollowPlayer follower in Followers) {
        //    follower.ResetNpc();
        //}

        StartCoroutine(SetPlayerToBorder());
    }

    public IEnumerator SetPlayerOnStreet() {
        CurrentPhase = Constants.PHASE_I;
        FirstPersonController.FreezePlayer(true);
        _fadeManager.FadeOutNow();
        _restarting = true;
        yield return new WaitForSeconds(1.7f);
        #region setHelpPost active
        if (HelpPost == null) {
            Debug.Log("helpost reference missing");
        }
        else {
            HelpPost.SetActive(true);
        }

        if (HelpPostNpc == null) {
            Debug.Log("helpostNpc reference missing");
        }
        else {
            HelpPostNpc.SetActive(true);
        }
        #endregion
        
        Player.transform.SetPositionAndRotation(StreetRespawn.position, StreetRespawn.rotation);
        Player.transform.eulerAngles = Vector3.zero;
        FirstPersonController.StunPlayer(false);
        secondChance.enabled = false;
        yield return new WaitForSeconds(SecondTryWaitTime);
        _fadeManager.FadeInNow();
        FirstPersonController.FreezePlayer(false);
        _soundManager.PlayReload();
        _restarting = false;
    }


    private IEnumerator SetPlayerToBorder() {
        if (Fall == null) {
            Debug.Log("Fall cut scene missing");
        }
        else {
            _falling = true;
            FallObjects.gameObject.SetActive(true);
            Fall.Play();
            StartCoroutine(_soundManager.waitForFall());
            _soundManager.PlayTalking();
            yield return new WaitForSeconds(3f);

            //foreach (FollowPlayer follower in Followers) {
            //    follower.ResetNpc(); //wip
            //}
            //Followers.Clear();

            _soundManager.Talking.Stop();
            _musicManager.StopMusic();
            _fadeManager.FadeOutNow();
            yield return new WaitForSeconds(BlackHoleTime);
            _fadeManager.FadeInNow();

            FallObjects.gameObject.SetActive(false);
            FirstPersonController fpc = (FirstPersonController)Player.GetComponent(typeof(FirstPersonController));
            Player.transform.position = BorderRespawn.position;
            Player.transform.rotation = BorderRespawn.rotation;
            fpc.enabled = true;
            FirstPersonController.StunPlayer(true);
            //_musicManager.PlayPhaseII();
            _musicManager.PlayPhaseI();
            _falling = false;
            yield return new WaitForSeconds(InitialBorderWaitTime);

            CurrentPhase = Constants.PHASE_II;
            secondChance.enabled = true;

        }
    }

    public void PlayCredits() {
        //FallObjects.SetActive(true);
        //StartCoroutine(creditsManager());

        //disable movement
        GameOver = true;
        FirstPersonController.StunPlayer(true);
        FirstPersonController.FreezePlayer(true);
        
        _musicManager.PlayPhaseIV();
        //black out
        _fadeManager.FadeOutNow();
        //start writing
        _Writer = writeCreditsText();
        StartCoroutine(_Writer);

    }

    private IEnumerator writeCreditsText() {
        yield return new WaitForSecondsRealtime(2f);
        _finalTextWriting = true;
        foreach (char letter in CreditsText.ToCharArray()) {
            CreditsTextContainer.text += letter;
            yield return new WaitForSecondsRealtime(LetterDelay);
        }
    }

    private void finishWriting() {
        StopCoroutine(_Writer);
        CreditsTextContainer.text = CreditsText;
        _finishedWriting = true;
        _finalTextWriting = false;
    }

    private void writeCreditNames() {
        _creditsplaying = true;
        CreditsTextContainer.text = "";
        Credits.Play();
        StartCoroutine(creditsHelper());
    }

    private IEnumerator creditsManager() {
        StartCoroutine(_soundManager.WaitForFallSafe());
        FallAndSafe.Play();
        Credits.Play();
        yield return new WaitForSeconds(6f);
        FallAndSafe.Pause();
        Credits.Pause();
        _musicManager.PlayPhaseIV();
        writeFinalText();

        //wait till the text is over
        while (_fps.enabled) {
            yield return new WaitForSeconds(0.1f);
        }

        FirstPersonController.StunPlayer(false);
        FirstPersonController.FreezePlayer(false);

        FallAndSafe.Play();
        Credits.Play();

        StartCoroutine(creditsHelper());
    }

    private void writeFinalText() {
        _fps.enabled = true;

        DialogueTrigger finalDialogue = GetComponent<DialogueTrigger>();
        finalDialogue.TriggerDialogue();
    }

    /// <summary>
    /// to activate the curser without letting the user controll the player
    /// will be executed when the credits will stop
    /// </summary>
    /// <returns></returns>
    private IEnumerator creditsHelper() {
        yield return new WaitForSeconds((float)Credits.duration - 13f);
        FirstPersonController.StunPlayer(true);
        FirstPersonController.FreezePlayer(true);
        _fps.enabled = false;
    }


    /// <summary>
    /// true -> firstrun; false -> second run
    /// </summary>
    /// <returns></returns>
    public static bool IsFirstRun() {
        return _firstRun;
    }

    public static bool IsRestarting() {
        return _restarting;
    }

    public static bool IsFalling() {
        return _falling;
    }

    public static void DisableFps() {        
        _fps.enabled = false;
    }
}
