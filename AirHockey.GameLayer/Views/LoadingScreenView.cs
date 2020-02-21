namespace AirHockey.GameLayer.Views
{
    using ComponentModel.Graphics;
    using Core.Base;
    using LoadingScreenViewContent;

    class LoadingScreenView : GameViewBase
    {
        public LoadingScreenView()
        {
            this.GameObjects.Add(new LoadingScreenUserInterfaceGameObject(this));
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
