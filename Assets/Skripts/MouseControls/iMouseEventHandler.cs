using UnityEngine;

/// <summary>
/// ��������� ����������� ������� �����
/// </summary>
public interface iMouseEventHandler
{
    /// <summary>
    /// ������� ������ �������
    /// </summary>
    public bool touchDown(Camera camera, Vector2 touchPosition);

    /// <summary>
    /// ������� �������� �������
    /// </summary>
    public bool touchMoved(Camera camera, Vector2 touchPosition);

    /// <summary>
    /// ������� ���������� �������
    /// </summary>
    public bool touchUp(Camera camera, Vector2 touchPosition);

    /// <summary>
    /// ������� �������� �������
    /// </summary>
    public bool doubleTouch(Camera camera, Vector2 firstTouch, Vector2 secondTouch);

    /// <summary>
    /// ������� ������� �����
    /// </summary>
    public bool mouseScroll(Camera camera, float value);
}
