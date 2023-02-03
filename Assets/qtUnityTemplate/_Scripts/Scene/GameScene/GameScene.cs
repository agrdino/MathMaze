using _Scripts.qtLib;
using _Scripts.System;
using TMPro;
using UnityEngine;

namespace _Scripts.Scene
{
    public class GameScene : sceneBase
    {
        [SerializeField] private TextMeshProUGUI _txtTime;
        [SerializeField] private TextMeshProUGUI _txtStep;
        [SerializeField] private TextMeshProUGUI _txtLevel;

        [SerializeField] private qtButton _btnMove;
        [SerializeField] private qtButton _btnRestartLevel;
        [SerializeField] private qtButton _btnBack;
        
        
        //cache
        private float _time;
        #region ----- INITIALIZE -----

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void InitEvent()
        {
            _btnMove.onClick.AddListener(OnButtonMoveClick);
            _btnRestartLevel.onClick.AddListener(OnButtonRestartLevelClick);
            _btnBack.onClick.AddListener(OnButtonBackClick);

            GameManager.instance.evtStartGame += StartGame;
            GameManager.instance.evtUpdateGame += UpdateGame;
        }

        public override void InitObject()
        {
        }

        protected override void OnExit()
        {
            _btnMove.onClick.RemoveAllListeners();   
            _btnRestartLevel.onClick.RemoveAllListeners();
            _btnBack.onClick.RemoveAllListeners();
            
            GameManager.instance.evtStartGame -= StartGame;
            GameManager.instance.evtUpdateGame -= UpdateGame;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            _txtTime.text = $"{(int) (Time.time - _time) / 60 :00} : {(int)(Time.time - _time) % 60 :00}";
        }

        #endregion

        #region ----- ANIMATION -----

        #endregion

        #region ----- PUBLIC FUCTION -----

        private void StartGame(float time, int level)
        {
            _txtLevel.text = $"Level {level}";
            _time = time;
            _txtTime.text = "00 : 00";
            _txtStep.text = $"Step 0";
        }

        private void UpdateGame(int step)
        {
            _txtStep.text = $"Step {step}";
        }

        #endregion

        #region ----- BUTTON EVENT -----

        private void OnButtonMoveClick()
        {
            StartCoroutine(GameManager.instance.StartMove());
        }

        private void OnButtonRestartLevelClick()
        {
            GameManager.instance.RestartLevel();
        }

        private void OnButtonBackClick()
        {
            UIManager.Instance.ShowScene(qtScene.EScene.MenuScene);
        }

        #endregion
    }
}
