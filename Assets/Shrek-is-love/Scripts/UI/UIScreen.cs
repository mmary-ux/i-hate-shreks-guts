public abstract class UIScreen : UIElement
{
    protected UIManager Manager { get; private set; }
    
    public void SetManager(UIManager manager)
    {
        Manager = manager;
    }
    
    public virtual void OnBackPressed()
    {
        Manager?.GoBack();
    }
}

// базовый класс для всех экранов