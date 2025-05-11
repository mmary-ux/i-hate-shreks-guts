using System;
using System.Collections.Generic;

public class UIManager
{
    private Stack<UIScreen> screenStack = new Stack<UIScreen>();
    private Dictionary<Type, UIScreen> screens = new Dictionary<Type, UIScreen>();
    
    public void RegisterScreen(UIScreen screen)
    {
        var type = screen.GetType();
        screens[type] = screen;
        screen.SetManager(this);
    }
    
    public void ShowScreen<T>() where T : UIScreen
    {
        if (screenStack.Count > 0)
        {
            screenStack.Peek().Hide();
        }
        
        var screen = screens[typeof(T)];
        screen.Show();
        screenStack.Push(screen);
    }
    
    public void GoBack()
    {
        if (screenStack.Count <= 1) return;
        
        var current = screenStack.Pop();
        current.Hide();
        
        var previous = screenStack.Peek();
        previous.Show();
    }
}

// контроллер UI