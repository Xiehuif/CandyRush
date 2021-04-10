using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 圆形遮罩镂空引导
/// </summary>
public class CircleGuidanceController : Singleton<CircleGuidanceController>
{
    private bool IsWorking = false;
    private Canvas canvas;

    /// <summary>
    /// 要高亮显示的目标
    /// </summary>
    public RectTransform target;

    /// <summary>
    /// 区域范围缓存
    /// </summary>
    private Vector3[] _corners = new Vector3[4];

    /// <summary>
    /// 镂空区域圆心
    /// </summary>
    private Vector4 _center;

    /// <summary>
    /// 镂空区域半径
    /// </summary>
    private float _radius = 100;

    /// <summary>
    /// 遮罩材质
    /// </summary>
    private Material _material;

    /// <summary>
    /// 当前高亮区域的半径
    /// </summary>
    private float _currentRadius,_wholeRadius;

    /// <summary>
    /// 高亮区域缩放的动画时间
    /// </summary>
    private float _shrinkTime = 2f;
    private float _curTime = 0f;
    /// <summary>
    /// 世界坐标向画布坐标转换
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="world">世界坐标</param>
    /// <returns>返回画布上的二维坐标</returns>
    private Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        Vector2 position;
        world = Camera.main.WorldToScreenPoint(world);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            world, Camera.main, out position);
        return position;
    }

    public void SetTarget(Vector3 WorldPos)
    {
        this.GetComponent<Image>().enabled = true;
        canvas = this.GetComponentInParent<Canvas>();
        this.target.anchoredPosition =  WorldToCanvasPos(canvas, WorldPos);
        RefreshMask();
    }

    public void RefreshMask()
    {
        TimeManager.Instance.Pause();
        //获取高亮区域的四个顶点的世界坐标
        target.GetWorldCorners(_corners);
        //计算最终高亮显示区域的半径
        _radius = Vector2.Distance(WorldToCanvasPos(canvas, _corners[0]), WorldToCanvasPos(canvas, _corners[2])) / 2f;
        //计算高亮显示区域的圆心
        float x = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
        float y = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
        Vector3 centerWorld = new Vector3(x, y, 0);
        Vector2 center = WorldToCanvasPos(canvas, centerWorld);
        //设置遮罩材料中的圆心变量
        Vector4 centerMat = new Vector4(center.x, center.y, 0, 0);
        _material = GetComponent<Image>().material;
        _material.SetVector("_Center", centerMat);
        //计算当前高亮显示区域的半径
        _wholeRadius = 600;
        _radius = 100;
        _material.SetFloat("_Slider", _wholeRadius);
        IsWorking = true;
    }

    /// <summary>
    /// 收缩速度
    /// </summary>
    private float _shrinkVelocity = 1f;

    private void Update()
    {
        if (IsWorking)
        {
            _curTime += Time.unscaledDeltaTime * _shrinkVelocity;
            //从当前半径到目标半径差值显示收缩动画
            float ratio = _curTime / _shrinkTime;
            _currentRadius = _radius * ratio + (1 - ratio) * _wholeRadius;
            _material.SetFloat("_Slider", _currentRadius);
            if(_curTime > _shrinkTime){
                _curTime = 0f;
                IsWorking = false;
                TimeManager.Instance.Continue();
                this.GetComponent<Image>().enabled = false;
            }
        }
    }
}