using System;

public interface IAmEndableAction : IAmActionable
{
    event Action OnActionEnded;
    void ActionEnded();
}