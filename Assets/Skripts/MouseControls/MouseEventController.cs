using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

/// <summary>
/// Класс - прокси. Передает управление только одному обработчику
/// </summary>
public class MouseEventController : iMouseEventHandler
{
    private iMouseEventHandler currentHandler;
    private List<iMouseEventHandler> handlers = new List<iMouseEventHandler>()
    {
        new CameraPositionController(),
        new HighlightedController(),
        new CameraSizeController()
    };

    delegate bool handlerCall(iMouseEventHandler handler);

    public bool doubleTouch(Camera camera, Vector2 firstTouch, Vector2 secondTouch)
    {
        handlerCall handlerCall = (iMouseEventHandler handler) => handler.doubleTouch(camera, firstTouch, secondTouch);
        return callHandlers(handlerCall);
    }

    public bool mouseScroll(Camera camera, float value)
    {
        handlerCall handlerCall = (iMouseEventHandler handler) => handler.mouseScroll(camera, value);
        return callHandlers(handlerCall);
    }

    public bool touchDown(Camera camera, Vector2 touchPosition)
    {
        handlerCall handlerCall = (iMouseEventHandler handler) => handler.touchDown(camera, touchPosition);
        return callHandlers(handlerCall);
    }

    public bool touchMoved(Camera camera, Vector2 touchPosition)
    {
        handlerCall handlerCall = (iMouseEventHandler handler) => handler.touchMoved(camera, touchPosition);
        return callHandlers(handlerCall);
    }

    public bool touchUp(Camera camera, Vector2 touchPosition)
    {
        handlerCall handlerCall = (iMouseEventHandler handler) => handler.touchUp(camera, touchPosition);
        return callHandlers(handlerCall);
    }

    private bool callHandlers(handlerCall func)
    {
        if (currentHandler != null)
        {
            
            var res = func(currentHandler);
            if (!res)
                currentHandler = null;
            return res;

        }
        foreach (var handler in handlers)
        {
            var res = func(handler);
            if (res)
            {
                currentHandler = handler;
                return res;
            }
        }
        return false;
    }

}
