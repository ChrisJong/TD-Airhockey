namespace AirHockey.GameLayer.Views.Core
{
    enum ViewTransitionPriority
    {
        Any,
        BeforeUpdate    // if this is processed after update, recall update on the new view
    }
}