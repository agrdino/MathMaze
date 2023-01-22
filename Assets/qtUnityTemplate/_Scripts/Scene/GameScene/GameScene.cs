using _Scripts.qtLib;
using _Scripts.System;
using UnityEngine;

namespace _Scripts.Scene.GameScene
{
    public class GameScene : sceneBase
    {
        [SerializeField] private qtButton _btnMove;
        
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void InitEvent()
        {
            _btnMove.onClick.AddListener(OnButtonMoveClick);
        }

        public override void InitObject()
        {
        }

        protected override void OnExit()
        {
            _btnMove.onClick.RemoveAllListeners();   
        }

        #region ----- ANIMATION -----

        #endregion

        #region ----- BUTTON EVENT -----

        private void OnButtonMoveClick()
        {
            StartCoroutine(GameManager.instance.StartMove());
        }

        #endregion
    }
}
