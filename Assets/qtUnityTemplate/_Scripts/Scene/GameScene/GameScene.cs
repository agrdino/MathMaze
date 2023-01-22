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
        [SerializeField] private qtButton _btnReset;
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
            _btnReset.onClick.AddListener(OnButtonResetClick);
            _btnBack.onClick.AddListener(OnButtonBackClick);
        }

        public override void InitObject()
        {
        }

        protected override void OnExit()
        {
            _btnMove.onClick.RemoveAllListeners();   
            _btnReset.onClick.RemoveAllListeners();
            _btnBack.onClick.RemoveAllListeners();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            _txtTime.text = $"{(int) (Time.time - _time) / 60} : {(int)(Time.time - _time) % 60}";
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

        private void OnButtonResetClick()
        {
            
        }

        private void OnButtonBackClick()
        {
            UIManager.Instance.ShowScene(qtScene.EScene.MenuScene);
        }

        #endregion
    }
}
